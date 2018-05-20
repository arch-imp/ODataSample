using System;
using System.Linq;
using System.Net.Http;
using System.Web.Http.Routing;
using System.Web.OData.Extensions;
using System.Web.OData.Routing;
using System.Web.OData.Routing.Template;
using Microsoft.OData;

namespace ProductService
{
    public static class Helpers
    {
        public static TKey GetKeyFromUri<TKey>(HttpRequestMessage request, Uri uri)
        {
            if (uri == null)
            {
                throw new ArgumentNullException("uri");
            }

            var urlHelper = request.GetUrlHelper() ?? new UrlHelper(request);

            var routeName = request.ODataProperties().RouteName;
            ODataRoute oDataRoute = request.GetConfiguration().Routes[routeName] as ODataRoute;
            var prefixName = oDataRoute.RoutePrefix;
            var requestUri = request.RequestUri.ToString();

            string serviceRoot = requestUri.Substring(0, requestUri.IndexOf(prefixName) + prefixName.Length);

            var odataPath = request.ODataProperties().Path;

            var keySegment = odataPath.Segments.OfType<KeySegmentTemplate>().LastOrDefault().Segment.Keys.LastOrDefault();

            if (keySegment.Key == null)
            {
                throw new InvalidOperationException("The link does not contain a key.");
            }
            var value = ODataUriUtils.ConvertFromUriLiteral(keySegment.Value.ToString(), ODataVersion.V4);
            return (TKey)value;
        }
    }
}