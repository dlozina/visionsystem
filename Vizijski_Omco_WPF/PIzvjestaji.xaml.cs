﻿using System;
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
using VizijskiSustavWPF.Reports;
using static VizijskiSustavWPF.Reports.ReportInterface;

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

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            
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

        private void dataGrid1_DragEnter(object sender, DragEventArgs e)
        {
            ScrollViewer scrollView = GetScrollbar(dataGrid1);
            scrollView.ScrollToVerticalOffset(scrollView.VerticalOffset + 1);
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

        private void dataGrid1_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void saveValues_Click(object sender, RoutedEventArgs e)
        {
            // Spremanje rezultata
        }

        private void loadValues_Click(object sender, RoutedEventArgs e)
        {
            // Povlacenje rezultata
        }

    }
}
