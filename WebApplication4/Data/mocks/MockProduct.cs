using ClassLibraryCafe;
using System;
using System.Collections.Generic;
using System.Linq;
using CafeWeb.Data.interfaces;

namespace CafeWeb.Data.mocks
{
    public class MockProduct : IProducts
    {
        public IEnumerable<Product> AllProducts
        {
            get
            {
                //return DB.db.Product.ToList();
                List<Product> prodList = DB.db.Product.ToList();
                return prodList;
                //return Enumerable.Empty<Product>();
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
