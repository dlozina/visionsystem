using System;
using System.Windows.Media;
using System.Windows;
using System.Threading;
using Newtonsoft.Json;
using System.IO;
using VizijskiSustavWPF.Reports;
using System.Collections.Generic;

namespace VizijskiSustavWPF
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static PIzvjestaji pIzvjestaji;
        public static PPostavke pPostavke;
        public static PRobot pRobot; 
        public static PPoroznost pPoroznost;
        public static PUcenje pUcenje;
        public static PVisine pVisine;
        public static PDijametri pDijametri;
        public static PRucno pRucno;
        public static PLCInterface PLC;
        public static MainWindow mwHandle;
        //public static ReportInterface MainReportInterface;
        public static ReportInterface initReportInterface;
        public static HALCON.HDevelopExport HDevExp;
        private bool _edgeDetection1 = false;
        private bool _edgeDetection2 = false;
        private bool _edgeDetection3 = false;
        private bool _edgeDetection4 = false;
        private bool _edgeDetection5 = false;
        private bool _edgeDetection6 = false;
        private bool _edgeDetection7 = false;
        private bool _edgeDetection8 = false;
        private bool _edgeDetection9 = false;
        // Result
        public static List<ReportInterface.DimensionLine> savedata = new List<ReportInterface.DimensionLine>();

        public App()
        {
            InitializeComponent();
            // Load saved data from JSON file
            String JSONstring = File.ReadAllText(@"C:\Users\kontakt\Documents\Work\Projekti\Vision_System_OMCO\App\VisionApp\Vizijski_Omco_WPF\bin\Debug\data\savedata.JSON");
            savedata = JsonConvert.DeserializeObject<List<ReportInterface.DimensionLine>>(JSONstring);
            // If JSON is empty we have null
            if (savedata == null)
            {
                savedata = new List<ReportInterface.DimensionLine>();
            }

            System.Diagnostics.Process.GetCurrentProcess().PriorityClass = System.Diagnostics.ProcessPriorityClass.High;
            //MainReportInterface = ((ReportInterface)Application.Current.FindResource("MainReport"));

            PLC = ((PLCInterface)Application.Current.FindResource("PLCinterf"));
            HDevExp = new HALCON.HDevelopExport();
            pIzvjestaji = new PIzvjestaji();
            pPostavke = new PPostavke();
            pRobot = new PRobot();
            pPoroznost = new PPoroznost();
            pUcenje = new PUcenje();
            pVisine = new PVisine();
            pDijametri = new PDijametri();
            pRucno = new PRucno();
            //Report interface
            //initReportInterface = new ReportInterface();

            PLC.StartCyclic(); // Possible system null reference
            PLC.Update_Online_Flag += new PLCInterface.OnlineMarker(PLCInterface_PLCOnlineChanged);
            PLC.Update_100_ms += new PLCInterface.UpdateHandler(PLC_Update_100_ms);
            HDevExp.UpdateResult += new HALCON.HDevelopExport.UpdateHandler(HalconUpdate);
            HDevExp.UpdateResultPick += new HALCON.HDevelopExport.UpdateHandlerPick(PickUpdate);
            HDevExp.PorosityDetected += new HALCON.HDevelopExport.PorosityDetectedEventHandler(PorosityIsDetected);
            HDevExp.PorosityDetectionStart += new HALCON.HDevelopExport.PorosityDetectionStartEventHandler(DetectionStart);
            HDevExp.PorosityDetectionHorStart += new HALCON.HDevelopExport.PorosityDetectionHorStartEventHandler(DetectionHorStart);
        }

       
        public static void ResetData()
        {
            savedata.Clear();
            string json = JsonConvert.SerializeObject(savedata.ToArray());
            File.WriteAllText(@"C:\Users\kontakt\Documents\Work\Projekti\Vision_System_OMCO\App\VisionApp\Vizijski_Omco_WPF\bin\Debug\data\savedata.JSON", json);
        }

        
        private void PLC_Update_100_ms(PLCInterface sender, PLCInterfaceEventArgs e)
        {
            String msg = "SISTEM SPREMAN";

            // Start analize slike D1 PRVOG RUBA S1 ********************************************************************
            if (((bool)e.StatusData.Kamere.CAM4ZahtjevZaAnalizomS1.Value) && ((float)e.StatusData.MjerenjeDiametara.BrojPonavljanjaSekvence.Value == 0.0f) && (!_edgeDetection1)) //Edge detection
            {
                Thread d1meassureS1 = new Thread(new ThreadStart(HDevExp.RunHalcon1)); // d1meassureS1.name = "Thread D1S1
                d1meassureS1.Name = "Thread D1S1";
                d1meassureS1.Start();
            }

            // Start analize slike D1 DRUGOG RUBA S2 *******************************************************************
            if (((bool)e.StatusData.Kamere.CAM4ZahtjevZaAnalizomS2.Value) && ((float)e.StatusData.MjerenjeDiametara.BrojPonavljanjaSekvence.Value == 0.0f) && (!_edgeDetection2))
            {
                Thread d1meassureS2 = new Thread(new ThreadStart(HDevExp.RunHalcon2));
                d1meassureS2.Name = "Thread D1S2";
                d1meassureS2.Start();
            }

            // Start analize slike D2 DRUGOG RUBA S1 *******************************************************************
            if (((bool)e.StatusData.Kamere.CAM4ZahtjevZaAnalizomS1.Value) && ((float)e.StatusData.MjerenjeDiametara.BrojPonavljanjaSekvence.Value == 1.0f) && (!_edgeDetection1))
            {
                Thread d2meassureS1 = new Thread(new ThreadStart(HDevExp.RunHalcon3));
                d2meassureS1.Name = "Thread D2S1";
                d2meassureS1.Start();
            }

            // Start analize slike D2 DRUGOG RUBA S2 *******************************************************************
            if (((bool)e.StatusData.Kamere.CAM4ZahtjevZaAnalizomS2.Value) && ((float)e.StatusData.MjerenjeDiametara.BrojPonavljanjaSekvence.Value == 1.0f) && (!_edgeDetection2))
            {
                Thread d2meassureS2 = new Thread(new ThreadStart(HDevExp.RunHalcon4));
                d2meassureS2.Name = "Thread D2S2";
                d2meassureS2.Start();
            }

            // Start analize slike D3 DRUGOG RUBA S1 *******************************************************************
            if (((bool)e.StatusData.Kamere.CAM4ZahtjevZaAnalizomS1.Value) && ((float)e.StatusData.MjerenjeDiametara.BrojPonavljanjaSekvence.Value == 2.0f) && (!_edgeDetection1))
            {
                Thread d3meassureS1 = new Thread(new ThreadStart(HDevExp.RunHalcon5));
                d3meassureS1.Name = "Thread D3S1";
                d3meassureS1.Start();
            }

            // Start analize slike D3 DRUGOG RUBA S2 *******************************************************************
            if (((bool)e.StatusData.Kamere.CAM4ZahtjevZaAnalizomS2.Value) && ((float)e.StatusData.MjerenjeDiametara.BrojPonavljanjaSekvence.Value == 2.0f) && (!_edgeDetection2))
            {
                Thread d3meassureS2 = new Thread(new ThreadStart(HDevExp.RunHalcon6));
                d3meassureS2.Name = "Thread D3S2";
                d3meassureS2.Start();
            }

            // Start analize slike D4 DRUGOG RUBA S1 *******************************************************************
            if (((bool)e.StatusData.Kamere.CAM4ZahtjevZaAnalizomS1.Value) && ((float)e.StatusData.MjerenjeDiametara.BrojPonavljanjaSekvence.Value == 3.0f) && (!_edgeDetection1))
            {
                Thread d4meassureS1 = new Thread(new ThreadStart(HDevExp.RunHalcon7));
                d4meassureS1.Name = "Thread D4S1";
                d4meassureS1.Start();
            }

            // Start analize slike D4 DRUGOG RUBA S2 *******************************************************************
            if (((bool)e.StatusData.Kamere.CAM4ZahtjevZaAnalizomS2.Value) && ((float)e.StatusData.MjerenjeDiametara.BrojPonavljanjaSekvence.Value == 3.0f) && (!_edgeDetection2))
            {
                Thread d4meassureS2 = new Thread(new ThreadStart(HDevExp.RunHalcon8));
                d4meassureS2.Name = "Thread D4S2";
                d4meassureS2.Start();
            }

            // Start analize slike za detekciju POROZNOSTI VERTIKALNO ***************************************************
            if (((bool)e.StatusData.Kamere.CAM2ZahtjevZaAnalizom.Value) && (!_edgeDetection3))
            {
                Thread porosityverth = new Thread(new ThreadStart(HDevExp.RunHalcon13));
                porosityverth.Name = "Thread PorosityVer";
                porosityverth.Start();

                // Start sa display-om, thread error
                //pPoroznost.PorosityVerWindow();
            }

            // Start analize slike za detekciju POROZNOSTI HORIZONTALNO ************************************************
            if (((bool)e.StatusData.Kamere.CAM3ZahtjevZaAnalizom.Value) && (!_edgeDetection4))
            {
                Thread porosityhorth = new Thread(new ThreadStart(HDevExp.RunHalcon14));
                porosityhorth.Name = "Thread PorosityHor";
                porosityhorth.Start();

                // Start sa display-om, thread error
                //pPoroznost.PorosityHorWindow();
            }

            // Start analize slike za robot PICK - TRIGGER 1 ***********************************************************
            if (((bool)e.StatusData.Kamere.CAM1ZahtjevZaAnalizomT1.Value) && (!_edgeDetection6))
            {
                // We call public method in class pRobot
                Thread pickTriggerT1 = new Thread(new ThreadStart(pRobot.RobotPickStartT1));
                pickTriggerT1.Name = "Thread pickTriggerT1";
                pickTriggerT1.Start();
            }
            // Start analize slike za robot PICK - TRIGGER 2 ***********************************************************
            if (((bool)e.StatusData.Kamere.CAM1ZahtjevZaAnalizomT2.Value) && (!_edgeDetection7))
            {
                // We call public method in class pRobot
                Thread pickTriggerT2 = new Thread(pRobot.RobotPickStartT2);
                pickTriggerT2.Name = "Thread pickTriggerT2";
                pickTriggerT2.Start();
            }

            // R-Os je prosla 360 i nije nasla porozni dio CAM2 
            if (((bool)e.StatusData.MjerenjePoroznosti.GotovoCAM2.Value) && (!_edgeDetection5))
            {
                HDevExp.Porositydetectedver = true;
                //HDevExp.Porositydetectedhor = true;
            }

            // R-Os je prosla 360 i nije nasla porozni dio CAM3 
            if (((bool)e.StatusData.MjerenjePoroznosti.GotovoCAM3.Value) && (!_edgeDetection8))
            {
                //HDevExp.Porositydetectedver = true;
                HDevExp.Porositydetectedhor = true;
            }

            // Save Data from PLC
            if (((bool)e.StatusData.Automatika.SnimiMjerenja.Value) && (!_edgeDetection9))
            {
                savedata.Add(new ReportInterface.DimensionLine
                {
                  String = "ABC - 1911",
                  Poroznost = false,  
                  Kote = "1",
                  Nazivno = 22.23f,
                  Mjereno = (float)e.StatusData.MjerenjeDiametara.Diametar1.Value,
                  DeltaMinus = -0.02f,
                  DeltaPlus = 0.02f,
                });

                string json = JsonConvert.SerializeObject(savedata.ToArray());
                File.WriteAllText(@"C:\Users\kontakt\Documents\Work\Projekti\Vision_System_OMCO\App\VisionApp\Vizijski_Omco_WPF\bin\Debug\data\savedata.JSON", json);
            }

            // Edge detection help marker
            _edgeDetection1 = (bool)e.StatusData.Kamere.CAM4ZahtjevZaAnalizomS1.Value == true;
            _edgeDetection2 = (bool)e.StatusData.Kamere.CAM4ZahtjevZaAnalizomS2.Value == true;
            _edgeDetection3 = (bool)e.StatusData.Kamere.CAM2ZahtjevZaAnalizom.Value == true; 
            _edgeDetection4 = (bool)e.StatusData.Kamere.CAM3ZahtjevZaAnalizom.Value == true; 
            _edgeDetection5 = (bool)e.StatusData.MjerenjePoroznosti.GotovoCAM2.Value == true;
            _edgeDetection6 = (bool)e.StatusData.Kamere.CAM1ZahtjevZaAnalizomT1.Value == true;
            _edgeDetection7 = (bool)e.StatusData.Kamere.CAM1ZahtjevZaAnalizomT2.Value == true;
            _edgeDetection8 = (bool)e.StatusData.MjerenjePoroznosti.GotovoCAM3.Value == true;
            _edgeDetection9 = (bool)e.StatusData.Automatika.SnimiMjerenja.Value == true;

            if (mwHandle != null)
            {
                mwHandle.tb_statusMessage.Dispatcher.BeginInvoke((Action)(() => { mwHandle.tb_statusMessage.Text = msg; }));
            }
        }

        // Event handler koji se poziva kad zavrsi analiza slike za mjerenje diametara
        private static void HalconUpdate(HALCON.HDevelopExport sender, HalconEventArgs e)
        {
            App.PLC.WriteTag(PLC.STATUS.Kamere.CAM4Rezultat, e.PXvalue);
            App.PLC.WriteTag(PLC.STATUS.Kamere.CAM4AnalizaOk, true);
            App.PLC.WriteTag(PLC.STATUS.Kamere.CAM4AnalizaOk, false);
        }
        // Event handler koji se poziva kada zavrsi analiza slike za pick
        private static void PickUpdate(HALCON.HDevelopExport sender, HalconEventArgs e)
        {
            App.PLC.WriteTag(PLC.STATUS.Kamere.CAM1RezultatX, e.RXcord);
            App.PLC.WriteTag(PLC.STATUS.Kamere.CAM1RezultatY, e.RYcord);
            App.PLC.WriteTag(PLC.STATUS.Kamere.CAM1AnalizaOk, true);
            App.PLC.WriteTag(PLC.STATUS.Kamere.CAM1AnalizaOk, false);
        }

        private static void DetectionStart(object source, EventArgs e)
        {
            App.PLC.WriteTag(PLC.STATUS.Kamere.CAM2AnalizaOk, true);
            App.PLC.WriteTag(PLC.STATUS.Kamere.CAM2AnalizaOk, false);
        }

        private static void DetectionHorStart(object source, EventArgs e)
        {
            App.PLC.WriteTag(PLC.STATUS.Kamere.CAM3AnalizaOk, true);
            App.PLC.WriteTag(PLC.STATUS.Kamere.CAM3AnalizaOk, false);
        }

        private static void PorosityIsDetected(object source, EventArgs e)
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
