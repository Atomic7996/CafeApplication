using CafeWeb.Models;
using System.Collections.Generic;

namespace CafeWeb.Data.interfaces
{
    public interface IProducts
    {
        IEnumerable<Product> AllProducts { get; }
        Product GetProductByID(int productID);
    }
}
