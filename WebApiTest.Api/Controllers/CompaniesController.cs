using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WebApiTest.Api.Dto;
using WebApiTest.Api.Entities;
using WebApiTest.Api.Entities.DatabaseEntities;
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
        [HttpGet("employees")]
        public async Task<ActionResult<MsgReturn<object>>> GetAllEmployees([FromQuery] QueryParameter queryParameter)
        {
            if (queryParameter == null) throw new ArgumentNullException(nameof(queryParameter));
            var employeesEntity = await _companyRepository.GetEmployeesWithAllCompany(queryParameter);
            var employeeDtos = _mapper.Map<IEnumerable<EmployeeDto>>(employeesEntity);
            return Ok(new MsgReturn<object>()
            {
                Msg = "查询所有员工成功!",
                Response = new
                {
                    List = employeeDtos,
                    totalCount = employeesEntity.TotalCount,
                    pageIndex = employeesEntity.PageIndex,
                    pageSize = employeesEntity.PageSize
                }
            });
        }


        [HttpGet(Name = nameof(GetCompanies))]
        [HttpHead]
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
                    totalCount = companies.TotalCount,
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
            var companyDto = _mapper.Map<CompanyDto>(company);
            return Ok(companyDto);
        }


        [HttpPost]
        public async Task<ActionResult<MsgReturn<CompanyDto>>> AddCompany(CompanyAddDto companyAddDto)
        {
            if (companyAddDto == null) return Ok(new MsgReturn<string>() {Success = false, Msg = "没有传进来公司!"});
            var company = _mapper.Map<Company>(companyAddDto); //此时 company  id 为 0000-000-00 
            _companyRepository.AddCompany(company); //执行添加操作后 
            if (!await _companyRepository.SaveAsync()
            ) //并且保存 操作 以后 company 就 会多 一个 id 为 0b95506b-29df-44ac-9459-62302cd45fd5
                return Ok(new MsgReturn<string>() {Success = false, Msg = "未知原因，没有添加上公司……"});
            var companyDto = _mapper.Map<CompanyDto>(company); //将插入完成的数据返回去
            return Ok(new MsgReturn<CompanyDto>()
                {Msg = "创建成功!", Response = companyDto}); //所以返回 创建 成功 的 companyDto 是 有 id 的!
        }

        [HttpDelete("{companyId}")]
        public async Task<ActionResult<MsgReturn<CompanyDto>>> DeleteCompany(Guid companyId)
        {
            var companyEntity = await _companyRepository.GetCompanyAsync(companyId);
            if (companyEntity == null) return Ok(new MsgReturn<string>() {Success = false, Msg = "没有找到对应的id的公司"});
            //要连 公司底下的员工也一并删除 的话， 就必须 先 把公司 底下的员工 查询出来 
            await _companyRepository.GetEmployeesAsync(companyId); //先查询 公司底下的员工
            _companyRepository.DeleteCompany(companyEntity); //然后 一并删除 掉
            if (!await _companyRepository.SaveAsync())
                return Ok(new MsgReturn<string>() {Msg = "没有删除上", Success = false});
            return Ok(new MsgReturn<EmployeeDto>() {Msg = "删除成功!"});
        }
    }
}