﻿using System;
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
    /// Логика взаимодействия для PageOrders.xaml
    /// </summary>
    public partial class PageOrders : Page
    {
        public PageOrders()
        {
            InitializeComponent();

            var currentOrders = DB.db.Order.ToList();
            var sort = new List<string>();
            var staff = DB.db.Staff.ToList();

            sort.Add("Сортировка");
            sort.Add("По названию, от А до Я");
            sort.Add("По названию, от Я до А"); ;
            sort.Add("По возрастанию стоимости"); ;
            sort.Add("По убыванию стоимости"); ;

            lvOrders.ItemsSource = currentOrders;

            staff.Insert(0, new Staff
            {
                LastName = "Все сотрудники"
            });

            cbStaff.SelectedValuePath = "StaffID";

            //cbSort.ItemsSource = sort;
            cbStaff.ItemsSource = staff;

            //cbSort.SelectedIndex = 0;
            cbStaff.SelectedIndex = 0;

            if (Properties.Settings.Default.globalRole == "admin")
            {
                lvOrders.ToolTip = null;
                lvOrders.MouseDoubleClick -= lvOrders_MouseDoubleClick;
                btnAdd.Visibility = Visibility.Hidden;
            }
                
        }

        void UpdateLvItems()
        {
            var currentOrders = DB.db.Order.ToList();

            //if (cbSort.SelectedIndex > 0)
            //{
            //    switch (cbSort.SelectedIndex)
            //    {
            //        case 1:
            //            currentOrders = currentOrders.OrderBy(p => p.Title).ToList();
            //            break;
            //        case 2:
            //            currentOrders = currentOrders.OrderByDescending(p => p.Title).ToList();
            //            break;
            //        case 3:
            //            currentOrders = currentOrders.OrderBy(p => p.Cost).ToList();
            //            break;
            //        case 4:
            //            currentOrders = currentOrders.OrderByDescending(p => p.Cost).ToList();
            //            break;

            //    }
            //}

            if (cbStaff.SelectedIndex > 0)
            {
                currentOrders = currentOrders.Where(s => s.StaffID == int.Parse(cbStaff.SelectedValue.ToString())).ToList();
            }

            if (tbFinder.Text != null)
            {
                currentOrders = currentOrders.Where(s => s.OrderDateTime.Date.ToString().Contains(calendar.SelectedDate.ToString())).ToList();
            }

            lvOrders.ItemsSource = currentOrders;

            tbRecordsCount.Text = lvOrders.Items.Count.ToString();
            tbRecordsCountAll.Text = DB.db.Order.Count().ToString();
        }

        private void tbFinder_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateLvItems();
        }

        private void cbSort_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateLvItems();
        }

        private void lvOrders_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            DB.db.ChangeTracker.Entries().ToList().ForEach(a => a.Reload());
            lvOrders.ItemsSource = DB.db.Order.ToList();
        }

        private void cbStaff_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateLvItems();
        }

        private void calendar_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateLvItems();
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            Manager.mainFrame.Navigate(new PageAddEditOrder(new Order()));
        }

        private void lvOrders_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {            
            Manager.mainFrame.Navigate(new PageAddEditOrder(lvOrders.SelectedItem as Order));            
        }
    }
}
