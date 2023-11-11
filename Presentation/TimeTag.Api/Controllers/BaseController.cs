using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace TimeTag.Api.Controllers;
public class BaseController : Controller
{
    public override void OnActionExecuting(ActionExecutingContext context)
    {
        
        base.OnActionExecuting(context);
    }
}