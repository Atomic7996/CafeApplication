using System.Collections.Generic;

namespace CafeWeb.ViewModels
{
    public class ProductsListViewModel
    {
        public IEnumerable<Product> getAllProducts { get; set; }
        public string currentType { get; set; }
    }
}
