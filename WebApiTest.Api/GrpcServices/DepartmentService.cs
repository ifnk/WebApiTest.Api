using System.Threading.Tasks;
using Grpc.Core;
using WebApiTest.Api.Services;
using static WebApiTest.Departments;

namespace WebApiTest.Api.GrpcServices
{
    public class DepartmentService : DepartmentsBase
    {
        private readonly IDepartmentRepository _departmentRepository;

        public DepartmentService(IDepartmentRepository departmentRepository)
        {
            _departmentRepository = departmentRepository;
        }

        public override async Task<GetAllDepartmentsResponse> GetAll(GetAllDepartmentsRequest request,
            ServerCallContext context)
        {
            var result = new GetAllDepartmentsResponse();
            var departments = await _departmentRepository.GetAllAsync();
            result.Departments.AddRange(departments); // add 添加 一个 addRange添加一堆……
            return result;
        }

        public override async Task<AddDepartmentResponse>
            Add(AddDepartmentRequest request, ServerCallContext context)
        {
            var department = await _departmentRepository.AddAsync(request.Department);
            var result = new AddDepartmentResponse()
            {
                Department = department
            };
            return result;
        }
    }
}