﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApiTest.Api.Entities;
using WebApiTest.Api.Entities.DatabaseEntities;

namespace WebApiTest.Api.Services
{
    public interface ICompanyRepository : IBaseRepository<Company>
    {
        //公司相关 的接口方法 
        Task<PaginatedList<Company>> GetCompaniesAsync([FromQuery] QueryParameter queryParameter);
        Task<IEnumerable<Company>> GetCompaniesAsync(IEnumerable<Guid> companyIds);
        Task<Company> GetCompanyAsync(Guid companyId);
        void AddCompany(Company company);
        void UpdateCompany(Company company);
        void DeleteCompany(Company company);
        Task<bool> CompanyExistAsync(Guid companyId);


        //员工相关 的接口方法 
        Task<PaginatedList<Employee>> GetEmployeesWithAllCompany([FromQuery] QueryParameter queryParameter);
        Task<IEnumerable<Employee>> GetEmployeesAsync(Guid companyId);
        Task<Employee> GetEmployeeAsync(Guid companyId, Guid employeeId);
        void AddEmployee(Guid companyId, Employee employee);
        void UpdateEmployee(Employee employee);
        void DeleteEmployee(Employee employee);
    }
}