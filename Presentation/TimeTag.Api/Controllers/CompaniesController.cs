using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Security.AccessControl;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TimeTag.Application.Abstractions;
using TimeTag.Application.DTO;
using TimeTag.Domain.Enums;
using static TimeTag.Persistence.Concretes.UserService;

namespace TimeTag.Api.Controllers
{
    [Route("[controller]")]
    public class CompaniesController : BaseController
    {
        private readonly IValidationService _validationService;
        private readonly ICryptoService _cryptoService;
        private readonly IUserService _userService;
        private readonly ILicanceService _licanceService;
        private readonly ICompanyService _companyService;
        EntityResultModel entityResultModel = new();

        public CompaniesController(
            IValidationService validationService,
            ICryptoService cryptoService,
            IUserService userService,
            ILicanceService licanceService,
            ICompanyService companyService
        )
        {

            _validationService = validationService;
            _cryptoService = cryptoService;
            _userService = userService;
            _licanceService = licanceService;
            _companyService = companyService;
        }

        [Authorize]
        [HttpPost("AddLicance")]
        public async Task<IActionResult> AddLicance()
        {
            var serialNumber =await _licanceService.AddLicance();
            entityResultModel.Result = EntityResult.Success;
            entityResultModel.ResultObject = serialNumber;
            return Ok(entityResultModel);
        }

        [Authorize]
        [HttpPost("AddCompany")]
        public async Task<IActionResult> AddCompany(string serialNumber, string title, string address, string description, string webSite)
        {
            var licanceId = await _licanceService.GetLicanceId(serialNumber);
            if(licanceId <= 0) 
            {
                entityResultModel.ResultMessage = "Lisans anahtarı geçersiz";
                return Ok(entityResultModel);
            }
            var addCompany = await _companyService.AddCompany(currentUser.Id, licanceId,title,address,description,webSite);            
            return Ok(addCompany);
        }


    }

}