using CafeApplication.Pages;
using CafeApplication.Windows;
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

namespace CafeApplication
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            Manager.mainFrame = windowFrame;

            switch (Properties.Settings.Default.globalRole)
            {
                case "admin":
                    windowFrame.Navigate(new PageMenuAdmin());
                    break;
                case "cashier":
                    windowFrame.Navigate(new PageMenuCashier());
                    break;
            }


            

            Staff staff = DB.db.Staff.Where(s => s.StaffID == Properties.Settings.Default.staffID).FirstOrDefault();

            tbName.Text = staff.LastName + " " + staff.FirstName;
            tbRole.Text = staff.StaffRole.ToString();
        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            new WindowAutorization().Show();
            this.Close();
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            if (Manager.mainFrame.CanGoBack)
            {
                Manager.mainFrame.GoBack();
            }
        }

        private void windowFrame_Navigated(object sender, NavigationEventArgs e)
        {
            if (Manager.mainFrame.CanGoBack)
            {
                btnBack.Visibility = Visibility.Visible;
            }
            else
            {
                btnBack.Visibility = Visibility.Hidden;
            }
        }
    }
}
