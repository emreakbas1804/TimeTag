using System;
using System.ComponentModel.DataAnnotations;
using System.Reflection.Metadata;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Microsoft.IdentityModel.Tokens;
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
        private readonly IFileService _fileService;
        private readonly ILocalizationService _localizationService;
        private readonly IUserService _userService;
        private readonly IValidationService _validationService;
        private readonly ICryptoService _cryptoService;
        EntityResultModel entityResultModel = new();

        public EmployeeController(
            ICompanyService companyService,
            IDepartmentService departmentService,
            IEmployeeService employeeService,
            IFileService fileService,
            ILocalizationService localizationService,
            IUserService userService,
            IValidationService validationService,
            ICryptoService cryptoService
        )
        {
            _companyService = companyService;
            _departmentService = departmentService;
            _employeeService = employeeService;
            _fileService = fileService;
            _localizationService = localizationService;
            _userService = userService;
            _validationService = validationService;
            _cryptoService = cryptoService;
        }

        #region  Employee

        [Authorize]
        [HttpPost("AddEmployee")]
        public async Task<IActionResult> AddEmployee(AddEmployeeDTO employeeModel)
        {

            var checkEmail = await _validationService.ValidateEmailAsync(employeeModel.Email);
            if (checkEmail.Result != EntityResult.Success) return Ok(checkEmail);

            var isCompanyExist = _companyService.IsCompanyExist(employeeModel.CompanyId);
            if (!isCompanyExist) { entityResultModel.ResultMessage = await _localizationService.getLocalization("txt_firma_bulunamadi", "Company not found"); return Ok(entityResultModel); }

            var isDepartmentExist = _departmentService.IsDepartmentExist(employeeModel.DepartmentId);
            if (!isDepartmentExist) { entityResultModel.ResultMessage = await _localizationService.getLocalization("txt_departman_bulunamadi", "Department not found"); return Ok(entityResultModel); };

            employeeModel.Password = _cryptoService.GenerateRandomPassword();
            var addUser = await _userService.AddUser(new RegisterDTO()
            {
                Name = employeeModel.NameSurname?.Split(" ")[0],
                Surname = employeeModel.NameSurname.Split(" ")[1],
                Email = employeeModel.Email,
                Password = employeeModel.Password,
                IsFirstLogin = true,
                Phone = employeeModel.Phone
            });
            if (addUser.Result != EntityResult.Success)
            {
                employeeModel.rlt_User_Id = await _userService.GetUserIdByEmail(employeeModel.Email);
            }
            else
            {
                employeeModel.rlt_User_Id = addUser.Id;
            }


            if (employeeModel.Photo != null)
            {
                string[] accepFileExtensions = { ".jpg", ".jpeg", ".png", ".webp", };
                var fileUpload = await _fileService.UploadFile(employeeModel.Photo, "/images/users/profileImages/", 10, accepFileExtensions);
                if (fileUpload.Result != EntityResult.Success) return Ok(fileUpload);
                employeeModel.rlt_FileUpload_Id = fileUpload.Id;
            }

            var addEmployee = await _employeeService.AddEmployee(employeeModel);

            return Ok(addEmployee);
        }

        [Authorize]
        [HttpPut("UpdateEmployee")]
        public async Task<IActionResult> UpdateEmployee([Required] int departmentId, [Required] int employeeId, string nameSurname, string title, string phone, string address, string email, bool isActive, DateTime birthDay, DateTime startedJobTime, IFormFile photo)
        {
            var isDepartmentExist = _departmentService.IsDepartmentExist(departmentId);
            if (!isDepartmentExist)
            {
                entityResultModel.ResultMessage = await _localizationService.getLocalization("txt_departman_bulunamadi", "Department not found");
            }
            int rlt_FileUpload_Id = 0;
            if (photo != null)
            {
                string[] accepFileExtensions = { ".jpg", ".jpeg", ".png", ".webp", };
                var fileUpload = await _fileService.UploadFile(photo, "/images/users/profileImages/", 10, accepFileExtensions);
                if (fileUpload.Result != EntityResult.Success) return Ok(fileUpload);
                rlt_FileUpload_Id = fileUpload.Id;
            }
            var updateEmployee = await _employeeService.UpdateEmployee(departmentId, employeeId, nameSurname, title, phone, address, email, isActive, birthDay, startedJobTime, rlt_FileUpload_Id);
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


        #region Employee logs to job

        [HttpPost("AddLogEmployeeToJob")]
        public async Task<IActionResult> AddLogEmployeeToJob(string token)
        {
            var type = await _employeeService.GetLogType(token);
            var loginEmployeeToJob = await _employeeService.AddLogEmployeeToJob(token, type);
            return Ok(loginEmployeeToJob);
        }


        [Authorize]
        [HttpGet("GetLogsEmployee")]
        public async Task<IActionResult> GetLogsEmployee(int employeeId, DateTime? startDate, DateTime? endDate, int page = 1, int count = 5)
        {
            var loginEmployeeToJobLog = await _employeeService.GetLogsEmployee(employeeId, startDate, endDate, page, count);
            return Ok(loginEmployeeToJobLog);
        }

        [Authorize]
        [HttpGet("GetLogsCurrentEmployee")]
        public async Task<IActionResult> GetLogsCurrentEmployee(DateTime? startDate, DateTime? endDate, int page = 1, int count = 5)
        {
            var employeeId = await _employeeService.GetEmployeeIdByUserId(currentUser.Id);
            var loginEmployeeToJobLog = await _employeeService.GetLogsEmployee(employeeId, startDate, endDate, page, count);
            return Ok(loginEmployeeToJobLog);
        }


        [Authorize]
        [HttpGet("GetLog")]
        public async Task<IActionResult> GetLog(int logId)
        {
            var logEntity = await _employeeService.GetLog(logId);
            return Ok(logEntity);
        }


        [Authorize]
        [HttpPut("UpdateLog")]
        public async Task<IActionResult> UpdateLog(int logId, LogType type, DateTime processTime)
        {
            var logEntity = await _employeeService.UpdateLog(logId, type, processTime);
            return Ok(logEntity);
        }





        #endregion

    }

}