using ClassLibraryCafe;
using System.Collections.Generic;

namespace WebApplication3.Data.Interfaces
{
    public interface IProducts
    {
        IEnumerable<Product> AllProducts { get; set; }
        Product GetProductByID(int productID);

    }
}
