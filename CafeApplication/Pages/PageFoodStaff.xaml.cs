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
    /// Логика взаимодействия для PageFoodStaff.xaml
    /// </summary>
    public partial class PageFoodStaff : Page
    {
        public PageFoodStaff()
        {
            InitializeComponent();
            PageStartUp();            
        }

        private void PageStartUp()
        {
            if (Properties.Settings.Default.globalRole != "manager")
            {
                lvFoodStaff.MouseDoubleClick -= lvFoodStaff_MouseDoubleClick;
                lvFoodStaff.ToolTip = null;
                btnAdd.Visibility = Visibility.Hidden;
            }

            var currentFoodStaff = DB.db.FoodStaff.ToList();
            var sort = new List<string>();

            sort.Add("Сортировка");
            sort.Add("По названию, от А до Я");
            sort.Add("По названию, от Я до А"); ;
            sort.Add("По возрастанию остатков"); ;
            sort.Add("По убыванию остатков"); ;

            lvFoodStaff.ItemsSource = currentFoodStaff;

            cbSort.ItemsSource = sort;

            cbSort.SelectedIndex = 0;
        }

        void UpdateLvItems()
        {
            var currentFoodStuff = DB.db.FoodStaff.ToList();

            if (cbSort.SelectedIndex > 0)
            {
                switch (cbSort.SelectedIndex)
                {
                    case 1:
                        currentFoodStuff = currentFoodStuff.OrderBy(p => p.Title).ToList();
                        break;
                    case 2:
                        currentFoodStuff = currentFoodStuff.OrderByDescending(p => p.Title).ToList();
                        break;
                    case 3:
                        currentFoodStuff = currentFoodStuff.OrderBy(p => p.CountInStock).ToList();
                        break;
                    case 4:
                        currentFoodStuff = currentFoodStuff.OrderByDescending(p => p.CountInStock).ToList();
                        break;
                }
            }
            
            if (tbFinder.Text != null)
                currentFoodStuff = currentFoodStuff.Where(s => s.Title.ToLower().Contains(tbFinder.Text)).ToList();

            lvFoodStaff.ItemsSource = currentFoodStuff;

            tbRecordsCount.Text = lvFoodStaff.Items.Count.ToString();
            tbRecordsCountAll.Text = DB.db.FoodStaff.Count().ToString();
        }

        private void tbFinder_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateLvItems();
        }

        private void cbSort_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateLvItems();
        }
                
        private void lvFoodStaff_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            DB.db.ChangeTracker.Entries().ToList().ForEach(a => a.Reload());
            lvFoodStaff.ItemsSource = DB.db.FoodStaff.ToList();
        }

        private void lvFoodStaff_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Manager.mainFrame.Navigate(new PageAddEditFoodStaff(lvFoodStaff.SelectedItem as FoodStaff));
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            Manager.mainFrame.Navigate(new PageAddEditFoodStaff(new FoodStaff()));
        }
    }
}
