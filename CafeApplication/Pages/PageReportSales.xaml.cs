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
    /// Логика взаимодействия для PageReportSales.xaml
    /// </summary>
    public partial class PageReportSales : Page
    {
        public PageReportSales()
        {
            InitializeComponent();
            PageStartUp();
        }

        private void PageStartUp()
        {
            var types = new List<string>();
            types.Add("Тип отчета");
            types.Add("Все продажи");
            types.Add("По блюдам");
            types.Add("По наборам");
            //types.Add("По товарам");

            cbTypes.ItemsSource = types;
            cbTypes.SelectedIndex = 0;

            cbSort.SelectedIndex = 0;
        }

        private void UpdateList()
        {
            var orderList = DB.db.Order.ToList();

            switch (cbTypes.SelectedIndex)
            {
                case 1:
                    calendar.IsEnabled = true;
                    cbSort.IsEnabled = false;
                    cbSort.Text = null;
                    spProds.Visibility = Visibility.Visible;
                    spCombos.Visibility = Visibility.Visible;
                    spSelectedProduct.Visibility = Visibility.Hidden;

                    if (calendar.SelectedDate != null)
                    {
                        orderList = orderList.Where(s => s.OrderDateTime.Date.ToString().Contains(calendar.SelectedDate.ToString())).ToList();
                        //orderList = orderList.Where(s => s.OrderDateTime.Date.Month.ToString().Contains(calendar.SelectedDate.Value.Month.ToString())).ToList();
                    }

                    tbProductsCount.Text = OrderProduct.productsAmount(orderList, null);
                    tbProductsPrice.Text = OrderProduct.productsCost(orderList, null);
                    tbCombosCount.Text = OrderCombo.combosAmount(orderList);
                    tbCombosPrice.Text = OrderCombo.combosCost(orderList);
                    break;

                case 2:
                    calendar.IsEnabled = false;
                    calendar.SelectedDate = null;
                    cbSort.IsEnabled = true;
                    spProds.Visibility = Visibility.Hidden;
                    spCombos.Visibility = Visibility.Hidden;
                    spSelectedProduct.Visibility = Visibility.Visible;

                    var sort = DB.db.Product.ToList();
                    sort.Insert(0, new Product { Title = "Блюда", ProductID=0 });

                    cbSort.ItemsSource = sort;
                    cbSort.SelectedValuePath = "ProductID";
                    //cbSort.Text = "Блюда";

                    if (cbSort.SelectedIndex > 0)
                    {
                        Product product = DB.db.Product.Where(p => p.ProductID.ToString() == cbSort.SelectedValue.ToString()).FirstOrDefault();
                        spSelectedProduct.Visibility = Visibility.Visible;
                        tbTitle.Text = product.Title;
                        tbCount.Text = OrderProduct.productsAmount(null, product);
                        tbCost.Text = OrderProduct.productsCost(null, product);
                    }
                    else
                    {
                        spSelectedProduct.Visibility = Visibility.Hidden;
                    }

                    
                    break;

                case 3:
                    calendar.IsEnabled = false;
                    calendar.SelectedDate = null;
                    cbSort.IsEnabled = false;
                    cbSort.Text = null;
                    spProds.Visibility = Visibility.Hidden;
                    spCombos.Visibility = Visibility.Hidden;
                    spSelectedProduct.Visibility = Visibility.Hidden;
                    break;

                default:
                    calendar.IsEnabled = false;
                    calendar.SelectedDate = null;
                    cbSort.IsEnabled = false;
                    cbSort.Text = null;
                    spProds.Visibility = Visibility.Hidden;
                    spCombos.Visibility = Visibility.Hidden;
                    spSelectedProduct.Visibility = Visibility.Hidden;
                    break;
            }
        }

        private void calendar_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateList();
        }

        private void cbTypes_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateList();
        }

        private void cbSort_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateList();
        }
    }
}
