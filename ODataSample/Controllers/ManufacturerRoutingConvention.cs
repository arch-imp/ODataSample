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
                SingletonSegment singletonSegment = (SingletonSegment)odataPath.Segments[0];
                string httpMethodName = GetActionNamePrefix(controllerContext.Request.Method);

                if (httpMethodName != null)
                {
                    return actionMap.FindMatchingAction(
                        httpMethodName + singletonSegment.Singleton.Name,
                        httpMethodName + "Singleton",
                        httpMethodName);
                }
            }

            return null;
        }

        private static string GetActionNamePrefix(HttpMethod method)
        {
            string actionNamePrefix;
            switch (method.Method.ToUpperInvariant())
            {
                case "GET":
                    actionNamePrefix = "Get";
                    break;
                case "PUT":
                    actionNamePrefix = "Put";
                    break;
                case "PATCH":
                case "MERGE":
                    actionNamePrefix = "Patch";
                    break;
                default:
                    return null;
            }

            return actionNamePrefix;
        }
    }
}
