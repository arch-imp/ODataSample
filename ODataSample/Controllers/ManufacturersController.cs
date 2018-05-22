using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.OData;
using System.Web.OData.Extensions;
using Microsoft.OData.UriParser;
using ODataSample.Models;

namespace ODataSample.Controllers
{
    public class ManufacturersController : ODataController
    {
        private readonly ICollection<Manufacturer> _manufacturers = new List<Manufacturer>
        {
            new Manufacturer
            {
                Id = new Guid("3CFE49BC-2FCD-4D61-8692-35E176E980D3"),
                Name = "Acme"
            },
            new Manufacturer
            {
                Id = new Guid("ED7DA258-158D-4445-8CFB-B8412888A56F"),
                Name = "Widgets-R-Us"
            }
        };

        [EnableQuery]
        public IQueryable<Manufacturer> Get()
        {
            return _manufacturers.AsQueryable();
        }

        [EnableQuery]
        public SingleResult<Manufacturer> Get([FromODataUri] Guid key)
        {
            return SingleResult.Create(_manufacturers.Where(o => o.Id == key).AsQueryable());
        }

        [EnableQuery]
        public SingleResult<Manufacturer> GetSingleton(string name)
        {
            var results = _manufacturers.Where(o => o.Name.Equals(name, StringComparison.OrdinalIgnoreCase));

            return SingleResult.Create(results.AsQueryable());
        }

    }
}