using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CafeApplication
{
    public partial class User
    {
        public override string ToString()
        {
            return String.Format(LastName + " " + FirstName + " " + Patronymic);
        }
    }
}
