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
    /// Логика взаимодействия для PageAddEditOrderProducts.xaml
    /// </summary>
    public partial class PageAddEditOrderProducts : Page
    {
        private List<ProductItemTemplate> _productItemTemplates = new List<ProductItemTemplate>();
        private Order order = new Order();

        public PageAddEditOrderProducts(Order selectedOrder)
        {
            InitializeComponent();
            ShowProducts();

            if (selectedOrder != null)
            {
                order = selectedOrder;
            }

            DataContext = this.order;
        }

        private void ShowProducts()
        {
            List<Product> products = DB.db.Product.ToList();

            if (!string.IsNullOrEmpty(tbFindFoodStaff.Text))
                products = products.Where(p => p.Title.ToLower().Contains(tbFindFoodStaff.Text.ToLower())).ToList();

            lvAllProducts.ItemsSource = products;

            lvSelectedProducts.Items.Clear();
            foreach (var productItemTemplate in _productItemTemplates)
                lvSelectedProducts.Items.Add(productItemTemplate.GridProductItemTemplate);

            foreach (var productItemTemplate in _productItemTemplates)
                products.Remove(productItemTemplate.product);
        }

        private void tbFindProduct_TextChanged(object sender, TextChangedEventArgs e)
        {
            List<Product> product = DB.db.Product.ToList();

            if (!string.IsNullOrEmpty(tbFindFoodStaff.Text))
                product = product.Where(fs => fs.Title.ToLower().Contains(tbFindFoodStaff.Text.ToLower())).ToList();

            lvAllProducts.ItemsSource = product;

            if (lvAllProducts.Items.Count == 0)
            {
                lvAllProducts.Visibility = Visibility.Hidden;
                tbAvailable.Visibility = Visibility.Visible;
            }
            else
            {
                lvAllProducts.Visibility = Visibility.Visible;
                tbAvailable.Visibility = Visibility.Hidden;
            }

            ShowProducts();         
        }

        private void tbCount_TextChanged(object sender, TextChangedEventArgs e)
        {
            decimal fullPrice = 0;
            foreach (var pit in _productItemTemplates)
            {
                if (!string.IsNullOrEmpty(pit.TbCount.Text))
                {
                    fullPrice += pit.product.Cost * int.Parse(pit.TbCount.Text);
                }
            }                

            tbFullPrice.Text = fullPrice.ToString();
        }

        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            tbFindFoodStaff.Text = null;
            ShowProducts();
        }

        private void lbAllProducts_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            btnGetProduct_Click(null, null);
        }

        private void lbAllProducts_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //btnGetProduct.IsEnabled = lvAllProducts.SelectedItem != null ? true : false;
        }

        private void btnGetProduct_Click(object sender, RoutedEventArgs e)
        {
            Product product = lvAllProducts.SelectedItem as Product;

            if (product != null)
            {
                ProductItemTemplate productItemTemplate = new ProductItemTemplate(product);
                productItemTemplate.TbCount.TextChanged += tbCount_TextChanged;
                _productItemTemplates.Add(productItemTemplate);
                ShowProducts();
                tbCount_TextChanged(null, null);
            }
        }

        private void btnCancelProduct_Click(object sender, RoutedEventArgs e)
        {
            Grid gridProductItemTemplate = lvSelectedProducts.SelectedItem as Grid;

            if (gridProductItemTemplate != null)
            {
                ProductItemTemplate productItemTemplate = _productItemTemplates.FirstOrDefault(pit => pit.GridProductItemTemplate == gridProductItemTemplate);
                if (productItemTemplate != null)
                {
                    _productItemTemplates.Remove(productItemTemplate);
                    ShowProducts();
                    productItemTemplate.TbCount.TextChanged += tbCount_TextChanged;
                    tbCount_TextChanged(null, null);
                }
            }
        }

        private void lbSelectedProducts_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            btnCancelProduct_Click(null, null);
        }

        private void lbSelectedProducts_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //btnCancelProduct.IsEnabled = lvSelectedProducts.SelectedItem != null ? true : false;
        }

        private bool CheckData()
        {
            bool trueData = true;
            string errorMessage = "Введены некорректные данные:\n";

            if (_productItemTemplates.Count == 0)
            {
                errorMessage += "Не выбран ни один товар\n";
                trueData = false;
            }

            if (!trueData)
                MessageBox.Show(errorMessage, "Внимание",
                    MessageBoxButton.OK, MessageBoxImage.Error);

            return trueData;
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            if (CheckData())
            {
                foreach (var item in DB.db.OrderProduct.Where(x => x.OrderID == order.OrderID))
                    DB.db.OrderProduct.Remove(item);

                if (order.OrderID == 0)
                {
                    order.OrderDateTime = DateTime.Now;
                    order.StaffID = Properties.Settings.Default.staffID;
                    DB.db.Order.Add(order);
                }

                foreach (var productItemTemplate in _productItemTemplates)
                {
                    OrderProduct op = new OrderProduct();
                    op.ProductID = productItemTemplate.product.ProductID;
                    op.OrderID = order.OrderID;
                    op.Count = int.Parse(productItemTemplate.TbCount.Text);

                    DB.db.OrderProduct.Add(op);
                }

                try
                {
                    DB.db.SaveChanges();
                    Manager.mainFrame.GoBack();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }            
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            Manager.mainFrame.GoBack();
            Manager.mainFrame.RemoveBackEntry();
        }
    }
}
