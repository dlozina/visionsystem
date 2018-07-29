using System.Windows;
using HalconDotNet;
using System.Threading;
using System.Drawing;
using System.Windows.Media;

namespace VizijskiSustavWPF
{
    
    public partial class PUcenje
    {
        private int _clickNumber = 1;
        public int ClickNumber
        {
            get { return _clickNumber; }
        }

        public PUcenje()
        {
            InitializeComponent();
            BpozicijaDijametri.Content = "POZICIJA D1S1";
            //App.PLC.Update_100_ms += new PLCInterface.UpdateHandler(updatePagePRucno_100ms);
            //App.PLC.Update_1_s += new PLCInterface.UpdateHandler(updatePagePRucno_1s);
        }

        private void Window_Unloaded(object sender, RoutedEventArgs e)
        {
            // Gasi kamere kad prebacis screen
            App.HDevExp.Exitloop2 = true;
            App.HDevExp.Exitloop3 = true;
            App.HDevExp.Exitloop4 = true;
            App.DiameterLightOFF();
            App.PorosityLightOFF();
        }

        //private void updatePagePRucno_100ms(object sender, PLCInterfaceEventArgs e)
        //{

        //}

        //private void updatePagePRucno_1s(object sender, PLCInterfaceEventArgs e)
        //{

        //}

        private void TeachCam4()
        {
            App.HDevExp.InitHalcon();
            HTuple windowId = hwindowTeach.HalconID;
            App.HDevExp.RunCam4(windowId,true,_clickNumber);
        }

        private void AnalizeD1S1()
        {
            App.HDevExp.InitHalcon();
            HTuple windowId = hwindowTeach.HalconID;
            App.HDevExp.RunHalcon1(windowId);
        }

        private void AnalizeD1S2()
        {
            App.HDevExp.InitHalcon();
            HTuple windowId = hwindowTeach.HalconID;
            App.HDevExp.RunHalcon2(windowId);
        }

        private void AnalizeD2S1()
        {
            App.HDevExp.InitHalcon();
            HTuple windowId = hwindowTeach.HalconID;
            App.HDevExp.RunHalcon3(windowId);
        }

        private void AnalizeD2S2()
        {
            App.HDevExp.InitHalcon();
            HTuple windowId = hwindowTeach.HalconID;
            App.HDevExp.RunHalcon4(windowId);
        }

        private void AnalizeD3S1()
        {
            App.HDevExp.InitHalcon();
            HTuple windowId = hwindowTeach.HalconID;
            App.HDevExp.RunHalcon5(windowId);
        }

        private void AnalizeD3S2()
        {
            App.HDevExp.InitHalcon();
            HTuple windowId = hwindowTeach.HalconID;
            App.HDevExp.RunHalcon6(windowId);
        }

        private void AnalizeD4S1()
        {
            App.HDevExp.InitHalcon();
            HTuple windowId = hwindowTeach.HalconID;
            App.HDevExp.RunHalcon7(windowId);
        }

        private void AnalizeD4S2()
        {
            App.HDevExp.InitHalcon();
            HTuple windowId = hwindowTeach.HalconID;
            App.HDevExp.RunHalcon8(windowId);
        }

        private void AnalizeD5S1()
        {
            App.HDevExp.InitHalcon();
            HTuple windowId = hwindowTeach.HalconID;
            App.HDevExp.RunDia5side1(windowId);
        }

        private void AnalizeD5S2()
        {
            App.HDevExp.InitHalcon();
            HTuple windowId = hwindowTeach.HalconID;
            App.HDevExp.RunDia5side2(windowId);
        }

        private void b_sTOPKamere_Click(object sender, RoutedEventArgs e)
        {
            App.HDevExp.Exitloop4 = true;
        }

        //private void b_clearScreen_Click(object sender, RoutedEventArgs e)
        //{
        //    HOperatorSet.ClearWindow(hwindowTeach.HalconID);
        //}

        // Ucenje dijametara
        private void BpozicijaDijametri_Click(object sender, RoutedEventArgs e)
        {
            // Prvi Klik D1S1
            if (_clickNumber == 1)
            {
                //PLC kontrola
                App.ActivateControlD1S1();
            }
            // Prvi Klik D2S1
            else if (_clickNumber == 2)
            {
                //PLC kontrola
                App.ActivateControlD2S1();
            }
            // Prvi Klik D3S1
            else if (_clickNumber == 3)
            {
                //PLC kontrola
                App.ActivateControlD3S1();
            }
            // Prvi Klik D4S1
            else if (_clickNumber == 4)
            {
                //PLC kontrola
                App.ActivateControlD4S1();
            }
            // Prvi Klik D4S2
            else if (_clickNumber == 5)
            {
                App.ActivateControlD4S2();
            }
            // Prvi Klik D3S2
            else if (_clickNumber == 6)
            {
                App.ActivateControlD3S2();
            }
            // Prvi Klik D2S2
            else if (_clickNumber == 7)
            {
                App.ActivateControlD2S2();
            }
            // Prvi Klik D1S2
            else if (_clickNumber == 8)
            {
                App.ActivateControlD1S2();
            }
            // Prvi Klik D5S1
            else if (_clickNumber == 9)
            {
                //PLC kontrola
                App.ActivateControlD5S1();
            }
            // Prvi Klik D5S2
            else if (_clickNumber == 10)
            {
                App.ActivateControlD5S2();
            }
        }

        private void Btest_Click(object sender, RoutedEventArgs e)
        {
            App.HDevExp.Exitloop4 = true;

            //while (App.HDevExp.FramegrabberClosed4 == false)
            //{
                //if (App.HDevExp.FramegrabberClosed4 == true)
                //{
                    // Prvi Klik D1S1
                    if (_clickNumber == 1)
                    {
                        App.HDevExp.Exitloop4 = true;
                        Thread TestAnalizeD1S1 = new Thread(AnalizeD1S1) { Name = "TestAnalizeD1S1Thread" };
                        TestAnalizeD1S1.Start();
                    }
                    // Prvi Klik D2S1
                    else if (_clickNumber == 2)
                    {
                        App.HDevExp.Exitloop4 = true;
                        Thread TestAnalizeD2S1 = new Thread(AnalizeD2S1) { Name = "TestAnalizeD2S1Thread" };
                        TestAnalizeD2S1.Start();
                    }
                    // Prvi Klik D3S1
                    else if (_clickNumber == 3)
                    {
                        App.HDevExp.Exitloop4 = true;
                        Thread TestAnalizeD3S1 = new Thread(AnalizeD3S1) { Name = "TestAnalizeD3S1Thread" };
                        TestAnalizeD3S1.Start();
                    }
                    // Prvi Klik D4S1
                    else if (_clickNumber == 4)
                    {
                        App.HDevExp.Exitloop4 = true;
                        Thread TestAnalizeD4S1 = new Thread(AnalizeD4S1) { Name = "TestAnalizeD4S1Thread" };
                        TestAnalizeD4S1.Start();

                    }
                    // Prvi Klik D4S2
                    else if (_clickNumber == 5)
                    {
                        App.HDevExp.Exitloop4 = true;
                        Thread TestAnalizeD4S2 = new Thread(AnalizeD4S2) { Name = "TestAnalizeD4S2Thread" };
                        TestAnalizeD4S2.Start();
                    }
                    // Prvi Klik D3S2
                    else if (_clickNumber == 6)
                    {
                        App.HDevExp.Exitloop4 = true;
                        Thread TestAnalizeD3S2 = new Thread(AnalizeD3S2) { Name = "TestAnalizeD3S2Thread" };
                        TestAnalizeD3S2.Start();
                    }
                    // Prvi Klik D2S2
                    else if (_clickNumber == 7)
                    {
                        App.HDevExp.Exitloop4 = true;
                        Thread TestAnalizeD2S2 = new Thread(AnalizeD2S2) { Name = "TestAnalizeD2S2Thread" };
                        TestAnalizeD2S2.Start();
                    }
                    // Prvi Klik D1S2
                    else if (_clickNumber == 8)
                    {
                        App.HDevExp.Exitloop4 = true;
                        Thread TestAnalizeD1S2 = new Thread(AnalizeD1S2) { Name = "TestAnalizeD1S2Thread" };
                        TestAnalizeD1S2.Start();
                    }
                    // Prvi Klik D5S1
                    else if (_clickNumber == 9)
                    {
                        App.HDevExp.Exitloop4 = true;
                        Thread TestAnalizeD5S1 = new Thread(AnalizeD5S1) { Name = "TestAnalizeD5S1Thread" };
                        TestAnalizeD5S1.Start();
                    }
                    // Prvi Klik D5S2
                    else if (_clickNumber == 10)
                    {
                        App.HDevExp.Exitloop4 = true;
                        Thread TestAnalizeD5S2 = new Thread(AnalizeD5S2) { Name = "TestAnalizeD5S2Thread" };
                        TestAnalizeD5S2.Start();
                    }
                //}
            //}
            //App.HDevExp.FramegrabberClosed4 = false;
        }

        private void BnauciPozicijuDijametri_Click(object sender, RoutedEventArgs e)
        {
            // Prvi Klik D1S1
            if (_clickNumber == 1)
            {
                // Omoguci idi na drugu poziciju
                // Label mora biti od iduceg promjera
                BpozicijaDijametri.Content = "POZICIJA D2S1";
                //PLC kontrola
                App.ActivateTeachD1S1();
            }
            // Prvi Klik D2S1
            else if (_clickNumber == 2)
            {
                // Omoguci idi na prvu poziciju
                BpozicijaDijametri.Content = "POZICIJA D3S1";
                App.ActivateTeachD2S1();
            }
            // Prvi Klik D3S1
            else if (_clickNumber == 3)
            {
                // Omoguci idi na drugu poziciju
                BpozicijaDijametri.Content = "POZICIJA D4S1";
                //PLC kontrola
                App.ActivateTeachD3S1();
            }
            // Prvi Klik D4S1
            else if (_clickNumber == 4)
            {
                // Omoguci idi na prvu poziciju
                BpozicijaDijametri.Content = "POZICIJA D4S2";
                App.ActivateTeachD4S1();
            }
            // Prvi Klik D4S2
            else if (_clickNumber == 5)
            {
                // Omoguci idi na drugu poziciju
                BpozicijaDijametri.Content = "POZICIJA D3S2";
                //PLC kontrola
                App.ActivateTeachD4S2();
            }
            // Prvi Klik D3S2
            else if (_clickNumber == 6)
            {
                // Omoguci idi na prvu poziciju
                BpozicijaDijametri.Content = "POZICIJA D2S2";
                App.ActivateTeachD3S2();
            }
            // Prvi Klik D2S2
            else if (_clickNumber == 7)
            {
                // Omoguci idi na drugu poziciju
                BpozicijaDijametri.Content = "POZICIJA D1S2";
                //PLC kontrola
                App.ActivateTeachD2S2();
            }
            // Prvi Klik D1S2
            else if (_clickNumber == 8)
            {
                // Omoguci idi na prvu poziciju
                BpozicijaDijametri.Content = "POZICIJA D5S1";
                App.ActivateTeachD1S2();
            }
            // Prvi Klik D5S1
            else if (_clickNumber == 9)
            {
                // Omoguci idi na drugu poziciju
                BpozicijaDijametri.Content = "POZICIJA D5S2";
                //PLC kontrola
                App.ActivateTeachD5S1();
            }
            // Prvi Klik D5S2
            else if (_clickNumber == 10)
            {
                // Omoguci idi na prvu poziciju
                BpozicijaDijametri.IsEnabled = false;
                BpozicijaDijametri.Content = "SPREMLJENO";
                BpozicijaDijametri.Foreground = new SolidColorBrush(Colors.Green);
                //BnauciPozicijuDijametri.IsEnabled = false;
                //BnauciPozicijuDijametri.Foreground = new SolidColorBrush(Colors.Gray);
                App.ActivateTeachD5S2();
            }
            _clickNumber++; 
        }

        

        private void BresetUcenjaDijametri_Click(object sender, RoutedEventArgs e)
        {
            _clickNumber = 1;
            BpozicijaDijametri.IsEnabled = true;
            BpozicijaDijametri.Foreground = new SolidColorBrush(Colors.Black);
            BpozicijaDijametri.Content = "POZICIJA D1S1";
            //
            BnauciPozicijuDijametri.IsEnabled = true;
            BnauciPozicijuDijametri.Foreground = new SolidColorBrush(Colors.Black);
        }

        private void b_startKamere_Click(object sender, RoutedEventArgs e)
        {
            App.HDevExp.Exitloop2 = true;
            App.HDevExp.Exitloop3 = true;
            App.HDevExp.Exitloop4 = false;
            App.DiameterLightON();
            App.PorosityLightOFF();
            hwindowTeach.HImagePart = new Rect(0, 0, 2448, 2050);
            Thread TeachCAM4Thread = new Thread(TeachCam4) { Name = "TeachCAM4Thread" };
            TeachCAM4Thread.Start();
        }

        private void BPrijasnjiRub_Click(object sender, RoutedEventArgs e)
        {
            //if (i >= 1 && i <= 10)
            //{
            //    i--;
            //}

            _clickNumber--;

            if (_clickNumber < 1)
            {
                _clickNumber = 1;
            }

            if (_clickNumber > 10)
            {
                _clickNumber = 10;
            }

            // Prvi Klik D1S1
            if (_clickNumber == 1)
            {
                // Omoguci idi na drugu poziciju
                BpozicijaDijametri.Content = "POZICIJA D1S1";
            }
            // Prvi Klik D2S1
            else if (_clickNumber == 2)
            {
                // Omoguci idi na prvu poziciju
                BpozicijaDijametri.Content = "POZICIJA D2S1";
            }
            // Prvi Klik D3S1
            else if (_clickNumber == 3)
            {
                // Omoguci idi na drugu poziciju
                BpozicijaDijametri.Content = "POZICIJA D3S1";
            }
            // Prvi Klik D4S1
            else if (_clickNumber == 4)
            {
                // Omoguci idi na prvu poziciju
                BpozicijaDijametri.Content = "POZICIJA D4S1";
            }
            // Prvi Klik D4S2
            else if (_clickNumber == 5)
            {
                // Omoguci idi na drugu poziciju
                BpozicijaDijametri.Content = "POZICIJA D4S2";
                //PLC kontrola
            }
            // Prvi Klik D3S2
            else if (_clickNumber == 6)
            {
                // Omoguci idi na prvu poziciju
                BpozicijaDijametri.Content = "POZICIJA D3S2";
            }
            // Prvi Klik D2S2
            else if (_clickNumber == 7)
            {
                // Omoguci idi na drugu poziciju
                BpozicijaDijametri.Content = "POZICIJA D2S2";
            }
            // Prvi Klik D1S2
            else if (_clickNumber == 8)
            {
                // Omoguci idi na prvu poziciju
                BpozicijaDijametri.Content = "POZICIJA D1S2";
            }
            // Prvi Klik D5S1
            else if (_clickNumber == 9)
            {
                //BpozicijaDijametri.Foreground = new SolidColorBrush(Colors.Black);
                // Omoguci idi na drugu poziciju
                BpozicijaDijametri.Content = "POZICIJA D5S1";
                //PLC kontrola
            }
            // Prvi Klik D5S2
            else if (_clickNumber == 10)
            {
                BpozicijaDijametri.Foreground = new SolidColorBrush(Colors.Black);
                // Omoguci idi na drugu poziciju
                BpozicijaDijametri.Content = "POZICIJA D5S2";
                //PLC kontrola
            }
        }

        private void BIduciRub_Click(object sender, RoutedEventArgs e)
        {
            //if (i >= 1 && i <= 10)
            //{
            //    i++;
            //}

            _clickNumber++;

            if (_clickNumber < 1)
            {
                _clickNumber = 1;
            }
            
            if (_clickNumber > 10)
            {
                _clickNumber = 10;
            }

            // Prvi Klik D1S1
            if (_clickNumber == 1)
            {
                // Omoguci idi na drugu poziciju
                BpozicijaDijametri.Content = "POZICIJA D1S1";
            }
            // Prvi Klik D2S1
            else if (_clickNumber == 2)
            {
                // Omoguci idi na prvu poziciju
                BpozicijaDijametri.Content = "POZICIJA D2S1";
            }
            // Prvi Klik D3S1
            else if (_clickNumber == 3)
            {
                // Omoguci idi na drugu poziciju
                BpozicijaDijametri.Content = "POZICIJA D3S1";
            }
            // Prvi Klik D4S1
            else if (_clickNumber == 4)
            {
                // Omoguci idi na prvu poziciju
                BpozicijaDijametri.Content = "POZICIJA D4S1";
            }
            // Prvi Klik D4S2
            else if (_clickNumber == 5)
            {
                // Omoguci idi na drugu poziciju
                BpozicijaDijametri.Content = "POZICIJA D4S2";
                //PLC kontrola
            }
            // Prvi Klik D3S2
            else if (_clickNumber == 6)
            {
                // Omoguci idi na prvu poziciju
                BpozicijaDijametri.Content = "POZICIJA D3S2";
            }
            // Prvi Klik D2S2
            else if (_clickNumber == 7)
            {
                // Omoguci idi na drugu poziciju
                BpozicijaDijametri.Content = "POZICIJA D2S2";
            }
            // Prvi Klik D1S2
            else if (_clickNumber == 8)
            {
                // Omoguci idi na prvu poziciju
                BpozicijaDijametri.Content = "POZICIJA D1S2";
            }
            // Prvi Klik D5S1
            else if (_clickNumber == 9)
            {
                // Omoguci idi na drugu poziciju
                BpozicijaDijametri.Content = "POZICIJA D5S1";
                //PLC kontrola
            }
            // Prvi Klik D5S2
            else if (_clickNumber == 10)
            {
                // Omoguci idi na drugu poziciju
                BpozicijaDijametri.Content = "POZICIJA D5S2";
                //PLC kontrola
            }
        }
    }
}
