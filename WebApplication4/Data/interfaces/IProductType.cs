using CafeWeb.Models;
using System.Collections.Generic;

namespace CafeWeb.Data.interfaces
{
    public interface IProductType
    {
        public IEnumerable<ProductType> allTypes { get; }
    }
}
