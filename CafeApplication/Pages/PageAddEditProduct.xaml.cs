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
    /// Логика взаимодействия для PageAddEditProduct.xaml
    /// </summary>
    public partial class PageAddEditProduct : Page
    {
        Product product = new Product();

        public PageAddEditProduct(Product selectedProduct)
        {
            InitializeComponent();

            if (selectedProduct != null)
            {
                product = selectedProduct;
            }

            if (product.ProductID == 0)
            {
                btnDelete.Visibility = Visibility.Hidden;
            }

            DataContext = product;

            cbTypes.ItemsSource = DB.db.ProductType.ToList();
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder errors = new StringBuilder();

            if (string.IsNullOrWhiteSpace(product.Title))
                errors.AppendLine("Укажите название");
            if (string.IsNullOrWhiteSpace(product.Cost.ToString()))
                errors.AppendLine("Укажите стоимость");

            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString(), "Внимание", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            if (product.ProductID == 0)
                DB.db.Product.Add(product);

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
                    DB.db.Product.Remove(product);
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
