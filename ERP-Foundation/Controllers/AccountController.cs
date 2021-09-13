using ERP_DataAccess;
using ERP_Foundation.Infrastructure.Helpers;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ERP_Foundation.Controllers
{
    public class AccountController : Controller
    {
        private ERPFoundationContext _context;
        public AccountController(ERPFoundationContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(string account, string password)
        {
            if (string.IsNullOrWhiteSpace(account) || string.IsNullOrWhiteSpace(password))
            {
                ViewBag.error = "Invalid Account.";
                return View("Index");
            }

            //var obj = _context.Users
            //    .Where(c => c.Account.Equals(account) && c.Password.Equals(password)).FirstOrDefault();

            var obj = _context.Users
                .Where(c => c.Account.ToUpper().Equals(account.ToUpper())).FirstOrDefault();

            if (obj != null)
            {
                // Hash compare for password
                // dosomething...

                if (obj.Password.Equals(password))
                {
                    SessionHelper.SetObjectAsJson(HttpContext.Session, "user", obj);

                    return RedirectToAction("Index", "Home");
                }
            }

            ViewBag.error = "Invalid Account.";

            return View("Index");
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            // or
            //HttpContext.Session.Remove("user");

            return View("Index");
        }
    }
}
