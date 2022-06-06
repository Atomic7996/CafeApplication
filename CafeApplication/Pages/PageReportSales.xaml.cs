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
        List<Order> orderList = DB.db.Order.ToList();
        List<string> mounths = new List<string>();
        List<Product> productsList = DB.db.Product.ToList();
        List<Combo> combosList = DB.db.Combo.ToList();

        public PageReportSales()
        {
            InitializeComponent();
            PageStartUp();
        }

        private void PageStartUp()
        {
            calendar.Visibility = Visibility.Hidden;
            cbSort.Visibility = Visibility.Hidden;

            var types = new List<string>();
            types.Add("Тип отчета");
            types.Add("Все продажи");
            types.Add("По блюдам");
            types.Add("По наборам");
            //types.Add("По сотрудникам");

            cbTypes.ItemsSource = types;
            cbTypes.SelectedIndex = 0;

            mounths.Add("Все месяцы");
            mounths.Add("Январь");
            mounths.Add("Февраль");
            mounths.Add("Март");
            mounths.Add("Апрель");
            mounths.Add("Май");
            mounths.Add("Июнь");
            mounths.Add("Июль");
            mounths.Add("Август");
            mounths.Add("Сентябрь");
            mounths.Add("Октябрь");
            mounths.Add("Декабрь");

            productsList.Insert(0, new Product { Title = "Все блюда" });
            combosList.Insert(0, new Combo { Title = "Все наборы" });
        }

        private void UpdateList()
        {
            orderList = DB.db.Order.ToList();

            switch (cbTypes.SelectedIndex)
            {
                case 1:
                    calendar.IsEnabled = true;
                    calendar.Visibility = Visibility.Visible;
                    cbSort.IsEnabled = true;
                    cbSort.Visibility = Visibility.Visible;
                    spProds.Visibility = Visibility.Visible;
                    spCombos.Visibility = Visibility.Visible;
                    spSelectedProduct.Visibility = Visibility.Hidden;
                    cbSort.ItemsSource = mounths;

                    if (calendar.SelectedDate != null)
                    {
                        orderList = orderList.Where(s => s.OrderDateTime.Date.ToString().Contains(calendar.SelectedDate.ToString())).ToList();
                        cbSort.SelectedIndex = 0;
                    }               

                    if (cbSort.SelectedIndex > 0)
                    {
                        orderList = orderList.Where(s => s.OrderDateTime.Date.Month.ToString().Contains(cbSort.SelectedIndex.ToString())).ToList();
                        calendar.Text = null;
                        calendar.SelectedDate = null;
                    }

                    tbProductsCount.Text = OrderProduct.productsAmount(orderList, null);
                    tbProductsPrice.Text = OrderProduct.productsCost(orderList, null);
                    tbCombosCount.Text = OrderCombo.combosAmount(orderList, null);
                    tbCombosPrice.Text = OrderCombo.combosCost(orderList, null);
                    break;

                case 2:
                    calendar.Visibility = Visibility.Hidden;
                    calendar.SelectedDate = null;
                    cbSort.IsEnabled = true;
                    spProds.Visibility = Visibility.Hidden;
                    spCombos.Visibility = Visibility.Hidden;
                    spSelectedProduct.Visibility = Visibility.Visible;

                    cbSort.ItemsSource = productsList;
                    cbSort.SelectedValuePath = "ProductID";

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
                        cbSort.SelectedIndex = 0;
                        spProds.Visibility = Visibility.Visible;
                        spSelectedProduct.Visibility= Visibility.Hidden;
                        tbProductsCount.Text = OrderProduct.productsAmount(orderList, null);
                        tbProductsPrice.Text = OrderProduct.productsCost(orderList, null);
                    } 
                    
                    break;

                case 3:
                    calendar.Visibility = Visibility.Hidden;
                    calendar.SelectedDate = null;
                    cbSort.IsEnabled = true;
                    spProds.Visibility = Visibility.Hidden;
                    spCombos.Visibility = Visibility.Hidden;
                    spSelectedProduct.Visibility = Visibility.Visible;

                    cbSort.ItemsSource = combosList;
                    cbSort.SelectedValuePath = "ComboID";

                    if (cbSort.SelectedIndex > 0)
                    {
                        Combo combo = DB.db.Combo.Where(p => p.ComboID.ToString() == cbSort.SelectedValue.ToString()).FirstOrDefault();
                        spSelectedProduct.Visibility = Visibility.Visible;
                        tbTitle.Text = combo.Title;
                        tbCount.Text = OrderCombo.combosAmount(null, combo);
                        tbCost.Text = OrderCombo.combosCost(null, combo);
                        
                    }
                    else
                    {
                        cbSort.SelectedIndex = 0;
                        spCombos.Visibility = Visibility.Visible;
                        spSelectedProduct.Visibility = Visibility.Hidden;
                        tbCombosCount.Text = OrderCombo.combosAmount(orderList, null);
                        tbCombosPrice.Text = OrderCombo.combosCost(orderList, null);
                    }

                    break;

                default:
                    calendar.Visibility = Visibility.Hidden;
                    calendar.SelectedDate = null;
                    cbSort.Visibility = Visibility.Hidden;
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
