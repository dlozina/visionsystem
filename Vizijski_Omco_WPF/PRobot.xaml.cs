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

        private void RobotPickStart()
        {
            App.HDevExp.InitHalcon();
            HTuple windowId = HwindowRobot.HalconID;
            App.HDevExp.RobotPick(windowId);
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

        private void BUzmiSliku_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            Thread robotPickThread = new Thread(RobotPickStart);
            robotPickThread.Start();
        }
    }
}
