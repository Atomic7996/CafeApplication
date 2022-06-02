using ClassLibraryCafe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CafeWebApplication
{
    public partial class Contact : Page
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
        public IQueryable<ClassLibraryCafe.Combo> lvCombos_GetData()
        {
            var list = DB.db.Combo.ToList().AsQueryable();
            var query = list.OfType<ClassLibraryCafe.Combo>();
            return query;
        }
    }
}