using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibraryCafe
{
    public partial class Combo
    {
        public string ValidImage => Image == null ? "../../Images/combo.png" : Image;

        public string ProductList
        {
            get
            {
                string product = "";
                foreach (var item in ComboProduct)
                {
                    product += item.Product.Title + " " + item.Count;
                    if (item != ComboProduct.Last())
                    {
                        product += ", ";
                    }
                }
                return product;
            }
        }
    }
}
