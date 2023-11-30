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
    public class CompanyController : BaseController
    {

        private readonly ILicanceService _licanceService;
        private readonly ICompanyService _companyService;
        private readonly IFileService _fileService;
        private readonly IDepartmentService _departmentService;
        EntityResultModel entityResultModel = new();

        public CompanyController(
            ILicanceService licanceService,
            ICompanyService companyService,
            IFileService fileService,
            IDepartmentService departmentService
        )
        {
            _licanceService = licanceService;
            _companyService = companyService;
            _fileService = fileService;
            _departmentService = departmentService;
        }

        [Authorize]
        [HttpPost("AddLicance")]
        public async Task<IActionResult> AddLicance()
        {
            var serialNumber = await _licanceService.AddLicance();
            entityResultModel.Result = EntityResult.Success;
            entityResultModel.ResultObject = serialNumber;
            return Ok(entityResultModel);
        }


        #region Company

        [Authorize]
        [HttpPost("AddCompany")]
        public async Task<IActionResult> AddCompany(string serialNumber, string title, string address, string description, string webSite, IFormFile logo)
        {
            var licanceId = await _licanceService.GetLicanceId(serialNumber);
            if (licanceId <= 0)
            {
                entityResultModel.ResultMessage = "Lisans anahtarı geçersiz";
                return Ok(entityResultModel);
            }

            int logoId = 0;
            if (logo != null)
            {
                string[] accepFileExtensions = { ".jpg", ".jpeg", ".png", ".webp", };
                var fileUpload = await _fileService.UploadFile(logo, "/images/companies/logos/", 10, accepFileExtensions);
                if (fileUpload.Result != EntityResult.Success) return Ok(fileUpload);
                logoId = fileUpload.Id;
            }

            var addCompany = await _companyService.AddCompany(currentUser.Id, licanceId, title, address, description, webSite, logoId);
            return Ok(addCompany);
        }

        [Authorize]
        [HttpPost("UpdateCompany")]
        [PermissionAttr(Permission = PermissonTags.Company)]
        public async Task<IActionResult> UpdateCompany(IFormFile logo, int companyId, string title, string address, string description, string webSite)
        {
            int logoId = 0;
            if (logo != null)
            {
                string[] accepFileExtensions = { ".jpg", ".jpeg", ".png", ".webp", };
                var fileUpload = await _fileService.UploadFile(logo, "/images/companies/logos/", 10, accepFileExtensions);
                if (fileUpload.Result != EntityResult.Success) return Ok(fileUpload);
                logoId = fileUpload.Id;
            }

            var updateCompany = await _companyService.UpdateCompany(logoId, companyId, title, address, description, webSite);
            return Ok(updateCompany);
        }

        [Authorize]
        [HttpGet("GetCompanies")]
        public async Task<IActionResult> GetCompanies()
        {
            var userCompanies = await _companyService.GetCompanies(currentUser.Id);
            entityResultModel.Result = EntityResult.Success;
            entityResultModel.ResultObject = userCompanies;
            return Ok(entityResultModel);
        }

        [Authorize]
        [HttpGet("GetCompany")]
        [PermissionAttr(Permission = PermissonTags.Company)]
        public async Task<IActionResult> GetCompany(int companyId)
        {
            var company = await _companyService.GetCompany(companyId);
            if (company == null)
            {
                entityResultModel.ResultMessage = "Firma bulunamadı.";
                return Ok(entityResultModel);
            }

            entityResultModel.Result = EntityResult.Success;
            entityResultModel.ResultObject = company;
            return Ok(entityResultModel);
        }

        #endregion


        #region  Company-Departments


        [Authorize]
        [HttpPost("AddDepartment")]
        public async Task<IActionResult> AddDepartment(int companyId, string name, string address, string description, string startJobTime, string finishJobTime)
        {
            var addDepartment = await _departmentService.AddDepartment(companyId, name, address, description, startJobTime, finishJobTime);
            return Ok(addDepartment);
        }

        [Authorize]
        [HttpPut("UpdateDepartment")]
        public async Task<IActionResult> UpdateDepartment(int departmentId, string name, string address, string description, string startJobTime, string finishJobTime)
        {
            var updateDepartment = await _departmentService.UpdateDepartment(departmentId, name, address, description, startJobTime, finishJobTime);
            return Ok(updateDepartment);
        }

        [Authorize]
        [HttpGet("GetDepartment")]
        public async Task<IActionResult> GetDepartment(int departmentId)
        {
            var department = await _departmentService.GetDepartment(departmentId);
            if (department == null)
            {
                entityResultModel.ResultMessage = "Departman bulunamadı.";
                return Ok(entityResultModel);
            }
            entityResultModel.Result = EntityResult.Success;
            entityResultModel.ResultObject = department;
            return Ok(entityResultModel);
        }


        [Authorize]
        [HttpGet("GetDepartments")]
        public async Task<IActionResult> GetDepartments(int companyId)
        {
            var departments = await _departmentService.GetDepartments(companyId);
            if (departments == null)
            {
                entityResultModel.ResultMessage = "Departman bulunamadı.";
                return Ok(entityResultModel);
            }
            entityResultModel.Result = EntityResult.Success;
            entityResultModel.ResultObject = departments;
            return Ok(entityResultModel);
        }

        [Authorize]
        [HttpGet("GetDepartmentsCount")]
        public async Task<IActionResult> GetDepartmentsCount(int companyId)
        {
            var departmentCount = await _departmentService.GetDepartmentsCount(companyId);           
            entityResultModel.Result = EntityResult.Success;
            entityResultModel.ResultObject = departmentCount;
            return Ok(entityResultModel);
        }



        [Authorize]
        [HttpDelete("DeleteDepartment")]
        public async Task<IActionResult> DeleteDepartment(int departmentId)
        {
            var deleteDepartment = await _departmentService.DeleteDepartment(departmentId);            
            return Ok(deleteDepartment);
        }



        #endregion







    }

}