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
            cbFilter.Visibility = Visibility.Hidden;

            var types = new List<string>();
            types.Add("Тип отчета");
            types.Add("Продажи");
            types.Add("По блюдам");
            types.Add("По наборам");
            //types.Add("По сотрудникам");

            cbTypes.ItemsSource = types;
            cbTypes.SelectedIndex = 0;

            cbSort.SelectedIndex = 0;

            cbFilter.SelectedIndex = 0;

            mounths.Add("Месяца");
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

            productsList.Insert(0, new Product { Title = "Блюда" });
            combosList.Insert(0, new Combo { Title = "Наборы" });
        }

        private void UpdateList()
        {
            orderList = DB.db.Order.ToList();

            switch (cbTypes.SelectedIndex)
            {
                case 1:
                    calendar.Visibility = Visibility.Visible;
                    cbSort.Visibility = Visibility.Visible;
                    spProds.Visibility = Visibility.Visible;
                    spCombos.Visibility = Visibility.Visible;
                    spSelected.Visibility = Visibility.Hidden;
                    cbFilter.Visibility = Visibility.Hidden;
                    brdBack.Visibility = Visibility.Visible;

                    cbSort.ItemsSource = mounths;

                    if (calendar.SelectedDate != null)
                    {
                        orderList = orderList.Where(s => s.OrderDateTime.Date.ToString().Contains(calendar.SelectedDate.ToString())).ToList();
                        cbSort.SelectedIndex = 0;
                    }

                    if (cbSort.SelectedIndex > 0)
                    {
                        orderList = orderList.Where(s => s.OrderDateTime.Date.Month.ToString().Contains(cbSort.SelectedIndex.ToString())).ToList();
                    }

                    tbProductsCount.Text = OrderProduct.productsAmount(orderList, null);
                    tbProductsPrice.Text = OrderProduct.productsCost(orderList, null);
                    tbCombosCount.Text = OrderCombo.combosAmount(orderList, null);
                    tbCombosPrice.Text = OrderCombo.combosCost(orderList, null);
                    break;

                case 2:
                    calendar.Visibility = Visibility.Hidden;
                    calendar.SelectedDate = null;
                    cbSort.Visibility = Visibility.Hidden;
                    spProds.Visibility = Visibility.Hidden;
                    spCombos.Visibility = Visibility.Hidden;
                    spSelected.Visibility = Visibility.Visible;
                    cbFilter.Visibility = Visibility.Visible;
                    brdBack.Visibility = Visibility.Visible;

                    cbFilter.ItemsSource = productsList;
                    cbFilter.SelectedValuePath = "ProductID";

                    if (cbFilter.SelectedIndex > 0)
                    {
                        Product product = DB.db.Product.Where(p => p.ProductID.ToString() == cbFilter.SelectedValue.ToString()).FirstOrDefault();
                        spSelected.Visibility = Visibility.Visible;
                        tbTitle.Text = product.Title;
                        tbCount.Text = OrderProduct.productsAmount(null, product);
                        tbCost.Text = OrderProduct.productsCost(null, product);
                    }
                    else
                    {
                        cbFilter.SelectedIndex = 0;
                        tbTitle.Text = "Всего блюд";
                        tbCount.Text = OrderProduct.productsAmount(orderList, null);
                        tbCost.Text = OrderProduct.productsCost(orderList, null);
                    } 
                    
                    break;

                case 3:
                    calendar.Visibility = Visibility.Hidden;
                    calendar.SelectedDate = null;
                    cbSort.Visibility = Visibility.Hidden;
                    spProds.Visibility = Visibility.Hidden;
                    spCombos.Visibility = Visibility.Hidden;
                    spSelected.Visibility = Visibility.Visible;
                    cbFilter.Visibility = Visibility.Visible;
                    brdBack.Visibility = Visibility.Visible;

                    cbFilter.ItemsSource = combosList;
                    cbFilter.SelectedValuePath = "ComboID";

                    if (cbFilter.SelectedIndex > 0)
                    {
                        Combo combo = DB.db.Combo.Where(p => p.ComboID.ToString() == cbFilter.SelectedValue.ToString()).FirstOrDefault();
                        spSelected.Visibility = Visibility.Visible;
                        tbTitle.Text = combo.Title;
                        tbCount.Text = OrderCombo.combosAmount(null, combo);
                        tbCost.Text = OrderCombo.combosCost(null, combo);
                    }
                    else
                    {
                        cbFilter.SelectedIndex = 0;
                        tbTitle.Text = "Всего наборов";
                        tbCount.Text = OrderCombo.combosAmount(orderList, null);
                        tbCost.Text = OrderCombo.combosCost(orderList, null);
                    }

                    break;

                default:
                    calendar.Visibility = Visibility.Hidden;
                    calendar.SelectedDate = null;
                    cbSort.Visibility = Visibility.Hidden;
                    cbSort.Text = null;
                    cbFilter.Visibility = Visibility.Hidden;
                    cbFilter.Text = null;
                    spProds.Visibility = Visibility.Hidden;
                    spCombos.Visibility = Visibility.Hidden;
                    spSelected.Visibility = Visibility.Hidden;
                    brdBack.Visibility = Visibility.Hidden;
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
            cbFilter.SelectedIndex = 0;
            UpdateList();            
        }

        private void calendar_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
                calendar.SelectedDate = null;
        }

        private void cbFilter_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateList();
        }
    }
}
