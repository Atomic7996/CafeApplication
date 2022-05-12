using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CafeApplication
{
    public partial class FoodStaff
    {
        public string ValidImage => Image == null ? "../../Resources/foodstaff.png" : Image;

        public string ValidColor
        {
            get
            {
                if (CountInStock < MinCount)
                {
                    return "LightCoral";
                }
                else 
                    return "";
            }
        }

        public override string ToString()
        {
            return Title;
        }
    }
}
