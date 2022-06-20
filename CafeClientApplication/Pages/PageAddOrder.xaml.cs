using ClassLibraryCafe;
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

namespace CafeClientApplication.Pages
{
    /// <summary>
    /// Логика взаимодействия для PageAddOrder.xaml
    /// </summary>
    public partial class PageAddOrder : Page
    {
        Order order = new Order();
        Coupon coupon;

        public PageAddOrder(Order selectedOrder)
        {
            InitializeComponent();

            if (selectedOrder != null)
            {
                order = selectedOrder;
            }

            DataContext = order;
        }

        private void CouponCheck()
        {
            if (!string.IsNullOrEmpty(tbCoupon.Text))
            {
                coupon = DB.db.Coupon.Where(s => s.PromoCode == tbCoupon.Text).FirstOrDefault();

                if (coupon == null)
                {
                    MessageBox.Show("Купон недействителен", "Внимание",
                        MessageBoxButton.OK, MessageBoxImage.Information);
                    tbCoupon.Text = null;
                    return;
                }
            }
        }

        private void Save()
        {
            if (string.IsNullOrEmpty(order.ValidList))
            {
                MessageBox.Show(String.Format("Пустой заказ"), "Уведомление",
                    MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (order.OrderID == 0)
            {
                order.OrderDateTime = DateTime.Now;
                order.UserID = Properties.Settings.Default.userID;

                if (coupon != null)
                    order.CouponID = coupon.CouponID;

                DB.db.Order.Add(order);
            }
            else if (coupon != null)
            {
                order.CouponID = coupon.CouponID;
            }

            try
            {
                DB.db.SaveChanges();
                MessageBox.Show(String.Format("Заказ сформирован, ваш номер - " + order.OrderID), "Уведомление",
                    MessageBoxButton.OK, MessageBoxImage.Information);
                Manager.mainFrame.GoBack();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void Delete()
        {
            if (MessageBox.Show("Вы точно хотите удалить запись?", "Внимание",
                MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                try
                {
                    foreach (var item in DB.db.OrderProduct.Where(x => x.OrderID == order.OrderID))
                        DB.db.OrderProduct.Remove(item);

                    foreach (var item in DB.db.OrderCombo.Where(x => x.OrderID == order.OrderID))
                        DB.db.OrderCombo.Remove(item);

                    DB.db.Order.Remove(order);
                    DB.db.SaveChanges();
                    //MessageBox.Show("Запись удалена", "Уведомление");
                    Manager.mainFrame.GoBack();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            Save();
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            Delete();
        }

        private void tbCoupon_LostFocus(object sender, RoutedEventArgs e)
        {
            CouponCheck();
            decimal? cost = order.SummaryCost;
            if (coupon != null)
            {
                cost -= cost * coupon.Sale / 100;
            }

            tbCost.Text = cost.ToString();
        }

        private void btnChangeProds_Click(object sender, RoutedEventArgs e)
        {
            Manager.mainFrame.Navigate(new PageAddEditOrderProducts((sender as Button).DataContext as Order));
        }

        private void btnChangeComs_Click(object sender, RoutedEventArgs e)
        {
            Manager.mainFrame.Navigate(new PageAddEditOrderCombos((sender as Button).DataContext as Order));
        }

        private void tbStructure_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            try
            {
                DB.db.ChangeTracker.Entries().ToList().ForEach(a => a.Reload());
                tbStructure.Text = order.ValidList;
                tbCost.Text = order.SummaryCost.ToString();
            }
            catch (Exception)
            {

            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            Manager.mainFrame.GoBack();
        }

        private void tbCoupon_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            
            if (Char.IsLetter(e.Text, 0))
            {
                e.Handled = false;
            }else if (Char.IsDigit(e.Text, 0))
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }

        private void tbCoupon_TextChanged(object sender, TextChangedEventArgs e)
        {
            tbCoupon.Text = tbCoupon.Text.ToUpper();
            tbCoupon.CaretIndex = tbCoupon.Text.Length;
        }
    }
}
