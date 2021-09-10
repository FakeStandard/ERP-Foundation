using ERP_Foundation.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace ERP_Foundation.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            Login();

            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        private void Login()
        {
            // 使用 Session 紀錄使用者資訊

            // connect db and verify login user and password.
            bool login = true;

            if (login)
            {
                // 將使用者資訊紀錄到 Session
                // Set Session
                HttpContext.Session.SetInt32("UserID", 1);
                HttpContext.Session.SetString("UserName", "系統管理者");

                // 設置靜態類別屬性提供給View顯示
                Users.ID = (int)HttpContext.Session.GetInt32("UserID");
                Users.Name = HttpContext.Session.GetString("UserName");
            }
            else
            {
                // 否則停留在登入頁面
            }
        }
    }
}
