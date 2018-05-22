using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.OData;
using ODataSample.Models;

namespace ODataSample.Controllers
{
    public class ProductsController : ODataController
    {
        private readonly ICollection<Product> _products = new List<Product>
        {
            new Product
            {
                Id = 100,
                Name = "Alpha"
            },
            new Product
            {
                Id = 200,
                Name = "Beta"
            }
        };

        [EnableQuery]
        public IQueryable<Product> Get()
        {
            return _products.AsQueryable();
        }

        [EnableQuery]
        public SingleResult<Product> Get([FromODataUri] int key)
        {
            return SingleResult.Create(_products.Where(o => o.Id == key).AsQueryable());
        }
    }
}