using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WebApiTest.Api.Dto;
using WebApiTest.Api.Entities;
using WebApiTest.Api.Services;

namespace WebApiTest.Api.Controllers
{
    [ApiController]
    [Route("api/companies")]
    public class CompaniesController : Controller
    {
        private readonly ICompanyRepository _companyRepository;
        private readonly IMapper _mapper;

        public CompaniesController(
            ICompanyRepository companyRepository,
            IMapper mapper
        )
        {
            _companyRepository = companyRepository ?? throw new ArgumentNullException(nameof(companyRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        //api/companies

        [HttpGet]
        public async Task<ActionResult<MsgReturn<object>>> GetCompanies([FromQuery] QueryParameter queryParameter)
        {
            //throw new Exception("测试 抛出 异常 ");
            var companies = await _companyRepository.GetCompaniesAsync(queryParameter);
            //从 companies (list) 转换 成为 companyDto(IEnumerable)
            var companyDtos = _mapper.Map<IEnumerable<CompanyDto>>(companies);
            return Ok(new MsgReturn<object>
            {
                Msg = "查询公司列表成功",
                Response = new
                {
                    List = companyDtos,
                    totalCount = companies.TotalItemsCount,
                    pageIndex = companies.PageIndex,
                    pageSize = companies.PageSize
                }
            });
        }

        //api/companies/123
        [HttpGet("{companyId}", Name = nameof(GetCompany))]
        public async Task<ActionResult<CompanyDto>> GetCompany(Guid companyId)
        {
            //先检查下 资源 存不存 在
            var company = await _companyRepository.GetCompanyAsync(companyId);
            //如果 资源 不存在 
            if (company == null) return NotFound();
            var CompanyDto = _mapper.Map<CompanyDto>(company);
            return Ok(CompanyDto);
        }

        [HttpPost]
        public async Task<ActionResult<MsgReturn<CompanyDto>>> AddCompany(CompanyAddDto companyAddDto)
        {
            if (companyAddDto == null) return Ok(new MsgReturn<string>() {Success = false, Msg = "没有传进来公司!"});
            var company = _mapper.Map<Company>(companyAddDto); //此时 company  id 为 0000-000-00 
            _companyRepository.AddCompany(company); //执行添加操作后 
            if (!await _companyRepository.SaveAsync() ) //并且保存 操作 以后 company 就 会多 一个 id 为 0b95506b-29df-44ac-9459-62302cd45fd5
                return Ok(new MsgReturn<string>() {Success = false, Msg = "未知原因，没有添加上公司……"});
            var companyDto = _mapper.Map<CompanyDto>(company); //将插入完成的数据返回去
            return Ok(new MsgReturn<CompanyDto>()
                {Msg = "创建成功!", Response = companyDto}); //所以返回 创建 成功 的 companyDto 是 有 id 的!
        }
    }
}