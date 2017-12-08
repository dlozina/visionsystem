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
using HalconDotNet;
using System.Threading;

namespace VizijskiSustavWPF
{
    /// <summary>
    /// Interaction logic for PRucno.xaml
    /// </summary>
    public partial class PRucno : Page
    {
        //private HDevelopExport HDevExp;

        
        public PRucno() // Constructor
        {

            InitializeComponent();
            App.HDevExp = new HDevelopExport(); // New Object
            App.PLC.Update_100_ms += new PLCInterface.UpdateHandler(updatePagePRucno_100ms);
            App.PLC.Update_1_s += new PLCInterface.UpdateHandler(updatePagePRucno_1s);

        }

        private void LiveCam2() // Method
        {
            HTuple WindowID = hWindowControlWPF1.HalconID;
            App.HDevExp.RunHalcon9(WindowID);
            
        }


        //private void Button_Click_1(object sender, RoutedEventArgs e)
        //{
        //    //App.HDevExp.InitHalcon();


        //    //Thread exportThread = new Thread(new ThreadStart(this.RunExport));
        //    //exportThread.Start();
        //}

        //private void Button_Click(object sender, RoutedEventArgs e)
        //{
        //    //HDevExp.Hv_cameraID = 1;
        //}

        //private void Button_Click_2(object sender, RoutedEventArgs e)
        //{
        //    //HDevExp.Hv_cameraID = 2;
        //}

        //private void Button_Click_3(object sender, RoutedEventArgs e)
        //{
        //    //HDevExp.Hv_cameraID = 3;
        //}

        //private void Button_Click_4(object sender, RoutedEventArgs e)
        //{
        //    //HDevExp.Hv_cameraID = 4;
        //}

        //private void Button_Click_5(object sender, RoutedEventArgs e)
        //{
           
            
        //}

        //private void Button_Click_6(object sender, RoutedEventArgs e)
        //{
        //    //App.HDevExp.InitHalcon();
        //    //Thread exportThread = new Thread(new ThreadStart(this.RunExport));
        //    //exportThread.Start();
        //}

        private void updatePagePRucno_100ms(object sender, PLCInterfaceEventArgs e)
        {
          

        
        }

        private void updatePagePRucno_1s(object sender, PLCInterfaceEventArgs e)
        {


        }

        private void b_ukljucikameru2_Click(object sender, RoutedEventArgs e)
        {
            Thread LiveCam2Thread = new Thread(new ThreadStart(this.LiveCam2));
            LiveCam2Thread.Start();
        }


    }

}
