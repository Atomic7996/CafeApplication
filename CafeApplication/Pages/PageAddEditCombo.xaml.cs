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
        OrderProduct comboProduct = new OrderProduct();

        public PageAddEditCombo(Combo selectedCombo)
        {
            InitializeComponent();
            PageStartUp(selectedCombo);            
        }

        private void PageStartUp(Combo selectedCombo)
        {
            if (selectedCombo != null)
                combo = selectedCombo;

            if (combo.ComboID == 0)
                btnDelete.Visibility = Visibility.Hidden;

            DataContext = combo;
        }

        private void Save()
        {
            StringBuilder errors = new StringBuilder();

            if (string.IsNullOrWhiteSpace(combo.Title))
                errors.AppendLine("Укажите название");
            if (string.IsNullOrWhiteSpace(combo.Cost.ToString()))
                errors.AppendLine("Укажите стоимость");

            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString(), "Внимание",
                    MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            if (combo.ComboID == 0)
                DB.db.Combo.Add(combo);

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

        private void Delete()
        {
            if (MessageBox.Show("Вы точно хотите удалить запись?", "Внимание", 
                MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                try
                {
                    if ((DB.db.OrderCombo.Where(x => x.ComboID == combo.ComboID)).FirstOrDefault() != null)
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

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            Save();
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            Delete();
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

        private void tbStructure_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            DB.db.ChangeTracker.Entries().ToList().ForEach(a => a.Reload());
            tbStructure.Text = combo.ProductList;
        }

        private void btnChange_Click(object sender, RoutedEventArgs e)
        {
            Manager.mainFrame.Navigate(new PageAddEditComboStructure((sender as Button).DataContext as Combo));
        }
    }
}
