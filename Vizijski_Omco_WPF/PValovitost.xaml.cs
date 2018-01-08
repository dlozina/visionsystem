using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using OxyPlot;
using OxyPlot.Wpf;
using System.IO;
using HalconDotNet;
using System.Threading;

namespace VizijskiSustavWPF
{
    
    public partial class PValovitost : Page
    {
        public PValovitost()
        {
            InitializeComponent();
        
        }

        private void TeachCAM4()
        {
            App.HDevExp.InitHalcon();
            HTuple WindowID = hwindowTeach.HalconID;
            App.HDevExp.RunHalcon15(WindowID);
        }

        private void AnalizeD1S1()
        {
            App.HDevExp.InitHalcon();
            HTuple WindowID = hwindowTeach.HalconID;
            App.HDevExp.RunHalcon16(WindowID);
        }

        private void AnalizeD1S2()
        {
            App.HDevExp.InitHalcon();
            HTuple WindowID = hwindowTeach.HalconID;
            App.HDevExp.RunHalcon17(WindowID);
        }

        private void AnalizeD2S1()
        {
            App.HDevExp.InitHalcon();
            HTuple WindowID = hwindowTeach.HalconID;
            App.HDevExp.RunHalcon18(WindowID);
        }

        private void AnalizeD2S2()
        {
            App.HDevExp.InitHalcon();
            HTuple WindowID = hwindowTeach.HalconID;
            App.HDevExp.RunHalcon19(WindowID);
        }

        private void AnalizeD3S1()
        {
            App.HDevExp.InitHalcon();
            HTuple WindowID = hwindowTeach.HalconID;
            App.HDevExp.RunHalcon20(WindowID);
        }

        private void AnalizeD3S2()
        {
            App.HDevExp.InitHalcon();
            HTuple WindowID = hwindowTeach.HalconID;
            App.HDevExp.RunHalcon21(WindowID);
        }

        private void AnalizeD4S1()
        {
            App.HDevExp.InitHalcon();
            HTuple WindowID = hwindowTeach.HalconID;
            App.HDevExp.RunHalcon22(WindowID);
        }

        private void AnalizeD4S2()
        {
            App.HDevExp.InitHalcon();
            HTuple WindowID = hwindowTeach.HalconID;
            App.HDevExp.RunHalcon23(WindowID);
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            
        }

        private void updatePage(object sender, PLCInterfaceEventArgs e)
        {
           
        }
        
        private void updatePage_100ms(object sender, PLCInterfaceEventArgs e)
        {
               
        }

        private void b_startKamere_Click(object sender, RoutedEventArgs e)
        {
            App.HDevExp.Teachloop = false;
            Thread TeachCAM4Thread = new Thread(new ThreadStart(this.TeachCAM4));
            TeachCAM4Thread.Name = "TeachCAM4Thread";
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
            Thread TestAnalizeD1S1 = new Thread(new ThreadStart(this.AnalizeD1S1));
            Name = "TestAnalizeD1S1Thread";
            TestAnalizeD1S1.Start();

        }

        private void b_analizaSlikeD1S2_Click(object sender, RoutedEventArgs e)
        {
            App.HDevExp.Teachloop = true;
            Thread TestAnalizeD1S2 = new Thread(new ThreadStart(this.AnalizeD1S2));
            Name = "TestAnalizeD1S2Thread";
            TestAnalizeD1S2.Start();
        }

        private void b_analizaSlikeD2S1_Click(object sender, RoutedEventArgs e)
        {
            App.HDevExp.Teachloop = true;
            Thread TestAnalizeD2S1 = new Thread(new ThreadStart(this.AnalizeD2S1));
            Name = "TestAnalizeD2S1Thread";
            TestAnalizeD2S1.Start();
        }

        private void b_analizaSlikeD2S2_Click(object sender, RoutedEventArgs e)
        {
            App.HDevExp.Teachloop = true;
            Thread TestAnalizeD2S2 = new Thread(new ThreadStart(this.AnalizeD2S2));
            Name = "TestAnalizeD2S2Thread";
            TestAnalizeD2S2.Start();
        }

        private void b_analizaSlikeD3S1_Click(object sender, RoutedEventArgs e)
        {
            App.HDevExp.Teachloop = true;
            Thread TestAnalizeD3S1 = new Thread(new ThreadStart(this.AnalizeD3S1));
            Name = "TestAnalizeD3S1Thread";
            TestAnalizeD3S1.Start();
        }

        private void b_analizaSlikeD3S2_Click(object sender, RoutedEventArgs e)
        {
            App.HDevExp.Teachloop = true;
            Thread TestAnalizeD3S2 = new Thread(new ThreadStart(this.AnalizeD3S2));
            Name = "TestAnalizeD3S2Thread";
            TestAnalizeD3S2.Start();
        }

        private void b_analizaSlikeD4S1_Click(object sender, RoutedEventArgs e)
        {
            App.HDevExp.Teachloop = true;
            Thread TestAnalizeD4S1 = new Thread(new ThreadStart(this.AnalizeD4S1));
            Name = "TestAnalizeD4S1Thread";
            TestAnalizeD4S1.Start();
        }

        private void b_analizaSlikeD4S2_Click(object sender, RoutedEventArgs e)
        {
            App.HDevExp.Teachloop = true;
            Thread TestAnalizeD4S2 = new Thread(new ThreadStart(this.AnalizeD4S2));
            Name = "TestAnalizeD4S2Thread";
            TestAnalizeD4S2.Start();
        }

        
    }
}
