using System.Web.Http;
using System.Web.OData.Builder;
using System.Web.OData.Extensions;
using ODataSample.Models;

namespace ODataSample
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // New code:
            ODataModelBuilder builder = new ODataConventionModelBuilder();
            builder.EntitySet<Product>("Products");
            builder.EntitySet<Supplier>("Suppliers");
            config.MapODataServiceRoute("ODataRoute", null, builder.GetEdmModel());
        }
    }
}
