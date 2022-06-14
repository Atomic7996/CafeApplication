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

            lbAllProducts.ItemsSource = products;

            lbSelectedProducts.Items.Clear();
            foreach (var productItemTemplate in _productItemTemplates)
                lbSelectedProducts.Items.Add(productItemTemplate.GridProductItemTemplate);
        }

        private void tbFindFoodStaff_TextChanged(object sender, TextChangedEventArgs e)
        {
            List<Product> product = DB.db.Product.ToList();

            if (!string.IsNullOrEmpty(tbFindFoodStaff.Text))
                product = product.Where(fs => fs.Title.ToLower().Contains(tbFindFoodStaff.Text.ToLower())).ToList();

            lbAllProducts.ItemsSource = product;
        }

        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            DB.db.Product.ToList();
        }

        private void lbAllProducts_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            btnGetProduct.IsEnabled = lbAllProducts.SelectedItem != null ? true : false;
        }

        private void lbAllProducts_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            btnGetProduct_Click(null, null);
        }

        private void btnGetProduct_Click(object sender, RoutedEventArgs e)
        {
            Product product = lbAllProducts.SelectedItem as Product;

            if (product != null)
            {
                ProductItemTemplate productItemTemplate = new ProductItemTemplate(product);
                _productItemTemplates.Add(productItemTemplate);
                ShowProducts();
            }
        }

        private void btnCancelProduct_Click(object sender, RoutedEventArgs e)
        {
            Grid gridProductItemTemplate = lbSelectedProducts.SelectedItem as Grid;

            if (gridProductItemTemplate != null)
            {
                ProductItemTemplate productItemTemplate = _productItemTemplates.FirstOrDefault(pit => pit.GridProductItemTemplate == gridProductItemTemplate);
                if (productItemTemplate != null)
                {
                    _productItemTemplates.Remove(productItemTemplate);
                    ShowProducts();
                }
            }
        }

        private void lbSelectedProducts_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            btnCancelProduct_Click(null, null);
        }

        private void lbSelectedProducts_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            btnCancelProduct.IsEnabled = lbSelectedProducts.SelectedItem != null ? true : false;
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
                    MessageBox.Show("Данные сохранены", "Уведомление",
                        MessageBoxButton.OK, MessageBoxImage.Information);
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
