namespace FundaClear.Business
{
    public static class Helper
    {
        public const string APIKEY = "123213";
        public const string TENANTID = "39";

        public static string GetVidhyartheAPIUrl()
        {
            return "https://vidyartheapi.fundaclear.in/api/v1";
        }

        public static string GetCatalogAPIUrl()
        {
            return "https://catalogapi.fundaclear.in/api/v1";
        }


        public static string GetConnectionString()
        {
            return "Data Source=164.52.195.234; Initial Catalog=Loyalty_dev; Integrated Security=False; User ID=Loyalty_dev;Password=Dell@2022; MultipleActiveResultSets=True";
        }          
    }
}
