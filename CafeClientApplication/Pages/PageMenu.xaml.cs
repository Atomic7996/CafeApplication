using CafeClientApplication.Windows;
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
    /// Логика взаимодействия для PageMenu.xaml
    /// </summary>
    public partial class PageMenu : Page
    {
        public PageMenu()
        {
            InitializeComponent();

            if (Properties.Settings.Default.globalRole == "guest")
            {
                btnOrders.Visibility = Visibility.Hidden;
                btnUser.Visibility = Visibility.Hidden;
            }
                
        }

        private void btnProducts_Click(object sender, RoutedEventArgs e)
        {
            Manager.mainFrame.Navigate(new PageProducts());
        }

        private void btnOrders_Click(object sender, RoutedEventArgs e)
        {
            Manager.mainFrame.Navigate(new PageOrders());
        }

        private void btnCombo_Click(object sender, RoutedEventArgs e)
        {
            Manager.mainFrame.Navigate(new PageCombos());
        }

        private void btnAddOrder_Click(object sender, RoutedEventArgs e)
        {
            Manager.mainFrame.Navigate(new PageAddOrder(new Order()));
        }

        private void btnUser_Click(object sender, RoutedEventArgs e)
        {
            User selectedUser = DB.db.User.Where(u => u.UserID == Properties.Settings.Default.userID).FirstOrDefault();
            new WindowRegistration(selectedUser).Show();
        }
    }
}
