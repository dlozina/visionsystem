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

        private void b_analizaSlikeD1S1_Click(object sender, RoutedEventArgs e)
        {

        }

        private void b_analizaSlikeD1S2_Click(object sender, RoutedEventArgs e)
        {

        }

        private void b_analizaSlikeD2S1_Click(object sender, RoutedEventArgs e)
        {

        }

        private void b_analizaSlikeD2S2_Click(object sender, RoutedEventArgs e)
        {

        }

        private void b_analizaSlikeD3S1_Click(object sender, RoutedEventArgs e)
        {

        }

        private void b_analizaSlikeD3S2_Click(object sender, RoutedEventArgs e)
        {

        }

        private void b_analizaSlikeD4S1_Click(object sender, RoutedEventArgs e)
        {

        }

        private void b_analizaSlikeD4S2_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
