using System;
using System.Windows;
using System.Windows.Media;

namespace VizijskiSustavWPF
{
    /// <summary>
    /// Interaction logic for PSrh.xaml
    /// </summary>
    public partial class PDijametri
    {
        public PDijametri()
        {
            InitializeComponent();
            //App.PLC.Update_1_s += new PLCInterface.UpdateHandler(updatePage);
            //App.PLC.Update_100_ms += new PLCInterface.UpdateHandler(updatePage_100);
            // Control is not enabled at the moment 29072018
            bb_pautoStart.IsEnabled = false;
            bb_pautoStart.Foreground = new SolidColorBrush(Colors.Gray);
            bb_pautoStop.IsEnabled = false;
            bb_pautoStop.Foreground = new SolidColorBrush(Colors.Gray);
            bb_ppause.IsEnabled = false;
            bb_ppause.Foreground = new SolidColorBrush(Colors.Gray);
            bb_preset.IsEnabled = false;
            bb_preset.Foreground = new SolidColorBrush(Colors.Gray);
        }

        //private void updatePage(object sender, PLCInterfaceEventArgs e)
        //{
        //    // Without Dispatcher it does not work
        //    Dispatcher.BeginInvoke(new Action(() =>
        //    {
        //        if ((float)e.StatusData.MjerenjeDiametara.BrojPonavljanjaSekvence.Value == 1.0f)
        //        {
        //            LbStatusLabel.Content = "BROJ 1";
        //        }
        //        else if ((float)e.StatusData.MjerenjeDiametara.BrojPonavljanjaSekvence.Value == 2.0f)
        //        {
        //            LbStatusLabel.Content = "BROJ 2";
        //        }
        //        else if ((float)e.StatusData.MjerenjeDiametara.BrojPonavljanjaSekvence.Value == 3.0f)
        //        {
        //            LbStatusLabel.Content = "BROJ 3";
        //        }
        //        else if ((float)e.StatusData.MjerenjeDiametara.BrojPonavljanjaSekvence.Value == 4.0f)
        //        {
        //            LbStatusLabel.Content = "BROJ 4";
        //        }

        //    }));
        //}

        //private void updatePage_100(object sender, PLCInterfaceEventArgs e)
        //{
        //    // Without Dispatcher it does not work
        //    Dispatcher.BeginInvoke(new Action(() =>
        //    {
        //        if ((float)e.StatusData.MjerenjeDiametara.BrojPonavljanjaSekvence.Value == 1.0f)
        //        {
        //            LbStatusLabel.Content = "BROJ 1";
        //        }
        //        else if ((float)e.StatusData.MjerenjeDiametara.BrojPonavljanjaSekvence.Value == 2.0f)
        //        {
        //            LbStatusLabel.Content = "BROJ 2";
        //        }
        //        else if ((float)e.StatusData.MjerenjeDiametara.BrojPonavljanjaSekvence.Value == 3.0f)
        //        {
        //            LbStatusLabel.Content = "BROJ 3";
        //        }
        //        else if ((float)e.StatusData.MjerenjeDiametara.BrojPonavljanjaSekvence.Value == 4.0f)
        //        {
        //            LbStatusLabel.Content = "BROJ 4";
        //        }

        //    }));

        //}

        // Dijametri
        private void CbDiametar1_Checked(object sender, RoutedEventArgs e)
        {
            App.ActivateDiameter1();
        }

        private void CbDiametar1_Unchecked(object sender, RoutedEventArgs e)
        {
            App.DeactivateDiameter1();
        }

        private void CbDiametar2_Checked(object sender, RoutedEventArgs e)
        {
            App.ActivateDiameter2();
        }

        private void CbDiametar2_Unchecked(object sender, RoutedEventArgs e)
        {
            App.DeactivateDiameter2();
        }

        private void CbDiametar3_Checked(object sender, RoutedEventArgs e)
        {
            App.ActivateDiameter3();
        }

        private void CbDiametar3_Unchecked(object sender, RoutedEventArgs e)
        {
            App.DeactivateDiameter3();
        }

        private void CbDiametar4_Checked(object sender, RoutedEventArgs e)
        {
            App.ActivateDiameter4();
        }

        private void CbDiametar4_Unchecked(object sender, RoutedEventArgs e)
        {
            App.DeactivateDiameter4();
        }

        private void CbDiametar5_Checked(object sender, RoutedEventArgs e)
        {
            App.ActivateDiameter5();
        }

        private void CbDiametar5_Unchecked(object sender, RoutedEventArgs e)
        {
            App.DeactivateDiameter5();
        }
        // Visina
        private void CbVisina1_Checked(object sender, RoutedEventArgs e)
        {
            App.ActivateHeight1();
        }

        private void CbVisina1_Unchecked(object sender, RoutedEventArgs e)
        {
            App.DeactivateHeight1();
        }

        private void CbVisina2_Checked(object sender, RoutedEventArgs e)
        {
            App.ActivateHeight2();
        }

        private void CbVisina2_Unchecked(object sender, RoutedEventArgs e)
        {
            App.DeactivateHeight2();
        }

        private void CbVisina3_Checked(object sender, RoutedEventArgs e)
        {
            App.ActivateHeight3();
        }

        private void CbVisina3_Unchecked(object sender, RoutedEventArgs e)
        {
            App.DeactivateHeight3();
        }
        // Poroznost
        private void CBporoznost_Checked(object sender, RoutedEventArgs e)
        {
            App.ActivatePoroznost();
        }

        private void CBporoznost_Unchecked(object sender, RoutedEventArgs e)
        {
            App.DeactivatePoroznost();
        }
        // String
        private void CBstring_Checked(object sender, RoutedEventArgs e)
        {
            App.ActivateString();
        }

        private void CBstring_Unchecked(object sender, RoutedEventArgs e)
        {
            App.DeactivateString();
        }

    }
}
