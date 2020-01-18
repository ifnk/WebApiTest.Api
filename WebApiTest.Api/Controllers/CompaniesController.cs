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
        public async Task<ActionResult <MsgReturn<object>>> GetCompanies([FromQuery] QueryParameter queryParameter)
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
        [HttpGet("{companyId}")]
        public async Task<ActionResult<CompanyDto>> GetCompany(Guid companyId)
        {
            //先检查下 资源 存不存 在
            var company = await _companyRepository.GetCompanyAsync(companyId);
            //如果 资源 不存在 
            if (company == null) return NotFound();
            var CompanyDto = _mapper.Map<CompanyDto>(company);
            return Ok(CompanyDto);
        }
    }
}