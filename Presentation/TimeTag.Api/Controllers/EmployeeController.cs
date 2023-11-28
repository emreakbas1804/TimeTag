using System;
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

        #region  Employee

        [Authorize]
        [HttpPost("AddEmployee")]
        public async Task<IActionResult> AddEmployee(AddEmployeeDTO employeeModel)
        {
            var isCompanyExist = _companyService.IsCompanyExist(employeeModel.CompanyId);
            if (!isCompanyExist)
            {
                entityResultModel.ResultMessage = "Firma bulunamadı.";
                return Ok(entityResultModel);
            }

            var isDepartmentExist = _departmentService.IsDepartmentExist(employeeModel.DepartmentId);
            if (!isDepartmentExist)
            {
                entityResultModel.ResultMessage = "Departman bulunamadı.";
            }

            var addEmployee = await _employeeService.AddEmployee(employeeModel);
            return Ok(addEmployee);
        }

        [Authorize]
        [HttpPut("UpdateEmployee")]
        public async Task<IActionResult> UpdateEmployee([Required] int departmentId, [Required] int employeeId, string nameSurname, string title, string phone, string address, string email, bool isActive, DateTime birthDay, DateTime startedJobTime)
        {
            var isDepartmentExist = _departmentService.IsDepartmentExist(departmentId);
            if (!isDepartmentExist)
            {
                entityResultModel.ResultMessage = "Departman bulunamadı.";
            }

            var updateEmployee = await _employeeService.UpdateEmployee(departmentId, employeeId, nameSurname, title, phone, address, email, isActive, birthDay, startedJobTime);
            return Ok(updateEmployee);
        }

        [Authorize]
        [HttpGet("GetEmployeesCompany")]
        public async Task<IActionResult> GetEmployeesCompany(int companyId, int? departmentId)
        {
            var employees = await _employeeService.GetEmployeesCompany(companyId, departmentId);
            return Ok(employees);
        }

        [Authorize]
        [HttpGet("GetEmployeesCompanyCount")]
        public async Task<IActionResult> GetEmployeesCompanyCount(int companyId, int? departmentId)
        {
            var employeesCount = await _employeeService.GetEmployeesCompanyCount(companyId, departmentId);
            entityResultModel.Result = EntityResult.Success;
            entityResultModel.ResultObject = employeesCount;
            return Ok(entityResultModel);
        }


        [Authorize]
        [HttpGet("GetEmployeeCompany")]
        public async Task<IActionResult> GetEmployeeCompany(int employeeId)
        {
            var employees = await _employeeService.GetEmployee(employeeId);
            return Ok(employees);
        }

        #endregion


        #region  Employee-Bank

        [Authorize]
        [HttpPost("AddEmployeeBank")]
        public async Task<IActionResult> AddEmployeeBank(int employeeId, string bankName, string ownerName, string iban)
        {
            var addEmployeeBank = await _employeeService.AddEmployeeBank(employeeId, bankName, ownerName, iban);
            return Ok(addEmployeeBank);
        }

        [Authorize]
        [HttpPut("UpdateEmployeeBank")]
        public async Task<IActionResult> UpdateEmployeeBank(int bankId, string bankName, string ownerName, string iban)
        {
            var updateEmployeeBank = await _employeeService.UpdateEmployeeBank(bankId, bankName, ownerName, iban);
            return Ok(updateEmployeeBank);
        }

        [Authorize]
        [HttpGet("GetEmployeeBanks")]
        public async Task<IActionResult> GetEmployeeBanks(int employeeId, int? bankId)
        {
            var employeeBanks = await _employeeService.GetEmployeeBanks(employeeId, bankId);
            return Ok(employeeBanks);
        }

        [Authorize]
        [HttpDelete("DeleteEmployeeBank")]
        public async Task<IActionResult> DeleteEmployeeBank(int bankId)
        {
            var employeeBanks = await _employeeService.DeleteEmployeeBank(bankId);
            return Ok(employeeBanks);
        }


        #endregion

        #region Employee login-logout to job

        [HttpPost("AddLoginEmployeeToJob")]
        public async Task<IActionResult> AddLoginEmployeeToJob(string token)
        {
            var loginEmployeeToJob = await _employeeService.AddLoginEmployeeToJob(token);
            return Ok(loginEmployeeToJob);
        }

        [HttpPost("AddLogoutEmployeeToJob")]
        public async Task<IActionResult> AddLogoutEmployeeToJob(string token)
        {
            var loginEmployeeToJob = await _employeeService.AddLogoutEmployeeToJob(token);
            return Ok(loginEmployeeToJob);
        }

        [Authorize]
        [HttpGet("GetLoginLogEmployee")]
        public async Task<IActionResult> GetLoginLogEmployee(int emploeeId)
        {
            var loginEmployeeToJobLog = await _employeeService.GetLoginLogEmployee(emploeeId);
            return Ok(loginEmployeeToJobLog);
        }

        [Authorize]
        [HttpGet("GetLogoutLogEmployee")]
        public async Task<IActionResult> GetLogoutLogEmployee(int emploeeId)
        {
            var loginEmployeeToJobLog = await _employeeService.GetLogoutLogEmployee(emploeeId);
            return Ok(loginEmployeeToJobLog);
        }



        #endregion

    }

}