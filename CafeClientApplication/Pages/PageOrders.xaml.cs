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
        public PageOrders()
        {
            InitializeComponent();
            PageStartUp();
        }

        private void PageStartUp()
        {
            var currentOrders = DB.db.Order.Where(o => o.UserID == Properties.Settings.Default.userID).ToList();
            var sort = new List<string>();

            sort.Add("Сортировка");
            sort.Add("По названию, от А до Я");
            sort.Add("По названию, от Я до А"); ;
            sort.Add("По возрастанию стоимости"); ;
            sort.Add("По убыванию стоимости"); ;

            lvOrders.ItemsSource = currentOrders;

            tbRecordsCount.Text = lvOrders.Items.Count.ToString();
            tbRecordsCountAll.Text = DB.db.Order.Where(o => o.UserID == Properties.Settings.Default.userID).Count().ToString();
        }

        void UpdateLvItems()
        {
            var currentOrders = DB.db.Order.Where(o => o.UserID == Properties.Settings.Default.userID).ToList();

            if (calendar.SelectedDate != null)
                currentOrders = currentOrders.Where(s => s.OrderDateTime.Date.ToString().Contains(calendar.SelectedDate.ToString())).ToList();

            lvOrders.ItemsSource = currentOrders;

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
            DB.db.ChangeTracker.Entries().ToList().ForEach(a => a.Reload());
            lvOrders.ItemsSource = DB.db.Order.Where(o => o.UserID == Properties.Settings.Default.userID).ToList();
        }

        private void cbStaff_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateLvItems();
        }

        private void calendar_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateLvItems();
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            Manager.mainFrame.Navigate(new PageAddOrder(new Order()));
        }

        private void cbUsers_SelectionChanged(object sender, SelectionChangedEventArgs e)
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
