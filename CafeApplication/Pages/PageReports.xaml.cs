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
    /// Логика взаимодействия для PageReports.xaml
    /// </summary>
    public partial class PageReports : Page
    {
        public PageReports()
        {
            InitializeComponent();
        }

        private void btnReport1_Click(object sender, RoutedEventArgs e)
        {
            Manager.mainFrame.Navigate(new PageReportSales());
        }

        private void btnReport2_Click(object sender, RoutedEventArgs e)
        {
            //Manager.mainFrame.Navigate();
        }
    }
}
