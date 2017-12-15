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
using System.ComponentModel;
using OxyPlot;
using OxyPlot.Wpf;
using System.IO;
using HalconDotNet;

namespace VizijskiSustavWPF
{
    /// <summary>
    /// Interaction logic for PSrh.xaml
    /// </summary>
    public partial class PSrh : Page
    {
        public static readonly DependencyProperty numberOfMeas = DependencyProperty.Register("NumberOfMeas", typeof(int), typeof(PSrh), new PropertyMetadata(10));

        public int NumberOfMeas
        {
            get { 
                return (int)GetValue(numberOfMeas);
            }
            set 
            {
                SetValue(numberOfMeas, value);
            }
        }
       

        List<DataPoint> points = new List<DataPoint>();
        LineSeries lineSeries = new LineSeries();
        BurrDetection burrDetection = new BurrDetection();
        float scalex;
        float scaley;

        public PSrh()
        {
            InitializeComponent();
            lineSeries.Color = Colors.Blue;
            lineSeries.StrokeThickness = 3;
            lineSeries.LineStyle = OxyPlot.LineStyle.Automatic;
            lineSeries.MarkerSize = 5;
            lineSeries.MarkerStroke = Colors.Green;
            lineSeries.CanTrackerInterpolatePoints = false;
            lineSeries.Title = "Srh";
            lineSeries.Smooth = false;
            lineSeries.ItemsSource = points;
   
            App.PLC.Update_1_s += new PLCInterface.UpdateHandler(updatePage);
            App.PLC.Update_100_ms += new PLCInterface.UpdateHandler(updatePage_100ms);
        }

        public void PorosityHorWindow()
        {
            HTuple WindowID = hWindowControlWPF2.HalconID;
            App.HDevExp.RunHalcon14(WindowID);
        }

        public void PorosityVerWindow()
        {
            HTuple WindowID = hWindowControlWPF2.HalconID;
            App.HDevExp.RunHalcon13(WindowID);
        }

        private void p_postaviPrvuTocku_Click(object sender, RoutedEventArgs e)
        {
            //App.PLC.WriteTag(App.PLC.CONTROL.VertikalnaOs.SetFirstPoint, true);
        }

        private void p_postaviDruguTocku_Click(object sender, RoutedEventArgs e)
        {
            //App.PLC.WriteTag(App.PLC.CONTROL.VertikalnaOs.SetSecondPoint, true);
        }

        private void p_novoMjerenje_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void p_pokreniMjerenje_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
                     
        }

        private void p_pokreniMjerenje_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
            
        }

        private void p_novoMjerenje_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            
        }

        private void p_novoMjerenje_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
           
        }
        
        private void updatePage(object sender, PLCInterfaceEventArgs e)
        {
            
            Dispatcher.BeginInvoke(new Action(() =>
             {
                 //if (((bool)e.ControlData.VertikalnaOs.SimpleBurrMeas.Value == true) && (p_novoMjerenje.Content != "STOP"))
                 //{
                 //    p_novoMjerenje.Foreground = Brushes.Red;
                 //    p_novoMjerenje.Content = "STOP";
                 //}
                 //else if (((bool)e.ControlData.VertikalnaOs.SimpleBurrMeas.Value == false) && (p_novoMjerenje.Foreground != Brushes.Black))
                 //{
                 //    if (p_novoMjerenje.Content == "STOP")
                 //    {
                 //        if (burrDetection.BurrList == null)
                 //            burrDetection.BurrList = new List<BurrLine>();
                 //        burrDetection.BurrList.Add(new BurrLine(0, OdabirLimovaKontrola.LineNumber, burrDetection.DetectOne(points)));
                 //        burrDetection.BurrList = burrDetection.BurrList;
                 //        App.MainReportInterface.BurrList.Clear();
                 //        if (burrDetection.BurrList != null)
                 //        {
                 //            App.MainReportInterface.BurrList.AddRange(burrDetection.BurrList);
                 //        }
                 //        dataGrid1.Items.Refresh();
                 //        App.pIzvjestaji.dataGridSrh.Items.Refresh();
                 //        App.MainReportInterface.Srh_max = burrDetection.MaxBurr;
                 //        App.MainReportInterface.Srh_postotak = burrDetection.BurrPercent;
                 //        App.MainReportInterface.Srh_pozicija = OdabirLimovaKontrola.LineNumber;
                 //        App.MainReportInterface.Srh_brojUzoraka = NumberOfMeas;
                 //    }
                     
                 //    p_novoMjerenje.Content = "POKRENI JEDNOKRATNO\n            MJERENJE";
                 //    p_novoMjerenje.Foreground = Brushes.Black;

                     
                 //}
             }));
        }

        private void updatePage_100ms(object sender, PLCInterfaceEventArgs e)
        {
            Dispatcher.BeginInvoke(new Action(() =>
              {
                  //minimapa.ActualPosition = new Point(Math.Abs((float)e.StatusData.HorizontalnaOs.ActualPosition.X.Value) * scalex, (float)e.StatusData.HorizontalnaOs.ActualPosition.Y.Value * scaley);
                  //minimapa.FirstPoint = new Point(Math.Abs((float)e.StatusData.RotacijskaOs.FirstPoint.X.Value) * scalex, (float)e.StatusData.RotacijskaOs.FirstPoint.Y.Value * scaley);
                  //minimapa.SecondPoint = new Point(Math.Abs((float)e.StatusData.RotacijskaOs.LastPoint.X.Value) * scalex, (float)e.StatusData.RotacijskaOs.LastPoint.Y.Value * scaley);

                  //if (((bool)e.StatusData.RotacijskaOs.AutomaticActive.Value == true) && (p_pokreniMjerenje.Content != "STOP"))
                  //{
                  //    p_pokreniMjerenje.Foreground = Brushes.Red;
                  //    p_pokreniMjerenje.Content = "STOP";
                  //}
                  //else if (((bool)e.StatusData.RotacijskaOs.AutomaticActive.Value == false) && (p_pokreniMjerenje.Foreground != Brushes.Black))
                  //{
                  //    p_pokreniMjerenje.Content = "POKRENI MJERENJE";
                  //    p_pokreniMjerenje.Foreground = Brushes.Black;

                  //    if (points.Count > 1)
                  //    {
                  //        points.Clear();
                  //        rawPlotData = App.PLC.ReadCustom(60, 100, 4000); // DB60 LENGTH_GAUGE_DB
                  //        for (int i = 0; i < 2000; i++)
                  //        {
                  //            Int16 srh = (Int16)((rawPlotData[i * 2] << 8) | rawPlotData[i * 2 + 1]);
                  //            if (srh == -32768)
                  //            {
                  //                if (((i) < ((i / 100) + 1) * 100) && ((i - ((i / 100) * 100) > 10)))
                  //                {
                  //                    double tempp = points[i - 1].Y;
                  //                    points.Add(new DataPoint(i * 0.1, tempp));
                  //                    continue;
                  //                }
                  //                else
                  //                {

                  //                    break;
                  //                }
                  //            }
                  //            points.Add(new DataPoint(i * 0.1, (float)srh / 2.0f));
                  //        }
                  //        points.Add(new DataPoint(points[points.Count - 1].X + 0.1, points[points.Count - 1].Y));
                  //        points.Add(new DataPoint(points[points.Count - 1].X + 0.1, points[points.Count - 1].Y));
                  //        //    points.Add(new DataPoint(points[points.Count - 1].X,points[points.Count - 1].Y));
                  //        Dispatcher.BeginInvoke(new Action(() =>
                  //        {
                  //            burrDetection.Position = OdabirLimovaKontrola.LineNumber; // position number
                  //        }));

                  //        burrDetection.BurrData = points; // setting this parameter will calculate burr data
                  //        Dispatcher.BeginInvoke(new Action(() =>
                  //        {
                  //            graf.InvalidatePlot(true);

                  //            l_maxSrh.Content = burrDetection.MaxBurr.ToString();
                  //            l_postotak.Content = burrDetection.BurrPercent.ToString();

                  //            // Za izvještaj
                  //            App.MainReportInterface.BurrList.Clear();
                  //            if (burrDetection.BurrList != null)
                  //            {
                  //                App.MainReportInterface.BurrList.AddRange(burrDetection.BurrList);
                  //            }
                  //            dataGrid1.Items.Refresh();
                  //            App.pIzvjestaji.dataGridSrh.Items.Refresh();
                  //            App.MainReportInterface.Srh_max = burrDetection.MaxBurr;
                  //            App.MainReportInterface.Srh_postotak = burrDetection.BurrPercent;
                  //            App.MainReportInterface.Srh_pozicija = OdabirLimovaKontrola.LineNumber;
                  //            App.MainReportInterface.Srh_brojUzoraka = NumberOfMeas;
                  //        }));
                  //    }
                  //}

                  //if ((bool)e.StatusData.RotacijskaOs.AutomaticSimpleActive.Value == true)
                  //{
                  //    float posX = (float)e.StatusData.HorizontalnaOs.ActualPosition.X.Value;
                  //    float posY = (float)e.StatusData.HorizontalnaOs.ActualPosition.Y.Value;
                  //    float lengthGaugeValue = ((float)e.StatusData.RotacijskaOs.LengthGaugeActualValue.Value) / 2000;

                  //    if (!firstPoint)
                  //    {
                  //        // Distance to previous point
                  //        float dist = (float)Math.Sqrt(Math.Pow(posX - posXold, 2) + Math.Pow(posY - posYold, 2));
                  //        if (dist > 0.1f) // Da točke na grafu ne budu preblizu
                  //        {
                  //            // Dodaj točku na graf
                  //            plotXvalue += dist;
                  //            if (points.Count > 100) points.RemoveAt(0);
                  //            points.Add(new OxyPlot.DataPoint(plotXvalue, lengthGaugeValue));
                  //            //x_os.Maximum = points[points.Count - 1].X;
                  //            //x_os.Minimum = points[0].X;

                  //            graf.InvalidatePlot();
                  //            posXold = posX;
                  //            posYold = posY;
                  //        }
                  //    }
                  //    else
                  //    {
                  //        // Nema starih točaka pa samo dodaj točku na graf (plotXvalue = 0.0f)
                  //        points.Add(new OxyPlot.DataPoint(plotXvalue, lengthGaugeValue));
                  //        firstPoint = false;
                  //        graf.InvalidatePlot();
                  //        posXold = posX;
                  //        posYold = posY;
                  //    }
                  //    //posXold = posX;
                  //    //posYold = posY;
                      
                  //}
              }));
        }
        public void GetBurrPlotImage()
        {
            try
            {
            //    double actualHeight = graf.ActualHeight;
            //    double actualWidth = graf.ActualWidth;

            //    double renderHeight = graf.ActualHeight;
            //    double renderWidth = graf.ActualWidth;

            //    RenderTargetBitmap renderTarget = new RenderTargetBitmap((int)renderWidth, (int)renderHeight, 96, 96, PixelFormats.Pbgra32);
            //    VisualBrush sourceBrush = new VisualBrush(graf);

            //    DrawingVisual drawingVisual = new DrawingVisual();
            //    DrawingContext drawingContext = drawingVisual.RenderOpen();

                //using (drawingContext)
                //{
                //    drawingContext.PushTransform(new ScaleTransform(1, 1));
                //    drawingContext.DrawRectangle(sourceBrush, null, new Rect(new Point(0, 0), new Point(actualWidth, actualHeight)));
                //}
                //renderTarget.Render(drawingVisual);

                //App.MainReportInterface.BurrPlotImage = (BitmapSource)renderTarget;
                if (App.MainReportInterface.BurrPlotImage == null) return;

                // Save to file for using in Excel report
                Directory.CreateDirectory("Slike"); // Ako već ne postoji, napravi folder za slike
                using (var fileStream = new System.IO.FileStream("Slike/srh_graf.png", System.IO.FileMode.Create))
                {
                    PngBitmapEncoder encoder = new PngBitmapEncoder();
                    encoder.Frames.Add(BitmapFrame.Create((BitmapSource)App.MainReportInterface.BurrPlotImage));
                    encoder.Save(fileStream);
                }
            }
            catch
            { }
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            
        }

        private void p_novoMjerenje_Click_1(object sender, RoutedEventArgs e)
        {
              
        }

        public void GetSrhSheetImage()
        {
            //App.MainReportInterface.Srh_limImage = OdabirLimovaKontrola.GetImageSource();
            if (App.MainReportInterface.Srh_limImage == null) return;

            // U file za Excel
            Directory.CreateDirectory("Slike"); // Ako već ne postoji, napravi folder za slike
            using (var fileStream = new System.IO.FileStream("Slike/srh_slika_lima.png", System.IO.FileMode.Create))
            {
                PngBitmapEncoder encoder = new PngBitmapEncoder();
                encoder.Frames.Add(BitmapFrame.Create((BitmapSource)App.MainReportInterface.Srh_limImage));
                encoder.Save(fileStream);
            }
             
        }

        public void reset()
        {
            points.Clear();
            burrDetection.Reset();
            App.MainReportInterface.BurrList.Clear();
            App.pIzvjestaji.dataGridSrh.Items.Refresh();
            App.MainReportInterface.Srh_max = 0;
            App.MainReportInterface.Srh_postotak = 0;
            App.MainReportInterface.Srh_brojUzoraka = 0;
            App.pIzvjestaji.dataGridSrh.Items.Refresh();
        }
        
    }
}
