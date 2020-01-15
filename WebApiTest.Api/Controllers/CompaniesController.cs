using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WebApiTest.Api.Dto;
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
        [HttpHead]
        public async Task<ActionResult<IEnumerable<CompanyDto>>> GetCompanies()
        {
            //throw new Exception("测试 抛出 异常 ");
            var companies = await _companyRepository.GetCompaniesAsync();
            //从 companies (list) 转换 成为 companyDto(IEnumerable)
            var companyDtos = _mapper.Map<IEnumerable<CompanyDto>>(companies);
            return Ok(companyDtos);
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