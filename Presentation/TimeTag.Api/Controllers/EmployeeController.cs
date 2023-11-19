using System.ComponentModel.DataAnnotations;
using System.Reflection.Metadata;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TimeTag.Application.Abstractions;
using TimeTag.Application.Attr;
using TimeTag.Application.DTO;
using TimeTag.Domain.Enums;

namespace TimeTag.Api.Controllers
{
    [Route("[controller]")]
    public class EmployeeController : BaseController
    {
        private readonly ICompanyService _companyService;
        private readonly IDepartmentService _departmentService;
        private readonly IEmployeeService _employeeService;
        EntityResultModel entityResultModel = new();

        public EmployeeController(            
            ICompanyService companyService,            
            IDepartmentService departmentService,
            IEmployeeService employeeService
        )
        {
            _companyService = companyService;
            _departmentService = departmentService;
            _employeeService = employeeService;
        }

        [HttpPost("AddEmployee")]
        public async Task<IActionResult> AddEmployee(AddEmployeeDTO employeeModel)
        {
            var isCompanyExist = _companyService.IsCompanyExist(employeeModel.CompanyId);
            if(!isCompanyExist)
            {
                entityResultModel.ResultMessage = "Firma bulunamadı.";
                return Ok(entityResultModel);
            }

            var isDepartmentExist = _departmentService.IsDepartmentExist(employeeModel.DepartmentId);
            if(!isDepartmentExist)
            {
                entityResultModel.ResultMessage = "Departman bulunamadı.";
            }

            var addEmployee = await _employeeService.AddEmployee(employeeModel);
            return Ok(addEmployee);
        }

        

    }

}