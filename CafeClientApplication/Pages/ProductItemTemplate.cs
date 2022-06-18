using ClassLibraryCafe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace CafeClientApplication.Pages
{
    public class ProductItemTemplate
    {
        private Grid _gridProductItemTemplate = null;
        private Product _product = null;
        private TextBox _tbCount = null;

        public Grid GridProductItemTemplate => _gridProductItemTemplate;
        public Product product => _product;
        public TextBox TbCount => _tbCount;

        public ProductItemTemplate(Product product)
        {
            _product = product;
            CreateProductItemTemplate();
        }

        private void CreateProductItemTemplate()
        {
            _gridProductItemTemplate = new Grid();
            _gridProductItemTemplate.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(100) });
            _gridProductItemTemplate.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(110) });
            _gridProductItemTemplate.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(450) });
            _gridProductItemTemplate.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(100) });
            _gridProductItemTemplate.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(200) });

            Border brdImage = new Border();
            //brdImage.Style = Styles.brdImage;
            brdImage.Width = 100;
            brdImage.Height = 100;
            brdImage.Margin = new Thickness(5);
            brdImage.CornerRadius = new CornerRadius(5);
            brdImage.BorderBrush = Brushes.Black;
            brdImage.BorderThickness = new Thickness(2);
            brdImage.Background = GetImageBrush();
            Grid.SetColumn(brdImage, 1);
            _gridProductItemTemplate.Children.Add(brdImage);

            StackPanel spProduct = new StackPanel();
            spProduct.VerticalAlignment = VerticalAlignment.Center;
            spProduct.HorizontalAlignment = HorizontalAlignment.Center;
            spProduct.Orientation = Orientation.Vertical;
            {
                TextBlock tblName = new TextBlock();
                //tblName.Style = Styles.tbAll;
                tblName.Text = _product.Title;
                spProduct.Children.Add(tblName);

                TextBlock tbDesc = new TextBlock();
                //tbDesc.Style = Styles.tbAll;
                tbDesc.Text = _product.Description;
                spProduct.Children.Add(tbDesc);
            }
            Grid.SetColumn(spProduct, 2);
            _gridProductItemTemplate.Children.Add(spProduct);

            Grid gridData = new Grid();
            gridData.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(40) });
            gridData.RowDefinitions.Add(new RowDefinition());
            {
                TextBlock tbCount = new TextBlock();
                //tbCount.Style = Styles.tbAll;
                tbCount.Text = "Кол-во";
                //tbCount.HorizontalAlignment = HorizontalAlignment.Center;
                Grid.SetRow(tbCount, 0);
                gridData.Children.Add(tbCount);

                _tbCount = new TextBox();
                //_tbCount.Style = Styles.tbStyle;
                _tbCount.MaxLength = 5;
                _tbCount.Text = "1";
                //_tbCount.Height = 50;
                _tbCount.PreviewTextInput += _tbCount_PreviewTextInput;
                Grid.SetRow(_tbCount, 1);
                gridData.Children.Add(_tbCount);
            }
            Grid.SetColumn(gridData, 3);
            _gridProductItemTemplate.Children.Add(gridData);

            Grid gridCost = new Grid();
            gridCost.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(40) });
            gridCost.RowDefinitions.Add(new RowDefinition());
            {
                TextBlock tblUnit = new TextBlock();
                //tblUnit.Style = Styles.tbAll;
                tblUnit.Text = "Стоимость:";
                //tblUnit.HorizontalAlignment = HorizontalAlignment.Center;
                Grid.SetRow(tblUnit, 0);
                gridCost.Children.Add(tblUnit);

                StackPanel spCost = new StackPanel();
                spCost.VerticalAlignment = VerticalAlignment.Center;
                spCost.HorizontalAlignment = HorizontalAlignment.Center;
                spCost.Orientation = Orientation.Horizontal;
                {
                    TextBlock tblUnit1 = new TextBlock();
                    //tblUnit1.Style = Styles.tbAll;
                    tblUnit1.Text = _product.Cost.ToString();
                    //tblUnit1.HorizontalAlignment = HorizontalAlignment.Center;
                    Grid.SetRow(tblUnit1, 1);
                    spCost.Children.Add(tblUnit1);

                    TextBlock tblUnit2 = new TextBlock();
                    //tblUnit2.Style = Styles.tbAll;
                    tblUnit2.Text = "₽";
                    //tblUnit2.HorizontalAlignment = HorizontalAlignment.Center;
                    spCost.Children.Add(tblUnit2);
                }
                Grid.SetRow(spCost, 1);
                gridCost.Children.Add(spCost);
            }
            Grid.SetColumn(gridCost, 4);
            _gridProductItemTemplate.Children.Add(gridCost);
        }

        private ImageBrush GetImageBrush()
        {
            ImageBrush brush = new ImageBrush();
            BitmapImage img = new BitmapImage(new Uri(_product.ValidImage, UriKind.Relative));

            brush.ImageSource = img;

            return brush;
        }

        private void _tbCount_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            char letter = e.Text[0];

            if (!char.IsDigit(letter))
                e.Handled = true;
        }
    }
}
