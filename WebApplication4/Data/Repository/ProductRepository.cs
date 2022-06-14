using CafeWeb.Data.interfaces;
using ClassLibraryCafe;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace CafeWeb.Data.Repository
{
    public class ProductRepository : IProducts
    {
        private readonly AppDBContent _appDBContent;

        public ProductRepository(AppDBContent appDBContent)
        {
            this._appDBContent = appDBContent;
        }

        public IEnumerable<Product> AllProducts => _appDBContent.Product;

        public Product GetProductByID(int productID) => _appDBContent.Product.FirstOrDefault(p => p.ProductID == productID);
    }
}
