using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Security.AccessControl;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TimeTag.Application.Abstractions;
using TimeTag.Application.DTO;
using TimeTag.Domain.Entities;
using TimeTag.Domain.Enums;
using TimeTag.Persistence.Context;
using static TimeTag.Persistence.Concretes.UserService;

namespace TimeTag.Api.Controllers
{
    [Route("[controller]")]
    [Authorize(Roles = "Admin")]
    public class AdminController : BaseController
    {
        private readonly IValidationService _validationService;
        private readonly IUserService _userService;
        private readonly ILocalizationService _localizationService;
        private readonly ILicanceService _licanceService;
        EntityResultModel entityResultModel = new();


        public AdminController(
            IValidationService validationService,
            ILocalizationService localizationService,
            ILicanceService licanceService,
            IUserService userService
        )
        {
            _validationService = validationService;
            _userService = userService;
            _licanceService = licanceService;
            _localizationService = localizationService;
        }


        [HttpPost("AddLicance")]
        public async Task<IActionResult> AddLicance()
        {
            var serialNumber = await _licanceService.AddLicance();
            entityResultModel.Result = EntityResult.Success;
            entityResultModel.ResultObject = serialNumber;
            return Ok(entityResultModel);
        }

        [HttpPost("AddLocalization")]
        public async Task<IActionResult> AddLocalization(string langCode, string tagName, string value)
        {
            var langId = await _localizationService.getLanguageIdByLangCode(langCode);
            if (langId == 0) return Ok(entityResultModel);

            await _localizationService.addLocalization(langId, tagName, value);
            entityResultModel.Result = EntityResult.Success;
            return Ok(entityResultModel);
        }

       


    }


}
