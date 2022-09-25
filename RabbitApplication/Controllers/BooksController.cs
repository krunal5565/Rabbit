using FundaClear.Business;
using FundaClearApp.Business;
using FundaClearApp.Business.BusinessModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;

namespace FundaClearApp.Controllers
{
    public class BooksController : Controller
    {
        public string connectionString;

        private readonly ILogger<BooksController> _logger;
        private readonly IConfiguration configuration;
      
        public BooksController(ILogger<BooksController> logger, IConfiguration iConfiguration)
        {
            _logger = logger;
            connectionString = iConfiguration.GetConnectionString("DefaultConnection");// ConnectionHelper.GetConnectionString();
            configuration = iConfiguration;
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult BorrowedBooks()
        {
            return View();
        }

        public PartialViewResult GetBookDetails(string id)
        {
            return PartialView("_BookDetails");
        }
        public ActionResult Catalog()
        {
            return View();
        }

        public ActionResult LoadCatalog()
        {
            try
            {
               CatalogAPIResponse objCatalogAPIResponse =  APIHelper.GetCatalog();

                List<Data> lstCategoryModel = objCatalogAPIResponse.data;
               
               return Json(new { data = lstCategoryModel });
            }
            catch (Exception)
            {
                throw;
            }
        }

        public ActionResult LoadBooks()
        {
            try
            {
                int pageSize = 10;
                int skip = 0;
                int recordsTotal = 0;


                var data = new List<CommonModel>();
                data.Add(new CommonModel { Name = "Book-1", ISBN = "1234", Supplier = "Supplier", Quantity="100", YOP = "1990" });
                data.Add(new CommonModel { Name = "Book-2", ISBN = "1234", Supplier = "Supplier", Quantity = "100", YOP = "1990" });
                data.Add(new CommonModel { Name = "Book-3", ISBN = "1234", Supplier = "Supplier", Quantity = "100", YOP = "1990" });
                data.Add(new CommonModel { Name = "Book-4", ISBN = "1234", Supplier = "Supplier", Quantity = "100", YOP = "1990" });
                data.Add(new CommonModel { Name = "Book-5", ISBN = "1234", Supplier = "Supplier", Quantity = "100", YOP = "1990" });
                data.Add(new CommonModel { Name = "Book-6", ISBN = "1234", Supplier = "Supplier", Quantity = "100", YOP = "1990" });
                data.Add(new CommonModel { Name = "Book-7", ISBN = "1234", Supplier = "Supplier", Quantity = "100", YOP = "1990" });
                data.Add(new CommonModel { Name = "Book-8", ISBN = "1234", Supplier = "Supplier", Quantity = "100", YOP = "1990" });
                data.Add(new CommonModel { Name = "Book-9", ISBN = "1234", Supplier = "Supplier", Quantity = "100", YOP = "1990" });
                data.Add(new CommonModel { Name = "Book-10", ISBN = "1234", Supplier = "Supplier", Quantity = "100", YOP = "1990" });
                data.Add(new CommonModel { Name = "Book-11", ISBN = "1234", Supplier = "Supplier", Quantity = "100", YOP = "1990" });
                data.Add(new CommonModel { Name = "Book-12", ISBN = "1234", Supplier = "Supplier", Quantity = "100", YOP = "1990" });
                data.Add(new CommonModel { Name = "Book-13", ISBN = "1234", Supplier = "Supplier", Quantity = "100", YOP = "1990" });
                data.Add(new CommonModel { Name = "Book-14", ISBN = "1234", Supplier = "Supplier", Quantity = "100", YOP = "1990" });
                data.Add(new CommonModel { Name = "Book-15", ISBN = "1234", Supplier = "Supplier", Quantity = "100", YOP = "1990" });
                data.Add(new CommonModel { Name = "Book-16", ISBN = "1234", Supplier = "Supplier", Quantity = "100", YOP = "1990" });
                data.Add(new CommonModel { Name = "Book-17", ISBN = "1234", Supplier = "Supplier", Quantity = "100", YOP = "1990" });
                data.Add(new CommonModel { Name = "Book-18", ISBN = "1234", Supplier = "Supplier", Quantity = "100", YOP = "1990" });
                data.Add(new CommonModel { Name = "Book-19", ISBN = "1234", Supplier = "Supplier", Quantity = "100", YOP = "1990" });
                data.Add(new CommonModel { Name = "Book-20", ISBN = "1234", Supplier = "Supplier", Quantity = "100", YOP = "1990" });

                return Json(new { recordsFiltered = recordsTotal, recordsTotal = data.Count, data = data });
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
