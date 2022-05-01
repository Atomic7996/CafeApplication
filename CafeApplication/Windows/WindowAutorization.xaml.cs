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

namespace CafeApplication.Windows
{
    /// <summary>
    /// Логика взаимодействия для WindowAutorization.xaml
    /// </summary>
    public partial class WindowAutorization : Window
    {
        public WindowAutorization()
        {
            InitializeComponent();
        }

        private void btnEnter_Click(object sender, RoutedEventArgs e)
        {
            Staff staff = DB.db.Staff.Where(s => s.Login == tbLogin.Text).FirstOrDefault();

            if (staff != null)
            {
                if (staff.Password == tbPass.Password)
                {
                    

                    switch (staff.RoleID)
                    {
                        case 1:
                            MainWindow mainWindow = new MainWindow(staff.RoleID);
                            mainWindow.tbName.Text = staff.LastName + " " + staff.FirstName;
                            mainWindow.Show();
                            this.Close();
                            break;
                        case 2:
                            MainWindow mainWindow1 = new MainWindow(staff.RoleID);
                            mainWindow1.tbName.Text = staff.LastName + " " + staff.FirstName;
                            mainWindow1.Show();
                            this.Close();
                            break;
                    }
                }
                else
                {
                    MessageBox.Show("Неверный пароль", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Information);
                    tbPass.Password = "";
                }
            }
            else
            {
                MessageBox.Show("Такого пользоватиеля не существует", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
    }
}
