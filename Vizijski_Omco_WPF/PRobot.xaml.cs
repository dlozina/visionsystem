using System;
using System.Threading;
using System.Windows.Controls;
using HalconDotNet;


namespace VizijskiSustavWPF
{
    /// <summary>
    /// Interaction logic for PRobot.xaml
    /// </summary>
    public partial class PRobot
    {
        //int i = 0;

        public PRobot()
        {
            InitializeComponent();
            //App.PLC.Update_1_s += new PLCInterface.UpdateHandler(updatePage);
            //App.PLC.Update_100_ms += new PLCInterface.UpdateHandler(updatePage_100);

        }

        //private void updatePage(object sender, PLCInterfaceEventArgs e)
        //{
           
        //}

        //private void updatePage_100(object sender, PLCInterfaceEventArgs e)
        //{

        //}

        private void LiveCam1()
        {
            App.HDevExp.InitHalcon();
            HTuple windowId = HwindowRobot.HalconID;
            App.HDevExp.RunCam1(windowId);
        }

        public void RobotPickStartT1(bool leftpallet)
        {
            Dispatcher.BeginInvoke((Action)(() => 
            {
                App.HDevExp.InitHalcon();
                HTuple windowId = HwindowRobot.HalconID;
                App.HDevExp.RobotPick(windowId, leftpallet);

            }));


            //App.HDevExp.InitHalcon();
            //this.Dispatcher.Invoke(() =>
            //{
            //    HTuple windowId = HwindowRobot.HalconID;
            //    App.HDevExp.RobotPick(windowId, false);
            //});
        }

        public void RobotPickStartT2()
        {
            //this.Dispatcher.Invoke(() =>
            //{
                App.HDevExp.InitHalcon();
                HTuple windowId = HwindowRobot.HalconID;
                App.HDevExp.RobotPick(windowId, true);
            //});
            //HTuple windowId = HwindowRobot.HalconID;
            //App.HDevExp.RobotPick(windowId, true);
        }

        private void BStartKamere_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            App.HDevExp.Exitloop1 = false;
            Thread liveCam1Thread = new Thread(LiveCam1);
            liveCam1Thread.Start();
        }

        private void BStopKamere_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            App.HDevExp.Exitloop1 = true;
        }

        private void BUzmiSliku1_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            Thread robotPick1Thread = new Thread(() => RobotPickStartT1(true));

            robotPick1Thread.Start();
        }

        private void BUzmiSliku2_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            Thread robotPick2Thread = new Thread(RobotPickStartT2);
            robotPick2Thread.Start();
        }

        private void Window_Unloaded(object sender, System.Windows.RoutedEventArgs e)
        {
            App.HDevExp.Exitloop1 = true;
        }

        //private void BmultiTest_Click(object sender, System.Windows.RoutedEventArgs e)
        //{

        //    if(i == 0)
        //    {
        //        App.ActivateControl1();
        //    }
        //    else if (i == 1)
        //    {
        //        App.ActivateControl2();
        //    }

        //    i++;
        //}
    }
}
