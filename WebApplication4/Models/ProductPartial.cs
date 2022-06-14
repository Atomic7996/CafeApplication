using ClassLibraryCafe;
using System.Linq;

namespace CafeWeb.Models
{
    public partial class Product
    {
        public override string ToString()
        {
            return Title;
        }

        public string ValidImage => Image == null ? "../../Images/product.png" : Image;

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
