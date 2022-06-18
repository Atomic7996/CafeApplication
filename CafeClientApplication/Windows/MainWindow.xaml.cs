using CafeClientApplication.Pages;
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

namespace CafeClientApplication
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        User user = new User();

        public MainWindow()
        {
            InitializeComponent();
            WindowStartUp();
        }

        private void WindowStartUp()
        {
            Manager.mainFrame = windowFrame;

            if (Properties.Settings.Default.globalRole == "user")
            {
                user = DB.db.User.Where(u => u.UserID == Properties.Settings.Default.userID).FirstOrDefault();
                tbName.Text = user.FirstName + " " + user.LastName;
                Manager.mainFrame.Navigate(new PageMenu());
            }
            else
            {
                tbName.Text += "Гость";
                Manager.mainFrame.Navigate(new PageMenu());
            }
        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            new WindowAuthorization().Show();
            this.Close();
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            if (Manager.mainFrame.CanGoBack)
                Manager.mainFrame.GoBack();
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

        private void Window_MouseEnter(object sender, MouseEventArgs e)
        {
            if (user.UserID == 0)
            {
                tbName.Text = "Гость";
                
            }
            else
            {
                tbName.Text = user.FirstName + " " + user.LastName;
            }
        }
    }
}
