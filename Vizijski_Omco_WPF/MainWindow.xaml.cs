﻿using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Navigation;

namespace VizijskiSustavWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        private DoubleAnimation animacija1 = new DoubleAnimation();
        private Frame pomocniFrame = new Frame();
        public MainWindow()
        {
            InitializeComponent();
            //App.PLC.Update_Online_Flag += new PLCInterface.OnlineMarker(updateOnlineFlag);
            animacija1.Completed += (sender, eArgs) =>  glavniFrame.HorizontalAlignment = HorizontalAlignment.Stretch;
            animacija1.Completed += (sender, eArgs) => pomocniFrame.HorizontalAlignment = HorizontalAlignment.Stretch;
            pomocniFrame.NavigationUIVisibility = NavigationUIVisibility.Hidden;
            pomocniFrame.Margin = new Thickness(10, 10, 10, 10);
            App.mwHandle = this;
            srh.IsEnabled = false;
            srh.Foreground = new SolidColorBrush(Colors.Gray);
            sablja.IsEnabled = false;
            sablja.Foreground = new SolidColorBrush(Colors.Gray);
        }

        #region POSTAVKE
        // POSTAVKE ////////////////////////////////////////////////////
        private void postavke_Click(object sender, RoutedEventArgs e)
        { 
            postavke.Margin = new Thickness(0, 0, 0, 4);
            dimenzije.Margin = new Thickness(0, 0, 4, 4);
            valovitost.Margin = new Thickness(0, 0, 4, 4);
            srh.Margin = new Thickness(0, 0, 4, 4);
            sablja.Margin = new Thickness(0, 0, 4, 4);
            kut.Margin = new Thickness(0, 0, 4, 4);
            rucno.Margin = new Thickness(0, 0, 4, 4);
            izvjestaji.Margin = new Thickness(0, 0, 4, 0);
            
            pomocniFrame.Visibility = Visibility.Hidden;
            glavniFrame.Content = App.pPostavke;
            glavniFrame.Visibility = Visibility.Visible;
            glavniFrame.NavigationService.RemoveBackEntry();
            pomocniFrame.NavigationService.RemoveBackEntry();
            App.PLC.ActiveScreen = 1;
        }

        private void postavke_MouseEnter(object sender, MouseEventArgs e)
        {
            if (glavniFrame.Content != App.pPostavke)
            {
                pomocniFrame.Visibility = Visibility.Visible;

                pomocniFrame.Content = App.pPostavke;
                try
                {
                    glavniGrid.Children.Add(pomocniFrame);
                }
                catch
                {
                }
                pomocniFrame.VerticalAlignment = VerticalAlignment.Stretch;
                
                animacija1.From = 0.0;
                animacija1.To = 1;
                animacija1.Duration = new Duration(TimeSpan.FromSeconds(0.25));
                pomocniFrame.BeginAnimation(OpacityProperty, animacija1);
                glavniFrame.Visibility = Visibility.Hidden;
                glavniFrame.NavigationService.RemoveBackEntry();
                pomocniFrame.NavigationService.RemoveBackEntry();
            }
            else
            {
                glavniFrame.Visibility = Visibility.Visible;
            }
        }

        private void postavke_MouseLeave(object sender, MouseEventArgs e)
        {
            if (glavniFrame.Content != App.pPostavke)
            {
                pomocniFrame.Visibility = Visibility.Hidden;
                if (uniformGrid.IsMouseOver)
                    glavniFrame.Visibility = Visibility.Visible;
                glavniFrame.NavigationService.RemoveBackEntry();
                pomocniFrame.NavigationService.RemoveBackEntry();
            }
        }
        #endregion

        #region ROBOT
        // DIMENZIJE --> ROBOT ///////////////////////////////////////////////////
        private void dimezije_Click(object sender, RoutedEventArgs e)
        {
            dimenzije.Margin = new Thickness(0, 0, 0, 4);
            postavke.Margin = new Thickness(0, 0, 4, 4);
            valovitost.Margin = new Thickness(0, 0, 4, 4);
            srh.Margin = new Thickness(0, 0, 4, 4);
            sablja.Margin = new Thickness(0, 0, 4, 4);
            kut.Margin = new Thickness(0, 0, 4, 4);
            rucno.Margin = new Thickness(0, 0, 4, 4);
            izvjestaji.Margin = new Thickness(0, 0, 4, 0);
          
            pomocniFrame.Visibility = Visibility.Hidden;
            glavniFrame.Content = App.pRobot;
            glavniFrame.Visibility = Visibility.Visible;
            glavniFrame.NavigationService.RemoveBackEntry();
            pomocniFrame.NavigationService.RemoveBackEntry();
            App.PLC.ActiveScreen = 2;
        }

        private void dimenzije_MouseEnter(object sender, MouseEventArgs e)
        {
            if (glavniFrame.Content != App.pRobot)
            {
                pomocniFrame.Visibility = Visibility.Visible;
                pomocniFrame.Content = App.pRobot;

                try
                {
                    glavniGrid.Children.Add(pomocniFrame);
                }
                catch
                {
                }
                pomocniFrame.VerticalAlignment = VerticalAlignment.Stretch;
                animacija1.From = 0.0;
                animacija1.To = 1;
                animacija1.Duration = new Duration(TimeSpan.FromSeconds(0.25));
                pomocniFrame.BeginAnimation(OpacityProperty, animacija1);
                glavniFrame.Visibility = Visibility.Hidden;
                glavniFrame.NavigationService.RemoveBackEntry();
                pomocniFrame.NavigationService.RemoveBackEntry();
            }
            else
            {
                glavniFrame.Visibility = Visibility.Visible;
            }
        }

        private void dimenzije_MouseLeave(object sender, MouseEventArgs e)
        {
            if (glavniFrame.Content != App.pRobot)
            {
                pomocniFrame.Visibility = Visibility.Hidden;
                if (uniformGrid.IsMouseOver)
                glavniFrame.Visibility = Visibility.Visible;
                glavniFrame.NavigationService.RemoveBackEntry();
                pomocniFrame.NavigationService.RemoveBackEntry();
            }
        }
        #endregion

        #region UCENJE
        // VALOVITOST --> UCENJE ///////////////////////////////////////////////////////
        private void valovitost_Click(object sender, RoutedEventArgs e)
        {
            valovitost.Margin = new Thickness(0, 0, 0, 4);
            dimenzije.Margin = new Thickness(0, 0, 4, 4);
            postavke.Margin = new Thickness(0, 0, 4, 4);
            srh.Margin = new Thickness(0, 0, 4, 4);
            sablja.Margin = new Thickness(0, 0, 4, 4);
            kut.Margin = new Thickness(0, 0, 4, 4);
            rucno.Margin = new Thickness(0, 0, 4, 4);
            izvjestaji.Margin = new Thickness(0, 0, 4, 0);
          
            pomocniFrame.Visibility = Visibility.Hidden;
            glavniFrame.Content = App.pUcenje;
            
            glavniFrame.Visibility = Visibility.Visible;
            App.PLC.ActiveScreen = 3;
        }

        private void valovitost_MouseEnter(object sender, MouseEventArgs e)
        {
            if (glavniFrame.Content != App.pUcenje)
            {
                pomocniFrame.Visibility = Visibility.Visible;
                pomocniFrame.Content = App.pUcenje;

                try
                {
                    glavniGrid.Children.Add(pomocniFrame);
                }
                catch
                {
                }
                pomocniFrame.VerticalAlignment = VerticalAlignment.Stretch;
                animacija1.From = 0.0;
                animacija1.To = 1;
                animacija1.Duration = new Duration(TimeSpan.FromSeconds(0.25));
                pomocniFrame.BeginAnimation(OpacityProperty, animacija1);
                glavniFrame.Visibility = Visibility.Hidden;
                glavniFrame.NavigationService.RemoveBackEntry();
                pomocniFrame.NavigationService.RemoveBackEntry();
            }
            else
            {
                glavniFrame.Visibility = Visibility.Visible;
            }
        }

        private void valovitost_MouseLeave(object sender, MouseEventArgs e)
        {
            if (glavniFrame.Content != App.pUcenje)
            {
                pomocniFrame.Visibility = Visibility.Hidden;
                if (uniformGrid.IsMouseOver)
                    glavniFrame.Visibility = Visibility.Visible;
                glavniFrame.NavigationService.RemoveBackEntry();
                pomocniFrame.NavigationService.RemoveBackEntry();
            }
        }
        #endregion

        #region POROZNOST
        // SRH -- > POROZNOST//////////////////////////////////////////////////////
        private void srh_Click(object sender, RoutedEventArgs e)
        {
            srh.Margin = new Thickness(0, 0, 0, 4);
            valovitost.Margin = new Thickness(0, 0, 4, 4);
            dimenzije.Margin = new Thickness(0, 0, 4, 4);
            postavke.Margin = new Thickness(0, 0, 4, 4);
            sablja.Margin = new Thickness(0, 0, 4, 4);
            kut.Margin = new Thickness(0, 0, 4, 4);
            rucno.Margin = new Thickness(0, 0, 4, 4);
            izvjestaji.Margin = new Thickness(0, 0, 4, 0);
         
            pomocniFrame.Visibility = Visibility.Hidden;
            glavniFrame.Content = App.pPoroznost;
           
            glavniFrame.Visibility = Visibility.Visible;
            App.PLC.ActiveScreen = 4;
        }

        private void srh_MouseEnter(object sender, MouseEventArgs e)
        {
            if (glavniFrame.Content != App.pPoroznost)
            {
                pomocniFrame.Visibility = Visibility.Visible;
                pomocniFrame.Content = App.pPoroznost;

                try
                {
                    glavniGrid.Children.Add(pomocniFrame);
                }
                catch
                {
                }
                pomocniFrame.VerticalAlignment = VerticalAlignment.Stretch;
                animacija1.From = 0.0;
                animacija1.To = 1;
                animacija1.Duration = new Duration(TimeSpan.FromSeconds(0.25));
                pomocniFrame.BeginAnimation(OpacityProperty, animacija1);
                glavniFrame.Visibility = Visibility.Hidden;
                glavniFrame.NavigationService.RemoveBackEntry();
                pomocniFrame.NavigationService.RemoveBackEntry();
            }
            else
            {
                glavniFrame.Visibility = Visibility.Visible;
            }
        }

        private void srh_MouseLeave(object sender, MouseEventArgs e)
        {
            if (glavniFrame.Content != App.pPoroznost)
            {
                pomocniFrame.Visibility = Visibility.Hidden;
                if (uniformGrid.IsMouseOver)
                    glavniFrame.Visibility = Visibility.Visible;
                glavniFrame.NavigationService.RemoveBackEntry();
                pomocniFrame.NavigationService.RemoveBackEntry();
            }
        }
        #endregion

        #region VISINE
        // SABLJA ///////////////////////////////////////////////////////

        private void sablja_Click(object sender, RoutedEventArgs e)
        {
            srh.Margin = new Thickness(0, 0, 4, 4);
            valovitost.Margin = new Thickness(0, 0, 4, 4);
            dimenzije.Margin = new Thickness(0, 0, 4, 4);
            postavke.Margin = new Thickness(0, 0, 4, 4);
            sablja.Margin = new Thickness(0, 0, 0, 4);
            kut.Margin = new Thickness(0, 0, 4, 4);
            rucno.Margin = new Thickness(0, 0, 4, 4);
            izvjestaji.Margin = new Thickness(0, 0, 4, 0);

            pomocniFrame.Visibility = Visibility.Hidden;
            glavniFrame.Content = App.pVisine;

            glavniFrame.Visibility = Visibility.Visible;
            App.PLC.ActiveScreen = 5;
        }

        private void sablja_MouseEnter(object sender, MouseEventArgs e)
        {
            if (glavniFrame.Content != App.pVisine)
            {
                pomocniFrame.Visibility = Visibility.Visible;
                pomocniFrame.Content = App.pVisine;

                try
                {
                    glavniGrid.Children.Add(pomocniFrame);
                }
                catch
                {
                }
                pomocniFrame.VerticalAlignment = VerticalAlignment.Stretch;
                animacija1.From = 0.0;
                animacija1.To = 1;
                animacija1.Duration = new Duration(TimeSpan.FromSeconds(0.25));
                pomocniFrame.BeginAnimation(OpacityProperty, animacija1);
                glavniFrame.Visibility = Visibility.Hidden;
                glavniFrame.NavigationService.RemoveBackEntry();
                pomocniFrame.NavigationService.RemoveBackEntry();
            }
            else
            {
                glavniFrame.Visibility = Visibility.Visible;
            }

        }

        private void sablja_MouseLeave(object sender, MouseEventArgs e)
        {
            if (glavniFrame.Content != App.pVisine)
            {
                pomocniFrame.Visibility = Visibility.Hidden;
                if (uniformGrid.IsMouseOver)
                    glavniFrame.Visibility = Visibility.Visible;
                glavniFrame.NavigationService.RemoveBackEntry();
                pomocniFrame.NavigationService.RemoveBackEntry();
            }
        }

        #endregion

        #region DIJAMETRI
        // KUT --> DIJAMETRI ///////////////////////////////////////////////////////

        private void kut_Click(object sender, RoutedEventArgs e)
        {
            srh.Margin = new Thickness(0, 0, 4, 4);
            valovitost.Margin = new Thickness(0, 0, 4, 4);
            dimenzije.Margin = new Thickness(0, 0, 4, 4);
            postavke.Margin = new Thickness(0, 0, 4, 4);
            sablja.Margin = new Thickness(0, 0, 4, 4);
            kut.Margin = new Thickness(0, 0, 0, 4);
            rucno.Margin = new Thickness(0, 0, 4, 4);
            izvjestaji.Margin = new Thickness(0, 0, 4, 0);

            pomocniFrame.Visibility = Visibility.Hidden;
            glavniFrame.Content = App.pDijametri;

            glavniFrame.Visibility = Visibility.Visible;
            App.PLC.ActiveScreen = 6;
        }

        private void kut_MouseEnter(object sender, MouseEventArgs e)
        {
            if (glavniFrame.Content != App.pDijametri)
            {
                pomocniFrame.Visibility = Visibility.Visible;
                pomocniFrame.Content = App.pDijametri;

                try
                {
                    glavniGrid.Children.Add(pomocniFrame);
                }
                catch
                {
                }
                pomocniFrame.VerticalAlignment = VerticalAlignment.Stretch;
                animacija1.From = 0.0;
                animacija1.To = 1;
                animacija1.Duration = new Duration(TimeSpan.FromSeconds(0.25));
                pomocniFrame.BeginAnimation(OpacityProperty, animacija1);
                glavniFrame.Visibility = Visibility.Hidden;
                glavniFrame.NavigationService.RemoveBackEntry();
                pomocniFrame.NavigationService.RemoveBackEntry();
            }
            else
            {
                glavniFrame.Visibility = Visibility.Visible;
            }
        }

        private void kut_MouseLeave(object sender, MouseEventArgs e)
        {
            if (glavniFrame.Content != App.pDijametri)
            {
                pomocniFrame.Visibility = Visibility.Hidden;
                if (uniformGrid.IsMouseOver)
                    glavniFrame.Visibility = Visibility.Visible;
                glavniFrame.NavigationService.RemoveBackEntry();
                pomocniFrame.NavigationService.RemoveBackEntry();
            }
        }
        #endregion

        #region RUCNO
        // RUCNO /////////////////////////////////////////////////////

        private void rucno_Click(object sender, RoutedEventArgs e)
        {
            dimenzije.Margin = new Thickness(0, 0, 4, 4);
            postavke.Margin = new Thickness(0, 0, 4, 4);
            valovitost.Margin = new Thickness(0, 0, 4, 4);
            srh.Margin = new Thickness(0, 0, 4, 4);
            sablja.Margin = new Thickness(0, 0, 4, 4);
            kut.Margin = new Thickness(0, 0, 4, 4);
            rucno.Margin = new Thickness(0, 0, 0, 4);
            izvjestaji.Margin = new Thickness(0, 0, 4, 0);

            pomocniFrame.Visibility = Visibility.Hidden;
            glavniFrame.Content = App.pRucno;
            glavniFrame.Visibility = Visibility.Visible;
            glavniFrame.NavigationService.RemoveBackEntry();
            pomocniFrame.NavigationService.RemoveBackEntry();
            App.PLC.ActiveScreen = 7;
        }

        private void rucno_MouseEnter(object sender, MouseEventArgs e)
        {
            if (glavniFrame.Content != App.pRucno)
            {
                pomocniFrame.Visibility = Visibility.Visible;
                pomocniFrame.Content = App.pRucno;

                try
                {
                    glavniGrid.Children.Add(pomocniFrame);
                }
                catch
                {
                }
                pomocniFrame.VerticalAlignment = VerticalAlignment.Stretch;
                animacija1.From = 0.0;
                animacija1.To = 1;
                animacija1.Duration = new Duration(TimeSpan.FromSeconds(0.25));
                pomocniFrame.BeginAnimation(OpacityProperty, animacija1);
                glavniFrame.Visibility = Visibility.Hidden;
                glavniFrame.NavigationService.RemoveBackEntry();
                pomocniFrame.NavigationService.RemoveBackEntry();
            }
            else
            {
                glavniFrame.Visibility = Visibility.Visible;
            }
        }

        private void rucno_MouseLeave(object sender, MouseEventArgs e)
        {
            if (glavniFrame.Content != App.pRucno)
            {
                pomocniFrame.Visibility = Visibility.Hidden;
                if (uniformGrid.IsMouseOver)
                    glavniFrame.Visibility = Visibility.Visible;
                glavniFrame.NavigationService.RemoveBackEntry();
                pomocniFrame.NavigationService.RemoveBackEntry();
            }
        }
        #endregion

        #region IZVJESTAJI
        // IZVJESTAJI //////////////////////////////////////////////////////
        private void izvjestaji_Click(object sender, RoutedEventArgs e)
        {
            //App.pIzvjestaji.dataGrid1.Items.Refresh();
            dimenzije.Margin = new Thickness(0, 0, 4, 4);
            srh.Margin = new Thickness(0, 0, 4, 4);
            valovitost.Margin = new Thickness(0, 0, 4, 4);
            sablja.Margin = new Thickness(0, 0, 4, 4);
            kut.Margin = new Thickness(0, 0, 4, 4);
            postavke.Margin = new Thickness(0, 0, 4, 4);
            rucno.Margin = new Thickness(0, 0, 4, 4);
            izvjestaji.Margin = new Thickness(0, 0, 0, 0);
            
            pomocniFrame.Visibility = Visibility.Hidden;
            glavniFrame.Content = App.pIzvjestaji;
           
            glavniFrame.Visibility = Visibility.Visible;
            App.PLC.ActiveScreen = 8;
        }

        private void izvjestaji_MouseEnter(object sender, MouseEventArgs e)
        {
            if (glavniFrame.Content != App.pIzvjestaji)
            {
                pomocniFrame.Visibility = Visibility.Visible;
                pomocniFrame.Content = App.pIzvjestaji;

                try
                {
                    glavniGrid.Children.Add(pomocniFrame);
                }
                catch
                {
                }
                pomocniFrame.VerticalAlignment = VerticalAlignment.Stretch;
                animacija1.From = 0.0;
                animacija1.To = 1;
                animacija1.Duration = new Duration(TimeSpan.FromSeconds(0.25));
                pomocniFrame.BeginAnimation(OpacityProperty, animacija1);
                glavniFrame.Visibility = Visibility.Hidden;
                glavniFrame.NavigationService.RemoveBackEntry();
                pomocniFrame.NavigationService.RemoveBackEntry();
            }
            else
            {
                glavniFrame.Visibility = Visibility.Visible;
            }
        }

        private void izvjestaji_MouseLeave(object sender, MouseEventArgs e)
        {
            if (glavniFrame.Content != App.pIzvjestaji)
            {
                pomocniFrame.Visibility = Visibility.Hidden;
                if (uniformGrid.IsMouseOver)
                    glavniFrame.Visibility = Visibility.Visible;
                glavniFrame.NavigationService.RemoveBackEntry();
                pomocniFrame.NavigationService.RemoveBackEntry();
            }
        }

        #endregion


        private void glavniGrid_MouseEnter(object sender, MouseEventArgs e)
        {
            if (glavniFrame.Visibility == Visibility.Hidden)
                glavniFrame.Visibility = Visibility.Visible;
        }

        private void glavniFrame_ContentRendered(object sender, EventArgs e)
        {

        }
    }
}
