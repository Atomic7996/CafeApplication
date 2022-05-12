using CafeApplication.Windows;
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
        List<decimal?> count = new List<decimal?>();
        int t = 0;

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

            //lbFoodStaff.ItemsSource = DB.db.ProductFoodStuff.Where(p => p.ProductID == product.ProductID).ToList();
            lbFoodStaff.ItemsSource = DB.db.FoodStaff.ToList();
            //lbFoodStaff.SelectedValuePath = "Title";

            //TextBox tb = lbFoodStaff.ItemTemplate.FindName("tbCount", lbFoodStaff) as TextBox;
            //tb = lbFoodStaff.SelectedItem.ToString();
        }

        private void AddProductFoodStaff()
        {
            int[] IDs = new int[lbFoodStaff.SelectedItems.Count];
            int i = 0;
            int j = 0;
            List<string> foodStaff = new List<string>();

            foreach (var item in lbFoodStaff.SelectedItems)
            {
                //MessageBox.Show(item.ToString());
                foodStaff.Add(item.ToString());
                //count.Add();
            }

            foreach (var item in foodStaff)
            {
                string title = foodStaff[i];
                IDs[i] = DB.db.FoodStaff.Where(x => x.Title.ToLower() == title.ToLower()).FirstOrDefault().FoodStuffID;
                i++;
            }
            
            foreach (var id in IDs)
            {
                //j = count.Count;
                productFoodStuff.FoodStaffID = id;
                productFoodStuff.ProductID = product.ProductID;
                //productFoodStuff.Count = count[j];
                productFoodStuff.Count = 1;
                j--;
                DB.db.ProductFoodStuff.Add(productFoodStuff);
                DB.db.SaveChanges();
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
                DB.db.ProductFoodStuff.Remove(item);

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
                    if ((DB.db.ComboProduct.Where(x => x.ProductID == product.ProductID)) != null || 
                        (DB.db.OrderProduct.Where(x => x.ProductID == product.ProductID)) != null)
                    {
                        MessageBox.Show("Товар используется в наборах или заказах, запись удалить невозможно", "Уведомление");
                    }
                    else
                    {
                        foreach (var item in DB.db.ProductFoodStuff.Where(x => x.ProductID == product.ProductID))
                            DB.db.ProductFoodStuff.Remove(item);

                        DB.db.Product.Remove(product);
                        DB.db.SaveChanges();
                        MessageBox.Show("Запись удалена", "Уведомление");
                        Manager.mainFrame.GoBack();
                    }                    
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
        }

        private void imgLogo_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            WindowImages window = new WindowImages("Product");
            window.ShowDialog();

            if (window.DialogResult == true)
            {
                product.Image = window.imageUrl;
                DataContext = null;
                DataContext = product;
            }
        }

        private void tbCount_LostFocus_1(object sender, RoutedEventArgs e)
        {
            //count.Insert(lbFoodStaff.SelectedIndex, decimal.Parse(((TextBox)sender).Text));
            //count.Add(decimal.Parse(((TextBox)sender).Text));
            //string str = "";

            //for (int i = 0; i < count.Count; i++)
            //{
            //    str += count[i].ToString() + '\n';
                
            //}
            ////MessageBox.Show(str.ToString());
            //MessageBox.Show(lbFoodStaff.SelectedItem.ToString());

        }
    }
}
