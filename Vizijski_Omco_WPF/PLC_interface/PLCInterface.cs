using System;
using System.Windows;
using System.Threading;
using Snap7;

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
                    Client.ConnectTo("192.168.0.1", 0, 1);
                    // Simulation PLC
                    //Client.ConnectTo("192.168.5.195", 0, 1);
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
                result = Client.DBRead(6, 0, 87, CyclicControlBuffer);
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
                    CONTROL.Kamere.PaliLaser.GetValueFromGroupBuffer(CyclicControlBuffer);

                    // Cilindri
                    CONTROL.Cilindri.PPOtvori.GetValueFromGroupBuffer(CyclicControlBuffer);
                    CONTROL.Cilindri.PPZatvori.GetValueFromGroupBuffer(CyclicControlBuffer);
                    CONTROL.Cilindri.StegaOtvori.GetValueFromGroupBuffer(CyclicControlBuffer);
                    CONTROL.Cilindri.StegaZatvori.GetValueFromGroupBuffer(CyclicControlBuffer);

                    // Odabir komada
                    CONTROL.Odabirkomada.tip_4.GetValueFromGroupBuffer(CyclicControlBuffer);
                    CONTROL.Odabirkomada.tip_5.GetValueFromGroupBuffer(CyclicControlBuffer);
                    CONTROL.Odabirkomada.tip_6.GetValueFromGroupBuffer(CyclicControlBuffer);
                    CONTROL.Odabirkomada.tip_7.GetValueFromGroupBuffer(CyclicControlBuffer);
                    // Ucenje
                    CONTROL.Ucenje.NoviNalog.GetValueFromGroupBuffer(CyclicControlBuffer);
                    CONTROL.Ucenje.ResetUcenja.GetValueFromGroupBuffer(CyclicControlBuffer);
                    CONTROL.Ucenje.ZadavanjeDijametara1.GetValueFromGroupBuffer(CyclicControlBuffer);
                    CONTROL.Ucenje.ZadavanjeDijametara2.GetValueFromGroupBuffer(CyclicControlBuffer);
                    CONTROL.Ucenje.ZadavanjeDijametara3.GetValueFromGroupBuffer(CyclicControlBuffer);
                    CONTROL.Ucenje.ZadavanjeDijametara4.GetValueFromGroupBuffer(CyclicControlBuffer);
                    CONTROL.Ucenje.ZadavanjeVisineV1.GetValueFromGroupBuffer(CyclicControlBuffer);
                    CONTROL.Ucenje.ZadavanjeVisineV2.GetValueFromGroupBuffer(CyclicControlBuffer);
                    CONTROL.Ucenje.ZadavanjeVisineV3.GetValueFromGroupBuffer(CyclicControlBuffer);
                    CONTROL.Ucenje.IdiUD1S1.GetValueFromGroupBuffer(CyclicControlBuffer);
                    CONTROL.Ucenje.IdiUD1S2.GetValueFromGroupBuffer(CyclicControlBuffer);
                    CONTROL.Ucenje.IdiUD2S1.GetValueFromGroupBuffer(CyclicControlBuffer);
                    CONTROL.Ucenje.IdiUD2S2.GetValueFromGroupBuffer(CyclicControlBuffer);
                    CONTROL.Ucenje.IdiUD3S1.GetValueFromGroupBuffer(CyclicControlBuffer);
                    CONTROL.Ucenje.IdiUD3S2.GetValueFromGroupBuffer(CyclicControlBuffer);
                    CONTROL.Ucenje.IdiUD4S1.GetValueFromGroupBuffer(CyclicControlBuffer);
                    CONTROL.Ucenje.IdiUD4S2.GetValueFromGroupBuffer(CyclicControlBuffer);
                    CONTROL.Ucenje.IdiNaV1.GetValueFromGroupBuffer(CyclicControlBuffer);
                    CONTROL.Ucenje.IdiNaV2.GetValueFromGroupBuffer(CyclicControlBuffer);
                    CONTROL.Ucenje.NauciD1S1.GetValueFromGroupBuffer(CyclicControlBuffer);
                    CONTROL.Ucenje.NauciD1S2.GetValueFromGroupBuffer(CyclicControlBuffer);
                    CONTROL.Ucenje.NauciD2S1.GetValueFromGroupBuffer(CyclicControlBuffer);
                    CONTROL.Ucenje.NauciD2S2.GetValueFromGroupBuffer(CyclicControlBuffer);
                    CONTROL.Ucenje.NauciD3S1.GetValueFromGroupBuffer(CyclicControlBuffer);
                    CONTROL.Ucenje.NauciD3S2.GetValueFromGroupBuffer(CyclicControlBuffer);
                    CONTROL.Ucenje.NauciD4S1.GetValueFromGroupBuffer(CyclicControlBuffer);
                    CONTROL.Ucenje.NauciD4S2.GetValueFromGroupBuffer(CyclicControlBuffer);
                }
            }
            return result;
        }

        private int ReadStatus()
        {
            int result = -99;
            if (Client.Connected())
                result = Client.DBRead(11, 0, 144, CyclicStatusBuffer);
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
                    STATUS.Kamere.CAM2Rezultat.GetValueFromGroupBuffer(CyclicStatusBuffer);
                    STATUS.Kamere.CAM3Rezultat.GetValueFromGroupBuffer(CyclicStatusBuffer);
                    STATUS.Kamere.CAM4Rezultat.GetValueFromGroupBuffer(CyclicStatusBuffer);
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
            //Thread.CurrentThread.Name = "PLCinterface_100msTick_Thread_" + second_counter.ToString();
            //second_counter++;

            
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
                    //result += ReadManual();
                }
                PLCInterfaceEventArgs p2 = new PLCInterfaceEventArgs();
                p2.ControlData = CONTROL;
                p2.CyclicControlBuffer = CyclicControlBuffer;
                //p2.StatusData = STATUS;

                if ((Update_1_s != null) && (result == 0))
                    Update_1_s(this, p2);

                updateCounter = 0;
            }

            updateCounter++;
            Clock_100_ms.Start();
        }

        private void onClockWatchdogTick(Object source, System.Timers.ElapsedEventArgs e)
        {
            //Thread.CurrentThread.Name = "PLCinterface_WatchdogTick_Thread" + PLCInterface.third_counter.ToString();
            //PLCInterface.third_counter++;

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

    #region Control and Status definitions
    public class Control
    {
        public horizontalnaOs HorizontalnaOs { get; set; } = new horizontalnaOs();
        public vertikalnaOs VertikalnaOs { get; set; } = new vertikalnaOs();
        public rotacijskaOs RotacijskaOs { get; set; } = new rotacijskaOs();
        public ticalo Ticalo { get; set; } = new ticalo();
        public mjerenjeticalom MjerenjeTicalom { get; set; } = new mjerenjeticalom();
        public mjerenjeporoznosti MjerenjePoroznosti { get; set; } = new mjerenjeporoznosti();
        public citanjestringa CitanjeStringa { get; set; } = new citanjestringa();
        public mjerenjediametara MjerenjeDiametara { get; set; } = new mjerenjediametara ();
        public kamere Kamere { get; set; } = new kamere();
        public cilindri Cilindri { get; set; } = new cilindri();
        public odabirkomada Odabirkomada { get; set; } = new odabirkomada();
        public ucenje Ucenje { get; set; } = new ucenje();

        public class horizontalnaOs
        {
            public plcTag IdiUHome { get; set; } = new plcTag(varType.BOOL, dataType.DB, 6, new Offset(0, 0), false);
            public plcTag IdiUPoziciju { get; set; } = new plcTag(varType.BOOL, dataType.DB, 6, new Offset(0, 1), false);
            public plcTag IdiUPozicijuTicala { get; set; } = new plcTag(varType.BOOL, dataType.DB, 6, new Offset(0, 2), false);
            public plcTag Reset { get; set; } = new plcTag(varType.BOOL, dataType.DB, 6, new Offset(0, 3), false);
            public plcTag ZadanaPozicija { get; set; } = new plcTag(varType.REAL, dataType.DB, 6, new Offset(2, 0), 0.0f);
            public plcTag JogPlus { get; set; } = new plcTag(varType.BOOL, dataType.DB, 6, new Offset(6, 0), false);
            public plcTag JogMinus { get; set; } = new plcTag(varType.BOOL, dataType.DB, 6, new Offset(6, 1), false);
            public plcTag IdiURub1 { get; set; } = new plcTag(varType.REAL, dataType.DB, 6, new Offset(8, 0), 0.0f);
            public plcTag IdiURub2 { get; set; } = new plcTag(varType.REAL, dataType.DB, 6, new Offset(12, 0), 0.0f);
            public plcTag AutomatikaStart { get; set; } = new plcTag(varType.BOOL, dataType.DB, 6, new Offset(16, 0), false);

        }
        public class vertikalnaOs
        {
            
            public plcTag IdiUHome { get; set; } = new plcTag(varType.BOOL, dataType.DB, 6, new Offset(18, 0), false);
            public plcTag IdiUPoziciju { get; set; } = new plcTag(varType.BOOL, dataType.DB, 6, new Offset(18, 1), false);
            public plcTag IdiUSafePoziciju { get; set; } = new plcTag(varType.BOOL, dataType.DB, 6, new Offset(18, 2), false);
            public plcTag Reset { get; set; } = new plcTag(varType.BOOL, dataType.DB, 6, new Offset(18, 3), false);
            public plcTag ZadanaPozicija { get; set; } = new plcTag(varType.REAL, dataType.DB, 6, new Offset(20, 0), 0.0f);
            public plcTag JogPlus { get; set; } = new plcTag(varType.BOOL, dataType.DB, 6, new Offset(24, 0), false);
            public plcTag JogMinus { get; set; } = new plcTag(varType.BOOL, dataType.DB, 6, new Offset(24, 1), false);
        }
        public class rotacijskaOs
        {
            
            public plcTag IdiUHome { get; set; } = new plcTag(varType.BOOL, dataType.DB, 6, new Offset(26, 0), false);
            public plcTag IdiUPoziciju { get; set; } = new plcTag(varType.BOOL, dataType.DB, 6, new Offset(26, 1), false);
            public plcTag Referenciraj { get; set; } = new plcTag(varType.BOOL, dataType.DB, 6, new Offset(26, 2), false);
            public plcTag Reset { get; set; } = new plcTag(varType.BOOL, dataType.DB, 6, new Offset(26, 3), false);
            public plcTag ZadanaPozicija { get; set; } = new plcTag(varType.REAL, dataType.DB, 6, new Offset(28, 0), 0.0f);
            public plcTag JogPlus { get; set; } = new plcTag(varType.BOOL, dataType.DB, 6, new Offset(32, 0), false);
            public plcTag JogMinus { get; set; } = new plcTag(varType.BOOL, dataType.DB, 6, new Offset(32, 1), false);
        }
        public class ticalo
        {
            public plcTag TicaloGore { get; set; } = new plcTag(varType.BOOL, dataType.DB, 6, new Offset(34, 0), false);
            public plcTag TicaloDolje { get; set; } = new plcTag(varType.BOOL, dataType.DB, 6, new Offset(34, 1), false);
            public plcTag Nuliraj { get; set; } = new plcTag(varType.BOOL, dataType.DB, 6, new Offset(34, 2), false);
            public plcTag PostaviNaVrijednost { get; set; } = new plcTag(varType.BOOL, dataType.DB, 6, new Offset(34, 3), false);
        }

        public class mjerenjeticalom
        {
            public plcTag Start { get; set; } = new plcTag(varType.BOOL, dataType.DB, 6, new Offset(36, 0), false);
            public plcTag Stop { get; set; } = new plcTag(varType.BOOL, dataType.DB, 6, new Offset(36, 1), false);
            public plcTag Pauza { get; set; } = new plcTag(varType.BOOL, dataType.DB, 6, new Offset(36, 2), false);
            public plcTag Reset { get; set; } = new plcTag(varType.BOOL, dataType.DB, 6, new Offset(36, 3), false);
        }

        public class mjerenjeporoznosti
        {
            public plcTag Start { get; set; } = new plcTag(varType.BOOL, dataType.DB, 6, new Offset(38, 0), false);
            public plcTag Stop { get; set; } = new plcTag(varType.BOOL, dataType.DB, 6, new Offset(38, 1), false);
            public plcTag Pauza { get; set; } = new plcTag(varType.BOOL, dataType.DB, 6, new Offset(38, 2), false);
            public plcTag Reset { get; set; } = new plcTag(varType.BOOL, dataType.DB, 6, new Offset(38, 3), false);
            public plcTag ZadanaPovrsina { get; set; } = new plcTag(varType.REAL, dataType.DB, 6, new Offset(40, 0), 0.0f);
        }

        public class mjerenjediametara
        {
            public plcTag Start { get; set; } = new plcTag(varType.BOOL, dataType.DB, 6, new Offset(44, 0), false);
            public plcTag Stop { get; set; } = new plcTag(varType.BOOL, dataType.DB, 6, new Offset(44, 1), false);
            public plcTag Pauza { get; set; } = new plcTag(varType.BOOL, dataType.DB, 6, new Offset(44, 2), false);
            public plcTag Reset { get; set; } = new plcTag(varType.BOOL, dataType.DB, 6, new Offset(44, 3), false);
        }

        public class citanjestringa
        {
            public plcTag Start { get; set; } = new plcTag(varType.BOOL, dataType.DB, 6, new Offset(46, 0), false);
            public plcTag Stop { get; set; } = new plcTag(varType.BOOL, dataType.DB, 6, new Offset(46, 1), false);
            public plcTag Pauza { get; set; } = new plcTag(varType.BOOL, dataType.DB, 6, new Offset(46, 2), false);
            public plcTag Reset { get; set; } = new plcTag(varType.BOOL, dataType.DB, 6, new Offset(46, 3), false);
        }

        public class kamere
        {
            public plcTag RasvjetaZaString { get; set; } = new plcTag(varType.BOOL, dataType.DB, 6, new Offset(48, 0), false);
            public plcTag RasvjetaZaDimenzije { get; set; } = new plcTag(varType.BOOL, dataType.DB, 6, new Offset(48, 1), false);
            public plcTag RasvjetaZaPoroznost { get; set; } = new plcTag(varType.BOOL, dataType.DB, 6, new Offset(48, 2), false);
            public plcTag PaliLaser { get; set; } = new plcTag(varType.BOOL, dataType.DB, 6, new Offset(48, 3), false);
            public plcTag CAM4zahtjevZaAnalizomS1 { get; set; } = new plcTag(varType.BOOL, dataType.DB, 6, new Offset(48, 4), false);
            public plcTag CAM4zahtjevZaAnalizomS2 { get; set; } = new plcTag(varType.BOOL, dataType.DB, 6, new Offset(48, 5), false);
        }

        public class cilindri
        {
            public plcTag PPOtvori { get; set; } = new plcTag(varType.BOOL, dataType.DB, 6, new Offset(50, 0), false);
            public plcTag PPZatvori { get; set; } = new plcTag(varType.BOOL, dataType.DB, 6, new Offset(50, 1), false);
            public plcTag StegaOtvori { get; set; } = new plcTag(varType.BOOL, dataType.DB, 6, new Offset(50, 2), false);
            public plcTag StegaZatvori { get; set; } = new plcTag(varType.BOOL, dataType.DB, 6, new Offset(50, 3), false);
        }

        public class odabirkomada
        {
            public plcTag tip_4 { get; set; } = new plcTag(varType.BOOL, dataType.DB, 6, new Offset(52, 0), false);
            public plcTag tip_5 { get; set; } = new plcTag(varType.BOOL, dataType.DB, 6, new Offset(52, 1), false);
            public plcTag tip_6 { get; set; } = new plcTag(varType.BOOL, dataType.DB, 6, new Offset(52, 2), false);
            public plcTag tip_7 { get; set; } = new plcTag(varType.BOOL, dataType.DB, 6, new Offset(52, 3), false);
        }

        public class ucenje
        {
            public plcTag NoviNalog { get; set; } = new plcTag(varType.BOOL, dataType.DB, 6, new Offset(54, 0), false);
            public plcTag ResetUcenja { get; set; } = new plcTag(varType.BOOL, dataType.DB, 6, new Offset(54, 1), false);
            public plcTag ZadavanjeDijametara1 { get; set; } = new plcTag(varType.REAL, dataType.DB, 6, new Offset(56, 0), 0.0f);
            public plcTag ZadavanjeDijametara2 { get; set; } = new plcTag(varType.REAL, dataType.DB, 6, new Offset(60, 0), 0.0f);
            public plcTag ZadavanjeDijametara3 { get; set; } = new plcTag(varType.REAL, dataType.DB, 6, new Offset(64, 0), 0.0f);
            public plcTag ZadavanjeDijametara4 { get; set; } = new plcTag(varType.REAL, dataType.DB, 6, new Offset(68, 0), 0.0f);
            public plcTag ZadavanjeVisineV1 { get; set; } = new plcTag(varType.REAL, dataType.DB, 6, new Offset(72, 0), 0.0f);
            public plcTag ZadavanjeVisineV2 { get; set; } = new plcTag(varType.REAL, dataType.DB, 6, new Offset(76, 0), 0.0f);
            public plcTag ZadavanjeVisineV3 { get; set; } = new plcTag(varType.REAL, dataType.DB, 6, new Offset(80, 0), 0.0f);
            public plcTag IdiUD1S1 { get; set; } = new plcTag(varType.BOOL, dataType.DB, 6, new Offset(84, 0), false);
            public plcTag IdiUD1S2 { get; set; } = new plcTag(varType.BOOL, dataType.DB, 6, new Offset(84, 1), false);
            public plcTag IdiUD2S1 { get; set; } = new plcTag(varType.BOOL, dataType.DB, 6, new Offset(84, 2), false);
            public plcTag IdiUD2S2 { get; set; } = new plcTag(varType.BOOL, dataType.DB, 6, new Offset(84, 3), false);
            public plcTag IdiUD3S1 { get; set; } = new plcTag(varType.BOOL, dataType.DB, 6, new Offset(84, 4), false);
            public plcTag IdiUD3S2 { get; set; } = new plcTag(varType.BOOL, dataType.DB, 6, new Offset(84, 5), false);
            public plcTag IdiUD4S1 { get; set; } = new plcTag(varType.BOOL, dataType.DB, 6, new Offset(84, 6), false);
            public plcTag IdiUD4S2 { get; set; } = new plcTag(varType.BOOL, dataType.DB, 6, new Offset(84, 7), false);
            public plcTag IdiNaV1 { get; set; } = new plcTag(varType.BOOL, dataType.DB, 6, new Offset(85, 0), false);
            public plcTag IdiNaV2 { get; set; } = new plcTag(varType.BOOL, dataType.DB, 6, new Offset(85, 1), false);
            public plcTag NauciD1S1 { get; set; } = new plcTag(varType.BOOL, dataType.DB, 6, new Offset(85, 2), false);
            public plcTag NauciD1S2 { get; set; } = new plcTag(varType.BOOL, dataType.DB, 6, new Offset(85, 3), false);
            public plcTag NauciD2S1 { get; set; } = new plcTag(varType.BOOL, dataType.DB, 6, new Offset(85, 4), false);
            public plcTag NauciD2S2 { get; set; } = new plcTag(varType.BOOL, dataType.DB, 6, new Offset(85, 5), false);
            public plcTag NauciD3S1 { get; set; } = new plcTag(varType.BOOL, dataType.DB, 6, new Offset(85, 6), false);
            public plcTag NauciD3S2 { get; set; } = new plcTag(varType.BOOL, dataType.DB, 6, new Offset(85, 7), false);
            public plcTag NauciD4S1 { get; set; } = new plcTag(varType.BOOL, dataType.DB, 6, new Offset(86, 0), false);
            public plcTag NauciD4S2 { get; set; } = new plcTag(varType.BOOL, dataType.DB, 6, new Offset(86, 1), false);
        }

    }

    public class Status
    {
        public horizontalnaOs HorizontalnaOs { get; set; } = new horizontalnaOs();
        public vertikalnaOs VertikalnaOs { get; set; } = new vertikalnaOs();
        public rotacijskaOs RotacijskaOs { get; set; } = new rotacijskaOs();
        public ticalo Ticalo { get; set; } = new ticalo();
        public mjerenjeticalom MjerenjeTicalom { get; set; } = new mjerenjeticalom();
        public mjerenjeporoznosti MjerenjePoroznosti { get; set; } = new mjerenjeporoznosti();
        public mjerenjediametara MjerenjeDiametara { get; set; } = new mjerenjediametara();
        public cilindri Cilindri { get; set; } = new cilindri();
        public kamere Kamere { get; set; } = new kamere();


        public class horizontalnaOs
        {
           
            public plcTag UHome { get; set; } = new plcTag(varType.BOOL, dataType.DB, 11, new Offset(0, 0), false);
            public plcTag UPoziciji { get; set; } = new plcTag(varType.BOOL, dataType.DB, 11, new Offset(0, 1), false);
            public plcTag UPozicijiTicala { get; set; } = new plcTag(varType.BOOL, dataType.DB, 11, new Offset(0, 2), false);
            public plcTag Greska { get; set; } = new plcTag(varType.BOOL, dataType.DB, 11, new Offset(0, 3), false);
            public plcTag AktualnaPozicija { get; set; } = new plcTag(varType.REAL, dataType.DB, 11, new Offset(2, 0), 0.0f);
        
        }

        public class vertikalnaOs
        {
            public plcTag UHome { get; set; } = new plcTag(varType.BOOL, dataType.DB, 11, new Offset(6, 0), false);
            public plcTag UPoziciji { get; set; } = new plcTag(varType.BOOL, dataType.DB, 11, new Offset(6, 1), false);
            public plcTag USafePoziciji { get; set; } = new plcTag(varType.BOOL, dataType.DB, 11, new Offset(6, 2), false);
            public plcTag Greska { get; set; } = new plcTag(varType.BOOL, dataType.DB, 11, new Offset(6, 3), false);
            public plcTag AktualnaPozicija { get; set; } = new plcTag(varType.REAL, dataType.DB, 11, new Offset(8, 0), 0.0f);
        }
        public class rotacijskaOs
        {
            public plcTag UHome { get; set; } = new plcTag(varType.BOOL, dataType.DB, 11, new Offset(12, 0), false);
            public plcTag UPoziciji { get; set; } = new plcTag(varType.BOOL, dataType.DB, 11, new Offset(12, 1), false);
            public plcTag Greska { get; set; } = new plcTag(varType.BOOL, dataType.DB, 11, new Offset(12, 2), false);
            public plcTag AktualnaPozicija { get; set; } = new plcTag(varType.REAL, dataType.DB, 11, new Offset(14, 0), 0.0f);
            public plcTag Referencirana { get; set; } = new plcTag(varType.BOOL, dataType.DB, 11, new Offset(18, 0), false);
        }

        public class ticalo
        {
            public plcTag TicaloDolje { get; set; } = new plcTag(varType.BOOL, dataType.DB, 11, new Offset(20, 0), false);
            public plcTag TicaloGore { get; set; } = new plcTag(varType.BOOL, dataType.DB, 11, new Offset(20, 1), false);
            public plcTag AktualnaPozicija { get; set; } = new plcTag(varType.REAL, dataType.DB, 11, new Offset(22, 0), 0.0f);
            public plcTag Referencirano { get; set; } = new plcTag(varType.BOOL, dataType.DB, 11, new Offset(26, 0), false);
            
        }

        public class mjerenjeticalom
        {
            public plcTag Gotovo { get; set; } = new plcTag(varType.BOOL, dataType.DB, 11, new Offset(28, 0), false);
            public plcTag Aktivno { get; set; } = new plcTag(varType.BOOL, dataType.DB, 11, new Offset(28, 1), false);
            public plcTag Greska { get; set; } = new plcTag(varType.BOOL, dataType.DB, 11, new Offset(28, 2), false);
            public plcTag IzvrsavanjeKoraka { get; set; } = new plcTag(varType.REAL, dataType.DB, 11, new Offset(30, 0), 0.0f);
            public plcTag BrojPonavljanjaSekvence { get; set; } = new plcTag(varType.REAL, dataType.DB, 11, new Offset(34, 0), 0.0f);
            public plcTag RotacijskaOs { get; set; } = new plcTag(varType.REAL, dataType.DB, 11, new Offset(38, 0), 0.0f);
            public plcTag HorizontalnaOs { get; set; } = new plcTag(varType.REAL, dataType.DB, 11, new Offset(42, 0), 0.0f);
            public plcTag Visina1 { get; set; } = new plcTag(varType.REAL, dataType.DB, 11, new Offset(46, 0), 0.0f);
            public plcTag Visina2 { get; set; } = new plcTag(varType.REAL, dataType.DB, 11, new Offset(50, 0), 0.0f);
            public plcTag Visina3 { get; set; } = new plcTag(varType.REAL, dataType.DB, 11, new Offset(54, 0), 0.0f);
        }

        public class mjerenjeporoznosti
        {
            public plcTag Gotovo { get; set; } = new plcTag(varType.BOOL, dataType.DB, 11, new Offset(58, 0), false);
            public plcTag Aktivno { get; set; } = new plcTag(varType.BOOL, dataType.DB, 11, new Offset(58, 1), false);
            public plcTag Greska { get; set; } = new plcTag(varType.BOOL, dataType.DB, 11, new Offset(58, 2), false);
            public plcTag IzvrsavanjeKoraka { get; set; } = new plcTag(varType.REAL, dataType.DB, 11, new Offset(60, 0), 0.0f);
            public plcTag BrojPonavljanjaSekvence { get; set; } = new plcTag(varType.REAL, dataType.DB, 11, new Offset(64, 0), 0.0f);
            public plcTag RotacijskaOs { get; set; } = new plcTag(varType.REAL, dataType.DB, 11, new Offset(68, 0), 0.0f);
            public plcTag HorizontalnaOs { get; set; } = new plcTag(varType.REAL, dataType.DB, 11, new Offset(72, 0), 0.0f);
            public plcTag PoroznostPronadena { get; set; } = new plcTag(varType.BOOL, dataType.DB, 11, new Offset(76, 0), false);
        }

        public class mjerenjediametara
        {
            public plcTag Gotovo { get; set; } = new plcTag(varType.BOOL, dataType.DB, 11, new Offset(78, 0), false);
            public plcTag Aktivno { get; set; } = new plcTag(varType.BOOL, dataType.DB, 11, new Offset(78, 1), false);
            public plcTag Greska { get; set; } = new plcTag(varType.BOOL, dataType.DB, 11, new Offset(78, 2), false);
            public plcTag IzvrsavanjeKoraka { get; set; } = new plcTag(varType.REAL, dataType.DB, 11, new Offset(80, 0), 0.0f);
            public plcTag BrojPonavljanjaSekvence { get; set; } = new plcTag(varType.REAL, dataType.DB, 11, new Offset(84, 0), 0.0f);
            public plcTag VertikalnaOs { get; set; } = new plcTag(varType.REAL, dataType.DB, 11, new Offset(88, 0), 0.0f);
            public plcTag HorizontalnaOsS1 { get; set; } = new plcTag(varType.REAL, dataType.DB, 11, new Offset(92, 0), 0.0f);
            public plcTag HorizontalnaOsS2 { get; set; } = new plcTag(varType.REAL, dataType.DB, 11, new Offset(96, 0), 0.0f);
            public plcTag AnaliziranaVrijednost { get; set; } = new plcTag(varType.REAL, dataType.DB, 11, new Offset(100, 0), 0.0f);
            public plcTag Diametar1 { get; set; } = new plcTag(varType.REAL, dataType.DB, 11, new Offset(104, 0), 0.0f);
            public plcTag Diametar2 { get; set; } = new plcTag(varType.REAL, dataType.DB, 11, new Offset(108, 0), 0.0f);
            public plcTag Diametar3 { get; set; } = new plcTag(varType.REAL, dataType.DB, 11, new Offset(112, 0), 0.0f);
            public plcTag Diametar4 { get; set; } = new plcTag(varType.REAL, dataType.DB, 11, new Offset(116, 0), 0.0f);
        }

        public class cilindri
        {
            public plcTag PPOtvorena { get; set; } = new plcTag(varType.BOOL, dataType.DB, 11, new Offset(120, 0), false);
            public plcTag PPZatvorena { get; set; } = new plcTag(varType.BOOL, dataType.DB, 11, new Offset(120, 1), false);
            public plcTag StegaOtvorena { get; set; } = new plcTag(varType.BOOL, dataType.DB, 11, new Offset(120, 2), false);
            public plcTag StegaZatvorena { get; set; } = new plcTag(varType.BOOL, dataType.DB, 11, new Offset(120, 3), false);
        }

        public class kamere
        {
            public plcTag CAM1ZahtjevZaAnalizomT1 { get; set; } = new plcTag(varType.BOOL, dataType.DB, 11, new Offset(122, 0), false);
            public plcTag CAM1ZahtjevZaAnalizomT2 { get; set; } = new plcTag(varType.BOOL, dataType.DB, 11, new Offset(122, 1), false);
            public plcTag CAM2ZahtjevZaAnalizom { get; set; } = new plcTag(varType.BOOL, dataType.DB, 11, new Offset(122, 2), false);
            public plcTag CAM3ZahtjevZaAnalizom { get; set; } = new plcTag(varType.BOOL, dataType.DB, 11, new Offset(122, 3), false);
            public plcTag CAM4ZahtjevZaAnalizomS1 { get; set; } = new plcTag(varType.BOOL, dataType.DB, 11, new Offset(122, 4), false);
            public plcTag CAM4ZahtjevZaAnalizomS2 { get; set; } = new plcTag(varType.BOOL, dataType.DB, 11, new Offset(122, 5), false);
            public plcTag CAM1AnalizaOk { get; set; } = new plcTag(varType.BOOL, dataType.DB, 11, new Offset(122, 6), false);
            public plcTag CAM2AnalizaOk { get; set; } = new plcTag(varType.BOOL, dataType.DB, 11, new Offset(122, 7), false);
            public plcTag CAM3AnalizaOk { get; set; } = new plcTag(varType.BOOL, dataType.DB, 11, new Offset(123, 0), false);
            public plcTag CAM4AnalizaOk { get; set; } = new plcTag(varType.BOOL, dataType.DB, 11, new Offset(123, 1), false);
            public plcTag CAM1AnalizaError { get; set; } = new plcTag(varType.BOOL, dataType.DB, 11, new Offset(123, 2), false);
            public plcTag CAM2AnalizaError { get; set; } = new plcTag(varType.BOOL, dataType.DB, 11, new Offset(123, 3), false);
            public plcTag CAM3AnalizaError { get; set; } = new plcTag(varType.BOOL, dataType.DB, 11, new Offset(123, 4), false);
            public plcTag CAM4AnalizaError { get; set; } = new plcTag(varType.BOOL, dataType.DB, 11, new Offset(123, 5), false);
            public plcTag CAM1RezultatX { get; set; } = new plcTag(varType.REAL, dataType.DB, 11, new Offset(124, 0), 0.0f);
            public plcTag CAM1RezultatY { get; set; } = new plcTag(varType.REAL, dataType.DB, 11, new Offset(128, 0), 0.0f);
            public plcTag CAM2Rezultat { get; set; } = new plcTag(varType.REAL, dataType.DB, 11, new Offset(132, 0), 0.0f);
            public plcTag CAM3Rezultat { get; set; } = new plcTag(varType.REAL, dataType.DB, 11, new Offset(136, 0), 0.0f);
            public plcTag CAM4Rezultat { get; set; } = new plcTag(varType.REAL, dataType.DB, 11, new Offset(140, 0), 0.0f);
        }
    }

    #endregion

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
