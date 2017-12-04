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

namespace VizijskiSustavWPF
{
    /// <summary>
    /// Interaction logic for PIzvjestaji.xaml
    /// </summary>
    public partial class PIzvjestaji : Page
    {
        private Point mousePosition;

        public PIzvjestaji()
        {
            InitializeComponent();
        }

        private void Image_MouseDown(object sender, MouseButtonEventArgs e)
        {
            //image.Source = App.MainReportInterface.SheetImageSource;
        }

        private void Page_MouseDown(object sender, MouseButtonEventArgs e)
        {
            //image.Source = App.MainReportInterface.SheetImageSource;
            //image.Source = App.
        }

        private void dataGrid1_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            
        }

        private void dataGrid1_DragEnter(object sender, DragEventArgs e)
        {
            ScrollViewer scrollView = GetScrollbar(dataGrid1);
            scrollView.ScrollToVerticalOffset(scrollView.VerticalOffset + 1);
        }

        private static ScrollViewer GetScrollbar(DependencyObject dep)
        {
            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(dep); i++)
            {
                var child = VisualTreeHelper.GetChild(dep, i);
                if (child != null && child is ScrollViewer)
                    return child as ScrollViewer;
                else
                {
                    ScrollViewer sub = GetScrollbar(child);
                    if (sub != null)
                        return sub;
                }
            }
            return null;
        }

        private void dataGrid1_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                if ((Mouse.GetPosition(dataGrid1).Y - mousePosition.Y) > 10)
                {
                    ScrollViewer scrollView = GetScrollbar(dataGrid1);
                    scrollView.ScrollToVerticalOffset(scrollView.VerticalOffset + 1);
                    mousePosition = Mouse.GetPosition(dataGrid1);
                }
                else if ((-Mouse.GetPosition(dataGrid1).Y + mousePosition.Y) > 10)
                {
                    ScrollViewer scrollView = GetScrollbar(dataGrid1);
                    scrollView.ScrollToVerticalOffset(scrollView.VerticalOffset - 1);
                    mousePosition = Mouse.GetPosition(dataGrid1);
                }
            }
            else
            {
                mousePosition = Mouse.GetPosition(dataGrid1);
            }
        }

        private void b_generateReport1_Click(object sender, RoutedEventArgs e)
        {
            string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
            App.MainReportInterface.WriteControlSheetReport(baseDirectory+"reports/controlSheetReportTemplate.xlsx", "Sheet1", App.MainReportInterface.Dimensions.Count);
        }

        private void b_generateReport2_Click(object sender, RoutedEventArgs e)
        {
            string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
            App.MainReportInterface.WriteReclamationReport(baseDirectory + "reports/reclamationReportTemplate.xlsx");
        }

        private void b_openReportFolder_Click(object sender, RoutedEventArgs e)
        {
            string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
            baseDirectory= baseDirectory.Replace("/","\\")+ "Izvjestaji"; 
            System.Diagnostics.Process.Start(@baseDirectory);
        }


    }
}
