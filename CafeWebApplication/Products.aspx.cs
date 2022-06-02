using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ClassLibraryCafe;

namespace CafeWebApplication
{
    public partial class About : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        // Возвращаемый тип можно изменить на IEnumerable, однако для обеспечения поддержки
        // постраничного просмотра и сортировки необходимо добавить следующие параметры:
        //     int maximumRows
        //     int startRowIndex
        //     out int totalRowCount
        //     string sortByExpression
        public IQueryable<ClassLibraryCafe.Product> lvProducts_GetData()
        {
            var list = DB.db.Product.ToList().AsQueryable();
            var query = list.OfType<ClassLibraryCafe.Product>();
            return query;
        }
    }
}