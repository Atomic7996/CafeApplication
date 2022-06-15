using CafeWeb.Data.interfaces;
using CafeWeb.Models;
using System.Collections.Generic;

namespace CafeWeb.Data.Repository
{
    public class ProductTypeRepository : IProductType
    {
        private readonly AppDBContent _appDBContent;

        public ProductTypeRepository(AppDBContent appDBContent)
        {
            this._appDBContent = appDBContent;
        }

        public IEnumerable<ProductType> allTypes => (IEnumerable<ProductType>)_appDBContent.ProductType;
    }
}
