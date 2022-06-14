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
    /// Логика взаимодействия для WindowChangePass.xaml
    /// </summary>
    public partial class WindowChangePass : Window
    {
        Staff staff = new Staff();

        public WindowChangePass(Staff selectedStaff)
        {
            InitializeComponent();

            staff = selectedStaff;
        }
        /// <summary>
        /// Проверка старого пароля и смена на новый
        /// </summary>
        private void ChangePassword()
        {
            if (staff.Password == tbOldPass.Password)
            {
                try
                {
                    staff.Password = tbNewPass.Password;

                    DB.db.SaveChanges();
                    MessageBox.Show("Пароль успешно изменен", "Уведомление");
                    DialogResult = true;
                    Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
            else
            {
                MessageBox.Show("Неверный старый пароль", "Предупреждение",
                    MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            ChangePassword();
        }
    }
}
