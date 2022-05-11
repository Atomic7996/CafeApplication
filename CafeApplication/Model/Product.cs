using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CafeApplication
{
    public partial class Product
    {
        public string ValidImage => Image == null ? "../../Resources/product.png" : Image;

        public string FoodStaffList
        {
            get
            {
                DB.db.ChangeTracker.Entries().ToList().ForEach(e => e.Reload());

                string foodStaff = "";
                foreach (var item in ProductFoodStuff)
                {
                    foodStaff += item.FoodStaff.Title;
                    if (item != ProductFoodStuff.Last())
                    {
                        foodStaff += ", ";
                    }
                }
                return foodStaff;
            }
        }

        
    }
}
