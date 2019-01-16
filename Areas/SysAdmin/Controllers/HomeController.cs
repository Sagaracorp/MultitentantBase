using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;
using System.Web.Security;
using SCBaseProject.Models;
using SCBaseProject.Helpers;

namespace SCBaseProject.Areas.SysAdmin.Controllers
{
    public class HomeController : Controller
    {
        private SCBaseProjectDBEntities db = new SCBaseProjectDBEntities();
        private string Default_SysAdmin_Username = WebConfigurationManager.AppSettings["SysAdmin_Username"];
        private string Default_SysAdmin_Password = WebConfigurationManager.AppSettings["SysAdmin_Password"];
        

        [HttpGet]
        [AllowAnonymous]
        public ActionResult Login()
        {
            Session.Abandon();
            FormsAuthentication.SignOut();
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginViewModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                var user = Default_SysAdmin_Username.ToUpper().Equals(model.Email.ToUpper()) && Default_SysAdmin_Password == model.Password;
                if (user)
                {
                    Session[SessionConstants.SYSADMIN_SESSION_INFO] = 1;
                    FormsAuthentication.SetAuthCookie(Default_SysAdmin_Username, false);
                    return RedirectToAction("Index");

                }
                else
                {
                    ModelState.AddModelError("", "Invalid username or password.");
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            Session.Abandon();
            FormsAuthentication.SignOut();
            return RedirectToAction("Login");
        }

        //
        // GET: /Sysadmin/Home/
        [SysadminValidation]
        public ActionResult Index()
        {

            SysadminHomeViewModel obj = new SysadminHomeViewModel()
            {
                User = db.SysUsers.Where(c => c.IsDeleted == false).Count(),
                Company = db.SysCompanies.Where(c => c.IsDeleted == false).Count(),
                
            };

            return View(obj);
        }

    }
}