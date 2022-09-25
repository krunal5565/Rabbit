using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FundaClearApp.Business.BusinessModel
{
    public class CatalogAPIResponse
    {
            public string version { get; set; }
            public string status { get; set; }
            public List<Data> data { get; set; }
    }

    public class Data
    {
        public string uniqueID { get; set; }
        public string id { get; set; }
        public string productName { get; set; }
        public string parentProductId { get; set; }
        public string categoryId { get; set; }
        public string description { get; set; }
    }
}
