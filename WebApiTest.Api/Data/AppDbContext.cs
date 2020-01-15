using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebApiTest.Api.Entities;

namespace WebApiTest.Api.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Company> Companies { get; set; }

        public DbSet<Employee> Employees { get; set; }

        // 当数据库创建时
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //这个是对字符串的长度格式 什么的做一些 限制 
            modelBuilder.Entity<Company>().Property(x => x.Name)
                .IsRequired() //名称必填
                .HasMaxLength(100); //最大长度100 
            //设置 数据 表之间 的关系 (外键)
            modelBuilder.Entity<Employee>()
                .HasOne(x => x.Company) //有一个父级
                .WithMany(x => x.Employees) //父级有很多员工 
                .HasForeignKey(x => x.CompanyId) //外键是 companyId 和 company 表的 主键Id 对应
                .OnDelete(DeleteBehavior.Restrict); //关联删除 


            //设置 种子数据 
            modelBuilder.Entity<Company>().HasData(
                new Company
                {
                    Id = Guid.Parse("64c208b0-e801-41c9-92f4-cdb76c5f3f1c"),
                    Name = "百度",
                    Introduction = "不好的公司",
                },
                new Company
                {
                    Id = Guid.Parse("b9179c40-52f9-42ce-8f67-6972bd297e59"),
                    Name = "新浪",
                    Introduction = "微薄",
                },
                new Company
                {
                    Id = Guid.Parse("489748f7-db29-4e9b-8a60-4c655bf7b9a4"),
                    Name = "小米",
                    Introduction = "买手机 的",
                }
            );

            modelBuilder.Entity<User>().HasData(
                new User()
                {
                    Id = Guid.Parse("dc359997-b795-44df-9820-9516e60b8f9c"),
                    Name = "12",
                    Password = "12",
                    Status = true
                }
            );

            modelBuilder.Entity<Employee>().HasData(
                new Employee()
                {
                    Id = Guid.Parse("d21af833-45a2-46ee-afe9-82886ee453bd"),
                    CompanyId = Guid.Parse("64c208b0-e801-41c9-92f4-cdb76c5f3f1c"),
                    DateOfBirth = new DateTime(1984, 11, 3),
                    EmployeeNo = "9527",
                    FirstName = "李",
                    LastName = "言红",
                    Gender = Gender.男,
                },
                new Employee()
                {
                    Id = Guid.Parse("857bb6b7-be73-4028-b5e9-bb70f155e8ad"),
                    CompanyId = Guid.Parse("64c208b0-e801-41c9-92f4-cdb76c5f3f1c"),
                    DateOfBirth = new DateTime(1991, 11, 3),
                    EmployeeNo = "9527",
                    FirstName = "徐",
                    LastName = "东东",
                    Gender = Gender.女,
                },
                new Employee()
                {
                    Id = Guid.Parse("ff5772d0-c235-4d19-84ef-b173c6d8f5f6"),
                    CompanyId = Guid.Parse("b9179c40-52f9-42ce-8f67-6972bd297e59"),
                    DateOfBirth = new DateTime(1984, 11, 3),
                    EmployeeNo = "9527",
                    FirstName = "菜",
                    LastName = "吁困",
                    Gender = Gender.男,
                },
                new Employee()
                {
                    Id = Guid.Parse("15fd5ef5-9fd0-4dbe-a885-43d2e9285857"),
                    CompanyId = Guid.Parse("b9179c40-52f9-42ce-8f67-6972bd297e59"),
                    DateOfBirth = new DateTime(1991, 11, 3),
                    EmployeeNo = "9527",
                    FirstName = "无",
                    LastName = "一番",
                    Gender = Gender.女,
                },
                new Employee()
                {
                    Id = Guid.Parse("7f5fa708-6327-4baf-abab-576b09c8b8c2"),
                    CompanyId = Guid.Parse("489748f7-db29-4e9b-8a60-4c655bf7b9a4"),
                    DateOfBirth = new DateTime(1984, 11, 3),
                    EmployeeNo = "9527",
                    FirstName = "雷",
                    LastName = "不死",
                    Gender = Gender.男,
                },
                new Employee()
                {
                    Id = Guid.Parse("a021b331-04cf-4d48-a5ce-0e38b0577f91"),
                    CompanyId = Guid.Parse("489748f7-db29-4e9b-8a60-4c655bf7b9a4"),
                    DateOfBirth = new DateTime(1991, 11, 3),
                    EmployeeNo = "9527",
                    FirstName = "路",
                    LastName = "胃病",
                    Gender = Gender.女,
                }
            );
        }
    }
}