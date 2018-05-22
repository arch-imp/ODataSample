using System.Web.Http;
using System.Web.OData.Builder;
using System.Web.OData.Extensions;
using System.Web.OData.Routing;
using System.Web.OData.Routing.Conventions;
using ODataSample.Controllers;
using ODataSample.Models;

namespace ODataSample
{
    public static class WebApiConfig
    {
        private static readonly string[] _manufacturers = new[]
        {
            "acme",
            "widgets-r-us"
        };

        public static void Register(HttpConfiguration config)
        {
            ODataModelBuilder builder = new ODataConventionModelBuilder();
            builder.EntityType<Product>();
            builder.EntityType<Manufacturer>();
            builder.EntitySet<Product>("Products");
            builder.EntitySet<Manufacturer>("Manufacturers");
            foreach (var manufacturer in _manufacturers)
            {
                builder.Singleton<Manufacturer>(manufacturer);
            }
            config.Count().Filter().OrderBy().Expand().Select().MaxTop(null);

            var conventions = ODataRoutingConventions.CreateDefaultWithAttributeRouting("ODataRoute", config);
            conventions.Insert(0, new ManufacturerRoutingConvention());

            config.MapODataServiceRoute("ODataRoute", 
                null, 
                builder.GetEdmModel(),
                new DefaultODataPathHandler(),
                conventions
            );
        }
    }
}
