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
            App.HDevExp.RunHalcon11(windowId);
        }

        public void RobotPickStartT1()
        {
            this.Dispatcher.Invoke(() =>
            {
                HTuple windowId = HwindowRobot.HalconID;
                App.HDevExp.RobotPick(windowId, false);
            });
            //HTuple windowId = HwindowRobot.HalconID;
            //App.HDevExp.RobotPick(windowId, false);
        }

        public void RobotPickStartT2()
        {
            this.Dispatcher.Invoke(() =>
            {
                HTuple windowId = HwindowRobot.HalconID;
                App.HDevExp.RobotPick(windowId, true);
            });
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
            Thread robotPick1Thread = new Thread(RobotPickStartT1);
            robotPick1Thread.Start();
        }

        private void BUzmiSliku2_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            Thread robotPick2Thread = new Thread(RobotPickStartT2);
            robotPick2Thread.Start();
        }
    }
}
