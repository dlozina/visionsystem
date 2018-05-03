using System;
using System.Windows.Media;
using System.Windows;
using System.Threading;
using Newtonsoft.Json;
using System.IO;
using VizijskiSustavWPF.Reports;
using System.Collections.Generic;
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
        // Database
        public static List<ReportInterface.DimensionLine> savedata = new List<ReportInterface.DimensionLine>();

        public App()
        {
            InitializeComponent();
            // Load saved data from JSON file
            string DataBaseFileName = "savedata.JSON";
            string DataBasePath = Path.Combine(Environment.CurrentDirectory, @"database", DataBaseFileName);
            String JSONstring = File.ReadAllText(DataBasePath);
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

        // Variable for empty window call
        HTuple windowID = new HTuple();

        public class UserInputData
        {
            // Data from textbox from another class
            static float _dijametar1;
            public static float Dijametar1
            {
                get { return _dijametar1; }
                set { value = _dijametar1; }
            }
            static float _dijametar2;
            public static float Dijametar2
            {
                get { return _dijametar2; }
                set { value = _dijametar2; }
            }
        }

        public static void ResetData()
        {
            savedata.Clear();
            string json = JsonConvert.SerializeObject(savedata.ToArray());
            string DataBaseFileName = "savedata.JSON";
            string DataBasePath = Path.Combine(Environment.CurrentDirectory, @"database", DataBaseFileName);
            String JSONstring = File.ReadAllText(DataBasePath);
            //File.WriteAllText(@"C:\Users\kontakt\Documents\Work\Projekti\Vision_System_OMCO\App\VisionApp\Vizijski_Omco_WPF\bin\x64\Debug\database\savedata.JSON", json);
            File.WriteAllText(DataBasePath, json);
        }

        private void PLC_Update_100_ms(PLCInterface sender, PLCInterfaceEventArgs e)


        {
            String msg = "SISTEM SPREMAN";

            // Start analize slike D1 PRVOG RUBA S1 ********************************************************************
            if (((bool)e.StatusData.Kamere.CAM4ZahtjevZaAnalizomS1.Value) && ((float)e.StatusData.MjerenjeDiametara.BrojPonavljanjaSekvence.Value == 1.0f) && (!_edgeDetection1)) //Edge detection
            {
                Thread d1meassureS1 = new Thread(() => HDevExp.RunHalcon1(windowID)); // d1meassureS1.name = "Thread D1S1
                d1meassureS1.Name = "Thread D1S1";
                d1meassureS1.Start();
            }

            // Start analize slike D1 DRUGOG RUBA S2 *******************************************************************
            if (((bool)e.StatusData.Kamere.CAM4ZahtjevZaAnalizomS2.Value) && ((float)e.StatusData.MjerenjeDiametara.BrojPonavljanjaSekvence.Value == 1.0f) && (!_edgeDetection2))
            {
                //Thread d1meassureS2 = new Thread(new ThreadStart(HDevExp.RunHalcon2));
                Thread d1meassureS2 = new Thread(() => HDevExp.RunHalcon2(windowID));
                d1meassureS2.Name = "Thread D1S2";
                d1meassureS2.Start();
            }

            // Start analize slike D2 DRUGOG RUBA S1 *******************************************************************
            if (((bool)e.StatusData.Kamere.CAM4ZahtjevZaAnalizomS1.Value) && ((float)e.StatusData.MjerenjeDiametara.BrojPonavljanjaSekvence.Value == 2.0f) && (!_edgeDetection1))
            {
                //Thread d2meassureS1 = new Thread(new ThreadStart(HDevExp.RunHalcon3));
                Thread d1meassureS2 = new Thread(() => HDevExp.RunHalcon3(windowID));
                d1meassureS2.Name = "Thread D2S1";
                d1meassureS2.Start();
            }

            // Start analize slike D2 DRUGOG RUBA S2 *******************************************************************
            if (((bool)e.StatusData.Kamere.CAM4ZahtjevZaAnalizomS2.Value) && ((float)e.StatusData.MjerenjeDiametara.BrojPonavljanjaSekvence.Value == 2.0f) && (!_edgeDetection2))
            {
                //Thread d2meassureS2 = new Thread(new ThreadStart(HDevExp.RunHalcon4));
                Thread d2meassureS2 = new Thread(() => HDevExp.RunHalcon4(windowID));
                d2meassureS2.Name = "Thread D2S2";
                d2meassureS2.Start();
            }

            // Start analize slike D3 DRUGOG RUBA S1 *******************************************************************
            if (((bool)e.StatusData.Kamere.CAM4ZahtjevZaAnalizomS1.Value) && ((float)e.StatusData.MjerenjeDiametara.BrojPonavljanjaSekvence.Value == 3.0f) && (!_edgeDetection1))
            {
                //Thread d3meassureS1 = new Thread(new ThreadStart(HDevExp.RunHalcon5));
                Thread d3meassureS1 = new Thread(() => HDevExp.RunHalcon5(windowID));
                d3meassureS1.Name = "Thread D3S1";
                d3meassureS1.Start();
            }

            // Start analize slike D3 DRUGOG RUBA S2 *******************************************************************
            if (((bool)e.StatusData.Kamere.CAM4ZahtjevZaAnalizomS2.Value) && ((float)e.StatusData.MjerenjeDiametara.BrojPonavljanjaSekvence.Value == 3.0f) && (!_edgeDetection2))
            {
                //Thread d3meassureS2 = new Thread(new ThreadStart(HDevExp.RunHalcon6));
                Thread d3meassureS2 = new Thread(() => HDevExp.RunHalcon6(windowID));
                d3meassureS2.Name = "Thread D3S2";
                d3meassureS2.Start();
            }

            // Start analize slike D4 DRUGOG RUBA S1 *******************************************************************
            if (((bool)e.StatusData.Kamere.CAM4ZahtjevZaAnalizomS1.Value) && ((float)e.StatusData.MjerenjeDiametara.BrojPonavljanjaSekvence.Value == 4.0f) && (!_edgeDetection1))
            {
                //Thread d4meassureS1 = new Thread(new ThreadStart(HDevExp.RunHalcon7));
                Thread d4meassureS1 = new Thread(() => HDevExp.RunHalcon7(windowID));
                d4meassureS1.Name = "Thread D4S1";
                d4meassureS1.Start();
            }

            // Start analize slike D4 DRUGOG RUBA S2 *******************************************************************
            if (((bool)e.StatusData.Kamere.CAM4ZahtjevZaAnalizomS2.Value) && ((float)e.StatusData.MjerenjeDiametara.BrojPonavljanjaSekvence.Value == 4.0f) && (!_edgeDetection2))
            {
                //Thread d4meassureS2 = new Thread(new ThreadStart(HDevExp.RunHalcon8));
                Thread d4meassureS2 = new Thread(() => HDevExp.RunHalcon8(windowID));
                d4meassureS2.Name = "Thread D4S2";
                d4meassureS2.Start();
            }

            // Start analize slike za detekciju POROZNOSTI VERTIKALNO ***************************************************
            if (((bool)e.StatusData.Kamere.CAM2ZahtjevZaAnalizom.Value) && (!_edgeDetection3))
            {
                Thread porosityverth = new Thread(new ThreadStart(pPoroznost.PorosityVerWindow));
                porosityverth.Name = "Thread PorosityVer";
                porosityverth.Start();

                // Start sa display-om, thread error
                //pPoroznost.PorosityVerWindow();
            }

            // Start analize slike za detekciju POROZNOSTI HORIZONTALNO ************************************************
            if (((bool)e.StatusData.Kamere.CAM3ZahtjevZaAnalizom.Value) && (!_edgeDetection4))
            {
                Thread porosityhorth = new Thread(new ThreadStart(pPoroznost.PorosityHorWindow));
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
                    String = "No.1",
                    Poroznost = true,
                    // D1
                    NazivnoD1 = (float)e.StatusData.Upisanevrijednosti.Dijametar1.Value,
                    MjerenoD1 = (float)e.StatusData.MjerenjeDiametara.Diametar1.Value,
                    DeltaPlusD1 = (float)e.StatusData.Upisanevrijednosti.Dijametar1DeltaPlus.Value,
                    DeltaMinusD1 = (float)e.StatusData.Upisanevrijednosti.Dijametar1DeltaMinus.Value,
                    // D2
                    NazivnoD2 = (float)e.StatusData.Upisanevrijednosti.Dijametar2.Value,
                    MjerenoD2 = (float)e.StatusData.MjerenjeDiametara.Diametar2.Value,
                    DeltaPlusD2 = (float)e.StatusData.Upisanevrijednosti.Dijametar2DeltaPlus.Value,
                    DeltaMinusD2 = (float)e.StatusData.Upisanevrijednosti.Dijametar2DeltaMinus.Value,
                    // D3
                    NazivnoD3 = (float)e.StatusData.Upisanevrijednosti.Dijametar3.Value,
                    MjerenoD3 = (float)e.StatusData.MjerenjeDiametara.Diametar3.Value,
                    DeltaPlusD3 = (float)e.StatusData.Upisanevrijednosti.Dijametar3DeltaPlus.Value,
                    DeltaMinusD3 = (float)e.StatusData.Upisanevrijednosti.Dijametar3DeltaMinus.Value,
                    // D4
                    NazivnoD4 = (float)e.StatusData.Upisanevrijednosti.Dijametar4.Value,
                    MjerenoD4 = (float)e.StatusData.MjerenjeDiametara.Diametar4.Value,
                    DeltaPlusD4 = (float)e.StatusData.Upisanevrijednosti.Dijametar4DeltaPlus.Value,
                    DeltaMinusD4 = (float)e.StatusData.Upisanevrijednosti.Dijametar4DeltaMinus.Value,
                    // D5
                    NazivnoD5 = (float)e.StatusData.Upisanevrijednosti.Dijametar5.Value,
                    MjerenoD5 = (float)e.StatusData.MjerenjeDiametara.Diametar5.Value,
                    DeltaPlusD5 = (float)e.StatusData.Upisanevrijednosti.Dijametar5DeltaPlus.Value,
                    DeltaMinusD5 = (float)e.StatusData.Upisanevrijednosti.Dijametar5DeltaMinus.Value,
                    // V1
                    NazivnoV1 = (float)e.StatusData.Upisanevrijednosti.Visina1.Value,
                    MjerenoV1 = (float)e.StatusData.MjerenjeTicalom.Visina1.Value,
                    DeltaPlusV1 = (float)e.StatusData.Upisanevrijednosti.Visina1DeltaPlus.Value,
                    DeltaMinusV1 = (float)e.StatusData.Upisanevrijednosti.Visina1DeltaMinus.Value,
                    // V2
                    NazivnoV2 = (float)e.StatusData.Upisanevrijednosti.Visina2.Value,
                    MjerenoV2 = (float)e.StatusData.MjerenjeTicalom.Visina2.Value,
                    DeltaPlusV2 = (float)e.StatusData.Upisanevrijednosti.Visina2DeltaPlus.Value,
                    DeltaMinusV2 = (float)e.StatusData.Upisanevrijednosti.Visina2DeltaMinus.Value,
                    // V3
                    NazivnoV3 = (float)e.StatusData.Upisanevrijednosti.Visina3.Value,
                    //NazivnoV3 = ((float)e.StatusData.Upisanevrijednosti.Visina2.Value - (float)e.StatusData.Upisanevrijednosti.Visina3.Value),
                    MjerenoV3 = (float)e.StatusData.MjerenjeTicalom.Visina3.Value,
                    DeltaPlusV3 = (float)e.StatusData.Upisanevrijednosti.Visina3DeltaPlus.Value,
                    DeltaMinusV3 = (float)e.StatusData.Upisanevrijednosti.Visina3DeltaMinus.Value,
                });

                string json = JsonConvert.SerializeObject(savedata.ToArray(), Formatting.Indented);
                string DataBaseFileName = "savedata.JSON";
                string DataBasePath = Path.Combine(Environment.CurrentDirectory, @"database", DataBaseFileName);
                File.WriteAllText(DataBasePath, json);
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
            Thread.Sleep(100);
            App.PLC.WriteTag(PLC.STATUS.Kamere.CAM4AnalizaOk, false);
        }
        // Event handler koji se poziva kada zavrsi analiza slike za pick
        private static void PickUpdate(HALCON.HDevelopExport sender, HalconEventArgs e)
        {
            App.PLC.WriteTag(PLC.STATUS.Kamere.CAM1RezultatX, e.RXcord);
            App.PLC.WriteTag(PLC.STATUS.Kamere.CAM1RezultatY, e.RYcord);
            App.PLC.WriteTag(PLC.STATUS.Kamere.CAM1RezultatAngle, e.AngleDeg);
            App.PLC.WriteTag(PLC.STATUS.Kamere.CAM1RezultatWorkpieceDiameter, e.WorkpieceDiameter);
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

        // Control
        public static void ActivateControlD1S1()
        {
            App.PLC.WriteTag(PLC.CONTROL.UcenjeBool.IdiUD1S1, true);
            App.PLC.WriteTag(PLC.CONTROL.UcenjeBool.IdiUD1S1, false);
        }

        public static void ActivateControlD1S2()
        {
            App.PLC.WriteTag(PLC.CONTROL.UcenjeBool.IdiUD1S2, true);
            App.PLC.WriteTag(PLC.CONTROL.UcenjeBool.IdiUD1S2, false);
        }

        public static void ActivateControlD2S1()
        {
            App.PLC.WriteTag(PLC.CONTROL.UcenjeBool.IdiUD2S1, true);
            App.PLC.WriteTag(PLC.CONTROL.UcenjeBool.IdiUD2S1, false);
        }

        public static void ActivateControlD2S2()
        {
            App.PLC.WriteTag(PLC.CONTROL.UcenjeBool.IdiUD2S2, true);
            App.PLC.WriteTag(PLC.CONTROL.UcenjeBool.IdiUD2S2, false);
        }

        public static void ActivateControlD3S1()
        {
            App.PLC.WriteTag(PLC.CONTROL.UcenjeBool.IdiUD3S1, true);
            App.PLC.WriteTag(PLC.CONTROL.UcenjeBool.IdiUD3S1, false);
        }

        public static void ActivateControlD3S2()
        {
            App.PLC.WriteTag(PLC.CONTROL.UcenjeBool.IdiUD3S2, true);
            App.PLC.WriteTag(PLC.CONTROL.UcenjeBool.IdiUD3S2, false);
        }

        public static void ActivateControlD4S1()
        {
            App.PLC.WriteTag(PLC.CONTROL.UcenjeBool.IdiUD4S1, true);
            App.PLC.WriteTag(PLC.CONTROL.UcenjeBool.IdiUD4S1, false);
        }

        public static void ActivateControlD4S2()
        {
            App.PLC.WriteTag(PLC.CONTROL.UcenjeBool.IdiUD4S2, true);
            App.PLC.WriteTag(PLC.CONTROL.UcenjeBool.IdiUD4S2, false);
        }

        public static void ActivateControlD5S1()
        {
            App.PLC.WriteTag(PLC.CONTROL.UcenjeBool.IdiUD5S1, true);
            App.PLC.WriteTag(PLC.CONTROL.UcenjeBool.IdiUD5S1, false);
        }

        public static void ActivateControlD5S2()
        {
            App.PLC.WriteTag(PLC.CONTROL.UcenjeBool.IdiUD5S2, true);
            App.PLC.WriteTag(PLC.CONTROL.UcenjeBool.IdiUD5S2, false);
        }

        // Teach
        public static void ActivateTeachD1S1()
        {
            App.PLC.WriteTag(PLC.CONTROL.UcenjeBool.NauciD1S1, true);
            App.PLC.WriteTag(PLC.CONTROL.UcenjeBool.NauciD1S1, false);
        }

        public static void ActivateTeachD1S2()
        {
            App.PLC.WriteTag(PLC.CONTROL.UcenjeBool.NauciD1S2, true);
            App.PLC.WriteTag(PLC.CONTROL.UcenjeBool.NauciD1S2, false);
        }

        public static void ActivateTeachD2S1()
        {
            App.PLC.WriteTag(PLC.CONTROL.UcenjeBool.NauciD2S1, true);
            App.PLC.WriteTag(PLC.CONTROL.UcenjeBool.NauciD2S1, false);
        }

        public static void ActivateTeachD2S2()
        {
            App.PLC.WriteTag(PLC.CONTROL.UcenjeBool.NauciD2S2, true);
            App.PLC.WriteTag(PLC.CONTROL.UcenjeBool.NauciD2S2, false);
        }
        public static void ActivateTeachD3S1()
        {
            App.PLC.WriteTag(PLC.CONTROL.UcenjeBool.NauciD3S1, true);
            App.PLC.WriteTag(PLC.CONTROL.UcenjeBool.NauciD3S1, false);
        }

        public static void ActivateTeachD3S2()
        {
            App.PLC.WriteTag(PLC.CONTROL.UcenjeBool.NauciD3S2, true);
            App.PLC.WriteTag(PLC.CONTROL.UcenjeBool.NauciD3S2, false);
        }

        public static void ActivateTeachD4S1()
        {
            App.PLC.WriteTag(PLC.CONTROL.UcenjeBool.NauciD4S1, true);
            App.PLC.WriteTag(PLC.CONTROL.UcenjeBool.NauciD4S1, false);
        }

        public static void ActivateTeachD4S2()
        {
            App.PLC.WriteTag(PLC.CONTROL.UcenjeBool.NauciD4S2, true);
            App.PLC.WriteTag(PLC.CONTROL.UcenjeBool.NauciD4S2, false);
        }

        public static void ActivateTeachD5S1()
        {
            App.PLC.WriteTag(PLC.CONTROL.UcenjeBool.NauciD5S1, true);
            App.PLC.WriteTag(PLC.CONTROL.UcenjeBool.NauciD5S1, false);
        }

        public static void ActivateTeachD5S2()
        {
            App.PLC.WriteTag(PLC.CONTROL.UcenjeBool.NauciD5S2, true);
            App.PLC.WriteTag(PLC.CONTROL.UcenjeBool.NauciD5S2, false);
        }

        public static void ActivateControlPorosityPosition()
        {
            //App.PLC.WriteTag(PLC.CONTROL.UcenjeBool.NauciPoroznost, true);
            //App.PLC.WriteTag(PLC.CONTROL.UcenjeBool.NauciPoroznost, false);
        }

        public static void ActivateControlTeachPorosityPosition()
        {
            App.PLC.WriteTag(PLC.CONTROL.UcenjeBool.NauciPoroznost, true);
            App.PLC.WriteTag(PLC.CONTROL.UcenjeBool.NauciPoroznost, false);
        }

        public static void ActivateDimenzije()
        {
            App.PLC.WriteTag(PLC.CONTROL.NacinRada.Dimenzije, true);
        }

        public static void DeactivateDimenzije()
        {
            App.PLC.WriteTag(PLC.CONTROL.NacinRada.Dimenzije, false);
        }

        public static void ActivatePoroznost()
        {
            App.PLC.WriteTag(PLC.CONTROL.NacinRada.Poroznost, true);
        }

        public static void DeactivatePoroznost()
        {
            App.PLC.WriteTag(PLC.CONTROL.NacinRada.Poroznost, false);
        }

        public static void ActivateString()
        {
            App.PLC.WriteTag(PLC.CONTROL.NacinRada.String, true);
        }

        public static void DeactivateString()
        {
            App.PLC.WriteTag(PLC.CONTROL.NacinRada.String, false);
        }

        public static void DiameterLightON()
        {
            App.PLC.WriteTag(PLC.CONTROL.Kamere.RasvjetaZaDimenzije, true);
        }

        public static void DiameterLightOFF()
        {
            App.PLC.WriteTag(PLC.CONTROL.Kamere.RasvjetaZaDimenzije, false);
        }

        public static void PorosityLightON()
        {
            App.PLC.WriteTag(PLC.CONTROL.Kamere.RasvjetaZaPoroznost, true);
        }

        public static void PorosityLightOFF()
        {
            App.PLC.WriteTag(PLC.CONTROL.Kamere.RasvjetaZaPoroznost, false);
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
