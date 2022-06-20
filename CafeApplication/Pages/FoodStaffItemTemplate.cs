using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.IO;

namespace CafeApplication
{
    public class FoodStaffItemTemplate
    {
        private Grid _gridFoodStafftItemTemplate = null;
        private FoodStaff _foodStaff = null;
        private TextBox _tbCount = null;

        public Grid GridFoodStaffItemTemplate => _gridFoodStafftItemTemplate;
        public FoodStaff foodStaff => _foodStaff;
        public TextBox TbCount => _tbCount;

        public FoodStaffItemTemplate(FoodStaff foodStaff)
        {
            _foodStaff = foodStaff;
            CreateGridFoodStaffItemTemplate();
        }

        private void CreateGridFoodStaffItemTemplate()
        {
            _gridFoodStafftItemTemplate = new Grid();
            _gridFoodStafftItemTemplate.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(100) });
            _gridFoodStafftItemTemplate.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(110) });
            _gridFoodStafftItemTemplate.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(450) });
            _gridFoodStafftItemTemplate.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(100) });
            _gridFoodStafftItemTemplate.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(200) });

            Border brdImage = new Border();
            brdImage.Style = Styles.brdImage;
            brdImage.Background = GetImageBrush();
            Grid.SetColumn(brdImage, 1);
            _gridFoodStafftItemTemplate.Children.Add(brdImage);

            StackPanel spFoodStaff = new StackPanel();
            spFoodStaff.VerticalAlignment = VerticalAlignment.Center;
            spFoodStaff.HorizontalAlignment = HorizontalAlignment.Left;
            spFoodStaff.Orientation = Orientation.Vertical;
            {
                TextBlock tblName = new TextBlock();
                tblName.Style = Styles.tbAll;
                tblName.FontWeight = FontWeights.Bold;
                tblName.Text = _foodStaff.Title;
                spFoodStaff.Children.Add(tblName);

                TextBlock tbDesc = new TextBlock();
                tbDesc.Style = Styles.tbAll;
                tbDesc.Text = _foodStaff.Description;
                spFoodStaff.Children.Add(tbDesc);
            }
            Grid.SetColumn(spFoodStaff, 2);
            _gridFoodStafftItemTemplate.Children.Add(spFoodStaff);

            Grid gridData = new Grid();
            gridData.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(40) });
            gridData.RowDefinitions.Add(new RowDefinition());
            {
                TextBlock tblCount = new TextBlock();
                tblCount.Text = "Кол-во";
                tblCount.Style = Styles.tbAll;
                Grid.SetRow(tblCount, 0);
                gridData.Children.Add(tblCount);

                _tbCount = new TextBox();
                _tbCount.MaxLength = 5;
                _tbCount.Text = "1";
                _tbCount.Style = Styles.tbStyle;
                _tbCount.PreviewTextInput += _tbCount_PreviewTextInput;
                Grid.SetRow(_tbCount, 1);
                gridData.Children.Add(_tbCount);
            }
            Grid.SetColumn(gridData, 3);
            _gridFoodStafftItemTemplate.Children.Add(gridData);

            Grid gridUnit = new Grid();
            gridUnit.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(40) });
            gridUnit.RowDefinitions.Add(new RowDefinition());
            {
                TextBlock tblUnit = new TextBlock();
                tblUnit.Text = "Ед. измереня: ";
                tblUnit.Style = Styles.tbAll;
                Grid.SetRow(tblUnit, 0);
                gridUnit.Children.Add(tblUnit);

                TextBlock tblUnit1 = new TextBlock();
                tblUnit1.Text = _foodStaff.Unit.ToString();
                tblUnit1.Style = Styles.tbAll;
                Grid.SetRow(tblUnit1, 1);
                gridUnit.Children.Add(tblUnit1);
            }
            Grid.SetColumn(gridUnit, 4);
            _gridFoodStafftItemTemplate.Children.Add(gridUnit);
        }

        private ImageBrush GetImageBrush()
        {
            ImageBrush brush = new ImageBrush();
            BitmapImage img = new BitmapImage(new Uri(foodStaff.ValidImage, UriKind.Relative));

            brush.ImageSource = img;

            return brush;            
        }

        private void _tbCount_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            string chars = "1234567890";

            if (!chars.Contains(e.Text[0]))
                e.Handled = true;
        }
    }
}
