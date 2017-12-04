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

        
        float currentPosX = 0.0f, currentPosY = 0.0f, currentPosR = 0.0f;

        public static readonly DependencyProperty distance = DependencyProperty.Register("Distance", typeof(float), typeof(PRucno), new PropertyMetadata());
        public float Distance
        {
            get { return (float)GetValue(distance); }
            set { SetValue(distance, value); }
        }

        public static readonly DependencyProperty firstPoint = DependencyProperty.Register("FirstPoint", typeof(float), typeof(PRucno), new PropertyMetadata(0.0f, new PropertyChangedCallback(OnPointChange)));
        public float FirstPoint
        {
            get { return (float)GetValue(firstPoint); }
            set { SetValue(firstPoint, value); }
        }

        public static readonly DependencyProperty secondPoint = DependencyProperty.Register("SecondPoint", typeof(float), typeof(PRucno), new PropertyMetadata(0.0f, new PropertyChangedCallback(OnPointChange)));
        public float SecondPoint
        {
            get { return (float)GetValue(secondPoint); }
            set { SetValue(secondPoint, value); }
        }

        // New dependency property for automatic measurement

        public static readonly DependencyProperty unosruba = DependencyProperty.Register("Unosruba", typeof(float), typeof(PRucno), new PropertyMetadata());
        public float Unosruba
        {
            get { return (float)GetValue(unosruba); }
            set { SetValue(unosruba, value); }
        }

        public static readonly DependencyProperty prvirub = DependencyProperty.Register("Prvirub", typeof(float), typeof(PRucno), new PropertyMetadata());
        public float Prvirub
        {
            get { return (float)GetValue(prvirub); }
            set { SetValue(prvirub, value); }
        }

        public static readonly DependencyProperty drugirub = DependencyProperty.Register("Drugirub", typeof(float), typeof(PRucno), new PropertyMetadata());
        public float Drugirub
        {
            get { return (float)GetValue(drugirub); }
            set { SetValue(drugirub, value); }
        }



        public PRucno() // Constructor
        {

            
           
            InitializeComponent();

            App.HDevExp = new HDevelopExport(); // New Object
            App.PLC.Update_100_ms += new PLCInterface.UpdateHandler(updatePagePRucno_100ms);
            App.PLC.Update_1_s += new PLCInterface.UpdateHandler(updatePagePRucno_1s);
      
        }

        private void RunExport() // Method
        {
            //HTuple WindowID = hWindowControlWPF1.HalconID;
            //App.HDevExp.RunHalcon2(WindowID);
            //this.Dispatcher.Invoke(new Action(() => {
             
            //}));
        }

        private void hWindowControlWPF1_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            //App.HDevExp.InitHalcon();


            //Thread exportThread = new Thread(new ThreadStart(this.RunExport));
            //exportThread.Start();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            //App.HDevExp.InitHalcon();


            //Thread exportThread = new Thread(new ThreadStart(this.RunExport));
            //exportThread.Start();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //HDevExp.Hv_cameraID = 1;
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            //HDevExp.Hv_cameraID = 2;
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            //HDevExp.Hv_cameraID = 3;
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            //HDevExp.Hv_cameraID = 4;
        }

        private void Button_Click_5(object sender, RoutedEventArgs e)
        {
            
        }

        private void Button_Click_6(object sender, RoutedEventArgs e)
        {
            //App.HDevExp.InitHalcon();


            //Thread exportThread = new Thread(new ThreadStart(this.RunExport));
            //exportThread.Start();
        }

        private void updatePagePRucno_100ms(object sender, PLCInterfaceEventArgs e)
        {
          

        
        }

        private void b_snimiPrvi_Click(object sender, RoutedEventArgs e)
        {
                //this.Dispatcher.Invoke(new Action(() =>
                //{
                //    lock (HDevelopExport.HalconLock)
                //    {
                //        FirstPoint = (((float)App.HDevExp.hv_output.D / 3) * 0.005f) + (float)App.PLC.STATUS.HorizontalnaOs.AktualnaPozicija.Value;
                        
                //    }
                //}));
               
            
        }

        private void b_snimiDrugi_Click(object sender, RoutedEventArgs e)
        {
            //lock (HDevelopExport.HalconLock)
            //{
            //    this.Dispatcher.Invoke(new Action(() =>
            //    {
            //        SecondPoint = (((float)App.HDevExp.hv_output.D / 3) * 0.005f) + (float)App.PLC.STATUS.HorizontalnaOs.AktualnaPozicija.Value;
            //    }));

            //}
        }

        private void of_actualPosX_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void bb_rasvjetaDimenzije_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void if_setpointPosX_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void bb_automatikaStart_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void bb_jogPlusX_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void updatePagePRucno_1s(object sender, PLCInterfaceEventArgs e)
        {
         

        
        }




        private static void OnPointChange(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            //PRucno promjenaKontrola = (PRucno)d;
            //// Korekcija 10 um
            //promjenaKontrola.Distance=Math.Abs(promjenaKontrola.FirstPoint-promjenaKontrola.SecondPoint) + 0.010f;
        }
































    }
}
