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
    /// Логика взаимодействия для PageMenu.xaml
    /// </summary>
    public partial class PageMenu : Page
    {
        public PageMenu(int roleID)
        {
            InitializeComponent();

            switch (roleID)
            {
                case 2:
                    btnStaff.Visibility = Visibility.Hidden;
                    break;
                case 1:
                    btnCombo.Visibility = Visibility.Hidden;
                    btnFoodStaff.Visibility = Visibility.Hidden;
                    btnProducts.Visibility = Visibility.Hidden;
                    break;
            }
        }

        private void btnProducts_Click(object sender, RoutedEventArgs e)
        {
            Manager.mainFrame.Navigate(new PageProducts());
        }

        private void btnFoodStaff_Click(object sender, RoutedEventArgs e)
        {
            Manager.mainFrame.Navigate(new PageFoodStaff());
        }

        private void btnOrders_Click(object sender, RoutedEventArgs e)
        {
            Manager.mainFrame.Navigate(new PageOrders());
        }

        private void btnCombo_Click(object sender, RoutedEventArgs e)
        {
            Manager.mainFrame.Navigate(new PageCombo());
        }

        private void btnStaff_Click(object sender, RoutedEventArgs e)
        {
            Manager.mainFrame.Navigate(new PageStaff());
        }
    }
}
