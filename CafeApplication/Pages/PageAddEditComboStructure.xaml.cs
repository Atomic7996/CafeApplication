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
    /// Логика взаимодействия для PageAddEditComboStructure.xaml
    /// </summary>
    public partial class PageAddEditComboStructure : Page
    {
        private List<ProductItemTemplate> _productItemTemplates = new List<ProductItemTemplate>();
        private Combo selectedCombo = new Combo();

        public PageAddEditComboStructure(Combo selectedCombo)
        {
            InitializeComponent();
            ShowProducts();

            if (selectedCombo != null)
                this.selectedCombo = selectedCombo;

            DataContext = this.selectedCombo;
        }

        private void ShowProducts()
        {
            List<Product> products = DB.db.Product.ToList();

            if (!string.IsNullOrEmpty(tbFindProduct.Text))
                products = products.Where(p => p.Title.ToLower().Contains(tbFindProduct.Text.ToLower())).ToList();

            lvAllProducts.ItemsSource = products;

            lvSelectedProducts.Items.Clear();
            foreach (var productItemTemplate in _productItemTemplates)
                lvSelectedProducts.Items.Add(productItemTemplate.GridProductItemTemplate);
        }

        private void tbFindFoodStaff_TextChanged(object sender, TextChangedEventArgs e)
        {
            List<Product> product = DB.db.Product.ToList();

            if (!string.IsNullOrEmpty(tbFindProduct.Text))
                product = product.Where(fs => fs.Title.ToLower().Contains(tbFindProduct.Text.ToLower())).ToList();

            lvAllProducts.ItemsSource = product;
        }

        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            DB.db.Product.ToList();
            tbFindProduct.Text = null;
        }

        private void lbAllFoodStaff_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //btnGetProduct.IsEnabled = lvAllProducts.SelectedItem != null ? true : false;
        }

        private void lbAllFoodStaff_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            btnGetProduct_Click(null, null);
        }

        private void btnGetProduct_Click(object sender, RoutedEventArgs e)
        {
            Product product = lvAllProducts.SelectedItem as Product;

            if (product != null)
            {
                ProductItemTemplate productItemTemplate = new ProductItemTemplate(product);
                _productItemTemplates.Add(productItemTemplate);
                ShowProducts();
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
                }
            }
        }

        private void lbSelectedFoodStaff_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            btnCancelProduct_Click(null, null);
        }

        private void lbSelectedFoodStaff_SelectionChanged(object sender, SelectionChangedEventArgs e)
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
                foreach (var item in DB.db.ComboProduct.Where(x => x.ComboID == selectedCombo.ComboID))
                    DB.db.ComboProduct.Remove(item);

                if (selectedCombo.ComboID == 0)
                    DB.db.Combo.Add(selectedCombo);

                foreach (var productItemTemplate in _productItemTemplates)
                {
                    ComboProduct cp = new ComboProduct();
                    cp.ProductID = productItemTemplate.product.ProductID;
                    cp.ComboID = selectedCombo.ComboID;
                    cp.Count = int.Parse(productItemTemplate.TbCount.Text);

                    DB.db.ComboProduct.Add(cp);
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
