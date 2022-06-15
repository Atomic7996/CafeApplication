using CafeWeb.Data.interfaces;
using CafeWeb.Models;
using System.Collections.Generic;

namespace CafeWeb.Data.mocks
{
    public class MockProductType : IProductType
    {
        public IEnumerable<ProductType> allTypes
        {
            get
            {
                return new List<ProductType> {
                    new ProductType { ProductTypeID = 100, Title = "type1" },
                    new ProductType { ProductTypeID = 102, Title = "type2" }
                    };
            }
        }
    }
}
