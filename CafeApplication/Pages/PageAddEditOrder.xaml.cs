using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CafeApplication.Pages
{
    /// <summary>
    /// Логика взаимодействия для PageAddEditOrder.xaml
    /// </summary>
    public partial class PageAddEditOrder : Page
    {
        Order order = new Order();
        OrderCombo orderCombo = new OrderCombo();
        OrderProduct orderProduct = new OrderProduct();

        public PageAddEditOrder(Order selectedOrder)
        {
            InitializeComponent();

            if (selectedOrder != null)
            {
                order = selectedOrder;
            }

            if (order.OrderID == 0)
            {
                btnDelete.Visibility = Visibility.Hidden;
            }

            DataContext = order;

            lbProducts.ItemsSource = GetProductsCombos("prod");
            lbCombos.ItemsSource = GetProductsCombos("combo");
        }

        private string[] GetProductsCombos(string type)
        {
            List<Product> prod = DB.db.Product.ToList();
            List<Combo> com = DB.db.Combo.ToList();

            string[] products = new string[prod.Count];
            for (int i = 0; i < prod.Count; i++)
            {
                products[i] = prod[i].Title;
            }

            string[] combos = new string[com.Count];
            for (int i = 0; i < com.Count; i++)
            {
                combos[i] = com[i].Title;
            }

            if (type == "prod")
            {
                return products;
            }
            return combos;
        }

        private void AddOrderProduct(string[] products)
        {
            int[] IDs = new int[lbProducts.SelectedItems.Count];
            int i = 0;

            foreach (var item in lbProducts.SelectedItems)
            {
                IDs[i] = DB.db.Product.Where(x => x.Title == item.ToString()).FirstOrDefault().ProductID;
                i++;
            }

            foreach (var id in IDs)
            {
                orderProduct.ProductID = id;
                orderProduct.OrderID = order.OrderID;
                DB.db.OrderProduct.Add(orderProduct);
                DB.db.SaveChanges();
            }
        }

        private void AddOrderCombo(string[] combos)
        {
            int[] IDs = new int[lbCombos.SelectedItems.Count];
            int i = 0;

            foreach (var item in lbCombos.SelectedItems)
            {
                IDs[i] = DB.db.Combo.Where(x => x.Title == item.ToString()).FirstOrDefault().ComboID;
                i++;
            }

            foreach (var id in IDs)
            {
                orderCombo.ComboID = id;
                orderCombo.OrderId = order.OrderID;
                DB.db.OrderCombo.Add(orderCombo);
                DB.db.SaveChanges();
            }

        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder errors = new StringBuilder();

            if (lbCombos.SelectedItems.Count == 0 && lbProducts.Items.Count == 0)
                errors.AppendLine("Выберите позицию!");

            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString(), "Внимание", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            foreach (var item in DB.db.OrderProduct.Where(x => x.OrderID == order.OrderID))
            {
                DB.db.OrderProduct.Remove(item);
            }

            string[] prods = new string[lbProducts.Items.Count];
            int i = 0;

            foreach (var item in lbProducts.Items)
            {
                prods[i] = item.ToString();
                i++;
            }

            string[] coms = new string[lbCombos.Items.Count];
            int j = 0;

            foreach (var item in lbCombos.Items)
            {
                    coms[j] = item.ToString();
                    j++;                
            }

            if (order.OrderID == 0)
            {
                order.OrderDateTime = DateTime.Now;
                order.StaffID = 1;
                DB.db.Order.Add(order);
            }

            AddOrderProduct(prods);
            AddOrderCombo(coms);

            try
            {
                DB.db.SaveChanges();
                MessageBox.Show("Данные сохранены", "Уведомление");
                Manager.mainFrame.GoBack();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
