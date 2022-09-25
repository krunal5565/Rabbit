using System;
using System.Collections.Generic;
using System.Net.Http;
using FundaClearApp.Business.BusinessModel;

namespace FundaClear.Business
{
    public static class APIHelper
    {
        public static  List<StudentAPIResponse> GetStudents()
        {
            List<StudentAPIResponse> lstStudentAPIModel = new List<StudentAPIResponse>();

            //try
            //{
            //    HttpClient clientNew = new HttpClient();
            //    clientNew.DefaultRequestHeaders.Add("VYDRTH-Tenant-Id", Helper.TENANTID);
            //    clientNew.DefaultRequestHeaders.Add("API-Key", Helper.APIKEY);
            //    HttpResponseMessage response = clientNew.GetAsync(Helper.GetVidhyartheAPIUrl() + "/Student").Result;

            //    lstStudentAPIModel = response.Content.ReadAsAsync<List<StudentAPIResponse>>().Result;
            //}
            //catch (Exception ex)
            //{

            //}
            return lstStudentAPIModel;
        }

        public static CatalogAPIResponse GetCatalog()
        {
            CatalogAPIResponse lstCatalogAPIResponse = new CatalogAPIResponse();

            //try
            //{
            //    HttpClient clientNew = new HttpClient();
            //    HttpResponseMessage response = clientNew.GetAsync(Helper.GetCatalogAPIUrl() + "/product/all").Result;

            //    lstCatalogAPIResponse = response.Content.ReadAsAsync<CatalogAPIResponse>().Result;
            //}
            //catch (Exception ex)
            //{

            //}
            return lstCatalogAPIResponse;
        }
    }
}
