using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Cartproducts.Models;
using System.Web.Security;

namespace Cartproducts.Controllers
{
    public class AccountController : Controller
    {
        // GET: Account
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(Models.Membership model)
        {
            using (var context = new CartEntities())
            {
                bool isValid = context.Users.Any(x=>x.UserName == model.UserName && x.Password == model.Password);
                if(isValid)
                {
                    FormsAuthentication.SetAuthCookie(model.UserName,false);
                    return RedirectToAction("Index", "Products");
                }
                ModelState.AddModelError("","Invalid username and password");
                return View();
            }
                
        }

        public ActionResult Signup()
        {
            var User = new User();
            
            User.RoleId = 2;
            return View(User);
        }

        [HttpPost]
        public ActionResult Signup(User model)
        {


            using (var context = new CartEntities())
            {
                context.Users.Add(model);
                context.SaveChanges();
            }
             return RedirectToAction("Login");
        }
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login");
        }
    }
}