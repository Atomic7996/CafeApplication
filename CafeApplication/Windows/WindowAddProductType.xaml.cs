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
using System.Windows.Shapes;

namespace CafeApplication.Windows
{
    /// <summary>
    /// Логика взаимодействия для WindowAddProductType.xaml
    /// </summary>
    public partial class WindowAddProductType : Window
    {
        ProductType productType = new ProductType();
        public WindowAddProductType()
        {
            InitializeComponent();
            DataContext = productType;
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                DB.db.ProductType.Add(productType);
                DB.db.SaveChanges();
                MessageBox.Show("Данные сохранены", "Уведомление");
                DialogResult = true;
                Close();
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
    }
}
