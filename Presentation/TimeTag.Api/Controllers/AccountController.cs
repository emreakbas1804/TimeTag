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
    public class AccountController : BaseController
    {        
        private readonly IValidationService _validationService;
        private readonly ICryptoService _cryptoService;
        private readonly IUserService _userService;
        EntityResultModel entityResultModel = new();
        
        public AccountController(            
            IValidationService validationService,
            ICryptoService cryptoService,
            IUserService userService
        )
        {
              
            _validationService = validationService;
            _cryptoService = cryptoService;
            _userService = userService;
        }


        [HttpPost("Register")]
        public async Task<IActionResult> Register(RegisterDTO model)
        {
         
            var checkEmail = _validationService.ValidateEmail(model.Email);
            if(checkEmail.Result != EntityResult.Success) return Ok(checkEmail);

            var checkPassword =_validationService.ValidatePassword(model.Password);
            if(checkPassword.Result != EntityResult.Success) return Ok(checkPassword);
            
            var addUserResult = await _userService.AddUser(model);            
            return Ok(addUserResult);
        }
        [AllowAnonymous]
        [HttpPost("Login")]
        public async Task<IActionResult> Login(string email, string password)
        {
            var checkEmail = _validationService.ValidateEmail(email);
            if(checkEmail.Result != EntityResult.Success) return Ok(checkEmail);

            var userLogin = await _userService.Login(email, password);                                            
            return Ok(userLogin);        
        }
        [Authorize]
        [HttpGet("Getuser")]        
        public IActionResult GetUser()
        {
            return Ok(currentUser);
        }
        

    }
    
}