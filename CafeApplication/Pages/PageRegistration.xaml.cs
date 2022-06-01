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
            PageStartUp(selectedStaff);            
        }

        private void PageStartUp(Staff selectedStaff)
        {
            if (selectedStaff != null)
                staff = selectedStaff;

            if (staff.StaffID == 0)
            {
                btnDel.Visibility = Visibility.Hidden;
                tbHeader.Text = "Регистрация";
            }
            else
            {
                tbHeader.Text = "Редактор сотрудника";
            }

            DataContext = staff;
            cbRole.ItemsSource = DB.db.StaffRole.ToList();
        }

        private void Save()
        {
            Staff checkStaff = DB.db.Staff.Where(s => s.Login == tbLogin.Text).FirstOrDefault();
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

            if (checkStaff != null)
            {
                if (staff.StaffID != checkStaff.StaffID)
                    errors.AppendLine("Такой логин уже занят");
            }

            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString(), "Внимание", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }            

            try
            {
                if (staff.StaffID == 0)
                    DB.db.Staff.Add(staff);

                DB.db.SaveChanges();
                MessageBox.Show("Данные сохранены", "Уведомление");
                Manager.mainFrame.GoBack();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void Delete()
        {
            if (MessageBox.Show("Вы точно хотите удалить запись?", "Внимание", 
                MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
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

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            Save();
        }

        private void btnDel_Click(object sender, RoutedEventArgs e)
        {
            Delete();
        }
    }
}
