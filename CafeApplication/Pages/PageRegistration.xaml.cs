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
    /// Логика взаимодействия для PageRegistration.xaml
    /// </summary>
    public partial class PageRegistration : Page
    {
        Staff staff = new Staff();

        public PageRegistration(Staff selectedStaff)
        {
            InitializeComponent();

            if (selectedStaff != null)
            {
                staff = selectedStaff;
            }

            if (staff.StaffID == 0)
            {
                btnDel.Visibility = Visibility.Hidden;
            }

            DataContext = staff;

            cbRole.ItemsSource = DB.db.StaffRole.ToList();
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder errors = new StringBuilder();

            if (string.IsNullOrWhiteSpace(staff.LastName))
                errors.AppendLine("Укажите Фамилию");
            if (string.IsNullOrWhiteSpace(staff.FirstName))
                errors.AppendLine("Укажите имя");
            if (string.IsNullOrWhiteSpace(staff.Patronymic))
                errors.AppendLine("Укажите отчество");
            if (string.IsNullOrWhiteSpace(staff.Login))
                errors.AppendLine("Укажите логин");
            if (string.IsNullOrWhiteSpace(staff.Password))
                errors.AppendLine("Придумайте пароль");

            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString(), "Внимание", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            if (staff.StaffID == 0)
                DB.db.Staff.Add(staff);

            try
            {
                DB.db.SaveChanges();
                MessageBox.Show("Данные сохранены", "Уведомление");
                Manager.mainFrame.GoBack();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void btnDel_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Вы точно хотите удалить запись?", "Внимание", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                try
                {
                    DB.db.Staff.Remove(staff);
                    DB.db.SaveChanges();
                    MessageBox.Show("Запись удалена", "Уведомление");
                    Manager.mainFrame.GoBack();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
        }
    }
}
