using System;
using System.Collections.Generic;

namespace CafeWeb.Models
{
    public class Coupon
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Coupon()
        {
            //this.Order = new HashSet<Order>();
        }

        public int CouponID { get; set; }
        public int ProductTypeID { get; set; }
        public decimal Sale { get; set; }
        public string PromoCode { get; set; }

        public virtual ProductType ProductType { get; set; }
        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<Order> Order { get; set; }
    }
}
