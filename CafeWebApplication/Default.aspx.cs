using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ClassLibraryCafe;

namespace CafeWebApplication
{
    public partial class _Default : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public IQueryable<ClassLibraryCafe.Coupon> lvProducts_GetData1()
        {
            var list = DB.db.Coupon.ToList().AsQueryable();
            var query = list.OfType<ClassLibraryCafe.Coupon>();
            return query;
        }
    }
}