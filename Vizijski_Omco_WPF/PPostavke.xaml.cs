using System.Threading;
using System.Windows;
using System.Windows.Forms;

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
        }

        private void BiskljuciAplikaciju_Click(object sender, RoutedEventArgs e)
        {

            if (System.Windows.MessageBox.Show("Da li želite zatvoriti aplikaciju?",
            "Potvrda", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                // Close the window 
                // Gasimo LIVE kamere
                App.HDevExp.Exitloop1 = true;
                App.HDevExp.Exitloop2 = true;
                App.HDevExp.Exitloop3 = true;
                App.HDevExp.Exitloop4 = true;
                Thread.Sleep(1000);
                App.Current.Shutdown();
            }
            else
            {
                // Do not close the window
            }
        }

        private void BizbrisiPodatke_Click(object sender, RoutedEventArgs e)
        {
            if (System.Windows.MessageBox.Show("Da li ste sigurni da želite izbrisati cijelu bazu podataka?",
            "Potvrda", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {
                App.ResetData();
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
        }

        private void BSaveDataLayer_Click(object sender, RoutedEventArgs e)
        {
            IFBrojSlojevaUlaznaLijevo.IsEnabled = false;
            IFBrojSlojevaUlaznaDesno.IsEnabled = false;
            IFBrojSlojevaKomadiOKLijevo.IsEnabled = false;
            IFBrojSlojevaKomadiOKDesno.IsEnabled = false;
            IFBrojSlojevaKomadiNOKLijevo.IsEnabled = false;
            IFBrojSlojevaKomadiNOKDesno.IsEnabled = false;
            IFBrojLimova.IsEnabled = false;
            IFDebljinaLimova.IsEnabled = false;
        }

        private void BBpaletaLimova_PreviewMouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            IFBrojLimova.IsEnabled = true;
            IFDebljinaLimova.IsEnabled = true;
        }

        //private void updatePage(object sender, PLCInterfaceEventArgs e)
        //{

        //}

        //private void updatePage_100(object sender, PLCInterfaceEventArgs e)
        //{

        //}

    }
}
