using CafeWebApp.Data.Interfaces;
using ClassLibraryCafe;

namespace CafeWebApp.Data.Mocks
{
    public class MockProduct : IProducts
    {
        public IEnumerable<Product> AllProducts 
        { 
            

            get
            {
                //return DB.db.Product.ToList();
                return Enumerable.Empty<Product>();
            }
            set
            {

            }
        }

        public Product GetProductByID(int productID)
        {
            throw new NotImplementedException();
        }
    }
}
