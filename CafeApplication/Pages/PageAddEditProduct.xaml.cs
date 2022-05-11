using System;
using System.Collections.Generic;
using System.Data.Entity;
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
        ProductFoodStuff productFoodStuff = new ProductFoodStuff();
        TextBox tb = new TextBox();

        public PageAddEditProduct(Product selectedProduct)
        {
            InitializeComponent();

            DB.db.ProductFoodStuff.Load();

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

            lbFoodStaff.ItemsSource = DB.db.FoodStaff.ToList();
            lbFoodStaff.SelectedValuePath = "Title";

            //tb = lbFoodStaff.ItemTemplate.FindName("tbCount", lbFoodStaff) as TextBox;
            tb = lbFoodStaff.SelectedItem.ToString();
        }

        private void AddProductFoodStaff()
        {
            int[] IDs = new int[lbFoodStaff.SelectedItems.Count];
            int i = 0;
            int j = 0;
            List<string> foodStaff = new List<string>();
            List<decimal?> count = new List<decimal?>();

            foreach (var item in lbFoodStaff.SelectedItems)
                foodStaff.Add(item.ToString());

            foreach (var item in foodStaff)
            {
                string title = foodStaff[i];
                IDs[i] = DB.db.FoodStaff.Where(x => x.Title.ToLower() == title.ToLower()).FirstOrDefault().FoodStuffID;
                //count.Add(decimal.Parse(tb.Text));
            }
            
            foreach (var id in IDs)
            {
                productFoodStuff.FoodStaffID = id;
                productFoodStuff.ProductID = product.ProductID;
                productFoodStuff.Count = count[j];
                j++;
                DB.db.ProductFoodStuff.Add(productFoodStuff);
                //DB.db.SaveChanges();
            }
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

            foreach (var item in DB.db.ProductFoodStuff.Where(x => x.ProductID == product.ProductID))
            {
                DB.db.ProductFoodStuff.Remove(item);
            }

            if (product.ProductID == 0)
                DB.db.Product.Add(product);

            AddProductFoodStaff();

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
