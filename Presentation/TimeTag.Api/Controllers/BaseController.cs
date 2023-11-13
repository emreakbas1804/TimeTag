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
    public override void OnActionExecuting(ActionExecutingContext context)
    {
        // jwt ile user alınacak ve current user set edilecek
        base.OnActionExecuting(context);
    }
    public override Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        if(_userService == null) _userService = HttpContext.RequestServices.GetService<IUserService>();
        return base.OnActionExecutionAsync(context, next);
    }
}