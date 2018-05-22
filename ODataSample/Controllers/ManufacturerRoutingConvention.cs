using System.Linq;
using System.Net.Http;
using System.Web.Http.Controllers;
using System.Web.OData.Routing.Conventions;
using Microsoft.OData.Edm;
using Microsoft.OData.UriParser;
using ODataPath = System.Web.OData.Routing.ODataPath;

namespace ODataSample.Controllers
{
    public class ManufacturerRoutingConvention : IODataRoutingConvention
    {
        public virtual string SelectController(ODataPath odataPath, HttpRequestMessage request)
        {
            SingletonSegment singletonSegment = odataPath.Segments.FirstOrDefault() as SingletonSegment;
            if (singletonSegment != null)
            {
                var edmType = odataPath.EdmType as EdmEntityType;
                if (edmType != null && edmType.Name == "Manufacturer")
                {
                    return "Manufacturers";
                }
            }

            return null;
        }

        public string SelectAction(ODataPath odataPath, HttpControllerContext controllerContext,
            ILookup<string, HttpActionDescriptor> actionMap)
        {
            if (odataPath.PathTemplate == "~/singleton")
            {
                const string actionName = "GetSingleton";
                if (actionMap.Contains(actionName))
                {
                    var segment = odataPath.Segments[0] as SingletonSegment;
                    controllerContext.RouteData.Values["name"] = segment.Singleton.Name;
                    return actionName;
                }
            }

            return null;
        }
    }
}
