using System;
using System.Collections.Generic;

namespace CafeWeb.Models
{
    public partial class ProductType
    {
        public ProductType()
        {
            this.Coupon = new HashSet<Coupon>();
            this.Product = new HashSet<Product>();
        }

        public int ProductTypeID { get; set; }
        public string Title { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Coupon> Coupon { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Product> Product { get; set; }
    }
}
