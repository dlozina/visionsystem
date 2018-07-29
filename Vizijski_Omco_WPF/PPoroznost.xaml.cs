using System.Windows;
using HalconDotNet;
using System.Threading;
using System;
using System.Windows.Media;

namespace VizijskiSustavWPF
{
    /// <summary>
    /// Interaction logic for PSrh.xaml
    /// </summary>
    public partial class PPoroznost
    {
        //private HALCON.HDevelopExport Hdevtest;

        public PPoroznost()
        {
            InitializeComponent();
            //App.PLC.Update_100_ms += new PLCInterface.UpdateHandler(updatePagePRucno_100ms);
            //App.PLC.Update_1_s += new PLCInterface.UpdateHandler(updatePagePRucno_1s);
            //Hdevtest = new HALCON.HDevelopExport();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //Hdevtest.InitHalcon();
        }
        //private void updatePagePRucno_100ms(object sender, PLCInterfaceEventArgs e)
        //{

        //}

        //private void updatePagePRucno_1s(object sender, PLCInterfaceEventArgs e)
        //{

        //}



        private void PorosityVerCall()
        {
            //this.Dispatcher.Invoke(() =>
            //{
            //HwindowPorsity.HImagePart = new Rect(0, 0, 3856, 2764);
            //});
            //App.HDevExp.InitHalcon();
            //HTuple windowId = HwindowPorsity.HalconID;
            //App.HDevExp.RunPorosityVertical(windowId);

            //Dispatcher.BeginInvoke((Action)(() =>
            //{
                //HwindowPorsity.HImagePart = new Rect(0, 0, 3856, 2764);
                //App.HDevExp.InitHalcon();
                //HTuple windowId = HwindowPorsity.HalconID;
                //App.HDevExp.RunPorosityVertical(windowId);
                App.HDevExp.RunPorosityVertical();
            //}));
        }

        public void PorosityVerWindow()
        {
            Thread porosityvert = new Thread(new ThreadStart(PorosityVerCall));
            porosityvert.Name = "Thread PorosityVer";
            porosityvert.Start();
        }

        private void PorosityHorCall()
        {
            //this.Dispatcher.Invoke(() =>
            //{
            //    HwindowPorsity.HImagePart = new Rect(0, 0, 2592, 1944);
            //});
            //App.HDevExp.InitHalcon();
            //HTuple windowId = HwindowPorsity.HalconID;
            //App.HDevExp.RunPorosityHorizontal(windowId);

            //Dispatcher.BeginInvoke((Action)(() =>
            //{
                //HwindowPorsity.HImagePart = new Rect(0, 0, 2592, 1944);
                //App.HDevExp.InitHalcon();
                //HTuple windowId = HwindowPorsity.HalconID;
                //App.HDevExp.RunPorosityHorizontal(windowId);
                App.HDevExp.RunPorosityHorizontal();
            //}));
        }

        public void PorosityHorWindow()
        {
            Thread porosityhor = new Thread(new ThreadStart(PorosityHorCall));
            porosityhor.Name = "Thread PorosityHor";
            porosityhor.Start();
        }

        //private void TeachCam2()
        //{
        //    App.HDevExp.InitHalcon();
        //    HTuple windowId = HwindowPorsity.HalconID;
        //    App.HDevExp.RunHalcon24(windowId);
        //}

        //private void TeachCam3()
        //{
        //    App.HDevExp.InitHalcon();
        //    HTuple windowId = HwindowPorsity.HalconID;
        //    App.HDevExp.RunHalcon25(windowId);
        //    //Hdevtest.RunHalcon25(windowId);
        //}

        //private void b_pstartKamere1_Click(object sender, RoutedEventArgs e)
        //{
        //    App.HDevExp.Teachloop2 = false;
        //    App.HDevExp.Teachloop3 = true;
        //    HwindowPorsity.HImagePart = new Rect(0, 0, 3856, 2764);
        //    Thread teachCam2Thread = new Thread(TeachCam2) { Name = "TeachCAM2Thread" };
        //    teachCam2Thread.Start();
        //}

        //private void b_pstartKamere2_Click(object sender, RoutedEventArgs e)
        //{

        //    App.HDevExp.Teachloop2 = true;
        //    App.HDevExp.Teachloop3 = false;
        //    //HwindowPorsity.HImagePart = new Rect(0, 0, 2592, 1944);
        //    Thread teachCam3Thread = new Thread(TeachCam3) { Name = "TeachCAM3Thread" };
        //    teachCam3Thread.Start();
        //}

        //private void b_psTOPKamere_Click(object sender, RoutedEventArgs e)
        //{
        //    App.HDevExp.Teachloop2 = true;
        //    App.HDevExp.Teachloop3 = true;
        //}
        private void TeachCam2()
        {
            App.HDevExp.InitHalcon();
            HTuple windowId = HwindowPorsity.HalconID;
            App.HDevExp.RunCam2(windowId, true);
        }

        private void TeachCam3()
        {
            App.HDevExp.InitHalcon();
            HTuple windowId = HwindowPorsity.HalconID;
            App.HDevExp.RunCam3(windowId, true);
        }

        private void BtestKamere1_Click(object sender, RoutedEventArgs e)
        {
            App.HDevExp.Exitloop2 = false;
            App.HDevExp.Exitloop3 = true;
            App.HDevExp.Exitloop4 = true;
            App.DiameterLightOFF();
            App.PorosityLightON();
            HwindowPorsity.HImagePart = new Rect(0, 0, 3856, 2764);
            Thread teachCam2Thread = new Thread(TeachCam2) { Name = "TeachCAM2Thread" };
            teachCam2Thread.Start();
        }

        private void BtestKamere2_Click(object sender, RoutedEventArgs e)
        {
            App.HDevExp.Exitloop2 = true;
            App.HDevExp.Exitloop3 = false;
            App.HDevExp.Exitloop4 = true;
            App.DiameterLightOFF();
            App.PorosityLightON();
            HwindowPorsity.HImagePart = new Rect(0, 0, 2592, 1944);
            Thread teachCam3Thread = new Thread(TeachCam3) { Name = "TeachCAM3Thread" };
            teachCam3Thread.Start();
        }

        private void BpozicijaPoroznost_Click(object sender, RoutedEventArgs e)
        {
            App.ActivateControlPorosityPosition();
        }

        private void BnauciPozicijuPoroznost_Click(object sender, RoutedEventArgs e)
        {
            BpozicijaPoroznost.IsEnabled = false;
            BpozicijaPoroznost.Foreground = new SolidColorBrush(Colors.Green);
            BpozicijaPoroznost.Content = "SPREMLJENO";
            BnauciPozicijuPoroznost.IsEnabled = false;
            BnauciPozicijuPoroznost.Foreground = new SolidColorBrush(Colors.Gray);
            App.ActivateControlTeachPorosityPosition();
        }

        private void BresetUcenjaPoroznost_Click(object sender, RoutedEventArgs e)
        {
            BpozicijaPoroznost.IsEnabled = true;
            BpozicijaPoroznost.Foreground = new SolidColorBrush(Colors.Black);
            BpozicijaPoroznost.Content = "POZICIJA";
            //
            BnauciPozicijuPoroznost.IsEnabled = true;
            BnauciPozicijuPoroznost.Foreground = new SolidColorBrush(Colors.Black);
        }
    }
}
