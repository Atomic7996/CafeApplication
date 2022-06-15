using System;
using System.Collections.Generic;

namespace CafeWeb
{
    public class ComboProduct
    {
        public int ComboID { get; set; }
        public int ProductID { get; set; }
        public Nullable<int> Count { get; set; }
        public int ComboProductID { get; set; }

        public virtual Combo Combo { get; set; }
        public virtual Product Product { get; set; }
    }
}
