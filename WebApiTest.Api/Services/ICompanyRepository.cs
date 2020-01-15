using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiTest.Api.Entities;

namespace WebApiTest.Api.Services
{
    public interface ICompanyRepository

    {
        //公司相关 的接口方法 
        Task<IEnumerable<Company>> GetCompaniesAsync();
        Task<Company> GetCompanyAsync(Guid companyId);
        void AddCompany(Company company);
        void UpdateCompany(Company company);
        void DeleteCompany(Company company);
        Task<bool> CompanyExistAsync(Guid companyId);


        //员工相关 的接口方法 
        Task<IEnumerable<Employee>> GetEmployeeAsync(Guid companyId);
        Task<Employee> GetEmployeeAsync(Guid companyId, Guid employeeId);
        void AddEmployee(Guid companyId, Employee employee);
        void UpdateEmployee(Employee employee);
        void DeleteEmployee(Employee employee);


        //操作完成后 要保存下……
        Task<bool> SaveAsync();
    }
}