using System.Linq;
using System.Web.OData;
using ODataSample.Models;

namespace ODataSample.Controllers
{
    public class ProductsController : ODataController
    {
        ProductsContext db = new ProductsContext();

        private bool ProductExists(int key)
        {
            return db.Products.Any(p => p.Id == key);
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}