using System;
using System.Web.Mvc;

namespace SCBaseProject.Areas.SysAdmin.Controllers
{
    internal class SysadminValidationAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext actionContext)
        {
            if (actionContext.HttpContext.Session[Helpers.SessionConstants.SYSADMIN_SESSION_INFO] == null)
            {
                actionContext.Result = new RedirectResult("~/SysAdmin/Home/Login");
            }

            base.OnActionExecuting(actionContext);
        }
    }
}