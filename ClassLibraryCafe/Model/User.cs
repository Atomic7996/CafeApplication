using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibraryCafe
{
    public partial class User
    {
        //public string ValidImage => Image == null ? "../../Resources/staff.png" : Image;

        public override string ToString()
        {
            return String.Format(LastName + " " + FirstName + " " + Patronymic);
        }
    }
}
