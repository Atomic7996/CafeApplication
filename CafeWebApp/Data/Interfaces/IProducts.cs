using ClassLibraryCafe;

namespace CafeWebApp.Data.Interfaces
{
    public interface IProducts
    {
        IEnumerable<Product> AllProducts { get; set; }
        Product GetProductByID(int productID);

    }
}
