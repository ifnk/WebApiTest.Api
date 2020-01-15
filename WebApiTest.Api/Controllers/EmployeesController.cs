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
    [Route("api/companies/{companyId}/employees")]
    public class EmployeesController : Controller
    {
        private readonly IMapper _mapper;
        private readonly ICompanyRepository _companyRepository;

        public EmployeesController(
            IMapper mapper,
            ICompanyRepository companyRepository
        )
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _companyRepository = companyRepository ?? throw new ArgumentNullException(nameof(companyRepository));
        }

        [HttpGet]
        //api/companies/{companyId}/employees
        public async Task<ActionResult<IEnumerable<EmployeeDto>>> GetEmployeesForCompany(Guid companyId)
        {
            //根据 公司 id 判断 公司 存不存在 
            if (!await _companyRepository.CompanyExistAsync(companyId)) return NotFound();
            var employees = await _companyRepository.GetEmployeeAsync(companyId);
            var employeeDtos = _mapper.Map<IEnumerable<EmployeeDto>>(employees);
            return Ok(new MsgReturn<object>
            {
                Msg = "查询员工列表成功",
                Response = new
                {
                    List = employeeDtos,
                }
            });

        }

        [HttpGet("{employeeId}")]

        //api/companies/{companyId}/employees/{employeeId}
        public async Task<ActionResult<EmployeeDto>> GetEmployeeForCompany(Guid companyId,
            Guid employeeId)
        {
            //根据 公司 id 判断 公司 存不存在 
            if (!await _companyRepository.CompanyExistAsync(companyId)) return NotFound();
            var employee = await _companyRepository.GetEmployeeAsync(companyId, employeeId);
            if (employee == null) return NotFound();
            var employeeDto = _mapper.Map<EmployeeDto>(employee);
            return Ok(employeeDto);
        }
    }
}