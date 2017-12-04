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
using System.Threading;
using System.Drawing;
using System.IO;
using Microsoft.Win32;

namespace VizijskiSustavWPF
{
    /// <summary>
    /// Interaction logic for PPostavke.xaml
    /// </summary>
    /// 
    
    public partial class PPostavke : Page
    {
        Regulator Reg = new Regulator();
        Algoritmi Alg = new Algoritmi();
        System.Timers.Timer saveSetupTimer= new System.Timers.Timer();
        string path = "zaglavljeStartup.txt";
        public PPostavke()
        {
            InitializeComponent();
            App.PLC.Update_100_ms += new PLCInterface.UpdateHandler(updatePage);
            App.PLC.Update_1_s += new PLCInterface.UpdateHandler(updatePage_1_s);
            saveSetupTimer.Elapsed += new System.Timers.ElapsedEventHandler(spremiZaglavljeUStartup);
            saveSetupTimer.Interval = 5000;
            saveSetupTimer.AutoReset = true;
           
        }


        private void updatePage(object sender, PLCInterfaceEventArgs e)
        {
            Dispatcher.BeginInvoke(new Action(() =>
            {
                //lb_tempGlass1.Content = (((int)e.StatusData.Temperature.Glass1.Value) / 10.0f).ToString();
                //lb_tempGlass2.Content = ((((int)e.StatusData.Temperature.Glass1.Value)-1) / 10.0f).ToString();
                //lb_tempLG1.Content = (((int)e.StatusData.Temperature.LG1.Value) / 10.0f).ToString();
                //lb_tempLG2.Content = (((int)e.StatusData.Temperature.LG2.Value) / 10.0f).ToString();
            }));

        }

        private void updatePage_1_s(object sender, PLCInterfaceEventArgs e)
        {
            Dispatcher.BeginInvoke(new Action(() =>
            {
                //if (e.ControlData != null)
                //    if ((bool)e.ControlData.Temperature.CompesationOn.Value)
                //    {
                //        b_ukljuci.Content = "ISKLJUČI";
                //        b_ukljuci.Foreground = System.Windows.Media.Brushes.Green;
                //    }
                //    else
                //    {
                //        b_ukljuci.Content = "UKLJUČI";
                //        b_ukljuci.Foreground = System.Windows.Media.Brushes.Black;
                //    }
            }));
        }

        

        private void b_prijavi_Click(object sender, RoutedEventArgs e)
        {
            if (tb_lozinka.Text == "0005SMV")
            {
                App.MainReportInterface.KorisnickoIme = tb_korisnickoIme.Text;
                b_prijavi.Foreground = new SolidColorBrush(Colors.Green);
            }
            else
            {
                App.MainReportInterface.KorisnickoIme = "";
                b_prijavi.Foreground = new SolidColorBrush(Colors.Red);
            }
        }

        private void b_ukljuci_Click(object sender, RoutedEventArgs e)
        {
            //App.PLC.WriteToggle(App.PLC.CONTROL.Temperature.CompesationOn);
        }

       

        private void b_zatvori_Click(object sender, RoutedEventArgs e)
        {
            App.Current.Shutdown();
        }

        private void b_reset_Click(object sender, RoutedEventArgs e)
        {
            //App.pSablja.reset();
            App.pSrh.reset();
            App.pValovitost.reset();
            App.pKut.reset();
            App.MainReportInterface.ManualThicknessMeas = 0;
        }

        private void spremiZaglavljeUStartup(object sender, System.Timers.ElapsedEventArgs e)
        {

            Dispatcher.BeginInvoke(new Action(() =>
            {
                string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
                var theStrings = new List<string>();
                theStrings.Add(tbTipTransformatora.Text);
                theStrings.Add(tbObjekt.Text);
                theStrings.Add(tbTvornickiBroj.Text);
                theStrings.Add(tbVrstaLima.Text);
                theStrings.Add(tbPaketBroj.Text);
                theStrings.Add(tbBrojKoluta.Text);
                theStrings.Add(tbCrtezBroj.Text);
                theStrings.Add(tbProizvodac.Text);
                theStrings.Add(tbSirina.Text);
                theStrings.Add(tbNazivnaDebljina.Text);
               
                // Save:
                File.WriteAllLines(path, theStrings);
            }));
        }

        private void tbTipTransformatora_Loaded(object sender, RoutedEventArgs e)
        {
            string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
           
            if (File.Exists(baseDirectory+path)) 
            {
                var theStrings = File.ReadLines(path);
                if (theStrings.ToArray().Length == 10)
                {
                    tbTipTransformatora.Text = theStrings.ToArray()[0];
                    tbObjekt.Text = theStrings.ToArray()[1];
                    tbTvornickiBroj.Text = theStrings.ToArray()[2];
                    tbVrstaLima.Text = theStrings.ToArray()[3];
                    tbPaketBroj.Text = theStrings.ToArray()[4];
                    tbBrojKoluta.Text = theStrings.ToArray()[5];
                    tbCrtezBroj.Text = theStrings.ToArray()[6];
                    tbProizvodac.Text = theStrings.ToArray()[7];
                    tbSirina.Text = theStrings.ToArray()[8];
                    float temp = 0;
                    Single.TryParse(theStrings.ToArray()[9].Replace(".", ","), out temp);
                    App.MainReportInterface.Valovitost_debljinaLima = temp;
                    //tbNazivnaDebljina.Text = theStrings.ToArray()[9];
                }
            }
            saveSetupTimer.Enabled = true;

        }

        private void b_spremi_Click(object sender, RoutedEventArgs e)
        {
            var theStrings = new List<string>();
            theStrings.Add(tbTipTransformatora.Text);
            theStrings.Add(tbObjekt.Text);
            theStrings.Add(tbTvornickiBroj.Text);
            theStrings.Add(tbVrstaLima.Text);
            theStrings.Add(tbPaketBroj.Text);
            theStrings.Add(tbBrojKoluta.Text);
            theStrings.Add(tbCrtezBroj.Text);
            theStrings.Add(tbProizvodac.Text);
            theStrings.Add(tbSirina.Text);
            theStrings.Add(tbNazivnaDebljina.Text);
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            if (saveFileDialog.ShowDialog() == true)
                File.WriteAllLines(saveFileDialog.FileName, theStrings);
        }

        private void b_ucitaj_Click(object sender, RoutedEventArgs e)
        {
            IEnumerable<string> theStrings;
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
            {
                theStrings = File.ReadLines(openFileDialog.FileName);
                tbTipTransformatora.Text = theStrings.ToArray()[0];
                tbObjekt.Text = theStrings.ToArray()[1];
                tbTvornickiBroj.Text = theStrings.ToArray()[2];
                tbVrstaLima.Text = theStrings.ToArray()[3];
                tbPaketBroj.Text = theStrings.ToArray()[4];
                tbBrojKoluta.Text = theStrings.ToArray()[5];
                tbCrtezBroj.Text = theStrings.ToArray()[6];
                tbProizvodac.Text = theStrings.ToArray()[7];
                tbSirina.Text = theStrings.ToArray()[8];
                float temp=0;
                Single.TryParse(theStrings.ToArray()[9].Replace(".", ","), out temp);
                App.MainReportInterface.Valovitost_debljinaLima = temp;
              //  tbNazivnaDebljina.Text = theStrings.ToArray()[9];
            }
            
           
        }


    }
}
