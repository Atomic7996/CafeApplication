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
    /// Логика взаимодействия для PageEditProductStructure.xaml
    /// </summary>
    public partial class PageEditProductStructure : Page
    {
        private List<FoodStaffItemTemplate> _foodStaffItemTemplates = new List<FoodStaffItemTemplate>();
        private Product selectedProduct = new Product();

        public PageEditProductStructure(Product product)
        {
            InitializeComponent();
            ShowFoodStaff();

            if (product != null)
                selectedProduct = product;

            DataContext = selectedProduct;
        }

        private void ShowFoodStaff()
        {
            List<FoodStaff> foodStaff = DB.db.FoodStaff.ToList();

            if (!string.IsNullOrEmpty(tbFindFoodStaff.Text))
                foodStaff = foodStaff.Where(fs => fs.Title.ToLower().Contains(tbFindFoodStaff.Text.ToLower())).ToList();

            lvAllFoodStaff.ItemsSource = foodStaff;

            lvSelectedFoodStaff.Items.Clear();
            foreach (var foodStaffItemTemplate in _foodStaffItemTemplates)
                lvSelectedFoodStaff.Items.Add(foodStaffItemTemplate.GridFoodStaffItemTemplate);

            foreach (var foodStaffItemTemplate in _foodStaffItemTemplates)
                foodStaff.Remove(foodStaffItemTemplate.foodStaff);
        }

        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            tbFindFoodStaff.Text = "";
            ShowFoodStaff();
        }

        private void lbAllProducts_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            btnGetProduct_Click(null, null);
        }

        private void lbAllProducts_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //btnGetProduct.IsEnabled = lvAllFoodStaff.SelectedItem != null ? true : false;
        }

        private void btnGetProduct_Click(object sender, RoutedEventArgs e)
        {
            FoodStaff foodStaff = lvAllFoodStaff.SelectedItem as FoodStaff;

            if (foodStaff != null)
            {
                FoodStaffItemTemplate foodStaffItemTemplate = new FoodStaffItemTemplate(foodStaff);
                _foodStaffItemTemplates.Add(foodStaffItemTemplate);
                ShowFoodStaff();
            }
        }

        private void btnCancelProduct_Click(object sender, RoutedEventArgs e)
        {
            Grid gridFoodStaffItemTemplate = lvSelectedFoodStaff.SelectedItem as Grid;

            if (gridFoodStaffItemTemplate != null)
            {
                FoodStaffItemTemplate foodStaffItemTemplate = _foodStaffItemTemplates.FirstOrDefault(fsit => fsit.GridFoodStaffItemTemplate == gridFoodStaffItemTemplate);
                if (foodStaffItemTemplate != null)
                {
                    _foodStaffItemTemplates.Remove(foodStaffItemTemplate);
                    ShowFoodStaff();
                }
            }
        }

        private void lbSelectedProducts_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            btnCancelProduct_Click(null, null);
        }

        private void lbSelectedProducts_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //btnCancelProduct.IsEnabled = lvSelectedFoodStaff.SelectedItem != null ? true : false;
        }

        private bool CheckData()
        {
            bool trueData = true;
            string errorMessage = "Введены некорректные данные:\n";

            if (_foodStaffItemTemplates.Count == 0)
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
                foreach (var item in DB.db.ProductFoodStuff.Where(x => x.ProductID == selectedProduct.ProductID))
                    DB.db.ProductFoodStuff.Remove(item);

                if (selectedProduct.ProductID == 0)
                    DB.db.Product.Add(selectedProduct);

                    foreach (var foodStaffItemTemplate in _foodStaffItemTemplates)
                {
                    ProductFoodStuff pfs = new ProductFoodStuff();
                    pfs.FoodStaffID = foodStaffItemTemplate.foodStaff.FoodStuffID;
                    pfs.ProductID = selectedProduct.ProductID;
                    pfs.Count = int.Parse(foodStaffItemTemplate.TbCount.Text);

                    DB.db.ProductFoodStuff.Add(pfs);
                }

                try
                {
                    DB.db.SaveChanges();
                    //MessageBox.Show("Данные сохранены", "Уведомление",MessageBoxButton.OK, MessageBoxImage.Information);
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

        private void tbFindFoodStaff_TextChanged(object sender, TextChangedEventArgs e)
        {
            List<FoodStaff> foodStaff = DB.db.FoodStaff.ToList();

            if (!string.IsNullOrEmpty(tbFindFoodStaff.Text))
                foodStaff = foodStaff.Where(fs => fs.Title.ToLower().Contains(tbFindFoodStaff.Text.ToLower())).ToList();

            lvAllFoodStaff.ItemsSource = foodStaff;

            if (lvAllFoodStaff.Items.Count == 0)
            {
                lvAllFoodStaff.Visibility = Visibility.Hidden;
                tbAvailable.Visibility = Visibility.Visible;
            }
            else
            {
                lvAllFoodStaff.Visibility = Visibility.Visible;
                tbAvailable.Visibility = Visibility.Hidden;
            }

            ShowFoodStaff();
        }
    }
}

