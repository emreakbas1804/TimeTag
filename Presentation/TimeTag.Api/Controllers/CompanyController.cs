using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Security.AccessControl;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TimeTag.Application.Abstractions;
using TimeTag.Application.DTO;
using TimeTag.Domain.Enums;
using static TimeTag.Persistence.Concretes.UserService;

namespace TimeTag.Api.Controllers
{
    [Route("[controller]")]
    public class CompanyController : BaseController
    {
        private readonly IValidationService _validationService;
        private readonly ICryptoService _cryptoService;
        private readonly IUserService _userService;
        private readonly ILicanceService _licanceService;
        private readonly ICompanyService _companyService;
        private readonly IFileService _fileService;
        EntityResultModel entityResultModel = new();

        public CompanyController(
            IValidationService validationService,
            ICryptoService cryptoService,
            IUserService userService,
            ILicanceService licanceService,
            ICompanyService companyService,
            IFileService fileService
        )
        {

            _validationService = validationService;
            _cryptoService = cryptoService;
            _userService = userService;
            _licanceService = licanceService;
            _companyService = companyService;
            _fileService = fileService;
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

        [Authorize]
        [HttpPost("AddCompany")]
        public async Task<IActionResult> AddCompany(string serialNumber, string title, string address, string description, string webSite)
        {
            var licanceId = await _licanceService.GetLicanceId(serialNumber);
            if (licanceId <= 0)
            {
                entityResultModel.ResultMessage = "Lisans anahtarı geçersiz";
                return Ok(entityResultModel);
            }

            var addCompany = await _companyService.AddCompany(currentUser.Id, licanceId, title, address, description, webSite);
            return Ok(addCompany);
        }

        [Authorize]
        [HttpPost("UpdateCompany")]
        public async Task<IActionResult> UpdateCompany(IFormFile logo, int companyId, string title, string address, string description, string webSite)
        {


            string[] accepFileExtensions = { ".jpg", ".jpeg", ".png", ".webp", };
            var fileUpload = await _fileService.UploadFile(logo, "/images/companies/logos/", 10, accepFileExtensions);
            if (fileUpload.Result != EntityResult.Success) return Ok(fileUpload);

            var addCompany = await _companyService.UpdateCompany(fileUpload.Id, companyId, title, address, description, webSite);
            return Ok(addCompany);
        }








    }

}