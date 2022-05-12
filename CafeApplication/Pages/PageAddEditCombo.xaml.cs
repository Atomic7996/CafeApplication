using CafeApplication.Windows;
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
    /// Логика взаимодействия для PageAddEditCombo.xaml
    /// </summary>
    public partial class PageAddEditCombo : Page
    {
        Combo combo = new Combo();
        ComboProduct comboProduct = new ComboProduct();

        public PageAddEditCombo(Combo selectedCombo)
        {
            InitializeComponent();

            if (selectedCombo != null)
            {
                combo = selectedCombo;
            }

            if (combo.ComboID == 0)
            {
                btnDelete.Visibility = Visibility.Hidden;
            }

            DataContext = combo;

            lbProducts.ItemsSource = GetProducts();
        }

        private string[] GetProducts()
        {
            List<Product> prod = DB.db.Product.ToList();
            string[] products = new string[prod.Count];
            for (int i = 0; i < prod.Count; i++)
            {
                products[i] = prod[i].Title;
            }
            return products;
        }

        private void AddComboProduct(string[] foodStaff)
        {
            int[] IDs = new int[lbProducts.SelectedItems.Count];
            int i = 0;
            foreach (var item in lbProducts.SelectedItems)
            {
                IDs[i] = DB.db.Product.Where(x => x.Title == item.ToString()).FirstOrDefault().ProductID;
                i++;
            }

            foreach (var id in IDs)
            {
                comboProduct.ProductID = id;
                comboProduct.ComboID = combo.ComboID;
                DB.db.ComboProduct.Add(comboProduct);
                DB.db.SaveChanges();
            }

        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder errors = new StringBuilder();

            if (string.IsNullOrWhiteSpace(combo.Title))
                errors.AppendLine("Укажите название");
            if (string.IsNullOrWhiteSpace(combo.Cost.ToString()))
                errors.AppendLine("Укажите стоимость");

            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString(), "Внимание", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            foreach (var item in DB.db.ComboProduct.Where(x => x.ComboID == combo.ComboID))
            {
                DB.db.ComboProduct.Remove(item);
            }

            string[] prods = new string[lbProducts.Items.Count];
            int i = 0;
            foreach (var item in lbProducts.Items)
            {
                prods[i] = item.ToString();
                i++;
            }

            if (combo.ComboID == 0)
                DB.db.Combo.Add(combo);

            AddComboProduct(prods);

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
                    if ((DB.db.OrderCombo.Where(x => x.ComboID == combo.ComboID)) != null)
                    {
                        MessageBox.Show("Набор используется в заказах, запись удалить невозможно", "Уведомление");
                    }
                    else
                    {
                        foreach (var item in DB.db.ComboProduct.Where(x => x.ComboID == combo.ComboID))
                            DB.db.ComboProduct.Remove(item);

                        DB.db.Combo.Remove(combo);
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
            WindowImages window = new WindowImages("Combo");
            window.ShowDialog();

            if (window.DialogResult == true)
            {
                combo.Image = window.imageUrl;
                DataContext = null;
                DataContext = combo;
            }
        }
    }
}
