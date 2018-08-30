using System.Threading;
using System.Windows;
using HalconDotNet;

namespace VizijskiSustavWPF
{
    /// <summary>
    /// Interaction logic for PPostavke.xaml
    /// </summary>
    /// 

    public partial class PPostavke
    {

        
        public PPostavke()
        {
            InitializeComponent();
            //App.PLC.Update_1_s += new PLCInterface.UpdateHandler(updatePage);
            //App.PLC.Update_100_ms += new PLCInterface.UpdateHandler(updatePage_100);
            BbUlaznaLijevo.IsEnabled = false;
            BbUlaznaDesno.IsEnabled = false;
            BbKomadiOkLijevo.IsEnabled = false;
            BbKomadiOkDesno.IsEnabled = false;
            BbKomadiNokLijevo.IsEnabled = false;
            BbKomadiNokDesno.IsEnabled = false;
        }

        private void BiskljuciAplikaciju_Click(object sender, RoutedEventArgs e)
        {

            if (MessageBox.Show("Da li želite zatvoriti aplikaciju?",
            "Potvrda", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                // Close the window 
                // Gasimo LIVE kamere
                App.HDevExp.Exitloop1 = true;
                App.HDevExp.Exitloop2 = true;
                App.HDevExp.Exitloop3 = true;
                App.HDevExp.Exitloop4 = true;
                HOperatorSet.CloseAllFramegrabbers();
                Thread.Sleep(1000);
                App.Current.Shutdown();
            }
            else
            {
                // Do not close the window
            }
        }

        private void BSaveDataDimensions_Click(object sender, RoutedEventArgs e)
        {
            kota_br1.IsEnabled = false;
            kota_br2.IsEnabled = false;
            kota_br3.IsEnabled = false;
            kota_br4.IsEnabled = false;
            kota_br5.IsEnabled = false;
            if_visina1.IsEnabled = false;
            if_visina2.IsEnabled = false;
            if_visina3.IsEnabled = false;
            if_visinaBaze.IsEnabled = false;
            // Unos Toleranca +
            IFdeltaPlus1.IsEnabled = false;
            IFdeltaPlus2.IsEnabled = false;
            IFdeltaPlus3.IsEnabled = false;
            IFdeltaPlus4.IsEnabled = false;
            IFdeltaPlus5.IsEnabled = false;
            IFdeltaPlusV1.IsEnabled = false;
            IFdeltaPlusV2.IsEnabled = false;
            IFdeltaPlusV3.IsEnabled = false;
            //IFdeltaPlusVB.IsEnabled = false;
            // Unos toleranca -
            IFdeltaMinus1.IsEnabled = false;
            IFdeltaMinus2.IsEnabled = false;
            IFdeltaMinus3.IsEnabled = false;
            IFdeltaMinus4.IsEnabled = false;
            IFdeltaMinus5.IsEnabled = false;
            IFdeltaMinusV1.IsEnabled = false;
            IFdeltaMinusV2.IsEnabled = false;
            IFdeltaMinusV3.IsEnabled = false;
            //IFdeltaMinusVB.IsEnabled = false;
        }

        private void BSaveDataLayer_Click(object sender, RoutedEventArgs e)
        {
            IFBrojSlojevaUlaznaLijevo.IsEnabled = false;
            IFBrojSlojevaUlaznaDesno.IsEnabled = false;
            IFBrojSlojevaKomadiOKLijevo.IsEnabled = false;
            IFBrojSlojevaKomadiOKDesno.IsEnabled = false;
            IFBrojSlojevaKomadiNOKLijevo.IsEnabled = false;
            IFBrojSlojevaKomadiNOKDesno.IsEnabled = false;
            //IFBrojLimova.IsEnabled = false;
            IFDebljinaLimova.IsEnabled = false;
        }

        private void BSaveDataReport_Click(object sender, RoutedEventArgs e)
        {
            TBbrojNacrta.IsEnabled = false;
            TBkupac.IsEnabled = false;
            TBkupacRef.IsEnabled = false;
            TBkupacPO.IsEnabled = false;
            TBkolicina.IsEnabled = false;
            TBispitivac.IsEnabled = false;
            TBdatum.IsEnabled = false;
        }

        private void BnewJob_Click(object sender, RoutedEventArgs e)
        {
            // Unos Zaglavlja
            TBbrojNacrta.IsEnabled = true;
            TBkupac.IsEnabled = true;
            TBkupacRef.IsEnabled = true;
            TBkupacPO.IsEnabled = true;
            TBkolicina.IsEnabled = true;
            TBispitivac.IsEnabled = true;
            TBdatum.IsEnabled = true;
            // Unos Mjerenih vrijednosti
            kota_br1.IsEnabled = true;
            kota_br2.IsEnabled = true;
            kota_br3.IsEnabled = true;
            kota_br4.IsEnabled = true;
            kota_br5.IsEnabled = true;
            if_visina1.IsEnabled = true;
            if_visina2.IsEnabled = true;
            if_visina3.IsEnabled = true;
            if_visinaBaze.IsEnabled = true;
            // Unos Toleranca +
            IFdeltaPlus1.IsEnabled = true;
            IFdeltaPlus2.IsEnabled = true;
            IFdeltaPlus3.IsEnabled = true;
            IFdeltaPlus4.IsEnabled = true;
            IFdeltaPlus5.IsEnabled = true;
            IFdeltaPlusV1.IsEnabled = true;
            IFdeltaPlusV2.IsEnabled = true;
            IFdeltaPlusV3.IsEnabled = true;
            //IFdeltaPlusVB.IsEnabled = true;
            // Unos toleranca -
            IFdeltaMinus1.IsEnabled = true;
            IFdeltaMinus2.IsEnabled = true;
            IFdeltaMinus3.IsEnabled = true;
            IFdeltaMinus4.IsEnabled = true;
            IFdeltaMinus5.IsEnabled = true;
            IFdeltaMinusV1.IsEnabled = true;
            IFdeltaMinusV2.IsEnabled = true;
            IFdeltaMinusV3.IsEnabled = true;
            //IFdeltaMinusVB.IsEnabled = true;
            // Vrijednost Slojeva paleta
            IFBrojSlojevaUlaznaLijevo.IsEnabled = true;
            IFBrojSlojevaUlaznaDesno.IsEnabled = true;
            IFBrojSlojevaKomadiOKLijevo.IsEnabled = true;
            IFBrojSlojevaKomadiOKDesno.IsEnabled = true;
            IFBrojSlojevaKomadiNOKLijevo.IsEnabled = true;
            IFBrojSlojevaKomadiNOKDesno.IsEnabled = true;
            //IFBrojLimova.IsEnabled = true;
            IFDebljinaLimova.IsEnabled = true;
        }

        //private void BBkomadiZaMjerenje_PreviewMouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        //{
        //    IFBrojSlojevaUlaznaLijevo.IsEnabled = true;
        //    IFBrojSlojevaUlaznaDesno.IsEnabled = true;
        //}

        //private void BBkomadiNok_PreviewMouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        //{
        //    IFBrojSlojevaKomadiNOKLijevo.IsEnabled = true;
        //    IFBrojSlojevaKomadiNOKDesno.IsEnabled = true;
        //}

        //private void BBkomadiOK_PreviewMouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        //{
        //    IFBrojSlojevaKomadiOKLijevo.IsEnabled = true;
        //    IFBrojSlojevaKomadiOKDesno.IsEnabled = true;
        //}

        //private void BBpaletaLimova_PreviewMouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        //{
        //    //IFBrojLimova.IsEnabled = true;
        //    IFDebljinaLimova.IsEnabled = true;
        //}

        //private void updatePage(object sender, PLCInterfaceEventArgs e)
        //{

        //}

        //private void updatePage_100(object sender, PLCInterfaceEventArgs e)
        //{

        //}

    }
}
