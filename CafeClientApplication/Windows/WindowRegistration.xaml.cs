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
using System.Windows.Shapes;

namespace CafeClientApplication.Windows
{
    /// <summary>
    /// Логика взаимодействия для WindowRegistration.xaml
    /// </summary>
    public partial class WindowRegistration : Window
    {
        User user = new User();

        public WindowRegistration(User selectedUser)
        {
            InitializeComponent();
            PageStartUp(selectedUser);
        }

        private void PageStartUp(User selectedUser)
        {
            if (selectedUser != null)
                user = selectedUser;

            if (user.UserID == 0)
            {
                btnDel.Visibility = Visibility.Hidden;
                tbHeader.Text = "Регистрация";
                spPass.Visibility = Visibility.Visible;
                btnPassword.Visibility = Visibility.Hidden;
                spBtns.Visibility = Visibility.Visible;
            }
            else
            {
                tbHeader.Text = "Редактор аккаунта";
                btnPassword.Visibility = Visibility.Visible;
                spPass.Visibility = Visibility.Hidden;
            }

            DataContext = user;
        }

        private void Save()
        {
            User checkUser = DB.db.User.Where(u => u.Login == tbLogin.Text).FirstOrDefault();
            StringBuilder errors = new StringBuilder();

            if (string.IsNullOrWhiteSpace(user.LastName))
                errors.AppendLine("Укажите Фамилию");
            if (string.IsNullOrWhiteSpace(user.FirstName))
                errors.AppendLine("Укажите имя");
            if (string.IsNullOrWhiteSpace(user.Patronymic))
                errors.AppendLine("Укажите отчество");
            if (string.IsNullOrWhiteSpace(user.Login))
                errors.AppendLine("Укажите логин");
            if (string.IsNullOrEmpty(tbPhone.Text) || string.IsNullOrWhiteSpace(tbPhone.Text))
                errors.AppendLine("Не введен телефон\n");
            else if (tbPhone.Text.Length < 18)
                errors.AppendLine("Введите телефон полностью\n");
            if (user.UserID == 0)
            {
                if (string.IsNullOrWhiteSpace(tbPassword.Password))
                    errors.AppendLine("Придумайте пароль");
                if (!PasswordCheck.IsStrong(tbPassword.Password))
                    errors.AppendLine("Пароль должен отвечать следующим требованиям:\nМинимум 6 символов\n" +
                        "Минимум 1 прописная буква\n" +
                        "Минимум 1 цифра\n" +
                        "По крайней мере один из следующих символов: ! @ # $ % ^");
            }

            if (checkUser != null)
            {
                if (user.UserID != checkUser.UserID)
                    errors.AppendLine("Такой логин уже занят");
            }

            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString(), "Внимание", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            try
            {
                if (user.UserID == 0)
                {
                    user.Password = tbPassword.Password;                    
                    user.CardSale = 0;
                    DB.db.User.Add(user);

                }

                DB.db.SaveChanges();
                //MessageBox.Show("Данные сохранены", "Уведомление");
                this.Close();
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
                    DB.db.User.Remove(user);
                    DB.db.SaveChanges();
                    //MessageBox.Show("Запись удалена", "Уведомление");
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

        private void btnPassword_Click(object sender, RoutedEventArgs e)
        {
            WindowChangePass window = new WindowChangePass(user);
            window.ShowDialog();

            if (window.DialogResult == true)
            {
                DataContext = null;
                DataContext = user;
            }
        }

        private void tbLastName_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!Char.IsLetter(e.Text, 0))
                e.Handled = true;
        }

        private void tbPhone_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!Char.IsDigit(e.Text, 0))
                e.Handled = true;
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
