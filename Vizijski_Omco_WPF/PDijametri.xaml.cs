using System;
using System.Windows;

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
    }
}
