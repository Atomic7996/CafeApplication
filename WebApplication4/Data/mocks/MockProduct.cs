using System;
using System.Collections.Generic;
using System.Linq;
using CafeWeb.Data.interfaces;

namespace CafeWeb.Data.mocks
{
    public class MockProduct : IProducts
    {
        private readonly IProductType _productType = new MockProductType();

        public IEnumerable<Product> AllProducts
        {
            get
            {
                return new List<Product>
                {
                    new Product { ProductID=1000, Title="Prod1", Description="abc", Cost=100, Image=null, ProductType= _productType.allTypes.First()}
                };
            }
        }

        public Product GetProductByID(int productID)
        {
            throw new NotImplementedException();
        }
    }
}
