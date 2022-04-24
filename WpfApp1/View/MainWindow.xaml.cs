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
        public MainWindow()
        {
            InitializeComponent();

            Application.Current.MainWindow.SizeChanged += ResizeServicesListColumns;
        }

        private void ResizeServicesListColumns(object sender, SizeChangedEventArgs args)
        {
            switch (listViewServices.ActualWidth)
            {
                case <= 900 and > 400:
                    AdaptWindowLarge();
                    break;
                case <= 400 and > 200:
                    AdaptWindowMiddle();
                    break;
                case <= 200 and > 100:
                    AdaptWindowLittle();
                    break;
                case <= 100:
                    AdaptWindowMinimum();
                    break;
                default:
                    AdaptWindowMaximum();
                    break;

            }
        }

        public void AdaptWindowMaximum()
        {
            double newColumnWidth = listViewServices.ActualWidth / gridViewServices.Columns.Count;
            foreach (var column in gridViewServices.Columns)
            {
                column.Width = newColumnWidth;
            }
        }

        public void AdaptWindowLarge()
        {
            listViewServices.SetValue(Grid.ColumnProperty, 0);
            mainGrid.ColumnDefinitions[0].Width = new GridLength(3, GridUnitType.Star);
            mainGrid.ColumnDefinitions[1].Width = new GridLength(1, GridUnitType.Auto);
            double newColumnWidth = listViewServices.ActualWidth / (gridViewServices.Columns.Count - 1);
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
            listViewServices.SetValue(Grid.ColumnProperty, 0);
            mainGrid.ColumnDefinitions[0].Width = new GridLength(3, GridUnitType.Star);
            mainGrid.ColumnDefinitions[1].Width = new GridLength(1, GridUnitType.Auto);
            double newColumnWidth = listViewServices.ActualWidth / (gridViewServices.Columns.Count - 2);
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
            listViewServices.SetValue(Grid.ColumnProperty, 0);
            mainGrid.ColumnDefinitions[0].Width = new GridLength(3, GridUnitType.Star);
            mainGrid.ColumnDefinitions[1].Width = new GridLength(1, GridUnitType.Auto);
            double newColumnWidth = listViewServices.ActualWidth / (gridViewServices.Columns.Count - 3);
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

        public void AdaptWindowMinimum()
        {
            mainGrid.ColumnDefinitions[0].Width = new GridLength(0, GridUnitType.Star);
            mainGrid.ColumnDefinitions[1].Width = new GridLength(1, GridUnitType.Star);

            mainGrid.RowDefinitions.Add(new RowDefinition());
            mainGrid.RowDefinitions[0].Height = new GridLength(1, GridUnitType.Auto);
            mainGrid.RowDefinitions[1].Height = new GridLength(1, GridUnitType.Star);

            listViewServices.SetValue(Grid.RowProperty, 1);
            listViewServices.SetValue(Grid.ColumnProperty, 1);

            double newColumnWidth = listViewServices.ActualWidth / (gridViewServices.Columns.Count - 3);
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
