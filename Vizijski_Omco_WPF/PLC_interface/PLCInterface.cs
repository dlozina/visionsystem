using System;
using System.Windows;
using System.Threading;
using Snap7;
using VizijskiSustavWPF.PLC_interface;

namespace VizijskiSustavWPF 
{
    public class PLCInterfaceEventArgs : EventArgs
    {
        private Control controlData;

        public Control ControlData
        {
            get { return controlData; }
            set { controlData = value; }
        }

        private Status statusData;
        public Status StatusData
        {
            get { return statusData; }
            set { statusData = value; }
        }

        public byte[]  CyclicStatusBuffer { get; set; } 
        public byte[] CyclicControlBuffer { get; set; }
    }

    public class OnlineMarkerEventArgs : EventArgs
    {
        bool onlineMark;

        public bool OnlineMark
        {
            get { return onlineMark; }
            set { onlineMark = value; }
        }
    }

    public class PLCInterface : DependencyObject
    {
        private int activeScreen;
        public int ActiveScreen
        {
            get { return activeScreen; }
            set { activeScreen = value; }
        }

        public Control CONTROL { get; set; } = new Control();
        public Status STATUS { get; set; } = new Status();

        public static object StatusControlLock = new object();
        public static object TimerLock = new object();
        
        public delegate void UpdateHandler(PLCInterface sender, PLCInterfaceEventArgs e);
        public delegate void OnlineMarker(PLCInterface sender, OnlineMarkerEventArgs e);
        
        public event UpdateHandler Update_1_s;
        public event UpdateHandler Update_100_ms;
        public event OnlineMarker Update_Online_Flag; 


        public bool OnlineMark;
        public int Errorcode;
        public S7Client Client;

        System.Timers.Timer Clock_100_ms;
        System.Timers.Timer WatchDogTimer;

        private byte[] CyclicStatusBuffer = new byte[65536];
        private byte[] ReadBuffer = new byte[65536];
        private byte[] CyclicControlBuffer = new byte[65536];
        private byte[] WatchdogBuffer = new byte[2];
        private short updateCounter = 0;

        public PLCInterface()
        {
            Client = new S7Client();
            Clock_100_ms = new System.Timers.Timer(100); 
            Clock_100_ms.Elapsed += onClock100msTick;
            Clock_100_ms.AutoReset = false;

            WatchDogTimer = new System.Timers.Timer(2000);
            WatchDogTimer.Elapsed += onClockWatchdogTick;
            WatchDogTimer.AutoReset = false;
        }
      
        public void StartCyclic()
        {
            Clock_100_ms.Start();
            WatchDogTimer.Start();
        }

        public void RestartInterface()
        {
            lock (PLCInterface.TimerLock)
            {
                Client = new S7Client();
                Clock_100_ms.Stop();
                Thread.Sleep(1000);
                while (!Client.Connected())
                {
                    // Real PLC
                    //Client.ConnectTo("192.168.0.1", 0, 1);
                    // Simulation PLC
                    Client.ConnectTo("192.168.125.88", 0, 1);
                    Thread.Sleep(200);
                    if (Client.Connected())
                    {
                        Clock_100_ms.Start();
                        WatchDogTimer.Start();
                    }
                }
            }
        }

        #region read functions
        private int ReadControl()
        {
            int result = -99;
            if (Client.Connected())
                result = Client.DBRead(6, 0, 196, CyclicControlBuffer);
            if (result == 0)
            {
                lock (StatusControlLock)
                {
                    // Horizontalna os
                    CONTROL.HorizontalnaOs.IdiUHome.GetValueFromGroupBuffer(CyclicControlBuffer);
                    CONTROL.HorizontalnaOs.IdiUPoziciju.GetValueFromGroupBuffer(CyclicControlBuffer);
                    CONTROL.HorizontalnaOs.IdiUPozicijuTicala.GetValueFromGroupBuffer(CyclicControlBuffer);
                    CONTROL.HorizontalnaOs.Reset.GetValueFromGroupBuffer(CyclicControlBuffer);
                    CONTROL.HorizontalnaOs.ZadanaPozicija.GetValueFromGroupBuffer(CyclicControlBuffer);
                    CONTROL.HorizontalnaOs.JogPlus.GetValueFromGroupBuffer(CyclicControlBuffer);
                    CONTROL.HorizontalnaOs.JogMinus.GetValueFromGroupBuffer(CyclicControlBuffer);
                    CONTROL.HorizontalnaOs.IdiURub1.GetValueFromGroupBuffer(CyclicControlBuffer);
                    CONTROL.HorizontalnaOs.IdiURub2.GetValueFromGroupBuffer(CyclicControlBuffer);
                    CONTROL.HorizontalnaOs.AutomatikaStart.GetValueFromGroupBuffer(CyclicControlBuffer);

                    // Vertikalna os
                    CONTROL.VertikalnaOs.IdiUHome.GetValueFromGroupBuffer(CyclicControlBuffer);
                    CONTROL.VertikalnaOs.IdiUPoziciju.GetValueFromGroupBuffer(CyclicControlBuffer);
                    CONTROL.VertikalnaOs.IdiUSafePoziciju.GetValueFromGroupBuffer(CyclicControlBuffer);
                    CONTROL.VertikalnaOs.Reset.GetValueFromGroupBuffer(CyclicControlBuffer);
                    CONTROL.VertikalnaOs.ZadanaPozicija.GetValueFromGroupBuffer(CyclicControlBuffer);
                    CONTROL.VertikalnaOs.JogPlus.GetValueFromGroupBuffer(CyclicControlBuffer);
                    CONTROL.VertikalnaOs.JogMinus.GetValueFromGroupBuffer(CyclicControlBuffer);

                    // Rotaciona os
                    CONTROL.RotacijskaOs.IdiUHome.GetValueFromGroupBuffer(CyclicControlBuffer);
                    CONTROL.RotacijskaOs.IdiUPoziciju.GetValueFromGroupBuffer(CyclicControlBuffer);
                    CONTROL.RotacijskaOs.Referenciraj.GetValueFromGroupBuffer(CyclicControlBuffer);
                    CONTROL.RotacijskaOs.Reset.GetValueFromGroupBuffer(CyclicControlBuffer);
                    CONTROL.RotacijskaOs.ZadanaPozicija.GetValueFromGroupBuffer(CyclicControlBuffer);
                    CONTROL.RotacijskaOs.JogPlus.GetValueFromGroupBuffer(CyclicControlBuffer);
                    CONTROL.RotacijskaOs.JogMinus.GetValueFromGroupBuffer(CyclicControlBuffer);

                    // Ticalo
                    CONTROL.Ticalo.TicaloGore.GetValueFromGroupBuffer(CyclicControlBuffer);
                    CONTROL.Ticalo.TicaloDolje.GetValueFromGroupBuffer(CyclicControlBuffer);
                    CONTROL.Ticalo.Nuliraj.GetValueFromGroupBuffer(CyclicControlBuffer);
                    CONTROL.Ticalo.PostaviNaVrijednost.GetValueFromGroupBuffer(CyclicControlBuffer);

                    // Mjerenje Ticalom
                    CONTROL.MjerenjeTicalom.Start.GetValueFromGroupBuffer(CyclicControlBuffer);
                    CONTROL.MjerenjeTicalom.Stop.GetValueFromGroupBuffer(CyclicControlBuffer);
                    CONTROL.MjerenjeTicalom.Pauza.GetValueFromGroupBuffer(CyclicControlBuffer);
                    CONTROL.MjerenjeTicalom.Reset.GetValueFromGroupBuffer(CyclicControlBuffer);

                    // Mjerenje Poroznosti
                    CONTROL.MjerenjePoroznosti.Start.GetValueFromGroupBuffer(CyclicControlBuffer);
                    CONTROL.MjerenjePoroznosti.Stop.GetValueFromGroupBuffer(CyclicControlBuffer);
                    CONTROL.MjerenjePoroznosti.Pauza.GetValueFromGroupBuffer(CyclicControlBuffer);
                    CONTROL.MjerenjePoroznosti.Reset.GetValueFromGroupBuffer(CyclicControlBuffer);
                    CONTROL.MjerenjePoroznosti.ZadanaPovrsina.GetValueFromGroupBuffer(CyclicControlBuffer);

                    // Mjerenje Diametara
                    CONTROL.MjerenjeDiametara.Start.GetValueFromGroupBuffer(CyclicControlBuffer);
                    CONTROL.MjerenjeDiametara.Stop.GetValueFromGroupBuffer(CyclicControlBuffer);
                    CONTROL.MjerenjeDiametara.Pauza.GetValueFromGroupBuffer(CyclicControlBuffer);
                    CONTROL.MjerenjeDiametara.Reset.GetValueFromGroupBuffer(CyclicControlBuffer);

                    // Citanje Stringa
                    CONTROL.CitanjeStringa.Start.GetValueFromGroupBuffer(CyclicControlBuffer);
                    CONTROL.CitanjeStringa.Stop.GetValueFromGroupBuffer(CyclicControlBuffer);
                    CONTROL.CitanjeStringa.Pauza.GetValueFromGroupBuffer(CyclicControlBuffer);
                    CONTROL.CitanjeStringa.Reset.GetValueFromGroupBuffer(CyclicControlBuffer);

                    // Kamere
                    CONTROL.Kamere.RasvjetaZaString.GetValueFromGroupBuffer(CyclicControlBuffer);
                    CONTROL.Kamere.RasvjetaZaDimenzije.GetValueFromGroupBuffer(CyclicControlBuffer);
                    CONTROL.Kamere.RasvjetaZaPoroznost.GetValueFromGroupBuffer(CyclicControlBuffer);
                    CONTROL.Kamere.RasvjetaZaD2Lijevo.GetValueFromGroupBuffer(CyclicControlBuffer);
                    CONTROL.Kamere.RasvjetaZaD2Desno.GetValueFromGroupBuffer(CyclicControlBuffer);

                    // Cilindri
                    CONTROL.Cilindri.PPOtvori.GetValueFromGroupBuffer(CyclicControlBuffer);
                    CONTROL.Cilindri.PPZatvori.GetValueFromGroupBuffer(CyclicControlBuffer);
                    CONTROL.Cilindri.StegaOtvori.GetValueFromGroupBuffer(CyclicControlBuffer);
                    CONTROL.Cilindri.StegaZatvori.GetValueFromGroupBuffer(CyclicControlBuffer);

                    // Robot
                    CONTROL.Robot.ZahtijevZaKomadom.GetValueFromGroupBuffer(CyclicControlBuffer);
                    CONTROL.Robot.PrekretKomada.GetValueFromGroupBuffer(CyclicControlBuffer);
                    CONTROL.Robot.KomadOk.GetValueFromGroupBuffer(CyclicControlBuffer);
                    CONTROL.Robot.KomadNok.GetValueFromGroupBuffer(CyclicControlBuffer);
                    CONTROL.Robot.ResetUlaznePaleteLijevo.GetValueFromGroupBuffer(CyclicControlBuffer);
                    CONTROL.Robot.ResetUlaznePaleteDesno.GetValueFromGroupBuffer(CyclicControlBuffer);
                    CONTROL.Robot.ResetOkPaleteLijevo.GetValueFromGroupBuffer(CyclicControlBuffer);
                    CONTROL.Robot.ResetOkPaleteDesno.GetValueFromGroupBuffer(CyclicControlBuffer);
                    CONTROL.Robot.ResetNokPaleteLijevo.GetValueFromGroupBuffer(CyclicControlBuffer);
                    CONTROL.Robot.ResetNokPaleteDesno.GetValueFromGroupBuffer(CyclicControlBuffer);
                    CONTROL.Robot.ResetLimovaPozicijaOdlaganja.GetValueFromGroupBuffer(CyclicControlBuffer);
                    CONTROL.Robot.PrekretKomadaDno.GetValueFromGroupBuffer(CyclicControlBuffer);

                    // Ucenjereal
                    CONTROL.UcenjeReal.ZadavanjeDijametara1.GetValueFromGroupBuffer(CyclicControlBuffer);
                    CONTROL.UcenjeReal.ZadavanjeDijametara2.GetValueFromGroupBuffer(CyclicControlBuffer);
                    CONTROL.UcenjeReal.ZadavanjeDijametara3.GetValueFromGroupBuffer(CyclicControlBuffer);
                    CONTROL.UcenjeReal.ZadavanjeDijametara4.GetValueFromGroupBuffer(CyclicControlBuffer);
                    CONTROL.UcenjeReal.ZadavanjeDijametara5.GetValueFromGroupBuffer(CyclicControlBuffer);
                    CONTROL.UcenjeReal.ZadavanjeVisineV1.GetValueFromGroupBuffer(CyclicControlBuffer);
                    CONTROL.UcenjeReal.ZadavanjeVisineV2.GetValueFromGroupBuffer(CyclicControlBuffer);
                    CONTROL.UcenjeReal.ZadavanjeVisineV3.GetValueFromGroupBuffer(CyclicControlBuffer);
                    CONTROL.UcenjeReal.ZadavanjeVisineBaze.GetValueFromGroupBuffer(CyclicControlBuffer);
                    
                    // Ucenjebool
                    CONTROL.UcenjeBool.NoviNalog.GetValueFromGroupBuffer(CyclicControlBuffer);
                    CONTROL.UcenjeBool.ResetUcenja.GetValueFromGroupBuffer(CyclicControlBuffer);
                    CONTROL.UcenjeBool.IdiUD1S1.GetValueFromGroupBuffer(CyclicControlBuffer);
                    CONTROL.UcenjeBool.IdiUD1S2.GetValueFromGroupBuffer(CyclicControlBuffer);
                    CONTROL.UcenjeBool.IdiUD2S1.GetValueFromGroupBuffer(CyclicControlBuffer);
                    CONTROL.UcenjeBool.IdiUD2S2.GetValueFromGroupBuffer(CyclicControlBuffer);
                    CONTROL.UcenjeBool.IdiUD3S1.GetValueFromGroupBuffer(CyclicControlBuffer);
                    CONTROL.UcenjeBool.IdiUD3S2.GetValueFromGroupBuffer(CyclicControlBuffer);
                    CONTROL.UcenjeBool.IdiUD4S1.GetValueFromGroupBuffer(CyclicControlBuffer);
                    CONTROL.UcenjeBool.IdiUD4S2.GetValueFromGroupBuffer(CyclicControlBuffer);
                    CONTROL.UcenjeBool.IdiUD5S1.GetValueFromGroupBuffer(CyclicControlBuffer);
                    CONTROL.UcenjeBool.IdiUD5S2.GetValueFromGroupBuffer(CyclicControlBuffer);
                    CONTROL.UcenjeBool.IdiNaV1.GetValueFromGroupBuffer(CyclicControlBuffer);
                    CONTROL.UcenjeBool.IdiNaV2.GetValueFromGroupBuffer(CyclicControlBuffer);
                    CONTROL.UcenjeBool.IdiNaV3.GetValueFromGroupBuffer(CyclicControlBuffer);
                    CONTROL.UcenjeBool.NauciD1S1.GetValueFromGroupBuffer(CyclicControlBuffer);
                    CONTROL.UcenjeBool.NauciD1S2.GetValueFromGroupBuffer(CyclicControlBuffer);
                    CONTROL.UcenjeBool.NauciD2S1.GetValueFromGroupBuffer(CyclicControlBuffer);
                    CONTROL.UcenjeBool.NauciD2S2.GetValueFromGroupBuffer(CyclicControlBuffer);
                    CONTROL.UcenjeBool.NauciD3S1.GetValueFromGroupBuffer(CyclicControlBuffer);
                    CONTROL.UcenjeBool.NauciD3S2.GetValueFromGroupBuffer(CyclicControlBuffer);
                    CONTROL.UcenjeBool.NauciD4S1.GetValueFromGroupBuffer(CyclicControlBuffer);
                    CONTROL.UcenjeBool.NauciD4S2.GetValueFromGroupBuffer(CyclicControlBuffer);
                    CONTROL.UcenjeBool.NauciD5S1.GetValueFromGroupBuffer(CyclicControlBuffer);
                    CONTROL.UcenjeBool.NauciD5S2.GetValueFromGroupBuffer(CyclicControlBuffer);
                    CONTROL.UcenjeBool.PoroznostHorPozicija.GetValueFromGroupBuffer(CyclicControlBuffer);
                    CONTROL.UcenjeBool.PoroznostVerPozicija.GetValueFromGroupBuffer(CyclicControlBuffer);
                    CONTROL.UcenjeBool.NauciPoroznost.GetValueFromGroupBuffer(CyclicControlBuffer);
                    
                    // Automatika
                    CONTROL.Automatika.ResetSvihMjerenja.GetValueFromGroupBuffer(CyclicControlBuffer);
                    
                    // Prekret
                    CONTROL.Prekret.PrekretLijevo.GetValueFromGroupBuffer(CyclicControlBuffer);
                    CONTROL.Prekret.PrekretDesno.GetValueFromGroupBuffer(CyclicControlBuffer);
                    CONTROL.Prekret.PrekretOtvori.GetValueFromGroupBuffer(CyclicControlBuffer);
                    CONTROL.Prekret.PrekretZatvori.GetValueFromGroupBuffer(CyclicControlBuffer);
                    
                    // Uzimanje komada
                    CONTROL.UzimanjeKomada.BrojSlojevaUlaznaPaletaLijevo.GetValueFromGroupBuffer(CyclicControlBuffer);
                    CONTROL.UzimanjeKomada.BrojSlojevaUlaznaPaletaDesno.GetValueFromGroupBuffer(CyclicControlBuffer);
                    CONTROL.UzimanjeKomada.BrojSlojevaKomadiOKPaletaLijevo.GetValueFromGroupBuffer(CyclicControlBuffer);
                    CONTROL.UzimanjeKomada.BrojSlojevaKomadiOKPaletaDesno.GetValueFromGroupBuffer(CyclicControlBuffer);
                    CONTROL.UzimanjeKomada.BrojSlojevaKomadiNOKPaletaLijevo.GetValueFromGroupBuffer(CyclicControlBuffer);
                    CONTROL.UzimanjeKomada.BrojSlojevaKomadiNOKPaletaDesno.GetValueFromGroupBuffer(CyclicControlBuffer);
                    CONTROL.UzimanjeKomada.BrojLimovaUPozicijiOdlaganja.GetValueFromGroupBuffer(CyclicControlBuffer);
                    CONTROL.UzimanjeKomada.DebljinaLimova.GetValueFromGroupBuffer(CyclicControlBuffer);

                    // Tolerance
                    CONTROL.Tolerance.Dijametar1DeltaMinus.GetValueFromGroupBuffer(CyclicControlBuffer);
                    CONTROL.Tolerance.Dijametar1DeltaPlus.GetValueFromGroupBuffer(CyclicControlBuffer);
                    CONTROL.Tolerance.Dijametar2DeltaMinus.GetValueFromGroupBuffer(CyclicControlBuffer);
                    CONTROL.Tolerance.Dijametar2DeltaPlus.GetValueFromGroupBuffer(CyclicControlBuffer);
                    CONTROL.Tolerance.Dijametar3DeltaMinus.GetValueFromGroupBuffer(CyclicControlBuffer);
                    CONTROL.Tolerance.Dijametar3DeltaPlus.GetValueFromGroupBuffer(CyclicControlBuffer);
                    CONTROL.Tolerance.Dijametar4DeltaMinus.GetValueFromGroupBuffer(CyclicControlBuffer);
                    CONTROL.Tolerance.Dijametar4DeltaPlus.GetValueFromGroupBuffer(CyclicControlBuffer);
                    CONTROL.Tolerance.Dijametar5DeltaMinus.GetValueFromGroupBuffer(CyclicControlBuffer);
                    CONTROL.Tolerance.Dijametar5DeltaPlus.GetValueFromGroupBuffer(CyclicControlBuffer);
                    CONTROL.Tolerance.Visina1DeltaMinus.GetValueFromGroupBuffer(CyclicControlBuffer);
                    CONTROL.Tolerance.Visina1DeltaPlus.GetValueFromGroupBuffer(CyclicControlBuffer);
                    CONTROL.Tolerance.Visina2DeltaMinus.GetValueFromGroupBuffer(CyclicControlBuffer);
                    CONTROL.Tolerance.Visina2DeltaPlus.GetValueFromGroupBuffer(CyclicControlBuffer);
                    CONTROL.Tolerance.Visina3DeltaMinus.GetValueFromGroupBuffer(CyclicControlBuffer);
                    CONTROL.Tolerance.Visina3DeltaPlus.GetValueFromGroupBuffer(CyclicControlBuffer);

                    // NacinRada
                    CONTROL.NacinRada.Dimenzije.GetValueFromGroupBuffer(CyclicControlBuffer);
                    CONTROL.NacinRada.Poroznost.GetValueFromGroupBuffer(CyclicControlBuffer);
                    CONTROL.NacinRada.String.GetValueFromGroupBuffer(CyclicControlBuffer);
                    CONTROL.NacinRada.Dijametar1Rucno.GetValueFromGroupBuffer(CyclicControlBuffer);
                    CONTROL.NacinRada.Dijametar2Rucno.GetValueFromGroupBuffer(CyclicControlBuffer);
                    CONTROL.NacinRada.Dijametar3Rucno.GetValueFromGroupBuffer(CyclicControlBuffer);
                    CONTROL.NacinRada.Dijametar4Rucno.GetValueFromGroupBuffer(CyclicControlBuffer);
                    CONTROL.NacinRada.Dijametar5Rucno.GetValueFromGroupBuffer(CyclicControlBuffer);

                    // OdabirDijametara
                    CONTROL.OdabirDijametra.Dijametar1.GetValueFromGroupBuffer(CyclicControlBuffer);
                    CONTROL.OdabirDijametra.Dijametar2.GetValueFromGroupBuffer(CyclicControlBuffer);
                    CONTROL.OdabirDijametra.Dijametar3.GetValueFromGroupBuffer(CyclicControlBuffer);
                    CONTROL.OdabirDijametra.Dijametar4.GetValueFromGroupBuffer(CyclicControlBuffer);
                    CONTROL.OdabirDijametra.Dijametar5.GetValueFromGroupBuffer(CyclicControlBuffer);
                    
                    // OdabirVisina
                    CONTROL.OdabirVisina.Visina1.GetValueFromGroupBuffer(CyclicControlBuffer);
                    CONTROL.OdabirVisina.Visina2.GetValueFromGroupBuffer(CyclicControlBuffer);
                    CONTROL.OdabirVisina.Visina3.GetValueFromGroupBuffer(CyclicControlBuffer);
                }
            }
            return result;
        }

        private int ReadStatus()
        {
            int result = -99;
            if (Client.Connected())
                result = Client.DBRead(11, 0, 256, CyclicStatusBuffer);
            if (result == 0)
            {
                lock (StatusControlLock)
                {
                    // Horizontalna os
                    STATUS.HorizontalnaOs.UHome.GetValueFromGroupBuffer(CyclicStatusBuffer);
                    STATUS.HorizontalnaOs.UPoziciji.GetValueFromGroupBuffer(CyclicStatusBuffer);
                    STATUS.HorizontalnaOs.UPozicijiTicala.GetValueFromGroupBuffer(CyclicStatusBuffer);
                    STATUS.HorizontalnaOs.Greska.GetValueFromGroupBuffer(CyclicStatusBuffer);
                    STATUS.HorizontalnaOs.AktualnaPozicija.GetValueFromGroupBuffer(CyclicStatusBuffer);

                    // Vertikalna os
                    STATUS.VertikalnaOs.UHome.GetValueFromGroupBuffer(CyclicStatusBuffer);
                    STATUS.VertikalnaOs.UPoziciji.GetValueFromGroupBuffer(CyclicStatusBuffer);
                    STATUS.VertikalnaOs.USafePoziciji.GetValueFromGroupBuffer(CyclicStatusBuffer);
                    STATUS.VertikalnaOs.Greska.GetValueFromGroupBuffer(CyclicStatusBuffer);
                    STATUS.VertikalnaOs.AktualnaPozicija.GetValueFromGroupBuffer(CyclicStatusBuffer);

                    // Rotaciona os
                    STATUS.RotacijskaOs.UHome.GetValueFromGroupBuffer(CyclicStatusBuffer);
                    STATUS.RotacijskaOs.UPoziciji.GetValueFromGroupBuffer(CyclicStatusBuffer);
                    STATUS.RotacijskaOs.Greska.GetValueFromGroupBuffer(CyclicStatusBuffer);
                    STATUS.RotacijskaOs.AktualnaPozicija.GetValueFromGroupBuffer(CyclicStatusBuffer);
                    STATUS.RotacijskaOs.Referencirana.GetValueFromGroupBuffer(CyclicStatusBuffer);

                    // Ticalo
                    STATUS.Ticalo.TicaloDolje.GetValueFromGroupBuffer(CyclicStatusBuffer);
                    STATUS.Ticalo.TicaloGore.GetValueFromGroupBuffer(CyclicStatusBuffer);
                    STATUS.Ticalo.AktualnaPozicija.GetValueFromGroupBuffer(CyclicStatusBuffer);
                    STATUS.Ticalo.Referencirano.GetValueFromGroupBuffer(CyclicStatusBuffer);

                    // Mjerenje ticalom
                    STATUS.MjerenjeTicalom.Gotovo.GetValueFromGroupBuffer(CyclicStatusBuffer);
                    STATUS.MjerenjeTicalom.Aktivno.GetValueFromGroupBuffer(CyclicStatusBuffer);
                    STATUS.MjerenjeTicalom.Greska.GetValueFromGroupBuffer(CyclicStatusBuffer);
                    STATUS.MjerenjeTicalom.IzvrsavanjeKoraka.GetValueFromGroupBuffer(CyclicStatusBuffer);
                    STATUS.MjerenjeTicalom.BrojPonavljanjaSekvence.GetValueFromGroupBuffer(CyclicStatusBuffer);
                    STATUS.MjerenjeTicalom.RotacijskaOs.GetValueFromGroupBuffer(CyclicStatusBuffer);
                    STATUS.MjerenjeTicalom.HorizontalnaOs.GetValueFromGroupBuffer(CyclicStatusBuffer);
                    STATUS.MjerenjeTicalom.Visina1.GetValueFromGroupBuffer(CyclicStatusBuffer);
                    STATUS.MjerenjeTicalom.Visina2.GetValueFromGroupBuffer(CyclicStatusBuffer);
                    STATUS.MjerenjeTicalom.Visina3.GetValueFromGroupBuffer(CyclicStatusBuffer);

                    // Mjerenje poroznosti
                    STATUS.MjerenjePoroznosti.Gotovo.GetValueFromGroupBuffer(CyclicStatusBuffer);
                    STATUS.MjerenjePoroznosti.Aktivno.GetValueFromGroupBuffer(CyclicStatusBuffer);
                    STATUS.MjerenjePoroznosti.Greska.GetValueFromGroupBuffer(CyclicStatusBuffer);
                    STATUS.MjerenjePoroznosti.IzvrsavanjeKoraka.GetValueFromGroupBuffer(CyclicStatusBuffer);
                    STATUS.MjerenjePoroznosti.BrojPonavljanjaSekvence.GetValueFromGroupBuffer(CyclicStatusBuffer);
                    STATUS.MjerenjePoroznosti.RotacijskaOs.GetValueFromGroupBuffer(CyclicStatusBuffer);
                    STATUS.MjerenjePoroznosti.HorizontalnaOs.GetValueFromGroupBuffer(CyclicStatusBuffer);
                    STATUS.MjerenjePoroznosti.PoroznostPronadena.GetValueFromGroupBuffer(CyclicStatusBuffer);
                    STATUS.MjerenjePoroznosti.GotovoCAM2.GetValueFromGroupBuffer(CyclicStatusBuffer);
                    STATUS.MjerenjePoroznosti.GotovoCAM3.GetValueFromGroupBuffer(CyclicStatusBuffer);

                    // Mjerenje diametara
                    STATUS.MjerenjeDiametara.Gotovo.GetValueFromGroupBuffer(CyclicStatusBuffer);
                    STATUS.MjerenjeDiametara.Aktivno.GetValueFromGroupBuffer(CyclicStatusBuffer);
                    STATUS.MjerenjeDiametara.Greska.GetValueFromGroupBuffer(CyclicStatusBuffer);
                    STATUS.MjerenjeDiametara.IzvrsavanjeKoraka.GetValueFromGroupBuffer(CyclicStatusBuffer);
                    STATUS.MjerenjeDiametara.BrojPonavljanjaSekvence.GetValueFromGroupBuffer(CyclicStatusBuffer);
                    STATUS.MjerenjeDiametara.VertikalnaOs.GetValueFromGroupBuffer(CyclicStatusBuffer);
                    STATUS.MjerenjeDiametara.HorizontalnaOsS1.GetValueFromGroupBuffer(CyclicStatusBuffer);
                    STATUS.MjerenjeDiametara.HorizontalnaOsS2.GetValueFromGroupBuffer(CyclicStatusBuffer);
                    STATUS.MjerenjeDiametara.AnaliziranaVrijednost.GetValueFromGroupBuffer(CyclicStatusBuffer);
                    STATUS.MjerenjeDiametara.Diametar1.GetValueFromGroupBuffer(CyclicStatusBuffer);
                    STATUS.MjerenjeDiametara.Diametar2.GetValueFromGroupBuffer(CyclicStatusBuffer);
                    STATUS.MjerenjeDiametara.Diametar3.GetValueFromGroupBuffer(CyclicStatusBuffer);
                    STATUS.MjerenjeDiametara.Diametar4.GetValueFromGroupBuffer(CyclicStatusBuffer);
                    STATUS.MjerenjeDiametara.Diametar5.GetValueFromGroupBuffer(CyclicStatusBuffer);

                    // Cilindri
                    STATUS.Cilindri.PPOtvorena.GetValueFromGroupBuffer(CyclicStatusBuffer);
                    STATUS.Cilindri.PPZatvorena.GetValueFromGroupBuffer(CyclicStatusBuffer);
                    STATUS.Cilindri.StegaOtvorena.GetValueFromGroupBuffer(CyclicStatusBuffer);
                    STATUS.Cilindri.StegaZatvorena.GetValueFromGroupBuffer(CyclicStatusBuffer);

                    // Kamere
                    STATUS.Kamere.CAM1ZahtjevZaAnalizomT1.GetValueFromGroupBuffer(CyclicStatusBuffer);
                    STATUS.Kamere.CAM1ZahtjevZaAnalizomT2.GetValueFromGroupBuffer(CyclicStatusBuffer);
                    STATUS.Kamere.CAM2ZahtjevZaAnalizom.GetValueFromGroupBuffer(CyclicStatusBuffer);
                    STATUS.Kamere.CAM3ZahtjevZaAnalizom.GetValueFromGroupBuffer(CyclicStatusBuffer);
                    STATUS.Kamere.CAM4ZahtjevZaAnalizomS1.GetValueFromGroupBuffer(CyclicStatusBuffer);
                    STATUS.Kamere.CAM4ZahtjevZaAnalizomS2.GetValueFromGroupBuffer(CyclicStatusBuffer);
                    STATUS.Kamere.CAM1AnalizaOk.GetValueFromGroupBuffer(CyclicStatusBuffer);
                    STATUS.Kamere.CAM2AnalizaOk.GetValueFromGroupBuffer(CyclicStatusBuffer);
                    STATUS.Kamere.CAM3AnalizaOk.GetValueFromGroupBuffer(CyclicStatusBuffer);
                    STATUS.Kamere.CAM4AnalizaOk.GetValueFromGroupBuffer(CyclicStatusBuffer);
                    STATUS.Kamere.CAM1AnalizaError.GetValueFromGroupBuffer(CyclicStatusBuffer);
                    STATUS.Kamere.CAM2AnalizaError.GetValueFromGroupBuffer(CyclicStatusBuffer);
                    STATUS.Kamere.CAM3AnalizaError.GetValueFromGroupBuffer(CyclicStatusBuffer);
                    STATUS.Kamere.CAM4AnalizaError.GetValueFromGroupBuffer(CyclicStatusBuffer);
                    STATUS.Kamere.CAM1RezultatX.GetValueFromGroupBuffer(CyclicStatusBuffer);
                    STATUS.Kamere.CAM1RezultatY.GetValueFromGroupBuffer(CyclicStatusBuffer);
                    STATUS.Kamere.CAM1RezultatAngle.GetValueFromGroupBuffer(CyclicStatusBuffer);
                    STATUS.Kamere.CAM1RezultatWorkpieceDiameter.GetValueFromGroupBuffer(CyclicStatusBuffer);
                    STATUS.Kamere.CAM2Rezultat.GetValueFromGroupBuffer(CyclicStatusBuffer);
                    STATUS.Kamere.CAM3Rezultat.GetValueFromGroupBuffer(CyclicStatusBuffer);
                    STATUS.Kamere.CAM4Rezultat.GetValueFromGroupBuffer(CyclicStatusBuffer);
                    
                    // Prekret
                    STATUS.Prekret.PrekretLijevo.GetValueFromGroupBuffer(CyclicStatusBuffer);
                    STATUS.Prekret.PrekretDesno.GetValueFromGroupBuffer(CyclicStatusBuffer);
                    STATUS.Prekret.PrekretOtvoren.GetValueFromGroupBuffer(CyclicStatusBuffer);
                    STATUS.Prekret.PrekretZatvoren.GetValueFromGroupBuffer(CyclicStatusBuffer);
                    
                    // Automatika
                    STATUS.Automatika.AutomatskiRad.GetValueFromGroupBuffer(CyclicStatusBuffer);
                    STATUS.Automatika.RucniRad.GetValueFromGroupBuffer(CyclicStatusBuffer);
                    STATUS.Automatika.SnimiMjerenja.GetValueFromGroupBuffer(CyclicStatusBuffer);
                    STATUS.Automatika.StopNaKrajuCiklusa.GetValueFromGroupBuffer(CyclicStatusBuffer);

                    // Upisane Vrijednosti
                    STATUS.Upisanevrijednosti.Dijametar1.GetValueFromGroupBuffer(CyclicStatusBuffer);
                    STATUS.Upisanevrijednosti.Dijametar1DeltaMinus.GetValueFromGroupBuffer(CyclicStatusBuffer);
                    STATUS.Upisanevrijednosti.Dijametar1DeltaPlus.GetValueFromGroupBuffer(CyclicStatusBuffer);
                    STATUS.Upisanevrijednosti.Dijametar2.GetValueFromGroupBuffer(CyclicStatusBuffer);
                    STATUS.Upisanevrijednosti.Dijametar2DeltaMinus.GetValueFromGroupBuffer(CyclicStatusBuffer);
                    STATUS.Upisanevrijednosti.Dijametar2DeltaPlus.GetValueFromGroupBuffer(CyclicStatusBuffer);
                    STATUS.Upisanevrijednosti.Dijametar3.GetValueFromGroupBuffer(CyclicStatusBuffer);
                    STATUS.Upisanevrijednosti.Dijametar3DeltaMinus.GetValueFromGroupBuffer(CyclicStatusBuffer);
                    STATUS.Upisanevrijednosti.Dijametar3DeltaPlus.GetValueFromGroupBuffer(CyclicStatusBuffer);
                    STATUS.Upisanevrijednosti.Dijametar4.GetValueFromGroupBuffer(CyclicStatusBuffer);
                    STATUS.Upisanevrijednosti.Dijametar4DeltaMinus.GetValueFromGroupBuffer(CyclicStatusBuffer);
                    STATUS.Upisanevrijednosti.Dijametar4DeltaPlus.GetValueFromGroupBuffer(CyclicStatusBuffer);
                    STATUS.Upisanevrijednosti.Dijametar5.GetValueFromGroupBuffer(CyclicStatusBuffer);
                    STATUS.Upisanevrijednosti.Dijametar5DeltaMinus.GetValueFromGroupBuffer(CyclicStatusBuffer);
                    STATUS.Upisanevrijednosti.Dijametar5DeltaPlus.GetValueFromGroupBuffer(CyclicStatusBuffer);
                    STATUS.Upisanevrijednosti.Visina1.GetValueFromGroupBuffer(CyclicStatusBuffer);
                    STATUS.Upisanevrijednosti.Visina1DeltaMinus.GetValueFromGroupBuffer(CyclicStatusBuffer);
                    STATUS.Upisanevrijednosti.Visina1DeltaPlus.GetValueFromGroupBuffer(CyclicStatusBuffer);
                    STATUS.Upisanevrijednosti.Visina2.GetValueFromGroupBuffer(CyclicStatusBuffer);
                    STATUS.Upisanevrijednosti.Visina2DeltaMinus.GetValueFromGroupBuffer(CyclicStatusBuffer);
                    STATUS.Upisanevrijednosti.Visina2DeltaPlus.GetValueFromGroupBuffer(CyclicStatusBuffer);
                    STATUS.Upisanevrijednosti.Visina3.GetValueFromGroupBuffer(CyclicStatusBuffer);
                    STATUS.Upisanevrijednosti.Visina3DeltaMinus.GetValueFromGroupBuffer(CyclicStatusBuffer);
                    STATUS.Upisanevrijednosti.Visina3DeltaPlus.GetValueFromGroupBuffer(CyclicStatusBuffer);
                }
            }
            return result;
        }

        public byte[] ReadCustom(int dbNumber, int startByte, int size)
        {
            int result = -99;
            lock (PLCInterface.TimerLock)
            {
                if (Client.Connected())
                {
                    result = Client.DBRead(dbNumber, startByte, size, ReadBuffer);
                }
            }
            return ReadBuffer;
        }
        #endregion

        #region write functions
        /// <summary>
        /// Writes one bit in DBmemory location, returns result of operation
        /// </summary>
        /// <param name="dbNumber"> data block number </param>
        /// <param name="startByte"> byte address in data block </param>
        /// <param name="bitInWord"> bit address in data block </param>
        /// <param name="operation"> operation parameter: acceptible values are "set", "reset", "toggle" </param>
        /// <returns></returns>
        public int WriteBit(int dbNumber, int startByte, int bitInWord, string operation)
        {
            byte[] _tempBuffer = new byte[2];
            int result = -99;
            lock (PLCInterface.TimerLock)
            {
                if (Client.Connected())
                {
                    result = Client.DBRead(dbNumber, startByte, 2, _tempBuffer);
                    switch (operation)
                    {
                        case "set":
                            S7.SetBitAt(ref _tempBuffer, 0, bitInWord, true);
                            break;
                        case "reset":
                            S7.SetBitAt(ref _tempBuffer, 0, bitInWord, false);
                            break;
                        case "toggle":
                            S7.SetBitAt(ref _tempBuffer, 0, bitInWord, !S7.GetBitAt(_tempBuffer, 0, bitInWord));
                            break;
                        default:
                            break;
                    }
                    result += Client.DBWrite(dbNumber, startByte, 2, _tempBuffer);
                }
                try
                {
                    if (result != 0)
                        throw new System.InvalidOperationException("write error");
                }
                finally
                {
                }
            }
            return result;
        }

        public int WriteTag(plcTag tag, object value)
        {
            byte[] _tempBuffer = new byte[4];
            int result = -99;
            lock (PLCInterface.TimerLock)
            {
                if (Client.Connected())
                {
                    switch (tag.VType)
                    {
                        case varType.BOOL:
                            result = Client.DBRead(tag.DbNumber, tag.Offset.ByteOffset, 2, _tempBuffer);
                            S7.SetBitAt(ref _tempBuffer, 0, tag.Offset.BitOffset, (bool)value);
                            result += Client.DBWrite(tag.DbNumber, tag.Offset.ByteOffset, 2, _tempBuffer);
                            break;
                        case varType.BYTE:
                            result = Client.DBRead(tag.DbNumber, tag.Offset.ByteOffset, 2, _tempBuffer);
                            S7.SetByteAt(_tempBuffer, 0, (byte)value);
                            result += Client.DBWrite(tag.DbNumber, tag.Offset.ByteOffset, 2, _tempBuffer);
                            break;
                        case varType.WORD:
                            result = Client.DBRead(tag.DbNumber, tag.Offset.ByteOffset, 2, _tempBuffer);
                            S7.SetWordAt(_tempBuffer, 0, (ushort)value);
                            result += Client.DBWrite(tag.DbNumber, tag.Offset.ByteOffset, 2, _tempBuffer);
                            break;
                        case varType.DWORD:
                            result = Client.DBRead(tag.DbNumber, tag.Offset.ByteOffset, 4, _tempBuffer);
                            S7.SetDWordAt(_tempBuffer, 0, (uint)value);
                            result += Client.DBWrite(tag.DbNumber, tag.Offset.ByteOffset, 4, _tempBuffer);
                            break;
                        case varType.INT:
                            result = Client.DBRead(tag.DbNumber, tag.Offset.ByteOffset, 2, _tempBuffer);
                            S7.SetIntAt(_tempBuffer, 0, (short)value);
                            result += Client.DBWrite(tag.DbNumber, tag.Offset.ByteOffset, 2, _tempBuffer);
                            break;
                        case varType.DINT:
                            result = Client.DBRead(tag.DbNumber, tag.Offset.ByteOffset, 4, _tempBuffer);
                            S7.SetDIntAt(_tempBuffer, 0, (int)value);
                            result += Client.DBWrite(tag.DbNumber, tag.Offset.ByteOffset, 4, _tempBuffer);
                            break;
                        case varType.REAL:
                            result = Client.DBRead(tag.DbNumber, tag.Offset.ByteOffset, 4, _tempBuffer);
                            S7.SetRealAt(_tempBuffer, 0, (float)value);
                            result += Client.DBWrite(tag.DbNumber, tag.Offset.ByteOffset, 4, _tempBuffer);
                            break;
                    }
                }
                try
                {
                    if (result != 0)
                        throw new System.InvalidOperationException("write error");
                }
                catch { }
                finally
                {
                }
            }
            return result;
        }

        public int WriteToggle(plcTag tag)
        {
            byte[] _tempBuffer = new byte[4];
            int result = -99;
            lock (PLCInterface.TimerLock)
            {
                if (Client.Connected())
                {
                    if (tag.VType == varType.BOOL)
                    {
                        if (tag.DType == dataType.DB)
                        {
                            result = Client.DBRead(tag.DbNumber, tag.Offset.ByteOffset, 2, _tempBuffer);
                            S7.SetBitAt(ref _tempBuffer, 0, tag.Offset.BitOffset, !S7.GetBitAt(_tempBuffer, 0, tag.Offset.BitOffset));
                            result += Client.DBWrite(tag.DbNumber, tag.Offset.ByteOffset, 2, _tempBuffer);
                        }
                        if (tag.DType == dataType.Q)
                        {
                            result = Client.ABRead(tag.Offset.ByteOffset,2, _tempBuffer);
                            S7.SetBitAt(ref _tempBuffer, 0, tag.Offset.BitOffset, !S7.GetBitAt(_tempBuffer, 0, tag.Offset.BitOffset));
                            result += Client.ABWrite(tag.Offset.ByteOffset,2,_tempBuffer);
                        }
                        
                    }
                }
                try
                {
                    if (result != 0)
                        throw new System.InvalidOperationException("write error");
                }
                catch { }
                finally
                {
                }
            }
            return result;
        }

        #endregion

        private void onClock100msTick(Object source, System.Timers.ElapsedEventArgs e)
        {
            int result;
            lock (TimerLock)
            {
                result = ReadStatus();
            }

            PLCInterfaceEventArgs p1 = new PLCInterfaceEventArgs();
            p1.StatusData = STATUS;
            p1.CyclicStatusBuffer = CyclicStatusBuffer;
            if (Update_100_ms != null)
                Update_100_ms(this, p1);

            if (updateCounter == 10)
            {
                result = 0;
                lock (TimerLock)
                {
                    result = ReadControl();
                    //result = ReadStatus();
                    //result += ReadManual();
                }
                PLCInterfaceEventArgs p2 = new PLCInterfaceEventArgs();
                p2.ControlData = CONTROL;
                //p2.StatusData = STATUS;
                p2.CyclicControlBuffer = CyclicControlBuffer;
                //p2.CyclicStatusBuffer = CyclicStatusBuffer;
                //p2.StatusData = STATUS;

                //p3
                //PLCInterfaceEventArgs p3 = new PLCInterfaceEventArgs();
                //p3.StatusData = STATUS;
                //p3.CyclicStatusBuffer = CyclicStatusBuffer;

                if ((Update_1_s != null) && (result == 0))
                //if ((Update_1_s != null))
                    Update_1_s(this, p2);
                    //Update_1_s(this, p3);

                updateCounter = 0;
            }

            updateCounter++;
            Clock_100_ms.Start();
        }

        private void onClockWatchdogTick(Object source, System.Timers.ElapsedEventArgs e)
        {
            lock (TimerLock)
            {
                int result = -99;
                Array.Clear(WatchdogBuffer, 0, WatchdogBuffer.Length);
                switch (activeScreen)
                {
                    case 0:
                        break;
                    case 1:
                        S7.SetBitAt(ref WatchdogBuffer, 0, 3, true);
                        break;
                    case 2:
                        S7.SetBitAt(ref WatchdogBuffer, 0, 4, true);
                        break;
                    case 3:
                        S7.SetBitAt(ref WatchdogBuffer, 0, 5, true);
                        break;
                    case 4:
                        S7.SetBitAt(ref WatchdogBuffer, 0, 6, true);
                        break;
                    case 5:
                        S7.SetBitAt(ref WatchdogBuffer, 0, 7, true);
                        break;
                    case 6:
                        S7.SetBitAt(ref WatchdogBuffer, 1, 0, true);
                        break;
                    case 7:
                        S7.SetBitAt(ref WatchdogBuffer, 1, 1, true);
                        break;
                    case 8:
                        S7.SetBitAt(ref WatchdogBuffer, 1, 2, true);
                        break;
                }

                S7.SetBitAt(ref WatchdogBuffer, 0, 1, true);
                result = Client.DBWrite(10, 0, 2, WatchdogBuffer);
                if (result == 0)
                    Errorcode = Client.DBRead(12, 0, 1, WatchdogBuffer);
                else
                    OnlineMark = false;
               
                if (Errorcode == 0)
                {
                    OnlineMark = S7.GetBitAt(WatchdogBuffer, 0, 0);
                }
                else
                {
                    OnlineMark = false;
                   
                }
            }

            OnlineMarkerEventArgs p = new OnlineMarkerEventArgs();
            p.OnlineMark = OnlineMark;
            if (Update_Online_Flag != null)
                Update_Online_Flag(this, p);
            if (OnlineMark)
            {
                WatchDogTimer.Start();
            }
            else
            {
                Errorcode = Errorcode+0;               
                RestartInterface();
            }
        }
    }

    #region plcTag definition
    public class plcTag
    {
        varType vType;
        public varType VType
        {
            get
            {
                return vType;
            }
        }

        dataType dType;
        public dataType DType
        {
            get
            {
                return dType;
            }
        }

        int dbNumber;
        public int DbNumber
        {
            get
            {
                return dbNumber;
            }
        }

        Offset offset;
        public Offset Offset
        {
            get { return offset; }
        }


        public object Value { get; set; }


        public plcTag(varType _vType, dataType _dType, int _dbNumber, Offset _offset, object _value)
        {
            vType = _vType;
            dType = _dType;
            offset = _offset;
            if (dType != dataType.DB)
            {
                dbNumber = 0;
            }
            else
            {
                dbNumber = _dbNumber;
            }
        }
        /// <summary>
        /// extract value from raw buffer
        /// </summary>
        /// <param name="buffer"> buffer length must be greater than Offset.ByteOffset+4, else do nothing </param>
        public void GetValueFromGroupBuffer(byte[] buffer)
        {
            if (buffer.Length<Offset.ByteOffset + 4)
                return;
            switch (VType)
            {
                case varType.BOOL:
                    Value = S7.GetBitAt(buffer, Offset.ByteOffset, Offset.BitOffset);
                    break;
                case varType.BYTE:
                    Value = S7.GetByteAt(buffer, Offset.ByteOffset);
                    break;
                case varType.WORD:
                    Value = S7.GetWordAt(buffer, Offset.ByteOffset);
                    break;
                case varType.DWORD:
                    Value = S7.GetDWordAt(buffer, Offset.ByteOffset);
                    break;
                case varType.INT:
                    Value = S7.GetIntAt(buffer, Offset.ByteOffset);
                    break;
                case varType.DINT:
                    Value = S7.GetDIntAt(buffer, Offset.ByteOffset);
                    break;
                case varType.REAL:
                    Value = S7.GetRealAt(buffer, Offset.ByteOffset);
                    break;
            }
        }
    }
    public struct Offset
    {
        short byteOffset;
        public short ByteOffset
        {
            get { return byteOffset; }
            set { byteOffset = value; }
        }

        short bitOffset;
        public short BitOffset
        {
            get { return bitOffset; }
            set { bitOffset = value; }
        }
        public Offset(short _byteOffset, short _bitOffset)
        {
            byteOffset = _byteOffset;
            bitOffset = _bitOffset;
        }
    }
    public enum varType { BOOL, BYTE, WORD, DWORD, INT, DINT, REAL }; // Enum
    public enum dataType { DB, I, Q, M, L, T };
    public enum buttonFunction { SetBitWhileKeyPressed, InvertBit, SetBit, ResetBit };
    #endregion
}
