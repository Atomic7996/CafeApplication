using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibraryCafe
{
    public class DB
    {
        public static CafeEntities db = new CafeEntities();

        static DB()
        {
            db = new CafeEntities();
        }


    }
}
