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
            _gridOrderItemTemplate.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(150) });
            _gridOrderItemTemplate.ColumnDefinitions.Add(new ColumnDefinition());
            _gridOrderItemTemplate.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(500) });
            _gridOrderItemTemplate.ColumnDefinitions.Add(new ColumnDefinition());
            _gridOrderItemTemplate.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(150) });

            Grid gridData = new Grid();
            gridData.ColumnDefinitions.Add(new ColumnDefinition());
            gridData.ColumnDefinitions.Add(new ColumnDefinition());
            gridData.ColumnDefinitions.Add(new ColumnDefinition());
            {
                StackPanel spNameDate = new StackPanel();
                spNameDate.VerticalAlignment = VerticalAlignment.Center;
                spNameDate.HorizontalAlignment = HorizontalAlignment.Center;
                spNameDate.Orientation = Orientation.Vertical;
                {
                    TextBlock tbName = new TextBlock();
                    //tbName.FontSize = 28;
                    if (_order.StaffID != null)
                        tbName.Text = _order.Staff.ToString();
                    if (_order.UserID != null)
                        tbName.Text = _order.User.ToString();

                    spNameDate.Children.Add(tbName);

                    TextBlock tbDate = new TextBlock();
                    //tbDate.FontSize = 28;
                    tbDate.Text = _order.OrderDateTime.ToString();
                    spNameDate.Children.Add(tbDate);
                }
                Grid.SetColumn(spNameDate, 1);
                _gridOrderItemTemplate.Children.Add(spNameDate);

                StackPanel spStructure = new StackPanel();
                spStructure.VerticalAlignment = VerticalAlignment.Center;
                spStructure.HorizontalAlignment = HorizontalAlignment.Center;
                spStructure.Orientation = Orientation.Vertical;
                {
                    TextBlock tbLabel = new TextBlock();
                    //tbLabel.FontSize = 28;
                    tbLabel.Text = "Состав:";
                    spStructure.Children.Add(tbLabel);

                    TextBlock tbStructure = new TextBlock();
                    //tbStructure.FontSize = 28;
                    //tbStructure.TextWrapping = TextWrapping.Wrap;
                    tbStructure.Text = _order.ValidList;
                    spStructure.Children.Add(tbStructure);
                }
                Grid.SetColumn(spStructure, 2);
                _gridOrderItemTemplate.Children.Add(spStructure);

                StackPanel spCost = new StackPanel();
                spCost.VerticalAlignment = VerticalAlignment.Center;
                spCost.HorizontalAlignment = HorizontalAlignment.Center;
                spCost.Orientation = Orientation.Vertical;
                {
                    TextBlock tbLabelc = new TextBlock();
                    //tbLabelc.FontSize = 28;
                    tbLabelc.Text = "Стоимость:";
                    spCost.Children.Add(tbLabelc);

                    StackPanel spInnerCost = new StackPanel();
                    spInnerCost.VerticalAlignment = VerticalAlignment.Center;
                    spInnerCost.HorizontalAlignment = HorizontalAlignment.Center;
                    spInnerCost.Orientation = Orientation.Horizontal;
                    {
                        TextBlock tbCost = new TextBlock();
                        //tbCost.FontSize = 28;
                        tbCost.Text = _order.SummaryCost.ToString();
                        spInnerCost.Children.Add(tbCost);

                        TextBlock tbCost2 = new TextBlock();
                        //tbCost2.FontSize = 28;
                        tbCost2.Text = "₽";
                        //tbCost2.HorizontalAlignment = HorizontalAlignment.Center;
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
