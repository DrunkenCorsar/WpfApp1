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
using System.Windows.Shapes;

namespace WpfApp1.View
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private double actualScrollBarWidth = SystemParameters.VerticalScrollBarWidth + 10;

        public MainWindow()
        {
            InitializeComponent();

            Application.Current.MainWindow.SizeChanged += ResizeServicesListColumns;
        }

        private void ResizeServicesListColumns(object sender, SizeChangedEventArgs args)
        {
            switch (args.NewSize.Width)
            {
                case <= 1100 and > 700:
                    AdaptWindowLarge();
                    break;
                case <= 700 and > 500:
                    AdaptWindowMiddle();
                    break;
                case <= 500 and > 400:
                    AdaptWindowLittle();
                    break;
                case <= 400:
                    AdaptWindowMinimum();
                    break;
                default:
                    AdaptWindowMaximum();
                    break;

            }
        }

        public void AdaptWindowMaximum()
        {
            while (mainGrid.RowDefinitions.Count > 1)
            {
                mainGrid.RowDefinitions.RemoveAt(mainGrid.RowDefinitions.Count - 1);
            }

            listViewServices.SetValue(Grid.ColumnProperty, 0);
            listViewServices.SetValue(Grid.RowProperty, 0);

            mainGrid.RowDefinitions[0].Height = new GridLength(1, GridUnitType.Star);
            mainGrid.ColumnDefinitions[0].Width = new GridLength(3, GridUnitType.Star);
            mainGrid.ColumnDefinitions[1].Width = new GridLength(1, GridUnitType.Auto);

            double newColumnWidth = (listViewServices.ActualWidth - actualScrollBarWidth) / gridViewServices.Columns.Count;
            if (newColumnWidth > 0)
                foreach (var column in gridViewServices.Columns)
                {
                    column.Width = newColumnWidth;
                }
        }

        public void AdaptWindowLarge()
        {
            while (mainGrid.RowDefinitions.Count > 1)
            {
                mainGrid.RowDefinitions.RemoveAt(mainGrid.RowDefinitions.Count - 1);
            }

            listViewServices.SetValue(Grid.ColumnProperty, 0);
            listViewServices.SetValue(Grid.RowProperty, 0);

            mainGrid.RowDefinitions[0].Height = new GridLength(1, GridUnitType.Star);
            mainGrid.ColumnDefinitions[0].Width = new GridLength(3, GridUnitType.Star);
            mainGrid.ColumnDefinitions[1].Width = new GridLength(1, GridUnitType.Auto);

            double newColumnWidth = (listViewServices.ActualWidth - actualScrollBarWidth) / (gridViewServices.Columns.Count - 1);
            if (newColumnWidth > 0)
                foreach (var column in gridViewServices.Columns)
                {
                    if (column.Header.ToString() != "Account")
                    {
                        column.Width = newColumnWidth;
                    }
                    else
                    {
                        column.Width = 0;
                    }
                }
        }

        public void AdaptWindowMiddle()
        {
            while (mainGrid.RowDefinitions.Count > 1)
            {
                mainGrid.RowDefinitions.RemoveAt(mainGrid.RowDefinitions.Count - 1);
            }

            listViewServices.SetValue(Grid.ColumnProperty, 0);
            listViewServices.SetValue(Grid.RowProperty, 0);

            mainGrid.RowDefinitions[0].Height = new GridLength(1, GridUnitType.Star);
            mainGrid.ColumnDefinitions[0].Width = new GridLength(3, GridUnitType.Star);
            mainGrid.ColumnDefinitions[1].Width = new GridLength(1, GridUnitType.Auto);

            double newColumnWidth = (listViewServices.ActualWidth - actualScrollBarWidth) / (gridViewServices.Columns.Count - 2);
            if (newColumnWidth > 0)
                foreach (var column in gridViewServices.Columns)
                {
                    if (column.Header.ToString() != "Account" && column.Header.ToString() != "DisplayName")
                    {
                        column.Width = newColumnWidth;
                    }
                    else
                    {
                        column.Width = 0;
                    }
                }
        }

        public void AdaptWindowLittle()
        {
            while (mainGrid.RowDefinitions.Count > 1)
            {
                mainGrid.RowDefinitions.RemoveAt(mainGrid.RowDefinitions.Count - 1);
            }

            listViewServices.SetValue(Grid.ColumnProperty, 0);
            listViewServices.SetValue(Grid.RowProperty, 0);

            mainGrid.RowDefinitions[0].Height = new GridLength(1, GridUnitType.Star);
            mainGrid.ColumnDefinitions[0].Width = new GridLength(3, GridUnitType.Star);
            mainGrid.ColumnDefinitions[1].Width = new GridLength(1, GridUnitType.Auto);

            double newColumnWidth = (listViewServices.ActualWidth - actualScrollBarWidth) / (gridViewServices.Columns.Count - 3);
            if (newColumnWidth > 0) foreach (var column in gridViewServices.Columns)
                {
                    if (column.Header.ToString() != "Account" && column.Header.ToString() != "DisplayName" && column.Header.ToString() != "Status")
                    {
                        column.Width = newColumnWidth;
                    }
                    else
                    {
                        column.Width = 0;
                    }
                }
        }

        public void AdaptWindowMinimum()
        {
            mainGrid.ColumnDefinitions[0].Width = new GridLength(0, GridUnitType.Star);
            mainGrid.ColumnDefinitions[1].Width = new GridLength(1, GridUnitType.Star);

            if (mainGrid.RowDefinitions.Count < 2)
            {
                mainGrid.RowDefinitions.Add(new RowDefinition());
            }
            mainGrid.RowDefinitions[0].Height = new GridLength(1, GridUnitType.Auto);
            mainGrid.RowDefinitions[1].Height = new GridLength(1, GridUnitType.Star);

            listViewServices.SetValue(Grid.RowProperty, 1);
            listViewServices.SetValue(Grid.ColumnProperty, 1);

            double newColumnWidth = (listViewServices.ActualWidth - actualScrollBarWidth) / (gridViewServices.Columns.Count - 3);
            if (newColumnWidth > 0)
                foreach (var column in gridViewServices.Columns)
                {
                    if (column.Header.ToString() != "Account" && column.Header.ToString() != "DisplayName" && column.Header.ToString() != "Status")
                    {
                        column.Width = newColumnWidth;
                    }
                    else
                    {
                        column.Width = 0;
                    }
                }
        }
    }
}
