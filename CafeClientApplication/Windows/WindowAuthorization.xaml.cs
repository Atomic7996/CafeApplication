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
using System.Windows.Shapes;
using ClassLibraryCafe;

namespace CafeClientApplication.Windows
{
    /// <summary>
    /// Логика взаимодействия для WindowAuthorization.xaml
    /// </summary>
    public partial class WindowAuthorization : Window
    {
        public WindowAuthorization()
        {
            InitializeComponent();
        }

        private void Login()
        {
            User user = DB.db.User.Where(u => u.Login == tbLogin.Text).FirstOrDefault();

            if (user != null)
            {
                if (user.Password == tbPass.Password)
                {
                    Properties.Settings.Default.globalRole = "user";
                    Properties.Settings.Default.userID = user.UserID;
                    new MainWindow().Show();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Неверный пароль", "Предупреждение",
                        MessageBoxButton.OK, MessageBoxImage.Information);
                    tbPass.Password = "";
                }
            }
            else
            {
                MessageBox.Show("Такого пользоватиеля не существует", "Предупреждение",
                    MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void btnEnter_Click(object sender, RoutedEventArgs e)
        {
            Login();
        }

        private void btnRegister_Click(object sender, RoutedEventArgs e)
        {
            new WindowRegistration(null).Show();
        }

        private void btnGuest_Click(object sender, RoutedEventArgs e)
        {
            Properties.Settings.Default.globalRole = "guest";
            Properties.Settings.Default.userID = 0;
            new MainWindow().Show();
            this.Close();
        }
    }
}
