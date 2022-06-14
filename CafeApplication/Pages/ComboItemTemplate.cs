using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace CafeApplication
{
    public class ComboItemTemplate
    {
        private Grid _gridComboItemTemplate = null;
        private Combo _combo = null;
        private TextBox _tbCount = null;

        public Grid GridComboItemTemplate => _gridComboItemTemplate;
        public Combo combo => _combo;
        public TextBox TbCount => _tbCount;

        public ComboItemTemplate(Combo combo)
        {
            _combo = combo;
            CreateCombotItemTemplate();
        }

        private void CreateCombotItemTemplate()
        {
            _gridComboItemTemplate = new Grid();
            _gridComboItemTemplate.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(64) });
            _gridComboItemTemplate.ColumnDefinitions.Add(new ColumnDefinition());
            _gridComboItemTemplate.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(90) });
            _gridComboItemTemplate.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(200) });

            Border brdImage = new Border();
            brdImage.Width = 64;
            brdImage.Height = 64;
            brdImage.Background = GetImageBrush();
            Grid.SetColumn(brdImage, 0);
            _gridComboItemTemplate.Children.Add(brdImage);

            StackPanel spCombo = new StackPanel();
            spCombo.VerticalAlignment = VerticalAlignment.Center;
            spCombo.HorizontalAlignment = HorizontalAlignment.Center;
            spCombo.Orientation = Orientation.Vertical;
            {
                TextBlock tblName = new TextBlock();
                tblName.FontSize = 20;
                tblName.Text = _combo.Title;
                spCombo.Children.Add(tblName);

                TextBlock tbDesc = new TextBlock();
                tbDesc.FontSize = 14;
                tbDesc.Text = _combo.Description;
                spCombo.Children.Add(tbDesc);
            }
            Grid.SetColumn(spCombo, 1);
            _gridComboItemTemplate.Children.Add(spCombo);

            Grid gridData = new Grid();
            gridData.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(40) });
            gridData.RowDefinitions.Add(new RowDefinition());
            {
                TextBlock tblCount = new TextBlock();
                tblCount.Text = "Кол-во";
                tblCount.HorizontalAlignment = HorizontalAlignment.Center;
                Grid.SetRow(tblCount, 0);
                gridData.Children.Add(tblCount);

                _tbCount = new TextBox();
                _tbCount.MaxLength = 5;
                _tbCount.Text = "1";
                _tbCount.Height = 40;
                _tbCount.PreviewTextInput += _tbCount_PreviewTextInput;
                Grid.SetRow(_tbCount, 1);
                gridData.Children.Add(_tbCount);
            }
            Grid.SetColumn(gridData, 2);
            _gridComboItemTemplate.Children.Add(gridData);

        }

        private ImageBrush GetImageBrush()
        {
            ImageBrush brush = new ImageBrush();
            BitmapImage img = new BitmapImage(new Uri(_combo.ValidImage, UriKind.Relative));

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
