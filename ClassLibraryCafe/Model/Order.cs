using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibraryCafe
{
    public partial class Order
    {
        public string ValidList
        {
            get
            {
                string product = "";
                foreach (var item in OrderProduct)
                {
                    product += item.Product.Title + " " + item.Count;
                    if (item != OrderProduct.Last())
                    {
                        product += ", ";
                    }
                }

                string combo = "";
                foreach (var item in OrderCombo)
                {
                    combo += item.Combo.Title + " " + item.Count;
                    if (item != OrderCombo.Last())
                    {
                        combo += ", ";
                    }
                }


                if (string.IsNullOrEmpty(combo))
                {
                    return product;
                }
                else if (string.IsNullOrEmpty(product))
                {
                    return combo;
                }
                else
                    return combo + ", " + product;
            }
        }

        public decimal? SummaryCost
        {
            get
            {
                decimal? price = 0;
                foreach (var item in OrderProduct)
                {
                    price += item.Product.Cost * item.Count;
                }
                foreach (var item in OrderCombo)
                {
                    price += item.Combo.Cost * item.Count;
                }

                if (Coupon != null)
                {
                    price -= price * Coupon.Sale / 100;
                }

                return price;
            }
        }

        public override string ToString()
        {
            return OrderDateTime.ToString();
        }
    }
}
