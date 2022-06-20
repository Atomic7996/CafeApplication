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
            _gridComboItemTemplate.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(100) });
            _gridComboItemTemplate.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(110) });
            _gridComboItemTemplate.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(450) });
            _gridComboItemTemplate.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(100) });
            _gridComboItemTemplate.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(200) });

            Border brdImage = new Border();
            brdImage.Style = Styles.brdImage;
            brdImage.Background = GetImageBrush();
            Grid.SetColumn(brdImage, 1);
            _gridComboItemTemplate.Children.Add(brdImage);

            StackPanel spCombo = new StackPanel();
            spCombo.VerticalAlignment = VerticalAlignment.Center;
            spCombo.HorizontalAlignment = HorizontalAlignment.Left;
            spCombo.Orientation = Orientation.Vertical;
            {
                TextBlock tblName = new TextBlock();
                tblName.Style = Styles.tbAll;
                tblName.FontWeight = FontWeights.Bold;
                tblName.Text = _combo.Title;
                spCombo.Children.Add(tblName);

                TextBlock tbDesc = new TextBlock();
                tbDesc.Style = Styles.tbAll;
                tbDesc.Text = _combo.Description;
                spCombo.Children.Add(tbDesc);
            }
            Grid.SetColumn(spCombo, 2);
            _gridComboItemTemplate.Children.Add(spCombo);

            Grid gridData = new Grid();
            gridData.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(40) });
            gridData.RowDefinitions.Add(new RowDefinition());
            {
                TextBlock tblCount = new TextBlock();
                tblCount.Style = Styles.tbAll;
                tblCount.Text = "Кол-во";
                Grid.SetRow(tblCount, 0);
                gridData.Children.Add(tblCount);

                _tbCount = new TextBox();
                _tbCount.Style = Styles.tbStyle;
                _tbCount.MaxLength = 5;
                _tbCount.Text = "1";
                _tbCount.PreviewTextInput += _tbCount_PreviewTextInput;
                Grid.SetRow(_tbCount, 1);
                gridData.Children.Add(_tbCount);
            }
            Grid.SetColumn(gridData, 3);
            _gridComboItemTemplate.Children.Add(gridData);

            Grid gridCost = new Grid();
            gridCost.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(40) });
            gridCost.RowDefinitions.Add(new RowDefinition());
            {
                TextBlock tblUnit = new TextBlock();
                tblUnit.Style = Styles.tbAll;
                tblUnit.Text = "Стоимость:";
                Grid.SetRow(tblUnit, 0);
                gridCost.Children.Add(tblUnit);

                StackPanel spCost = new StackPanel();
                spCost.VerticalAlignment = VerticalAlignment.Center;
                spCost.HorizontalAlignment = HorizontalAlignment.Left;
                spCost.Orientation = Orientation.Horizontal;
                {
                    TextBlock tblUnit1 = new TextBlock();
                    tblUnit1.Style = Styles.tbAll;
                    tblUnit1.Text = _combo.Cost.ToString();
                    Grid.SetRow(tblUnit1, 1);
                    spCost.Children.Add(tblUnit1);

                    TextBlock tblUnit2 = new TextBlock();
                    tblUnit2.Style = Styles.tbAll;
                    tblUnit2.Text = "₽";
                    spCost.Children.Add(tblUnit2);
                }
                Grid.SetRow(spCost, 1);
                gridCost.Children.Add(spCost);
            }
            Grid.SetColumn(gridCost, 4);
            _gridComboItemTemplate.Children.Add(gridCost);
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
