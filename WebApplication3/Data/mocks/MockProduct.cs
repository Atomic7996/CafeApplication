using ClassLibraryCafe;
using System;
using System.Collections.Generic;
using System.Linq;
using WebApplication3.Data.Interfaces;

namespace WebApplication3.Data.mocks
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
