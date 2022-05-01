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
    /// Логика взаимодействия для PageAddEditFoodStaff.xaml
    /// </summary>
    public partial class PageAddEditFoodStaff : Page
    {
        FoodStaff foodStaff = new FoodStaff();

        public PageAddEditFoodStaff(FoodStaff selectedFoodStaff)
        {
            InitializeComponent();

            if (selectedFoodStaff != null)
            {
                foodStaff = selectedFoodStaff;
            }

            if (foodStaff.FoodStuffID == 0)
            {
                btnDelete.Visibility = Visibility.Hidden;
            }

            DataContext = foodStaff;
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder errors = new StringBuilder();

            if (string.IsNullOrWhiteSpace(foodStaff.Title))
                errors.AppendLine("Укажите название");
            if (string.IsNullOrWhiteSpace(foodStaff.CountInStock.ToString()))
                errors.AppendLine("Укажите кол-во на складе");
            if (string.IsNullOrWhiteSpace(foodStaff.MinCount.ToString()))
                errors.AppendLine("Укажите мин. кол-во");
            if (string.IsNullOrWhiteSpace(foodStaff.Unit))
                errors.AppendLine("Укажите ед. измерения");

            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString(), "Внимание", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            if (foodStaff.FoodStuffID == 0)
                DB.db.FoodStaff.Add(foodStaff);

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

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Вы точно хотите удалить запись?", "Внимание", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                try
                {
                    DB.db.FoodStaff.Remove(foodStaff);
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

        private void imgLogo_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

        }
    }
}
