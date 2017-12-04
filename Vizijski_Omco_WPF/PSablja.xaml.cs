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

namespace VizijskiSustavWPF
{
    /// <summary>
    /// Interaction logic for PSrh.xaml
    /// </summary>
    public partial class PSablja : Page
    {
        public static readonly DependencyProperty numberOfMeas = DependencyProperty.Register("NumberOfMeas", typeof(int), typeof(PSablja), new PropertyMetadata(10));

       
        public int NumberOfMeas
        {
            get
            {
                return (int)GetValue(numberOfMeas);
            }
            set { SetValue(numberOfMeas, value); }
        }

        List<DataPoint> points = new List<DataPoint>();
        LineSeries lineSeries = new LineSeries();
        float scalex;
        float scaley;
        public PSablja()
        {
            InitializeComponent();
            lineSeries.Color = Colors.Blue;
            lineSeries.StrokeThickness = 3;
            lineSeries.LineStyle = OxyPlot.LineStyle.Automatic;
            lineSeries.MarkerSize = 5;
            lineSeries.MarkerStroke = Colors.Green;
            lineSeries.CanTrackerInterpolatePoints = true;
            lineSeries.Title = "Sablja";
            lineSeries.Smooth = true;
            lineSeries.ItemsSource = points;
            //graf.Series.Add(lineSeries);
            //minimapa.FirstPointVisible = true;
            //minimapa.SecondPointVisible = true;
            App.PLC.Update_1_s += new PLCInterface.UpdateHandler(updatePage);
            //App.PLC.Update_100_ms += new PLCInterface.UpdateHandler(updatePage_100ms);
        }

        private void p_postaviPrvuTocku_Click(object sender, RoutedEventArgs e)
        {
            App.PLC.WriteTag(App.PLC.CONTROL.Ticalo.TicaloDolje, true);
        }

        private void p_postaviDruguTocku_Click(object sender, RoutedEventArgs e)
        {
            App.PLC.WriteTag(App.PLC.CONTROL.Ticalo.Nuliraj, true);
        }

        private void p_pokreniMjerenje_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            //if (!(bool)App.PLC.STATUS.Saber.AutomaticActive.Value)
            //{
            //    App.PLC.WriteTag(App.PLC.CONTROL.Ticalo.NumberOfMeas, (short)NumberOfMeas);
            //    App.PLC.WriteTag(App.PLC.CONTROL.Ticalo.Stop, false);
            //    App.PLC.WriteTag(App.PLC.CONTROL.Ticalo.TicaloGore, true);
            //}
            //else
            //{
            //    App.PLC.WriteTag(App.PLC.CONTROL.Ticalo.TicaloGore, false);
            //    App.PLC.WriteTag(App.PLC.CONTROL.Ticalo.Stop, true);
            //}
        }

        private void p_pokreniMjerenje_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
            App.PLC.WriteTag(App.PLC.CONTROL.Ticalo.TicaloGore, false);
            //App.PLC.WriteTag(App.PLC.CONTROL.Ticalo.Stop, false);
        }

        public void GetCamberPlotImage()
        {
            try
            {
                //double actualHeight = graf.ActualHeight;
                //double actualWidth = graf.ActualWidth;

                //double renderHeight = graf.ActualHeight;
                //double renderWidth = graf.ActualWidth;

                //RenderTargetBitmap renderTarget = new RenderTargetBitmap((int)renderWidth, (int)renderHeight, 96, 96, PixelFormats.Pbgra32);
                //VisualBrush sourceBrush = new VisualBrush(graf);

                DrawingVisual drawingVisual = new DrawingVisual();
                DrawingContext drawingContext = drawingVisual.RenderOpen();

                using (drawingContext)
                {
                    //drawingContext.PushTransform(new ScaleTransform(1, 1));
                    //drawingContext.DrawRectangle(sourceBrush, null, new Rect(new Point(0, 0), new Point(actualWidth, actualHeight)));
                }
                //renderTarget.Render(drawingVisual);

                //App.MainReportInterface.SabljaPlotImage = (BitmapSource)renderTarget;
                if (App.MainReportInterface.SabljaPlotImage == null) return;

                // Save to file for using in Excel report
                Directory.CreateDirectory("Slike"); // Ako već ne postoji, napravi folder za slike
                using (var fileStream = new System.IO.FileStream("Slike/sablja_graf.png", System.IO.FileMode.Create))
                {
                    PngBitmapEncoder encoder = new PngBitmapEncoder();
                    encoder.Frames.Add(BitmapFrame.Create((BitmapSource)App.MainReportInterface.SabljaPlotImage));
                    encoder.Save(fileStream);
                }

                // Pohrani ime lima
                //App.MainReportInterface.SheetName = OdabirLimovaKontrola.SheetName;
            }
            catch
            { }
        }

        double distancePointToLine(Point p1, Point p2, AreaPoint p)
        {
            double A, B, C;
            A = (p2.Y - p1.Y) / (p2.X - p1.X);
            B = -1;
            C = p1.Y - A * p1.X;
            return ((A * p.X + B * p.Y + C) / Math.Sqrt(A * A + B * B));
        }
        private void updatePage(object sender, PLCInterfaceEventArgs e)
        {
            //if ((bool)e.StatusData.Saber.AutomaticActive.Value)
            //{
            //    points.Clear();
            //    double saber;
            //    double max = Double.MinValue, min = Double.MaxValue;
            //    Point p1, p2;
            //    p1 = new Point((float)e.StatusData.Saber.FirstPoint.X.Value, (float)e.StatusData.Saber.FirstPoint.Y.Value);
            //    p2 = new Point((float)e.StatusData.Saber.SecondPoint.X.Value, (float)e.StatusData.Saber.SecondPoint.Y.Value);
            //    float resolution = (float)Math.Sqrt(Math.Pow(p1.X - p2.X, 2) + Math.Pow(p1.Y - p2.Y, 2)) / ((int)e.ControlData.Ticalo.NumberOfMeas.Value - 1);
                
            //    for (int i = 0; i < (int)e.ControlData.Ticalo.NumberOfMeas.Value; i++)
            //    {
            //        saber = -distancePointToLine(p1, p2, e.StatusData.Saber.POINTS[i]);
            //        if (saber < min) min = saber;
            //        else if (saber > max) max = saber;

            //        if ((-10f > saber) || (saber > 10f))
            //        {
            //            break;
            //        }
            //        points.Add(new DataPoint(i , saber));
            //    }

            //    Dispatcher.BeginInvoke(new Action(() =>
            //    {
            //        graf.InvalidatePlot(true);
            //        App.MainReportInterface.Sablja_duljina = (float)Math.Sqrt(Math.Pow(p1.X - p2.X, 2) + Math.Pow(p1.Y - p2.Y, 2));
            //        App.MainReportInterface.Sablja_visina = (float)(max - min);
            //        App.MainReportInterface.Sablja_posto = App.MainReportInterface.Sablja_visina / App.MainReportInterface.Sablja_duljina * 100.0f;

            //    }));

               
            //}
            Dispatcher.BeginInvoke(new Action(() =>
            {
               
                //App.MainReportInterface.Sablja_pozicija = OdabirLimovaKontrola.LineNumber;
            }));

        }

        //private void updatePage_100ms(object sender, PLCInterfaceEventArgs e)
        //{
        //    Dispatcher.BeginInvoke(new Action(() =>
        //    {
        //        //minimapa.ActualPosition = new Point(Math.Abs((float)e.StatusData.HorizontalnaOs.ActualPosition.X.Value) * scalex, (float)e.StatusData.HorizontalnaOs.ActualPosition.Y.Value * scaley);
        //        //minimapa.FirstPoint = new Point(Math.Abs((float)e.StatusData.Saber.FirstPoint.X.Value) * scalex, (float)e.StatusData.Saber.FirstPoint.Y.Value * scaley);
        //        //minimapa.SecondPoint = new Point(Math.Abs((float)e.StatusData.Saber.SecondPoint.X.Value) * scalex, (float)e.StatusData.Saber.SecondPoint.Y.Value * scaley);

        //        //if (((bool)e.StatusData.Saber.AutomaticActive.Value == true) && (p_pokreniMjerenje.Content != "STOP"))
        //        //{
        //        //    p_pokreniMjerenje.Foreground = Brushes.Red;
        //        //    p_pokreniMjerenje.Content = "STOP";
        //        //}
        //        //else if (((bool)e.StatusData.Saber.AutomaticActive.Value == false) && (p_pokreniMjerenje.Foreground != Brushes.Black))
        //        //{
        //        //    p_pokreniMjerenje.Content = "POKRENI MJERENJE";
        //        //    p_pokreniMjerenje.Foreground = Brushes.Black;
        //        //}
        //    }));
        //}

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            //scalex = (float)minimapa.ActualWidth / 5000.0f;
            //scaley = (float)minimapa.ActualHeight / 1020.0f;
        }

    //public void GetSabljaSheetImage()
    //{
    //    App.MainReportInterface.Sablja_limImage = OdabirLimovaKontrola.GetImageSource();
    //    if (App.MainReportInterface.Sablja_limImage == null) return;

    //    // U file za Excel
    //    Directory.CreateDirectory("Slike"); // Ako već ne postoji, napravi folder za slike
    //    using (var fileStream = new System.IO.FileStream("Slike/sablja_slika_lima.png", System.IO.FileMode.Create))
    //    {
    //        PngBitmapEncoder encoder = new PngBitmapEncoder();
    //        encoder.Frames.Add(BitmapFrame.Create((BitmapSource)App.MainReportInterface.Sablja_limImage));
    //        encoder.Save(fileStream);
    //    }

    //    // Pohrani ime lima
    //    App.MainReportInterface.SheetName = OdabirLimovaKontrola.SheetName;
    //}

    //public void reset()
    //{
    //    points.Clear();
    //    graf.InvalidatePlot(true);
    //    App.MainReportInterface.Sablja_duljina = 0;
    //    App.MainReportInterface.Sablja_visina = 0;
    //    App.MainReportInterface.Sablja_posto = 0;
    //}

}
}
