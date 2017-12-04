using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Windows.Shapes;
using System.Windows.Media;
using System.Threading.Tasks;
using System.Windows;
using System.Threading;
using HalconDotNet;


namespace VizijskiSustavWPF
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static PIzvjestaji pIzvjestaji;
        public static PPostavke pPostavke;
        public static PDimenzije pDimenzije; 
        public static PSrh pSrh;
        public static PValovitost pValovitost;
        public static PSablja pSablja;
        public static PKut pKut;
        public static PRucno pRucno;
        public static PLCInterface PLC;
        public static string  ReportPath = "reports/ControlSheet.xlsx";
        public static MainWindow mwHandle;
        public static ReportInterface MainReportInterface;
        public static Algoritmi AutoSearch = new Algoritmi();
        public static HDevelopExport HDevExp;
        private bool edgeDetection1 =false;
        private bool edgeDetection2 = false;



        public App()
        {
            System.Diagnostics.Process.GetCurrentProcess().PriorityClass = System.Diagnostics.ProcessPriorityClass.High;
            InitializeComponent();
            MainReportInterface = ((ReportInterface)Application.Current.FindResource("MainReport"));
           
            PLC = ((PLCInterface)Application.Current.FindResource("PLCinterf"));
            pIzvjestaji = new PIzvjestaji();
            pPostavke = new PPostavke();
            pDimenzije = new PDimenzije();
            pSrh = new PSrh();
            pValovitost = new PValovitost();
            pSablja = new PSablja();
            pKut = new PKut();
            pRucno = new PRucno();
            pRucno = new PRucno();

            App.PLC.StartCyclic();
            App.PLC.Update_Online_Flag += new PLCInterface.OnlineMarker(PLCInterface_PLCOnlineChanged);
            App.PLC.Update_100_ms += new PLCInterface.UpdateHandler(PLC_Update_100_ms);
            App.HDevExp.UpdateResult += new HDevelopExport.UpdateHandler(HalconUpdate);

        }

        private void PLC_Update_100_ms(PLCInterface sender, PLCInterfaceEventArgs e)
        {
            String msg = "";



            // Start analize slike D1 PRVOG RUBA S1 ********************************************************************
            if (((bool)e.StatusData.Kamere.CAM4ZahtjevZaAnalizomS1.Value) && ((float)e.StatusData.MjerenjeDiametara.BrojPonavljanjaSekvence.Value == 0.0f) && (!edgeDetection1)) //Edge detection
            {

                //HDevExp.InitHalcon(); // Nije potrebno, sluzi samo za prikaz
                Thread exportThread = new Thread(new ThreadStart(HDevExp.RunHalcon1));
                exportThread.Start();
            }


            // Start analize slike D1 DRUGOG RUBA S2 *******************************************************************
            if (((bool)e.StatusData.Kamere.CAM4ZahtjevZaAnalizomS2.Value) && ((float)e.StatusData.MjerenjeDiametara.BrojPonavljanjaSekvence.Value == 0.0f) && (!edgeDetection2))
            {

                //HDevExp.InitHalcon(); // Nije potrebno, sluzi samo za prikaz
                //Thread exportThread = new Thread(new ThreadStart(this.RunDia1S2));
                Thread exportThread = new Thread(new ThreadStart(HDevExp.RunHalcon2));
                exportThread.Start();

            }


            // Start analize slike D2 DRUGOG RUBA S1 *******************************************************************
            if (((bool)e.StatusData.Kamere.CAM4ZahtjevZaAnalizomS1.Value) && ((float)e.StatusData.MjerenjeDiametara.BrojPonavljanjaSekvence.Value == 1.0f) && (!edgeDetection1))
            {

                //HDevExp.InitHalcon(); // Nije potrebno, sluzi samo za prikaz
                Thread exportThread = new Thread(new ThreadStart(HDevExp.RunHalcon3));
                exportThread.Start();
            }


            // Start analize slike D2 DRUGOG RUBA S2 *******************************************************************
            if (((bool)e.StatusData.Kamere.CAM4ZahtjevZaAnalizomS2.Value) && ((float)e.StatusData.MjerenjeDiametara.BrojPonavljanjaSekvence.Value == 1.0f) && (!edgeDetection2))
            {

                //HDevExp.InitHalcon(); // Nije potrebno, sluzi samo za prikaz
                Thread exportThread = new Thread(new ThreadStart(HDevExp.RunHalcon4));
                exportThread.Start();
            }


            // Start analize slike D3 DRUGOG RUBA S1 *******************************************************************
            if (((bool)e.StatusData.Kamere.CAM4ZahtjevZaAnalizomS1.Value) && ((float)e.StatusData.MjerenjeDiametara.BrojPonavljanjaSekvence.Value == 2.0f) && (!edgeDetection1))
            {

                //HDevExp.InitHalcon(); // Nije potrebno, sluzi samo za prikaz
                Thread exportThread = new Thread(new ThreadStart(HDevExp.RunHalcon5));
                exportThread.Start();
            }


            // Start analize slike D3 DRUGOG RUBA S2 *******************************************************************
            if (((bool)e.StatusData.Kamere.CAM4ZahtjevZaAnalizomS2.Value) && ((float)e.StatusData.MjerenjeDiametara.BrojPonavljanjaSekvence.Value == 2.0f) && (!edgeDetection2))
            {

                //HDevExp.InitHalcon(); // Nije potrebno, sluzi samo za prikaz
                Thread exportThread = new Thread(new ThreadStart(HDevExp.RunHalcon6));
                exportThread.Start();
            }


            // Start analize slike D4 DRUGOG RUBA S1 *******************************************************************
            if (((bool)e.StatusData.Kamere.CAM4ZahtjevZaAnalizomS1.Value) && ((float)e.StatusData.MjerenjeDiametara.BrojPonavljanjaSekvence.Value == 3.0f) && (!edgeDetection1))
            {

                //HDevExp.InitHalcon(); // Nije potrebno, sluzi samo za prikaz
                Thread exportThread = new Thread(new ThreadStart(HDevExp.RunHalcon7));
                exportThread.Start();
            }


            // Start analize slike D4 DRUGOG RUBA S2 *******************************************************************
            if (((bool)e.StatusData.Kamere.CAM4ZahtjevZaAnalizomS2.Value) && ((float)e.StatusData.MjerenjeDiametara.BrojPonavljanjaSekvence.Value == 3.0f) && (!edgeDetection2))
            {

                //HDevExp.InitHalcon(); // Nije potrebno, sluzi samo za prikaz
                Thread exportThread = new Thread(new ThreadStart(HDevExp.RunHalcon8));
                exportThread.Start();
            }


            edgeDetection1 = (bool)e.StatusData.Kamere.CAM4ZahtjevZaAnalizomS1.Value == true;  //Edge detection help marker
            edgeDetection2 = (bool)e.StatusData.Kamere.CAM4ZahtjevZaAnalizomS2.Value == true;  //Edge detection help marker

            if (mwHandle != null)
            {
                mwHandle.tb_statusMessage.Dispatcher.BeginInvoke((Action)(() => { mwHandle.tb_statusMessage.Text = msg; }));
            }


        }

        // Diametar 1 strana 1
        private void RunDia1S1()
        {

            HDevExp.RunHalcon1();

        }
        // Diametar 1 strana 2
        private void RunDia1S2()
        {

            HDevExp.RunHalcon2();

        }
        // Diametar 2 strana 1
        private void RunDia2S1()
        {

            HDevExp.RunHalcon3();

        }
        // Diametar 2 strana 2
        private void RunDia2S2()
        {

            HDevExp.RunHalcon4();

        }
        // Diametar 3 strana 1
        private void RunDia3S1()
        {

            HDevExp.RunHalcon5();

        }
        // Diametar 3 strana 2
        private void RunDia3S2()
        {

            HDevExp.RunHalcon6();

        }
        // Diametar 4 strana 1
        private void RunDia4S1()
        {

            HDevExp.RunHalcon7();

        }
        // Diametar 4 strana 2
        private void RunDia4S2()
        {

            HDevExp.RunHalcon8();

        }

        // Event handler koji se poziva kad zavrsi analiza slike za mjerenje promjera
        private void HalconUpdate(HDevelopExport sender, HalconEventArgs e)
        {
            App.PLC.WriteTag(PLC.STATUS.Kamere.CAM4Rezultat, e.PXvalue);
            App.PLC.WriteTag(PLC.STATUS.Kamere.CAM4AnalizaOk, true);
            App.PLC.WriteTag(PLC.STATUS.Kamere.CAM4AnalizaOk, false);
        }

        // Event handler koji se poziva kad PLC postane online ili offline (Ethernet kabel se spoji ili odspoji).
        private void PLCInterface_PLCOnlineChanged(object sender, OnlineMarkerEventArgs e)
        {
            if (e.OnlineMark)
            {
                mwHandle.onlineFlag.Dispatcher.BeginInvoke((Action)(() => {mwHandle.onlineFlag.Fill = new LinearGradientBrush(Colors.Green, Colors.White, 0.0); }));
                mwHandle.t_connectionStatus.Dispatcher.BeginInvoke((Action)(() => { mwHandle.t_connectionStatus.Text = "PLC Status: Online"; }));
            }
            else
            {
                mwHandle.onlineFlag.Dispatcher.BeginInvoke((Action)(() => { mwHandle.onlineFlag.Fill = new LinearGradientBrush((Color)ColorConverter.ConvertFromString("#FF979797"), Colors.White, 0.0); }));
                mwHandle.t_connectionStatus.Dispatcher.BeginInvoke((Action)(() => { mwHandle.t_connectionStatus.Text = "PLC Status: Offline"; }));
            }
        }

    }
}
