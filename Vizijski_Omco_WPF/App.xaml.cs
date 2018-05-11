using System;
using System.Windows.Media;
using System.Windows;
using System.Threading;
using Newtonsoft.Json;
using System.IO;
using VizijskiSustavWPF.Reports;
using System.Collections.Generic;
using HalconDotNet;
using VizijskiSustavWPF.VisionControl;

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
        public static HDevelopExport HDevExp;
        // Run only once flag (When we recive PLC signal)
        private bool _oneCallFlagD1S1 = false;
        private bool _oneCallFlagD1S2 = false;
        private bool _oneCallFlagD2S1 = false;
        private bool _oneCallFlagD2S2 = false;
        private bool _oneCallFlagD3S1 = false;
        private bool _oneCallFlagD3S2 = false;
        private bool _oneCallFlagD4S1 = false;
        private bool _oneCallFlagD4S2 = false;
        private bool _oneCallFlagD5S1 = false;
        private bool _oneCallFlagD5S2 = false;
        private bool _oneCallFlagPorHor = false;
        private bool _onceCallFlagPorVer = false;
        private bool _oneCallFlagPick = false;
        private bool _oneCallFlagPorFound = false;
        private bool _oneCallFlagPorVerNotFound = false;
        private bool _oneCallFlagPorHorNotFound = false;
        private bool _oneCallFlagSaveData = false;

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
            HDevExp = new HDevelopExport();
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
            HDevExp.UpdateResult += new HDevelopExport.UpdateHandler(HalconUpdate);
            HDevExp.UpdateResultPick += new HDevelopExport.UpdateHandlerPick(PickUpdate);
            HDevExp.PorosityDetected += new HDevelopExport.PorosityDetectedEventHandler(PorosityIsDetected);
            HDevExp.PorosityDetectionStart += new HDevelopExport.PorosityDetectionStartEventHandler(DetectionStart);
            HDevExp.PorosityDetectionHorStart += new HDevelopExport.PorosityDetectionHorStartEventHandler(DetectionHorStart); 
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
            if (((bool)e.StatusData.Kamere.CAM4ZahtjevZaAnalizomS1.Value) && ((float)e.StatusData.MjerenjeDiametara.BrojPonavljanjaSekvence.Value == 1.0f) && _oneCallFlagD1S1)
            {
                _oneCallFlagD1S1 = false;
                Thread d1meassureS1 = new Thread(() => HDevExp.RunHalcon1(windowID)); // d1meassureS1.name = "Thread D1S1
                d1meassureS1.Name = "Thread D1S1";
                d1meassureS1.Start();
            }
            else if(!(bool)e.StatusData.Kamere.CAM4ZahtjevZaAnalizomS1.Value)
            {
                _oneCallFlagD1S1 = true;
            }

            // Start analize slike D1 DRUGOG RUBA S2 *******************************************************************
            if (((bool)e.StatusData.Kamere.CAM4ZahtjevZaAnalizomS2.Value) && ((float)e.StatusData.MjerenjeDiametara.BrojPonavljanjaSekvence.Value == 1.0f) && _oneCallFlagD1S2)
            {
                _oneCallFlagD1S2 = false;
                //Thread d1meassureS2 = new Thread(new ThreadStart(HDevExp.RunHalcon2));
                Thread d1meassureS2 = new Thread(() => HDevExp.RunHalcon2(windowID));
                d1meassureS2.Name = "Thread D1S2";
                d1meassureS2.Start();
            }
            else if (!(bool)e.StatusData.Kamere.CAM4ZahtjevZaAnalizomS2.Value)
            {
                _oneCallFlagD1S2 = true;
            }

            // Start analize slike D2 DRUGOG RUBA S1 *******************************************************************
            if (((bool)e.StatusData.Kamere.CAM4ZahtjevZaAnalizomS1.Value) && ((float)e.StatusData.MjerenjeDiametara.BrojPonavljanjaSekvence.Value == 2.0f) && _oneCallFlagD2S1)
            {

                _oneCallFlagD2S1 = false;
                //Thread d2meassureS1 = new Thread(new ThreadStart(HDevExp.RunHalcon3));
                Thread d1meassureS2 = new Thread(() => HDevExp.RunHalcon3(windowID));
                d1meassureS2.Name = "Thread D2S1";
                d1meassureS2.Start();
            }
            else if(!(bool)e.StatusData.Kamere.CAM4ZahtjevZaAnalizomS1.Value)
            {
                _oneCallFlagD2S1 = true;
            }

            // Start analize slike D2 DRUGOG RUBA S2 *******************************************************************
            if (((bool)e.StatusData.Kamere.CAM4ZahtjevZaAnalizomS2.Value) && ((float)e.StatusData.MjerenjeDiametara.BrojPonavljanjaSekvence.Value == 2.0f) && _oneCallFlagD2S2)
            {
                _oneCallFlagD2S2 = false;
                //Thread d2meassureS2 = new Thread(new ThreadStart(HDevExp.RunHalcon4));
                Thread d2meassureS2 = new Thread(() => HDevExp.RunHalcon4(windowID));
                d2meassureS2.Name = "Thread D2S2";
                d2meassureS2.Start();
            }
            else if (!(bool)e.StatusData.Kamere.CAM4ZahtjevZaAnalizomS2.Value)
            {
                _oneCallFlagD2S2 = true;
            }

            // Start analize slike D3 DRUGOG RUBA S1 *******************************************************************
            if (((bool)e.StatusData.Kamere.CAM4ZahtjevZaAnalizomS1.Value) && ((float)e.StatusData.MjerenjeDiametara.BrojPonavljanjaSekvence.Value == 3.0f) && _oneCallFlagD3S1)
            {
                _oneCallFlagD3S1 = false;
                //Thread d3meassureS1 = new Thread(new ThreadStart(HDevExp.RunHalcon5));
                Thread d3meassureS1 = new Thread(() => HDevExp.RunHalcon5(windowID));
                d3meassureS1.Name = "Thread D3S1";
                d3meassureS1.Start();
            }
            else if (!(bool)e.StatusData.Kamere.CAM4ZahtjevZaAnalizomS1.Value)
            {
                _oneCallFlagD3S1 = true;
            }

            // Start analize slike D3 DRUGOG RUBA S2 *******************************************************************
            if (((bool)e.StatusData.Kamere.CAM4ZahtjevZaAnalizomS2.Value) && ((float)e.StatusData.MjerenjeDiametara.BrojPonavljanjaSekvence.Value == 3.0f) && _oneCallFlagD3S2)
            {
                _oneCallFlagD3S2 = false;
                //Thread d3meassureS2 = new Thread(new ThreadStart(HDevExp.RunHalcon6));
                Thread d3meassureS2 = new Thread(() => HDevExp.RunHalcon6(windowID));
                d3meassureS2.Name = "Thread D3S2";
                d3meassureS2.Start();
            }
            else if (!(bool)e.StatusData.Kamere.CAM4ZahtjevZaAnalizomS2.Value)
            {
                _oneCallFlagD3S2 = true;
            }

            // Start analize slike D4 DRUGOG RUBA S1 *******************************************************************
            if (((bool)e.StatusData.Kamere.CAM4ZahtjevZaAnalizomS1.Value) && ((float)e.StatusData.MjerenjeDiametara.BrojPonavljanjaSekvence.Value == 4.0f) && _oneCallFlagD4S1)
            {
                _oneCallFlagD4S1 = false;
                //Thread d4meassureS1 = new Thread(new ThreadStart(HDevExp.RunHalcon7));
                Thread d4meassureS1 = new Thread(() => HDevExp.RunHalcon7(windowID));
                d4meassureS1.Name = "Thread D4S1";
                d4meassureS1.Start();
            }
            else if (!(bool)e.StatusData.Kamere.CAM4ZahtjevZaAnalizomS1.Value)
            {
                _oneCallFlagD4S1 = true;
            }

            // Start analize slike D4 DRUGOG RUBA S2 *******************************************************************
            if (((bool)e.StatusData.Kamere.CAM4ZahtjevZaAnalizomS2.Value) && ((float)e.StatusData.MjerenjeDiametara.BrojPonavljanjaSekvence.Value == 4.0f) && _oneCallFlagD4S2)
            {
                _oneCallFlagD4S2 = false;
                //Thread d4meassureS2 = new Thread(new ThreadStart(HDevExp.RunHalcon8));
                Thread d4meassureS2 = new Thread(() => HDevExp.RunHalcon8(windowID));
                d4meassureS2.Name = "Thread D4S2";
                d4meassureS2.Start();
            }
            else if (!(bool)e.StatusData.Kamere.CAM4ZahtjevZaAnalizomS2.Value)
            {
                _oneCallFlagD4S2 = true;
            }

            // Start analize slike D5 DRUGOG RUBA S1 *******************************************************************
            if (((bool)e.StatusData.Kamere.CAM4ZahtjevZaAnalizomS1.Value) && ((float)e.StatusData.MjerenjeDiametara.BrojPonavljanjaSekvence.Value == 5.0f) && _oneCallFlagD5S1)
            {
                _oneCallFlagD5S1 = false;
                //Thread d4meassureS1 = new Thread(new ThreadStart(HDevExp.RunHalcon7));
                Thread d5meassureS1 = new Thread(() => HDevExp.RunHalcon7(windowID));
                d5meassureS1.Name = "Thread D5S1";
                d5meassureS1.Start();
            }
            else if (!(bool)e.StatusData.Kamere.CAM4ZahtjevZaAnalizomS1.Value)
            {
                _oneCallFlagD5S1 = true;
            }

            // Start analize slike D5 DRUGOG RUBA S2 *******************************************************************
            if (((bool)e.StatusData.Kamere.CAM4ZahtjevZaAnalizomS2.Value) && ((float)e.StatusData.MjerenjeDiametara.BrojPonavljanjaSekvence.Value == 5.0f) && _oneCallFlagD5S2)
            {
                _oneCallFlagD5S2 = false;
                //Thread d4meassureS2 = new Thread(new ThreadStart(HDevExp.RunHalcon8));
                Thread d5meassureS2 = new Thread(() => HDevExp.RunHalcon8(windowID));
                d5meassureS2.Name = "Thread D4S2";
                d5meassureS2.Start();
            }
            else if (!(bool)e.StatusData.Kamere.CAM4ZahtjevZaAnalizomS2.Value)
            {
                _oneCallFlagD5S2 = true;
            }

            // Start analize slike za detekciju POROZNOSTI VERTIKALNO ***************************************************
            if (((bool)e.StatusData.Kamere.CAM2ZahtjevZaAnalizom.Value) && _onceCallFlagPorVer)
            {
                _onceCallFlagPorVer = false;
                Thread porosityverth = new Thread(new ThreadStart(pPoroznost.PorosityVerWindow));
                porosityverth.Name = "Thread PorosityVer";
                porosityverth.Start();

                // Start sa display-om, thread error
                //pPoroznost.PorosityVerWindow();
            }
            else if (!(bool) e.StatusData.Kamere.CAM2ZahtjevZaAnalizom.Value)
            {
                _onceCallFlagPorVer = true;
            }

            // Start analize slike za detekciju POROZNOSTI HORIZONTALNO ************************************************
            if (((bool)e.StatusData.Kamere.CAM3ZahtjevZaAnalizom.Value) && _oneCallFlagPorHor)
            {
                _oneCallFlagPorHor = false;
                Thread porosityhorth = new Thread(new ThreadStart(pPoroznost.PorosityHorWindow));
                porosityhorth.Name = "Thread PorosityHor";
                porosityhorth.Start();

                // Start sa display-om, thread error
                //pPoroznost.PorosityHorWindow();
            }
            else if (!(bool)e.StatusData.Kamere.CAM3ZahtjevZaAnalizom.Value)
            {
                _oneCallFlagPorHor = true;
            }

            // Start analize slike za robot PICK - TRIGGER 1 ***********************************************************
            if (((bool)e.StatusData.Kamere.CAM1ZahtjevZaAnalizomT1.Value) && _oneCallFlagPick)
            {
                _oneCallFlagPick = false;
                // We call public method in class pRobot
                Thread pickTriggerT1 = new Thread(new ThreadStart(pRobot.RobotPickStartT1));
                pickTriggerT1.Name = "Thread pickTriggerT1";
                pickTriggerT1.Start();
            }
            else if (!(bool)e.StatusData.Kamere.CAM1ZahtjevZaAnalizomT1.Value)
            {
                _oneCallFlagPick = true;
            }

            // Start analize slike za robot PICK - TRIGGER 2 ***********************************************************
            //if (((bool)e.StatusData.Kamere.CAM1ZahtjevZaAnalizomT2.Value) && (!_edgeDetection7))
            //{
            //    // We call public method in class pRobot
            //    Thread pickTriggerT2 = new Thread(pRobot.RobotPickStartT2);
            //    pickTriggerT2.Name = "Thread pickTriggerT2";
            //    pickTriggerT2.Start();
            //}

            // R-Os je prosla 360 i nije nasla porozni dio CAM2 
            if (((bool)e.StatusData.MjerenjePoroznosti.GotovoCAM2.Value) && _oneCallFlagPorVerNotFound)
            {
                _oneCallFlagPorVerNotFound = false;
                HDevExp.Porositydetectedver = true;
                //HDevExp.Porositydetectedhor = true;
            }
            else if (!(bool)e.StatusData.MjerenjePoroznosti.GotovoCAM2.Value)
            {
                _oneCallFlagPorVerNotFound = true;
            }

            // R-Os je prosla 360 i nije nasla porozni dio CAM3 
            if (((bool)e.StatusData.MjerenjePoroznosti.GotovoCAM3.Value) && _oneCallFlagPorHorNotFound)
            {
                _oneCallFlagPorHorNotFound = false;
                //HDevExp.Porositydetectedver = true;
                HDevExp.Porositydetectedhor = true;
            }
            else if (!(bool)e.StatusData.MjerenjePoroznosti.GotovoCAM3.Value)
            {
                _oneCallFlagPorHorNotFound = true;
            }

            // Save Data from PLC
            if (((bool)e.StatusData.Automatika.SnimiMjerenja.Value) && _oneCallFlagSaveData)
            {
                _oneCallFlagSaveData = false;
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
                    // Ne Radi, probati
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
            else if (!(bool)e.StatusData.Automatika.SnimiMjerenja.Value)
            {
                _oneCallFlagSaveData = true;
            }

            if (mwHandle != null)
            {
                mwHandle.tb_statusMessage.Dispatcher.BeginInvoke((Action)(() => { mwHandle.tb_statusMessage.Text = msg; }));
            }
        }

        // Event handler koji se poziva kad zavrsi analiza slike za mjerenje diametara
        private static void HalconUpdate(HDevelopExport sender, HalconEventArgs e)
        {
            App.PLC.WriteTag(PLC.STATUS.Kamere.CAM4Rezultat, e.PXvalue);
            App.PLC.WriteTag(PLC.STATUS.Kamere.CAM4AnalizaOk, true);
            Thread.Sleep(100);
            App.PLC.WriteTag(PLC.STATUS.Kamere.CAM4AnalizaOk, false);
        }
        // Event handler koji se poziva kada zavrsi analiza slike za pick
        private static void PickUpdate(HDevelopExport sender, HalconEventArgs e)
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
