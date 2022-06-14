using ClassLibraryCafe;
using System.Collections.Generic;

namespace CafeWeb.ViewModels
{
    public class ProductsListViewModel
    {
        public IEnumerable<Product> getAllProducts { get; set; }
    }
}
