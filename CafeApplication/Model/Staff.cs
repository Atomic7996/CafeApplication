using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CafeApplication
{
    public partial class Staff
    {
        public string ValidImage => Image == null ? "../../Resources/staff.png" : Image;

        public string ValidColor
        {
            get
            {
                if (StaffID == Properties.Settings.Default.staffID)
                {
                    return "LightGreen";
                }
                else
                    return "";
            }
        }

        public override string ToString()
        {
            return String.Format(LastName + " " + FirstName + " " + Patronymic);
        }
    }
}
