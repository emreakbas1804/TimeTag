using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using TimeTag.Application.Abstractions;
using TimeTag.Application.DTO;

namespace TimeTag.Api.Controllers;
public class BaseController : Controller
{
    private IUserService _userService;
    public CurrentUser currentUser;    
    public override Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        if(_userService == null) _userService = HttpContext.RequestServices.GetService<IUserService>();
        return base.OnActionExecutionAsync(context, next);
    }
    public override void OnActionExecuting(ActionExecutingContext context)
    {
        var jwtToken = HttpContext.Request.Headers["Authorization"].ToString().Replace("\"\"", "").Replace("Bearer", "").Trim();
        if(!string.IsNullOrEmpty(jwtToken))
        {
            currentUser = _userService.GetCurrentUser(jwtToken);
        }
        
        base.OnActionExecuting(context);
    }
}