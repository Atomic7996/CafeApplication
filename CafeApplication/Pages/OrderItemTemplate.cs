using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace CafeApplication
{
    public class OrderItemTemplate
    {
        private Grid _gridOrderItemTemplate = null;
        private Order _order = null;

        public Grid GridOrderItemTemplate => _gridOrderItemTemplate;
        public Order order => _order;

        public OrderItemTemplate(Order order)
        {
            _order = order;
            CreateOrderItemTemplate();
        }

        private void CreateOrderItemTemplate()
        {
            _gridOrderItemTemplate = new Grid();
            _gridOrderItemTemplate.ColumnDefinitions.Add(new ColumnDefinition());
            _gridOrderItemTemplate.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(450) });
            _gridOrderItemTemplate.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(450) });
            _gridOrderItemTemplate.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(150) });
            _gridOrderItemTemplate.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(150) });
            _gridOrderItemTemplate.ColumnDefinitions.Add(new ColumnDefinition());

            Grid gridData = new Grid();
            gridData.ColumnDefinitions.Add(new ColumnDefinition());
            gridData.ColumnDefinitions.Add(new ColumnDefinition());
            gridData.ColumnDefinitions.Add(new ColumnDefinition());
            {
                StackPanel spNameDate = new StackPanel();
                spNameDate.VerticalAlignment = VerticalAlignment.Center;
                spNameDate.HorizontalAlignment = HorizontalAlignment.Left;
                spNameDate.Orientation = Orientation.Vertical;
                {
                    TextBlock tbName = new TextBlock();
                    tbName.Style = Styles.tbAll;
                    tbName.FontWeight = FontWeights.Bold;
                    if (_order.StaffID != null)
                        tbName.Text = _order.Staff.ToString();
                    if (_order.UserID != null)
                        tbName.Text = _order.User.ToString();

                    spNameDate.Children.Add(tbName);

                    TextBlock tbDate = new TextBlock();
                    tbDate.Style = Styles.tbAll;
                    tbDate.Text = _order.OrderDateTime.ToString();
                    spNameDate.Children.Add(tbDate);
                }
                Grid.SetColumn(spNameDate, 1);
                _gridOrderItemTemplate.Children.Add(spNameDate);

                StackPanel spStructure = new StackPanel();
                spStructure.VerticalAlignment = VerticalAlignment.Center;
                spStructure.HorizontalAlignment = HorizontalAlignment.Left;
                spStructure.Orientation = Orientation.Vertical;
                {
                    TextBlock tbLabel = new TextBlock();
                    tbLabel.Style = Styles.tbAll;
                    tbLabel.FontWeight = FontWeights.Bold;
                    tbLabel.Text = "Состав:";
                    spStructure.Children.Add(tbLabel);

                    TextBlock tbStructure = new TextBlock();
                    tbStructure.Style = Styles.tbAll;
                    tbStructure.Text = _order.ValidList;
                    spStructure.Children.Add(tbStructure);
                }
                Grid.SetColumn(spStructure, 2);
                _gridOrderItemTemplate.Children.Add(spStructure);

                StackPanel spCost = new StackPanel();
                spCost.VerticalAlignment = VerticalAlignment.Center;
                spCost.HorizontalAlignment = HorizontalAlignment.Left;
                spCost.Orientation = Orientation.Vertical;
                {
                    TextBlock tbLabelc = new TextBlock();
                    tbLabelc.Style = Styles.tbAll;
                    tbLabelc.FontWeight = FontWeights.Bold;
                    tbLabelc.Text = "Стоимость:";
                    spCost.Children.Add(tbLabelc);

                    StackPanel spInnerCost = new StackPanel();
                    spInnerCost.VerticalAlignment = VerticalAlignment.Center;
                    spInnerCost.HorizontalAlignment = HorizontalAlignment.Left;
                    spInnerCost.Orientation = Orientation.Horizontal;
                    {
                        TextBlock tbCost = new TextBlock();
                        tbCost.Style = Styles.tbAll;
                        tbCost.Text = _order.SummaryCost.ToString();
                        spInnerCost.Children.Add(tbCost);

                        TextBlock tbCost2 = new TextBlock();
                        tbCost2.Style = Styles.tbAll;
                        tbCost2.Text = " ₽";
                        spInnerCost.Children.Add(tbCost2);
                    }
                    spCost.Children.Add(spInnerCost);
                }
                Grid.SetColumn(spCost, 3);
                _gridOrderItemTemplate.Children.Add(spCost);
            }
            Grid.SetColumn(gridData, 0);
            _gridOrderItemTemplate.Children.Add(gridData);
        }
    }
}
