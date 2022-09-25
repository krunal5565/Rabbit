using FundaClear.Business;
using FundaClearApp.Business.BusinessModel;
using FundaClearApp.Utilities;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;

namespace FundaClearApp.Controllers
{
    public class UserController : Controller
    {
        public string connectionString;

        private readonly ILogger<UserController> _logger;
        private readonly IConfiguration configuration;

        public UserController(ILogger<UserController> logger, IConfiguration iConfiguration)
        {
            _logger = logger;
            connectionString = iConfiguration.GetConnectionString("DefaultConnection");// ConnectionHelper.GetConnectionString();
            configuration = iConfiguration;
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult LoadStudents()
        {
            try
            {
                List<StudentAPIResponse> lstStudentModel =  APIHelper.GetStudents();

                return Json(new {  recordsTotal = lstStudentModel.Count, data = lstStudentModel });
            }
            catch (Exception)
            {
                throw;
            }
        }

        public ActionResult Grid()
        {
            return View();
        }

        public IActionResult Login()
        {
            return View();
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

        //[HttpPost]
        //public ActionResult Login(TenantDTO model)
        //{
        //    if (model != null && !string.IsNullOrEmpty(model.LoginId) && !string.IsNullOrEmpty(model.PassKey))
        //    {
        //        //TenantManager objTenantManager = new TenantManager(connectionString);
                
        //        //ResponseDTO objResponseDTO = objTenantManager.CheckLogin(model.LoginId, model.PassKey);

        //        //if (objResponseDTO.Status)
        //        //{
        //        //    //Create the identity for the user  
        //        //    //var identity = new ClaimsIdentity(new[] { new Claim(ClaimTypes.Name, model.LoginId) },
        //        //    //    CookieAuthenticationDefaults.AuthenticationScheme);

        //        //   var identity = new ClaimsIdentity(new[] {
        //        //    new Claim(ClaimTypes.Name, model.LoginId),
        //        //}, CookieAuthenticationDefaults.AuthenticationScheme);


        //        //    var principal = new ClaimsPrincipal(identity);

        //        //    var login = HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

        //        //    TenantDTO objTenantDTO = objResponseDTO.Data as TenantDTO;

        //            HttpContext.Session.SetString("TenantId", "TestUser");
        //            HttpContext.Session.SetString("TenantName", "TestUser");

        //            return RedirectToAction("Dashboard", "Account");
        //        //}
        //        //else
        //        //{
        //        //    TempData[Constants.KeyErrorMessage] = Constants.ErrorInvalidLoginCredentials;
        //        //}
        //    }
        //    else
        //    {
        //        TempData[Constants.KeyErrorMessage] = Constants.ErrorEnterUsernamePassword;
        //    }

        //    return RedirectToAction("Login", "Account");
        //}

        public ActionResult Signup()
        {
            return View();
        }


        //[HttpPost]
        //public ActionResult Signup(TenantDTO model)
        //{
        //    string errorMessage = ValidationHelper.ValidateSignup(model);

        //    if (string.IsNullOrEmpty(errorMessage))
        //    {
        //        TenantManager objTenantManager = new TenantManager(connectionString);

        //        model.CreatedBy = model.TenantName;
        //        model.CreatedDate = DateTime.UtcNow;
        //        model.ActiveStatus = true;

        //        ResponseDTO responseDTO = objTenantManager.Save(model);

        //        if (responseDTO != null)
        //        {
        //            if(responseDTO.Status)
        //            {
        //                TempData[Constants.KeySuccessMessage] = Constants.SuccessSignup;
        //                model = new TenantDTO();
        //                return RedirectToAction("Login", "Account");
        //            }
        //            else
        //            {
        //                TempData[Constants.KeySuccessMessage] = responseDTO.Message;
        //            }
        //        }
        //        else
        //        {
        //            TempData[Constants.KeySuccessMessage] = Constants.ErrorOpps; 
        //        }
        //    }
        //    else
        //    {
        //        TempData[Constants.KeyErrorMessage] = errorMessage;
        //    }

        //    return View(model);
        //}
    }
}
