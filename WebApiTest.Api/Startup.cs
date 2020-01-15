using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using WebApiTest.Api.Data;
using WebApiTest.Api.Repostories;
using WebApiTest.Api.Services;

namespace WebApiTest.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            //每一次http请求 的生命 周期 

            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());


            services.AddDbContext<AppDbContext>(option =>
            {
                option.UseSqlite(Configuration.GetConnectionString("DefaultConnection"));
            });


            //jwt 服务 
            // 首先新建一个密钥 
            SecurityKey securityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes("chenNingJsonWebToken"));

            // 然后开启认证服务
            services.AddAuthentication("Bearer")
                .AddJwtBearer(o =>
                {
                    o.TokenValidationParameters = new TokenValidationParameters()
                    {
                        ValidateIssuerSigningKey = true, //1. 是否开启密钥认证 
                        IssuerSigningKey = securityKey, //1 . 设置认证密钥

                        ValidateIssuer = true, //2.是否验证 发行 人 
                        ValidIssuer = "chenNingJwtApi", //2. 发行 人  姓名

                        ValidateAudience = true, //3.是否验证 订阅 人 
                        ValidAudience = "chenNingJwtApp", //3.订阅 人  姓名

                        RequireExpirationTime = true, //4.是否验证 过期时间  
                        ValidateLifetime = true, //5.生命周期 
                    };
                });

            services.AddScoped<ICompanyRepository, CompanyRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                //返回异常页面 
                app.UseDeveloperExceptionPage();

                ////也可以应对 异常  返回 一些 配置  啦
                //app.UseExceptionHandler(appbuilder =>
                //{
                //    appbuilder.Run(async context =>
                //    {
                //        context.Response.StatusCode = 500;
                //        await context.Response.WriteAsync("服务器有错误啦！");
                //        //实际项目中这里是要记录 下日志 的…… 
                //    });
                //});
            }

            app.UseRouting();

            app.UseAuthentication(); //jwt 的认证 中间 件 一定要 在  UseAuthorization


            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}