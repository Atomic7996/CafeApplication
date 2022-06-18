using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibraryCafe
{
    public partial class OrderCombo
    {
        public static string combosAmount(List<Order> orderList, Combo selectedCombo)
        {
            var oc = DB.db.OrderCombo.ToList();
            int? total = 0;

            if (orderList == null)
            {
                var newList = DB.db.OrderCombo.Where(p => p.ComboID == selectedCombo.ComboID).ToList();

                for (int i = 0; i < newList.Count; i++)
                {
                    foreach (var item in newList.Where(o => o.OrderID == newList[i].OrderID))
                        total += item.Count;
                }

            }
            else
            {
                for (int i = 0; i < orderList.Count; i++)
                {
                    foreach (var item in oc.Where(o => o.OrderID == orderList[i].OrderID))
                        total += item.Count;
                }
            }

            return total.ToString();
        }

        public static string combosCost(List<Order> orderList, Combo selectedCombo)
        {
            var oc = DB.db.OrderCombo.ToList();
            Combo combo = new Combo();
            decimal? total = 0;

            if (orderList == null)
            {
                var newList = DB.db.OrderCombo.Where(c => c.ComboID == selectedCombo.ComboID).ToList();

                for (int i = 0; i < newList.Count; i++)
                {
                    foreach (var item in newList.Where(o => o.OrderID == newList[i].OrderID))
                    {
                        combo = DB.db.Combo.Where(c => c.ComboID == item.ComboID).FirstOrDefault();
                        total += combo.Cost * item.Count;
                    }
                }

            }
            else
            {
                for (int i = 0; i < orderList.Count; i++)
                {
                    foreach (var item in oc.Where(o => o.OrderID == orderList[i].OrderID))
                    {
                        combo = DB.db.Combo.Where(p => p.ComboID == item.ComboID).FirstOrDefault();
                        total += combo.Cost * item.Count;
                    }
                }
            }

            return total.ToString(); ;
        }
    }
}
