using System;
using System.Collections.Generic;
using System.IO;
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
    /// Логика взаимодействия для WindowImages.xaml
    /// </summary>
    public partial class WindowImages : Window
    {
        public string imageUrl { get; private set; }

        public WindowImages(string type)
        {
            InitializeComponent();
            ShowImages(type);
        }

        private void ShowImages(string type)
        {
            const int rowLength = 5;
            string[] imgs = null;
            Style imgStyle = Application.Current.FindResource("imgEffect") as Style;

            switch (type)
            {
                case "FoodStaff":
                    imgs = Directory.GetFiles("../../Images/FoodStaff");
                    break;
                case "combo":
                    imgs = Directory.GetFiles("../../Images/Products");
                    break;
                case "Combo":
                    imgs = Directory.GetFiles("../../Images/Combos");
                    break;
            }

            var column = new StackPanel
            {
                Orientation = Orientation.Vertical,
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center
            };

            for (int i = 0; i <= imgs.Length / rowLength; i++)
            {
                var row = new StackPanel
                {
                    Orientation = Orientation.Horizontal,
                    Margin = new Thickness(5, 5, 5, 5),
                };

                for (int j = rowLength * i; j < (rowLength * i) + rowLength; j++)
                {
                    if (j == imgs.Length)
                        break;

                    var image = new Image
                    {
                        Width = 150,
                        Height = 150,
                        Margin = new Thickness(5, 5, 5, 5),
                        Source = new BitmapImage(new Uri(imgs[j], UriKind.Relative)),
                        Cursor = Cursors.Hand,
                        Tag = imgs[j],
                    };

                    image.MouseLeftButtonDown += Image_MouseLeftButtonDown;
                    image.Style = imgStyle;

                    row.Children.Add(image);
                }
                gImages.Children.Add(row);
            }
            gImages.Children.Add(column);
        }

        private void Image_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var image = (Image)sender;
            imageUrl = image.Tag.ToString();
            DialogResult = true;
            Close();
        }
    }
}
