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
    /// Логика взаимодействия для PageOrders.xaml
    /// </summary>
    public partial class PageOrders : Page
    {
        private List<OrderItemTemplate> _orderItemTemplates = new List<OrderItemTemplate>();

        public PageOrders()
        {
            InitializeComponent();
            PageStartUp();                
        }

        private void PageStartUp()
        {
            var sort = new List<string>();
            var staff = DB.db.Staff.Where(s => s.RoleID == 2).ToList();
            var users = DB.db.User.ToList();

            sort.Add("Сортировка");
            sort.Add("По названию, от А до Я");
            sort.Add("По названию, от Я до А"); ;
            sort.Add("По возрастанию стоимости"); ;
            sort.Add("По убыванию стоимости"); ;

            staff.Insert(0, new Staff
            {
                LastName = "Кассиры"
            });

            users.Insert(0, new User
            {
                LastName = "Пользователи"
            });

            cbStaff.SelectedValuePath = "StaffID";
            cbStaff.ItemsSource = staff;
            cbStaff.SelectedIndex = 0;

            cbUsers.SelectedValuePath = "UserID";
            cbUsers.ItemsSource = users;
            cbUsers.SelectedIndex = 0;


            if (Properties.Settings.Default.globalRole == "manager")
                btnAdd.Visibility = Visibility.Hidden;
        }

        void UpdateLvItems()
        {
            var currentOrders = DB.db.Order.ToList();

            if (calendar.SelectedDate != null)
                currentOrders = currentOrders.Where(o => o.OrderDateTime.Date.ToString().Contains(calendar.SelectedDate.ToString())).ToList();

            if (cbStaff.SelectedIndex > 0)
            {
                cbUsers.IsEnabled = false;
                currentOrders = currentOrders.Where(s => s.StaffID == int.Parse(cbStaff.SelectedValue.ToString())).ToList();
            }
            else
                cbUsers.IsEnabled = true;


            if (cbUsers.SelectedIndex > 0)
            {
                cbStaff.IsEnabled = false;
                currentOrders = currentOrders.Where(s => s.UserID == int.Parse(cbUsers.SelectedValue.ToString())).ToList();
            }
            else
                cbStaff.IsEnabled = true;


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
            tbRecordsCountAll.Text = DB.db.Order.Count().ToString();

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

        private void cbSort_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateLvItems();
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
            Manager.mainFrame.Navigate(new PageAddEditOrder(new Order()));
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
