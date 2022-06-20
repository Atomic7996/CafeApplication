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
    /// Логика взаимодействия для PageOrders.xaml
    /// </summary>
    public partial class PageOrders : Page
    {
        private List<OrderItemTemplate> _orderItemTemplates = new List<OrderItemTemplate>();

        public PageOrders()
        {
            InitializeComponent();
            UpdateLvItems();
        }

        void UpdateLvItems()
        {
            var currentOrders = DB.db.Order.Where(o => o.UserID == Properties.Settings.Default.userID).ToList();

            if (calendar.SelectedDate != null)
                currentOrders = currentOrders.Where(o => o.OrderDateTime.Date.ToString().Contains(calendar.SelectedDate.ToString())).ToList();

            _orderItemTemplates.Clear();
            lvOrders.ItemsSource = null;
            lvOrders.Items.Clear();

            foreach (var order in currentOrders)
            {
                OrderItemTemplate orderItemTemplate = new OrderItemTemplate(order);
                _orderItemTemplates.Add(orderItemTemplate);
            }

            foreach (var orderItemTemplate in _orderItemTemplates)
                lvOrders.Items.Add(orderItemTemplate.GridOrderItemTemplate);

            tbRecordsCount.Text = lvOrders.Items.Count.ToString();
            tbRecordsCountAll.Text = DB.db.Order.Where(o => o.UserID == Properties.Settings.Default.userID).Count().ToString();

            if (lvOrders.Items.Count == 0)
            {
                lvOrders.Visibility = Visibility.Hidden;
                tbAvailable.Visibility = Visibility.Visible;
            }
            else
            {
                lvOrders.Visibility = Visibility.Visible;
                tbAvailable.Visibility = Visibility.Hidden;
            }
        }

        private void lvOrders_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            try
            {
                DB.db.ChangeTracker.Entries().ToList().ForEach(a => a.Reload());
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void calendar_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateLvItems();
        }

        private void calendar_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            var key = e.Key;

            if (key == Key.Escape)
                calendar.SelectedDate = null;
        }
    }
}
