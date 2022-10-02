using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;

namespace FundaClearApp.Controllers
{
    public class AccountController : Controller
    {
        public string connectionString;

        private readonly ILogger<AccountController> _logger;
        private readonly IConfiguration configuration;
      
        public AccountController(ILogger<AccountController> logger, IConfiguration iConfiguration)
        {
            _logger = logger;
            connectionString = iConfiguration.GetConnectionString("DefaultConnection");// ConnectionHelper.GetConnectionString();
            configuration = iConfiguration;
        }

       
        public IActionResult Logout()
        {
            HttpContext.Session = null;

            var login = HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login");
        }

        //[Authorize]
        public IActionResult Dashboard()
        {
            return View();
        }

       
        public ActionResult Signup()
        {
            return View();
        }
    }
}
