using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using WebApiTest.Api.Dto;
using WebApiTest.Api.Entities;
using WebApiTest.Api.Entities.DatabaseEntities;
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
            var employees = await _companyRepository.GetEmployeesAsync(companyId);
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

        [HttpPost]
        public async Task<ActionResult<MsgReturn<EmployeeDto>>> CreateEmployeeForCompany(Guid companyId,
            EmployeeAddDto employeeAddDto)
        {
            //添加前检查一下是不是有这个公司存在 
            if (!await _companyRepository.CompanyExistAsync(companyId))
                return Ok(new MsgReturn<string>() {Msg = "要添加的公司不存在!", Success = false});
            //先将 addDto 转换 为 实体 employee
            var employee = _mapper.Map<Employee>(employeeAddDto);
            //调用  ef core add 方法 
            _companyRepository.AddEmployee(companyId, employee);
            if (!await _companyRepository.SaveAsync())
                return Ok(new MsgReturn<string>() {Msg = "添加员工没有成功", Success = false});
            //这时候 employee 已经 有了 id 主键
            //将他转换 成 employeeDto 返回 给 app
            var employeeDto = _mapper.Map<EmployeeDto>(employee);
            return Ok(new MsgReturn<EmployeeDto>() {Msg = "添加成功", Response = employeeDto});
        }

        [HttpPut("{employeeId}")]
        public async Task<ActionResult<MsgReturn<EmployeeDto>>> UpdateEmployeeForCompany(Guid companyId,
            Guid employeeId, EmployeeUpdateDto employeeUpdateDto)
        {
            if (!await _companyRepository.CompanyExistAsync(companyId))
                return Ok(new MsgReturn<string>() {Msg = "没有找到对应的公司!", Success = false});
            var employee = await _companyRepository.GetEmployeeAsync(companyId, employeeId);
            if (employee == null)
                return Ok(new MsgReturn<string>() {Msg = "没有找到要修改的员工!", Success = false});
            //将 employee 转换 为 employeeUpdateDto
            //把 传 进来 的 employeeUpdateDto 的值 更新 到 employeeUpdateDto
            //将 employeeUpdateDto 映射回去
            _mapper.Map(employeeUpdateDto, employee);
            _companyRepository.UpdateEmployee(employee);
            if (!await _companyRepository.SaveAsync())
                return Ok(new MsgReturn<string>() {Msg = "没有修改上", Success = false});

            return Ok(new MsgReturn<EmployeeDto>() {Msg = "修改成功!"});
        }


        [HttpDelete("{employeeId}")]
        public async Task<ActionResult<MsgReturn<EmployeeDto>>> DeleteEmployeeForCompany(Guid companyId,
            Guid employeeId)
        {
            if (!await _companyRepository.CompanyExistAsync(companyId))
                return Ok(new MsgReturn<string>() {Msg = "没有找到对应的公司!", Success = false});
            var employeeEntity = await _companyRepository.GetEmployeeAsync(companyId, employeeId);
            if (employeeEntity == null)
                return Ok(new MsgReturn<string>() {Msg = "没有找到要删除的员工!", Success = false});
            _companyRepository.DeleteEmployee(employeeEntity);
            if (!await _companyRepository.SaveAsync())
                return Ok(new MsgReturn<string>() {Msg = "没有删除上", Success = false});

            return Ok(new MsgReturn<EmployeeDto>() {Msg = "删除成功!"});
        }

        // patch 太麻烦 了 ，放弃……
        // [HttpPatch("{employeeId}")]
        // public async Task<ActionResult<MsgReturn<EmployeeDto>>> PatchEmployeeForCompany(Guid companyId,
        //     Guid employeeId,
        //     JsonPatchDocument<EmployeeUpdateDto> patchDocument)
        // {
        //     if (!await _companyRepository.CompanyExistAsync(companyId))
        //         return Ok(new MsgReturn<string>() {Msg = "没有找到对应的公司!", Success = false});
        //     Employee employeeEntity = await _companyRepository.GetEmployeeAsync(companyId, employeeId);
        //     if (employeeEntity == null)
        //         return Ok(new MsgReturn<string>() {Msg = "没有找到要修改的员工!", Success = false});
        //     var dtoToPatch = _mapper.Map<EmployeeUpdateDto>(employeeEntity); //将entity 转换 成 updateDto
        //
        //     //需要处理验证错误
        //     patchDocument.ApplyTo(dtoToPatch);
        //     _mapper.Map(dtoToPatch, employeeEntity); //将 dto 转换 为 entity
        //     _companyRepository.UpdateEmployee(employeeEntity);
        //
        //     if (!await _companyRepository.SaveAsync())
        //         return Ok(new MsgReturn<string>() {Msg = "没有修改上", Success = false});
        //
        //     return Ok(new MsgReturn<EmployeeDto>() {Msg = "修改成功!"});
        // }
    }
}