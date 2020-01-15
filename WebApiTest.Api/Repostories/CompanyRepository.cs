using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebApiTest.Api.Data;
using WebApiTest.Api.Entities;
using WebApiTest.Api.Services;

namespace WebApiTest.Api.Repostories
{
    public class CompanyRepository : ICompanyRepository
    {
        private readonly AppDbContext _context;

        public CompanyRepository(AppDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<IEnumerable<Company>> GetCompaniesAsync()
        {
            return await _context.Companies.ToListAsync();
        }

        public async Task<Company> GetCompanyAsync(Guid companyId)
        {
            return companyId == Guid.Empty
                ? throw new ArgumentNullException(nameof(companyId))
                : await _context.Companies.FirstOrDefaultAsync(x => x.Id == companyId);
        }

        public void AddCompany(Company company)
        {
            if (company != null)
            {
                company.Id = Guid.NewGuid();
                foreach (var employee in company.Employees)
                {
                    employee.Id = Guid.NewGuid();
                }

                _context.Companies.Add(company);
            }
            else
            {
                throw new ArgumentNullException(nameof(company));
            }
        }

        public void UpdateCompany(Company company)
        {
        }

        public void DeleteCompany(Company company)
        {
            if (company != null) _context.Companies.Remove(company);
            throw new ArgumentNullException(nameof(company));
        }

        public async Task<bool> CompanyExistAsync(Guid companyId)
        {
            if (companyId == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(companyId));
            }

            return await _context.Companies.AnyAsync(x => x.Id == companyId);
        }

        public async Task<IEnumerable<Employee>> GetEmployeeAsync(Guid companyId)
        {
            if (companyId == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(companyId));
            }

            return await _context.Employees
                .Where(x => x.CompanyId == companyId)
                .OrderBy(x => x.EmployeeNo)
                .ToListAsync();
        }

        public async Task<Employee> GetEmployeeAsync(Guid companyId, Guid employeeId)
        {
            if (companyId == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(companyId));
            }

            if (employeeId == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(employeeId));
            }

            return await _context.Employees
                .Where(x => x.CompanyId == companyId && x.Id == employeeId)
                .FirstOrDefaultAsync();
        }

        public void AddEmployee(Guid companyId, Employee employee)
        {
            if (companyId == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(companyId));
            }

            if (employee == null)
            {
                throw new ArgumentNullException(nameof(employee));
            }

            employee.CompanyId = companyId;

            _context.Employees.Add(employee);
        }

        public void UpdateEmployee(Employee employee)
        {
        }

        public void DeleteEmployee(Employee employee)
        {
            if (employee != null) _context.Employees.Remove(employee);
            throw new ArgumentNullException(nameof(employee));
        }

        public async Task<bool> SaveAsync()
        {
            return await _context.SaveChangesAsync() >= 0;
        }
    }
}