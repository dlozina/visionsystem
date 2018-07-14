using System.Windows;
using HalconDotNet;
using System.Threading;

namespace VizijskiSustavWPF
{
    /// <summary>
    /// Interaction logic for PRucno.xaml
    /// </summary>
    public partial class PRucno
    {
        public PRucno() // Constructor
        {
            InitializeComponent();
            /*App.HDevExp = new HDevelopExport();*/ // New Object
            //App.PLC.Update_100_ms += new PLCInterface.UpdateHandler(updatePagePRucno_100ms);
            //App.PLC.Update_1_s += new PLCInterface.UpdateHandler(updatePagePRucno_1s);
        }

        private void LiveCam1() // Method
        {
            App.HDevExp.InitHalcon();
            HTuple windowId = hwindow.HalconID;
            App.HDevExp.RunCam1(windowId);
            //Dispatcher.Invoke(() =>
            //{
            //    App.mwHandle.tb_cameraOnline.Text = "Camera: Offline";
            //    App.mwHandle.tb_cameraOnline.UpdateLayout();
            //});
        }

        private void LiveCam2() // Method
        {
            App.HDevExp.InitHalcon();
            HTuple windowId = hwindow.HalconID;
            App.HDevExp.RunCam2(windowId, false);
            //Dispatcher.Invoke(() =>
            //{
            //    App.mwHandle.tb_cameraOnline.Text = "Camera: Offline";
            //    App.mwHandle.tb_cameraOnline.UpdateLayout();
            //});
        }

        private void LiveCam3() // Method
        {
            App.HDevExp.InitHalcon();
            HTuple windowId = hwindow.HalconID;
            App.HDevExp.RunCam3(windowId, false);
            //Dispatcher.Invoke(() =>
            //{
            //    App.mwHandle.tb_cameraOnline.Text = "Camera: Offline";
            //    App.mwHandle.tb_cameraOnline.UpdateLayout();
            //});
        }

        private void LiveCam4() // Method
        {
            App.HDevExp.InitHalcon();
            HTuple windowId = hwindow.HalconID;
            //App.HDevExp.RunCam4(windowId, true);
            App.HDevExp.RunCam4(windowId, false, 0);
            //Dispatcher.Invoke(() =>
            //{
            //    App.mwHandle.tb_cameraOnline.Text = "Camera: Offline";
            //    App.mwHandle.tb_cameraOnline.UpdateLayout();
            //});
        }


        //private void updatePagePRucno_100ms(object sender, PLCInterfaceEventArgs e)
        //{

        //}

        //private void updatePagePRucno_1s(object sender, PLCInterfaceEventArgs e)
        //{

        //}

        private void b_ukljucikameru1_Click(object sender, RoutedEventArgs e)
        {
            izgasiKameru();
            App.HDevExp.Exitloop1 = false;
            //App.HDevExp.Exitloop2 = true;
            //App.HDevExp.Exitloop3 = true;
            //App.HDevExp.Exitloop4 = true; 
            b_ukljucikameru1.IsEnabled = false;
            b_ukljucikameru2.IsEnabled = true;
            b_ukljucikameru3.IsEnabled = true;
            b_ukljucikameru4.IsEnabled = true;
            hwindow.HImagePart = new Rect(0, 0, 1280, 1024);
            // CAM1 call
            App.mwHandle.tb_cameraOnline.Text = "Camera: No.1 Online";
            App.mwHandle.tb_cameraOnline.UpdateLayout();
            Thread liveCam1Thread = new Thread(LiveCam1);
            liveCam1Thread.Start();
        }

        private void b_ukljucikameru2_Click(object sender, RoutedEventArgs e)
        {
            izgasiKameru();
            //App.HDevExp.Exitloop1 = true;
            App.HDevExp.Exitloop2 = false;
            //App.HDevExp.Exitloop3 = true;
            //App.HDevExp.Exitloop4 = true;   
            b_ukljucikameru1.IsEnabled = true;
            b_ukljucikameru2.IsEnabled = false;
            b_ukljucikameru3.IsEnabled = true;
            b_ukljucikameru4.IsEnabled = true;
            hwindow.HImagePart = new Rect(0, 0, 3856, 2764);
            // CAM2 call
            App.mwHandle.tb_cameraOnline.Text = "Camera: No.2 Online";
            App.mwHandle.tb_cameraOnline.UpdateLayout();
            Thread liveCam2Thread = new Thread(LiveCam2);
            liveCam2Thread.Start();
        }


        private void b_ukljucikameru3_Click(object sender, RoutedEventArgs e)
        {
            izgasiKameru();
            //App.HDevExp.Exitloop1 = true;
            //App.HDevExp.Exitloop2 = true;
            App.HDevExp.Exitloop3 = false;
            //App.HDevExp.Exitloop4 = true;
            b_ukljucikameru1.IsEnabled = true;
            b_ukljucikameru2.IsEnabled = true;
            b_ukljucikameru3.IsEnabled = false;
            b_ukljucikameru4.IsEnabled = true;
            hwindow.HImagePart = new Rect(0, 0, 2592, 1944);
            // CAM3 call
            App.mwHandle.tb_cameraOnline.Text = "Camera: No.3 Online";
            App.mwHandle.tb_cameraOnline.UpdateLayout();
            Thread liveCam3Thread = new Thread(LiveCam3);
            liveCam3Thread.Start();
        }

        private void b_ukljucikameru4_Click(object sender, RoutedEventArgs e)
        {
            izgasiKameru();
            //App.HDevExp.Exitloop1 = true;
            //App.HDevExp.Exitloop2 = true;
            //App.HDevExp.Exitloop3 = true;
            App.HDevExp.Exitloop4 = false;
            b_ukljucikameru1.IsEnabled = true;
            b_ukljucikameru2.IsEnabled = true;
            b_ukljucikameru3.IsEnabled = true;
            b_ukljucikameru4.IsEnabled = false;
            //hwindow.HImagePart = new Rect(0, 0, 3856, 2764);
            hwindow.HImagePart = new Rect(0, 0, 2448, 2050);
            // CAM4 call
            App.mwHandle.tb_cameraOnline.Text = "Camera: No.4 Online";
            App.mwHandle.tb_cameraOnline.UpdateLayout();
            Thread liveCam4Thread = new Thread(LiveCam4);
            liveCam4Thread.Start();
        }

        private void b_izgasiKameru_Click(object sender, RoutedEventArgs e)
        {
            b_ukljucikameru1.IsEnabled = true;
            b_ukljucikameru2.IsEnabled = true;
            b_ukljucikameru3.IsEnabled = true;
            b_ukljucikameru4.IsEnabled = true;

            App.HDevExp.Exitloop1 = true;
            App.HDevExp.Exitloop2 = true;
            App.HDevExp.Exitloop3 = true;
            App.HDevExp.Exitloop4 = true;

            Dispatcher.Invoke(() =>
            {
                App.mwHandle.tb_cameraOnline.Text = "Camera: Offline";
                App.mwHandle.tb_cameraOnline.UpdateLayout();
            });
        }

        private void izgasiKameru()
        {
            App.HDevExp.Exitloop1 = true;
            App.HDevExp.Exitloop2 = true;
            App.HDevExp.Exitloop3 = true;
            App.HDevExp.Exitloop4 = true;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void Window_Unloaded(object sender, RoutedEventArgs e)
        {
            App.HDevExp.Exitloop1 = true;
            App.HDevExp.Exitloop2 = true;
            App.HDevExp.Exitloop3 = true;
            App.HDevExp.Exitloop4 = true;
        }

        //private void b_zatvoriKadar_Click(object sender, RoutedEventArgs e)
        //{
        //    HOperatorSet.CloseAllFramegrabbers();
        //}


    }

}
