using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;

namespace FundaClearApp.Controllers
{
    public class SupplierController : Controller
    {
        public string connectionString;

        private readonly ILogger<SupplierController> _logger;
        private readonly IConfiguration configuration;
      
        public SupplierController(ILogger<SupplierController> logger, IConfiguration iConfiguration)
        {
            _logger = logger;
            connectionString = iConfiguration.GetConnectionString("DefaultConnection");// ConnectionHelper.GetConnectionString();
            configuration = iConfiguration;
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult LoadSupplier()
        {
            try
            {
                int pageSize = 10;
                int skip = 0;
                int recordsTotal = 0;

                recordsTotal = 3;

                var data = new List<SupplierModel>();
                data.Add(new SupplierModel { Name = "supplier-11", MobileNumber = "1234567890", Address ="address" });
                data.Add(new SupplierModel { Name = "supplier-12", MobileNumber = "1234567890", Address = "address" });
                data.Add(new SupplierModel { Name = "supplier-13", MobileNumber = "1234567890", Address = "address" });
                data.Add(new SupplierModel { Name = "supplier-14", MobileNumber = "1234567890", Address = "address" });
                data.Add(new SupplierModel { Name = "supplier-15", MobileNumber = "1234567890", Address = "address" });
                data.Add(new SupplierModel { Name = "supplier-16", MobileNumber = "1234567890", Address = "address" });
                data.Add(new SupplierModel { Name = "supplier-21", MobileNumber = "1234567890", Address = "address" });
                data.Add(new SupplierModel { Name = "supplier-22", MobileNumber = "1234567890", Address = "address" });
                data.Add(new SupplierModel { Name = "supplier-23", MobileNumber = "1234567890", Address = "address" });
                data.Add(new SupplierModel { Name = "supplier-24", MobileNumber = "1234567890", Address = "address" });
                data.Add(new SupplierModel { Name = "supplier-25", MobileNumber = "1234567890", Address = "address" });
                data.Add(new SupplierModel { Name = "supplier-26", MobileNumber = "1234567890", Address = "address" });

                return Json(new { recordsFiltered = recordsTotal, recordsTotal = data.Count, data = data });
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
