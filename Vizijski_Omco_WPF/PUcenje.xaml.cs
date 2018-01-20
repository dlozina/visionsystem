using System.Windows;
using HalconDotNet;
using System.Threading;

namespace VizijskiSustavWPF
{
    
    public partial class PUcenje
    {
        public PUcenje()
        {
            InitializeComponent();
            //App.PLC.Update_100_ms += new PLCInterface.UpdateHandler(updatePagePRucno_100ms);
            //App.PLC.Update_1_s += new PLCInterface.UpdateHandler(updatePagePRucno_1s);
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
            App.HDevExp.RunHalcon15(windowId);
        }

        private void AnalizeD1S1()
        {
            App.HDevExp.InitHalcon();
            HTuple windowId = hwindowTeach.HalconID;
            App.HDevExp.RunHalcon16(windowId);
        }

        private void AnalizeD1S2()
        {
            App.HDevExp.InitHalcon();
            HTuple windowId = hwindowTeach.HalconID;
            App.HDevExp.RunHalcon17(windowId);
        }

        private void AnalizeD2S1()
        {
            App.HDevExp.InitHalcon();
            HTuple windowId = hwindowTeach.HalconID;
            App.HDevExp.RunHalcon18(windowId);
        }

        private void AnalizeD2S2()
        {
            App.HDevExp.InitHalcon();
            HTuple windowId = hwindowTeach.HalconID;
            App.HDevExp.RunHalcon19(windowId);
        }

        private void AnalizeD3S1()
        {
            App.HDevExp.InitHalcon();
            HTuple windowId = hwindowTeach.HalconID;
            App.HDevExp.RunHalcon20(windowId);
        }

        private void AnalizeD3S2()
        {
            App.HDevExp.InitHalcon();
            HTuple windowId = hwindowTeach.HalconID;
            App.HDevExp.RunHalcon21(windowId);
        }

        private void AnalizeD4S1()
        {
            App.HDevExp.InitHalcon();
            HTuple windowId = hwindowTeach.HalconID;
            App.HDevExp.RunHalcon22(windowId);
        }

        private void AnalizeD4S2()
        {
            App.HDevExp.InitHalcon();
            HTuple windowId = hwindowTeach.HalconID;
            App.HDevExp.RunHalcon23(windowId);
        }

        private void b_startKamere_Click(object sender, RoutedEventArgs e)
        {
            App.HDevExp.Teachloop = false;
            Thread TeachCAM4Thread = new Thread(TeachCam4) {Name = "TeachCAM4Thread"};
            TeachCAM4Thread.Start();
        }

        private void b_sTOPKamere_Click(object sender, RoutedEventArgs e)
        {
            App.HDevExp.Teachloop = true;
        }

        private void b_clearScreen_Click(object sender, RoutedEventArgs e)
        {
            HOperatorSet.ClearWindow(hwindowTeach.HalconID);
        }

        private void b_analizaSlikeD1S1_Click(object sender, RoutedEventArgs e)
        {
            App.HDevExp.Teachloop = true;
            Thread TestAnalizeD1S1 = new Thread(AnalizeD1S1);
            Name = "TestAnalizeD1S1Thread";
            TestAnalizeD1S1.Start();

        }

        private void b_analizaSlikeD1S2_Click(object sender, RoutedEventArgs e)
        {
            App.HDevExp.Teachloop = true;
            Thread TestAnalizeD1S2 = new Thread(AnalizeD1S2);
            Name = "TestAnalizeD1S2Thread";
            TestAnalizeD1S2.Start();
        }

        private void b_analizaSlikeD2S1_Click(object sender, RoutedEventArgs e)
        {
            App.HDevExp.Teachloop = true;
            Thread TestAnalizeD2S1 = new Thread(AnalizeD2S1);
            Name = "TestAnalizeD2S1Thread";
            TestAnalizeD2S1.Start();
        }

        private void b_analizaSlikeD2S2_Click(object sender, RoutedEventArgs e)
        {
            App.HDevExp.Teachloop = true;
            Thread TestAnalizeD2S2 = new Thread(AnalizeD2S2);
            Name = "TestAnalizeD2S2Thread";
            TestAnalizeD2S2.Start();
        }

        private void b_analizaSlikeD3S1_Click(object sender, RoutedEventArgs e)
        {
            App.HDevExp.Teachloop = true;
            Thread TestAnalizeD3S1 = new Thread(AnalizeD3S1);
            Name = "TestAnalizeD3S1Thread";
            TestAnalizeD3S1.Start();
        }

        private void b_analizaSlikeD3S2_Click(object sender, RoutedEventArgs e)
        {
            App.HDevExp.Teachloop = true;
            Thread TestAnalizeD3S2 = new Thread(AnalizeD3S2);
            Name = "TestAnalizeD3S2Thread";
            TestAnalizeD3S2.Start();
        }

        private void b_analizaSlikeD4S1_Click(object sender, RoutedEventArgs e)
        {
            App.HDevExp.Teachloop = true;
            Thread TestAnalizeD4S1 = new Thread(AnalizeD4S1);
            Name = "TestAnalizeD4S1Thread";
            TestAnalizeD4S1.Start();
        }

        private void b_analizaSlikeD4S2_Click(object sender, RoutedEventArgs e)
        {
            App.HDevExp.Teachloop = true;
            Thread TestAnalizeD4S2 = new Thread(AnalizeD4S2);
            Name = "TestAnalizeD4S2Thread";
            TestAnalizeD4S2.Start();
        }

        
    }
}
