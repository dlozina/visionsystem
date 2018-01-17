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
        public static PVisine pVisine;
        public static PKut pKut;
        public static PRucno pRucno;
        public static PLCInterface PLC;
        //public static string  ReportPath = "reports/ControlSheet.xlsx";
        public static MainWindow mwHandle;
        //public static Algoritmi AutoSearch = new Algoritmi();
        public static HDevelopExport HDevExp;
        private bool edgeDetection1 =false;
        private bool edgeDetection2 = false;
        private bool edgeDetection3 = false;
        private bool edgeDetection4 = false;
        private bool edgeDetection5 = false;
        
        public App()
        {
            System.Diagnostics.Process.GetCurrentProcess().PriorityClass = System.Diagnostics.ProcessPriorityClass.High;
            InitializeComponent();
           
            PLC = ((PLCInterface)Application.Current.FindResource("PLCinterf"));
            pIzvjestaji = new PIzvjestaji();
            pPostavke = new PPostavke();
            pDimenzije = new PDimenzije();
            pSrh = new PSrh();
            pValovitost = new PValovitost();
            pVisine = new PVisine();
            pKut = new PKut();
            pRucno = new PRucno();
            pRucno = new PRucno();

            App.PLC.StartCyclic();
            App.PLC.Update_Online_Flag += new PLCInterface.OnlineMarker(PLCInterface_PLCOnlineChanged);
            App.PLC.Update_100_ms += new PLCInterface.UpdateHandler(PLC_Update_100_ms);
            App.HDevExp.UpdateResult += new HDevelopExport.UpdateHandler(HalconUpdate);
            App.HDevExp.PorosityDetected += new HDevelopExport.PorosityDetectedEventHandler(PorosityIsDetected);
            App.HDevExp.PorosityDetectionStart += new HDevelopExport.PorosityDetectionStartEventHandler(DetectionStart);
        }

        private void PLC_Update_100_ms(PLCInterface sender, PLCInterfaceEventArgs e)
        {
            String msg = "";

            // Start analize slike D1 PRVOG RUBA S1 ********************************************************************
            if (((bool)e.StatusData.Kamere.CAM4ZahtjevZaAnalizomS1.Value) && ((float)e.StatusData.MjerenjeDiametara.BrojPonavljanjaSekvence.Value == 0.0f) && (!edgeDetection1)) //Edge detection
            {
                Thread d1meassureS1 = new Thread(new ThreadStart(HDevExp.RunHalcon1)); // d1meassureS1.name = "Thread D1S1
                d1meassureS1.Name = "Thread D1S1";
                d1meassureS1.Start();
            }

            // Start analize slike D1 DRUGOG RUBA S2 *******************************************************************
            if (((bool)e.StatusData.Kamere.CAM4ZahtjevZaAnalizomS2.Value) && ((float)e.StatusData.MjerenjeDiametara.BrojPonavljanjaSekvence.Value == 0.0f) && (!edgeDetection2))
            {
                Thread d1meassureS2 = new Thread(new ThreadStart(HDevExp.RunHalcon2));
                d1meassureS2.Name = "Thread D1S2";
                d1meassureS2.Start();
            }

            // Start analize slike D2 DRUGOG RUBA S1 *******************************************************************
            if (((bool)e.StatusData.Kamere.CAM4ZahtjevZaAnalizomS1.Value) && ((float)e.StatusData.MjerenjeDiametara.BrojPonavljanjaSekvence.Value == 1.0f) && (!edgeDetection1))
            {
                Thread d2meassureS1 = new Thread(new ThreadStart(HDevExp.RunHalcon3));
                d2meassureS1.Name = "Thread D2S1";
                d2meassureS1.Start();
            }

            // Start analize slike D2 DRUGOG RUBA S2 *******************************************************************
            if (((bool)e.StatusData.Kamere.CAM4ZahtjevZaAnalizomS2.Value) && ((float)e.StatusData.MjerenjeDiametara.BrojPonavljanjaSekvence.Value == 1.0f) && (!edgeDetection2))
            {
                Thread d2meassureS2 = new Thread(new ThreadStart(HDevExp.RunHalcon4));
                d2meassureS2.Name = "Thread D2S2";
                d2meassureS2.Start();
            }

            // Start analize slike D3 DRUGOG RUBA S1 *******************************************************************
            if (((bool)e.StatusData.Kamere.CAM4ZahtjevZaAnalizomS1.Value) && ((float)e.StatusData.MjerenjeDiametara.BrojPonavljanjaSekvence.Value == 2.0f) && (!edgeDetection1))
            {
                Thread d3meassureS1 = new Thread(new ThreadStart(HDevExp.RunHalcon5));
                d3meassureS1.Name = "Thread D3S1";
                d3meassureS1.Start();
            }

            // Start analize slike D3 DRUGOG RUBA S2 *******************************************************************
            if (((bool)e.StatusData.Kamere.CAM4ZahtjevZaAnalizomS2.Value) && ((float)e.StatusData.MjerenjeDiametara.BrojPonavljanjaSekvence.Value == 2.0f) && (!edgeDetection2))
            {
                Thread d3meassureS2 = new Thread(new ThreadStart(HDevExp.RunHalcon6));
                d3meassureS2.Name = "Thread D3S2";
                d3meassureS2.Start();
            }

            // Start analize slike D4 DRUGOG RUBA S1 *******************************************************************
            if (((bool)e.StatusData.Kamere.CAM4ZahtjevZaAnalizomS1.Value) && ((float)e.StatusData.MjerenjeDiametara.BrojPonavljanjaSekvence.Value == 3.0f) && (!edgeDetection1))
            {
                Thread d4meassureS1 = new Thread(new ThreadStart(HDevExp.RunHalcon7));
                d4meassureS1.Name = "Thread D4S1";
                d4meassureS1.Start();
            }

            // Start analize slike D4 DRUGOG RUBA S2 *******************************************************************
            if (((bool)e.StatusData.Kamere.CAM4ZahtjevZaAnalizomS2.Value) && ((float)e.StatusData.MjerenjeDiametara.BrojPonavljanjaSekvence.Value == 3.0f) && (!edgeDetection2))
            {
                Thread d4meassureS2 = new Thread(new ThreadStart(HDevExp.RunHalcon8));
                d4meassureS2.Name = "Thread D4S2";
                d4meassureS2.Start();
            }

            // Start analize slike za detekciju POROZNOSTI VERTIKALNO ***************************************************
            if (((bool)e.StatusData.Kamere.CAM2ZahtjevZaAnalizom.Value) && (!edgeDetection3))
            {
                // We call public method in class pShr
                Thread porosityverth = new Thread(new ThreadStart(pSrh.PorosityVerWindow));
                porosityverth.Name = "Thread PorosityVer";
                porosityverth.Start();
            }

            // Start analize slike za detekciju POROZNOSTI HORIZONTALNO ***********************************************
            if (((bool)e.StatusData.Kamere.CAM3ZahtjevZaAnalizom.Value) && (!edgeDetection4))
            {
                // We call public method in class pShr
                Thread porosityhorth = new Thread(new ThreadStart(pSrh.PorosityHorWindow));
                porosityhorth.Name = "Thread PorosityHor";
                porosityhorth.Start();
            }

            // R-Os je prosla 360 i nije nasla porozni dio
            if (((bool)e.StatusData.MjerenjePoroznosti.Gotovo.Value) && (!edgeDetection5))
            {
                HDevExp.Porositydetectedver = true;
            }

            // Edge detection help marker
            edgeDetection1 = (bool)e.StatusData.Kamere.CAM4ZahtjevZaAnalizomS1.Value == true;
            edgeDetection2 = (bool)e.StatusData.Kamere.CAM4ZahtjevZaAnalizomS2.Value == true;
            edgeDetection3 = (bool)e.StatusData.Kamere.CAM2ZahtjevZaAnalizom.Value == true; 
            edgeDetection4 = (bool)e.StatusData.Kamere.CAM3ZahtjevZaAnalizom.Value == true; 
            edgeDetection5 = (bool)e.StatusData.MjerenjePoroznosti.Gotovo.Value == true;

            if (mwHandle != null)
            {
                mwHandle.tb_statusMessage.Dispatcher.BeginInvoke((Action)(() => { mwHandle.tb_statusMessage.Text = msg; }));
            }
        }

        // Event handler koji se poziva kad zavrsi analiza slike za mjerenje diametara
        private void HalconUpdate(HDevelopExport sender, HalconEventArgs e)
        {
            App.PLC.WriteTag(PLC.STATUS.Kamere.CAM4Rezultat, e.PXvalue);
            App.PLC.WriteTag(PLC.STATUS.Kamere.CAM4AnalizaOk, true);
            App.PLC.WriteTag(PLC.STATUS.Kamere.CAM4AnalizaOk, false);
        }

        private void DetectionStart(object source, EventArgs e)
        {
            App.PLC.WriteTag(PLC.STATUS.Kamere.CAM2AnalizaOk, true);
            App.PLC.WriteTag(PLC.STATUS.Kamere.CAM2AnalizaOk, false);
        }

        private void PorosityIsDetected(object source, EventArgs e)
        {
            App.PLC.WriteTag(PLC.STATUS.MjerenjePoroznosti.PoroznostPronadena, true);
            App.PLC.WriteTag(PLC.STATUS.MjerenjePoroznosti.PoroznostPronadena, false);
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
