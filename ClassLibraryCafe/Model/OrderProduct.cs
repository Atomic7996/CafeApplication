using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibraryCafe
{
    public partial class OrderProduct
    {
        public static string productsAmount(List<Order> orderList, Product selectedProduct)
        {
            var op = DB.db.OrderProduct.ToList();
            int? total = 0;

            if (orderList == null)
            {
                var newList = DB.db.OrderProduct.Where(p => p.ProductID == selectedProduct.ProductID).ToList();

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
                    foreach (var item in op.Where(o => o.OrderID == orderList[i].OrderID))
                        total += item.Count;
                }
            }

            return total.ToString(); ;
        }

        public static string productsCost(List<Order> orderList, Product selectedProduct)
        {
            var op = DB.db.OrderProduct.ToList();
            Product product = new Product();
            decimal? total = 0;

            if (orderList == null)
            {
                var newList = DB.db.OrderProduct.Where(p => p.ProductID == selectedProduct.ProductID).ToList();

                for (int i = 0; i < newList.Count; i++)
                {
                    foreach (var item in newList.Where(o => o.OrderID == newList[i].OrderID))
                    {
                        product = DB.db.Product.Where(p => p.ProductID == item.ProductID).FirstOrDefault();
                        total += product.Cost * item.Count;
                    }
                }
            }
            else
            {
                for (int i = 0; i < orderList.Count; i++)
                {
                    foreach (var item in op.Where(o => o.OrderID == orderList[i].OrderID))
                    {
                        product = DB.db.Product.Where(p => p.ProductID == item.ProductID).FirstOrDefault();
                        total += product.Cost * item.Count;
                    }
                }
            }

            return total.ToString(); ;
        }
    }
}
