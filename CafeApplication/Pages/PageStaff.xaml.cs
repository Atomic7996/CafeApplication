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
    /// Логика взаимодействия для PageStaff.xaml
    /// </summary>
    public partial class PageStaff : Page
    {
        public PageStaff()
        {
            InitializeComponent();
            PageStartUp();            
        }

        private void PageStartUp()
        {
            var currentStaff = DB.db.Staff.ToList();
            var sort = new List<string>();
            var filter = DB.db.StaffRole.ToList();

            sort.Add("Сортировка");
            sort.Add("По фамилии, от А до Я");
            sort.Add("По фамилии, от Я до А"); ;

            lvStaff.ItemsSource = currentStaff;

            filter.Insert(0, new StaffRole
            {
                Title = "Роли"
            });

            cbFilter.DisplayMemberPath = "Title";
            cbFilter.SelectedValuePath = "RoleID";

            cbSort.ItemsSource = sort;
            cbFilter.ItemsSource = filter;

            cbSort.SelectedIndex = 0;
            cbFilter.SelectedIndex = 0;
        }

        void UpdateLvItems()
        {
            var currentStaff = DB.db.Staff.ToList();

            if (cbSort.SelectedIndex > 0)
            {
                switch (cbSort.SelectedIndex)
                {
                    case 1:
                        currentStaff = currentStaff.OrderBy(p => p.LastName).ToList();
                        break;
                    case 2:
                        currentStaff = currentStaff.OrderByDescending(p => p.LastName).ToList();
                        break;
                }
            }

            if (cbFilter.SelectedIndex > 0)
                currentStaff = currentStaff.Where(s => s.RoleID == int.Parse(cbFilter.SelectedValue.ToString())).ToList();

            if (tbFinder.Text != null)
                currentStaff = currentStaff.Where(s => s.LastName.ToLower().Contains(tbFinder.Text.ToLower())).ToList();

            lvStaff.ItemsSource = currentStaff;

            tbRecordsCount.Text = lvStaff.Items.Count.ToString();
            tbRecordsCountAll.Text = DB.db.Staff.Count().ToString();

            if (lvStaff.Items.Count == 0)
            {
                lvStaff.Visibility = Visibility.Hidden;
                tbAvailable.Visibility = Visibility.Visible;
            }
            else
            {
                lvStaff.Visibility = Visibility.Visible;
                tbAvailable.Visibility = Visibility.Hidden;
            }
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            Manager.mainFrame.Navigate(new PageRegistration(new Staff()));
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

        private void lvStaff_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            try
            {
                DB.db.ChangeTracker.Entries().ToList().ForEach(a => a.Reload());
                UpdateLvItems();
            }
            catch (Exception)
            {
            }            
        }

        private void lvStaff_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Manager.mainFrame.Navigate(new PageRegistration(lvStaff.SelectedItem as Staff));
        }
    }
}
