using ClassLibraryCafe;
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

namespace CafeClientApplication.Pages
{
    /// <summary>
    /// Логика взаимодействия для PageProducts.xaml
    /// </summary>
    public partial class PageProducts : Page
    {
        public PageProducts()
        {
            InitializeComponent();
            PageStartUp();
        }
        /// <summary>
        /// Подготовка данных для элементов страницы
        /// </summary>
        private void PageStartUp()
        {
            var currentProducts = DB.db.Product.ToList();
            var sort = new List<string>();
            var filter = DB.db.ProductType.ToList();

            sort.Add("Сортировка");
            sort.Add("По названию, от А до Я");
            sort.Add("По названию, от Я до А"); ;
            sort.Add("По возрастанию стоимости"); ;
            sort.Add("По убыванию стоимости");

            lvProducts.ItemsSource = currentProducts;

            filter.Insert(0, new ProductType
            {
                Title = "Все типы"
            });

            cbFilter.DisplayMemberPath = "Title";
            cbFilter.SelectedValuePath = "ProductTypeID";

            cbSort.ItemsSource = sort;
            cbFilter.ItemsSource = filter;

            cbSort.SelectedIndex = 0;
            cbFilter.SelectedIndex = 0;
        }
        /// <summary>
        /// Обновление списка выводимых блюд при поиске, сортировке и фильтрации
        /// </summary>
        private void UpdateLvItems()
        {
            var currentProducts = DB.db.Product.ToList();
            //Сортировка данных
            if (cbSort.SelectedIndex > 0)
            {
                switch (cbSort.SelectedIndex)
                {
                    case 1:
                        currentProducts = currentProducts.OrderBy(p => p.Title).ToList();
                        break;
                    case 2:
                        currentProducts = currentProducts.OrderByDescending(p => p.Title).ToList();
                        break;
                    case 3:
                        currentProducts = currentProducts.OrderBy(p => p.Cost).ToList();
                        break;
                    case 4:
                        currentProducts = currentProducts.OrderByDescending(p => p.Cost).ToList();
                        break;
                }
            }
            //Фильтрация данных
            if (cbFilter.SelectedIndex > 0)
                currentProducts = currentProducts.Where(s => s.ProductTypeID == int.Parse(cbFilter.SelectedValue.ToString())).ToList();
            //Поиск
            if (tbFinder.Text != null)
                currentProducts = currentProducts.Where(s => s.Title.ToLower().Contains(tbFinder.Text.ToLower())).ToList();
            //Вывод данных
            lvProducts.ItemsSource = currentProducts;
            //Количество выведенных записей
            tbRecordsCount.Text = lvProducts.Items.Count.ToString();
            tbRecordsCountAll.Text = DB.db.Product.Count().ToString();

            if (lvProducts.Items.Count == 0)
            {
                lvProducts.Visibility = Visibility.Hidden;
                tbAvailable.Visibility = Visibility.Visible;
            }
            else
            {
                lvProducts.Visibility = Visibility.Visible;
                tbAvailable.Visibility = Visibility.Hidden;
            }
        }

        private void tbFinder_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateLvItems();
        }

        private void cbSort_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateLvItems();
        }

        private void cbFilter_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateLvItems();
        }

        private void lvProducts_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            try
            {
                DB.db.ChangeTracker.Entries().ToList().ForEach(a => a.Reload());
                lvProducts.ItemsSource = DB.db.Product.ToList();
            }
            catch (Exception)
            {

            }
        }
    }
}
