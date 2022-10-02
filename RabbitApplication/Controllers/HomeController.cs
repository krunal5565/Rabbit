using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RabbitApplication.Data;
using RabbitApplication.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System.Security.Claims;
using RabbitApplication.Helpers;
using RabbitApplication.Entity;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace RabbitApplication.Controllers
{
    public class HomeController : Controller
    {

        private readonly ApplicationDbContext _context;
        private readonly IConfiguration _config;

        public HomeController(ApplicationDbContext context, IConfiguration config)
        {
            _context = context;
            _config = config;
        }

        public   IActionResult Index()
        {
            var jobProfile =  _context.JobProfile.ToList();

            List<JobProfileModel> lstJobProfileModel = new List<JobProfileModel>();

            foreach (JobProfile objJobProfile in jobProfile)
            {
                lstJobProfileModel.Add(ApplicationHelper.BindJobProfileEntityToModel(objJobProfile));
            }

            return View(lstJobProfileModel);
        }

        // GET: HomeController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: HomeController/Create
        public ActionResult Create()
        {
            return View();
        }

        public ActionResult About()
        {
            return View();
        }


        public ActionResult Services()
        {
            return View();
        }


        public ActionResult Contact()
        {
            return View();
        }

        public ActionResult Portfolio()
        {
            return View();
        }

        public ActionResult Pricing()
        {
            return View();
        }

        public ActionResult Team()
        {
            return View();
        }

        // POST: HomeController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

       
    }
}
