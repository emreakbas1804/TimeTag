using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using TimeTag.Api.Extensions;
using TimeTag.Application.Abstractions;
using TimeTag.Application.Attr;
using TimeTag.Application.DTO;

namespace TimeTag.Api.Controllers;
public class BaseController : Controller
{
    private IUserService _userService;
    private IPermissionService _permissonService;
    public CurrentUser currentUser;
    public override Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        if (_userService == null) _userService = HttpContext.RequestServices.GetService<IUserService>();
        if(_permissonService == null) _permissonService = HttpContext.RequestServices.GetService<IPermissionService>();

        return base.OnActionExecutionAsync(context, next);
    }
    public override void OnActionExecuting(ActionExecutingContext context)
    {
        var jwtToken = HttpContext.Request.Headers["Authorization"].ToString().Replace("\"\"", "").Replace("Bearer", "").Trim();
        if (!string.IsNullOrEmpty(jwtToken))
        {
            currentUser = _userService.GetCurrentUser(jwtToken);
            //var actionDescriptor = context.ActionDescriptor as ControllerActionDescriptor;
            // if (actionDescriptor != null)
            // {
            //     var requiredCommonFeatureAttribute = actionDescriptor.MethodInfo.GetCustomAttributes(false).FirstOrDefault(q => q.GetType() == typeof(PermissionAttr)) as PermissionAttr; // Actionda Attribute olup olmadığını kontrol et
            //     if(requiredCommonFeatureAttribute != null)
            //     {
                    
            //         var companyId = int.Parse(context.ActionArguments["companyId"]?.ToString());
            //         var departmentId = int.Parse(context.ActionArguments["departmentId"]?.ToString());
            //         if(companyId >0 )
            //         {
            //             var hasPermissionForThisAction = _permissonService.HasUserThisPermissionInThisCompanyByTagName(userId : currentUser.Id,departmentId : departmentId,companyId : companyId,permissionTagName:requiredCommonFeatureAttribute.Permission);
            //             if(!hasPermissionForThisAction)
            //             {
            //                 // there is no permisson
            //                 context.Result = BadRequest();
            //             }
            //         }
                    
            //     }
            // }
        }

        base.OnActionExecuting(context);
    }
}