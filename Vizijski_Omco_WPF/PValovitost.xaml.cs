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
    /// Interaction logic for PValovitost.xaml
    /// </summary>
    public partial class PValovitost : Page
    {
        List<DataPoint> points = new List<DataPoint>();
        LineSeries lineSeries = new LineSeries();
        RippleDetection rippleDetection = new RippleDetection();
        float scalex;
        float scaley;
        public PValovitost()
        {
            InitializeComponent();
            lineSeries.Color = Colors.Blue;
            lineSeries.StrokeThickness = 3;
            lineSeries.LineStyle = OxyPlot.LineStyle.Automatic;
            lineSeries.MarkerSize = 5;
            lineSeries.MarkerStroke = Colors.Green;
            lineSeries.CanTrackerInterpolatePoints = true;
            lineSeries.Title = "Valovitost";
            lineSeries.ItemsSource = points;
            //graf.Series.Add(lineSeries);
            //minimapa.FirstPointVisible = true;
            //minimapa.SecondPointVisible = true;
            App.PLC.Update_1_s += new PLCInterface.UpdateHandler(updatePage);
            App.PLC.Update_100_ms += new PLCInterface.UpdateHandler(updatePage_100ms);
        }

        private void b_potvrdiPrvuTocku_Click(object sender, RoutedEventArgs e)
        {
            App.PLC.WriteTag(App.PLC.CONTROL.HorizontalnaOs.IdiUPozicijuTicala, true);
        }
        
        private void b_potvrdiDruguTočku_Click(object sender, RoutedEventArgs e)
        {
            App.PLC.WriteTag(App.PLC.CONTROL.HorizontalnaOs.ZadanaPozicija, true);
        }
        
        private void b_pokreniMjerenje_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            //if ((bool)App.PLC.STATUS.VertikalnaOs.AutomaticActive.Value == false)
            //{
            //    App.PLC.WriteTag(App.PLC.CONTROL.HorizontalnaOs.IdiUHome, true);
            //    App.PLC.WriteTag(App.PLC.CONTROL.HorizontalnaOs.JogPlus, false);
            //    App.PLC.WriteTag(App.PLC.CONTROL.HorizontalnaOs.IdiUPoziciju, false);
            //}
            //else
            //{
            //    App.PLC.WriteTag(App.PLC.CONTROL.HorizontalnaOs.IdiUHome, false);
            //    App.PLC.WriteTag(App.PLC.CONTROL.HorizontalnaOs.IdiUPoziciju, false);
            //    App.PLC.WriteTag(App.PLC.CONTROL.HorizontalnaOs.JogPlus, true);
            //}
        }
        
        private void b_pokreniMjerenje_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
            App.PLC.WriteTag(App.PLC.CONTROL.HorizontalnaOs.IdiUHome, false);
            App.PLC.WriteTag(App.PLC.CONTROL.HorizontalnaOs.JogPlus, false);
            App.PLC.WriteTag(App.PLC.CONTROL.HorizontalnaOs.IdiUPoziciju, false);
        }
        
        private void b_pokreniKompenzaciju_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            //if ((bool)App.PLC.STATUS.VertikalnaOs.AutomaticActive.Value == false)
            //{
            //    App.PLC.WriteTag(App.PLC.CONTROL.HorizontalnaOs.IdiUHome, false);
            //    App.PLC.WriteTag(App.PLC.CONTROL.HorizontalnaOs.JogPlus, false);
            //    App.PLC.WriteTag(App.PLC.CONTROL.HorizontalnaOs.IdiUPoziciju, true);
            //}
            //else
            //{
            //    App.PLC.WriteTag(App.PLC.CONTROL.HorizontalnaOs.IdiUHome, false);
            //    App.PLC.WriteTag(App.PLC.CONTROL.HorizontalnaOs.IdiUPoziciju, false);
            //    App.PLC.WriteTag(App.PLC.CONTROL.HorizontalnaOs.JogPlus, true);
            //}
        }
        
        private void b_pokreniKompenzaciju_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
            App.PLC.WriteTag(App.PLC.CONTROL.HorizontalnaOs.IdiUHome, false);
            App.PLC.WriteTag(App.PLC.CONTROL.HorizontalnaOs.JogPlus, false);
            App.PLC.WriteTag(App.PLC.CONTROL.HorizontalnaOs.IdiUPoziciju, false);
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            //scalex = (float)minimapa.ActualWidth / 5000.0f;
            //scaley = (float)minimapa.ActualHeight / 1020.0f;
        }

        private void updatePage(object sender, PLCInterfaceEventArgs e)
        {
            //if ((bool)e.StatusData.VertikalnaOs.AutomaticActive.Value)
            //{
            //    points.Clear();

            //    byte[] tempByte = App.PLC.ReadCustom(30, 36, 2000); // DB30 LASER_SENSOR_DB, DATA_LOG_BUFFER offset 42.0, Array[0..999] of Int (16 bit!), db30.dbd36 = Resolution
            //    float resolution = Snap7.S7.GetRealAt(tempByte, 0);
            //    for (int i = 6; i < 1006; i++)
            //    {

            //        Int16 ripple = (Int16)((tempByte[i * 2] << 8) | tempByte[i * 2 + 1]);
            //        if (ripple == -32768)
            //        {
            //            break;
            //        }
            //        points.Add(new DataPoint(i*resolution, ripple/1000.0f)); // 

            //    }
            //    rippleDetection.RippleData = points;
                
            //    Dispatcher.BeginInvoke(new Action(() =>
            //    {
                    

            //        try
            //        {
            //            rippleDetection.Smooth();
            //            graf.InvalidatePlot(true);
            //            rippleDetection.EdgeThicknes = (int)(Convert.ToDouble(tb_debljina.Text.Replace(".", ",")) * 1000);

            //            rippleDetection.Detect();
            //        }
            //        catch
            //        {

            //        }
            //        lb_brojValova.Content = rippleDetection.NoOfWaves.ToString();
            //        lb_dužinaVala.Content = rippleDetection.EdgeLength.ToString();
            //        lb_faktorValovitosti.Content = rippleDetection.Ratio.ToString();
            //        lb_visinaVala.Content = (rippleDetection.EdgeHeight/1000.0f).ToString();

            //        App.MainReportInterface.Valovitost_brojValova = rippleDetection.NoOfWaves;
            //        App.MainReportInterface.Valovitost_duzinaVala = rippleDetection.EdgeLength;
            //        App.MainReportInterface.Valovitost_faktor = rippleDetection.Ratio;
            //        App.MainReportInterface.Valovitost_visinaVala = rippleDetection.EdgeHeight / 1000.0f;
            //        App.MainReportInterface.Valovitost_pozicija = OdabirLimovaKontrola.LineNumber;
            //    }));
            //}
        }
        
        private void updatePage_100ms(object sender, PLCInterfaceEventArgs e)
        {
            Dispatcher.BeginInvoke(new Action(() =>
            {
                //minimapa.ActualPosition = new Point(Math.Abs((float)e.StatusData.HorizontalnaOs.ActualPosition.X.Value) * scalex, (float)e.StatusData.HorizontalnaOs.ActualPosition.Y.Value * scaley);
                //minimapa.FirstPoint = new Point(Math.Abs((float)e.StatusData.VertikalnaOs.FirstPoint.X.Value) * scalex, (float)e.StatusData.VertikalnaOs.FirstPoint.Y.Value * scaley);
                //minimapa.SecondPoint = new Point(Math.Abs((float)e.StatusData.VertikalnaOs.LastPoint.X.Value) * scalex, (float)e.StatusData.VertikalnaOs.LastPoint.Y.Value * scaley);
                //if (((bool)e.StatusData.VertikalnaOs.AutomaticActive.Value == true) && ((bool)e.StatusData.VertikalnaOs.SheetAbsent.Value == false))
                //{
                //    b_pokreniMjerenje.Foreground = Brushes.Red;
                //    b_pokreniMjerenje.Content = "STOP";
                //}
                //else
                //{
                //    b_pokreniMjerenje.Content = "POKRENI MJERENJE";
                //    b_pokreniMjerenje.Foreground = Brushes.Black;
                //}

                //if (((bool)e.StatusData.VertikalnaOs.AutomaticActive.Value == true) && ((bool)e.StatusData.VertikalnaOs.SheetAbsent.Value == true))
                //{
                //    b_pokreniKompenzaciju.Foreground = Brushes.Red;
                //    b_pokreniKompenzaciju.Content = "STOP";
                //}
                //else
                //{
                //    b_pokreniKompenzaciju.Content = "POKRENI KOMPENZACIJU";
                //    b_pokreniKompenzaciju.Foreground = Brushes.Black;
                //}
            }));
            
        }
        
        public void GetRipplePlotImage()
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

                //App.MainReportInterface.RipplePlotImage = (BitmapSource)renderTarget;
                //if (App.MainReportInterface.RipplePlotImage == null) return;

                // Save to file for using in Excel report
                Directory.CreateDirectory("Slike"); // Ako već ne postoji, napravi folder za slike
                using (var fileStream = new System.IO.FileStream("Slike/valovitost_graf.png", System.IO.FileMode.Create))
                {
                    PngBitmapEncoder encoder = new PngBitmapEncoder();
                    encoder.Frames.Add(BitmapFrame.Create((BitmapSource)App.MainReportInterface.RipplePlotImage));
                    encoder.Save(fileStream);
                }

                // Pohrani ime lima
                //App.MainReportInterface.SheetName = OdabirLimovaKontrola.SheetName;
            }
            catch
            { }
        }

        private void tb_debljina_LostFocus(object sender, RoutedEventArgs e)
        {
            try
            {
                RippleDetection r = new RippleDetection();
                //r.EdgeThicknes = (int)(Convert.ToDouble(tb_debljina.Text.Replace(".", ",")) * 1000);

                points.Clear();

                byte[] tempByte = App.PLC.ReadCustom(30, 36, 2000); // DB30 LASER_SENSOR_DB, DATA_LOG_BUFFER offset 42.0, Array[0..999] of Int (16 bit!), db30.dbd36 = Resolution
                float resolution = Snap7.S7.GetRealAt(tempByte, 0);
                for (int i = 6; i < 1006; i++)
                {

                    Int16 ripple = (Int16)((tempByte[i * 2] << 8) | tempByte[i * 2 + 1]);
                    if (ripple == -32768)
                    {
                        break;
                    }
                    points.Add(new DataPoint(i * resolution, ripple/1000.0f)); // 

                }

                r.RippleData = points;
                r.Smooth();
                //graf.InvalidatePlot(true);

                r.Detect();
                App.MainReportInterface.Valovitost_brojValova = r.NoOfWaves;
                App.MainReportInterface.Valovitost_duzinaVala = r.EdgeLength;
                App.MainReportInterface.Valovitost_faktor = r.Ratio;
                App.MainReportInterface.Valovitost_visinaVala = r.EdgeHeight/1000.0f;
                //App.MainReportInterface.Valovitost_pozicija = OdabirLimovaKontrola.LineNumber;
            }
            catch
            { }
        }

        public void GetValovitostSheetImage()
        {
            //App.MainReportInterface.Valovitost_limImage = OdabirLimovaKontrola.GetImageSource();
            if (App.MainReportInterface.Valovitost_limImage == null) return;

            // U file za Excel
            Directory.CreateDirectory("Slike"); // Ako već ne postoji, napravi folder za slike
            using (var fileStream = new System.IO.FileStream("Slike/valovitost_slika_lima.png", System.IO.FileMode.Create))
            {
                PngBitmapEncoder encoder = new PngBitmapEncoder();
                encoder.Frames.Add(BitmapFrame.Create((BitmapSource)App.MainReportInterface.Valovitost_limImage));
                encoder.Save(fileStream);
            }

            // Pohrani ime lima
            //App.MainReportInterface.SheetName = OdabirLimovaKontrola.SheetName;
        }

        private void tb_debljina_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                try
                {
                    RippleDetection r = new RippleDetection();
                    //r.EdgeThicknes = (int)(Convert.ToDouble(tb_debljina.Text.Replace(".",",")) * 1000);

                    points.Clear();

                    byte[] tempByte = App.PLC.ReadCustom(30, 36, 2000); // DB30 LASER_SENSOR_DB, DATA_LOG_BUFFER offset 42.0, Array[0..999] of Int (16 bit!), db30.dbd36 = Resolution
                    float resolution = Snap7.S7.GetRealAt(tempByte, 0);
                    for (int i = 6; i < 1006; i++)
                    {

                        Int16 ripple = (Int16)((tempByte[i * 2] << 8) | tempByte[i * 2 + 1]);
                        if (ripple == -32768)
                        {
                            break;
                        }
                        points.Add(new DataPoint(i * resolution, ripple/1000.0f)); // 

                    }

                    r.RippleData = points;
                    r.Smooth();
                    //graf.InvalidatePlot(true);

                    r.Detect();
                    App.MainReportInterface.Valovitost_brojValova = r.NoOfWaves;
                    App.MainReportInterface.Valovitost_duzinaVala = r.EdgeLength;
                    App.MainReportInterface.Valovitost_faktor = r.Ratio;
                    App.MainReportInterface.Valovitost_visinaVala = r.EdgeHeight / 1000.0f;
                    //App.MainReportInterface.Valovitost_pozicija = OdabirLimovaKontrola.LineNumber;
                }
                catch { }
            }
            
        }

        public void reset()
        {
            App.MainReportInterface.Valovitost_brojValova = 0;
            App.MainReportInterface.Valovitost_duzinaVala = 0;
            App.MainReportInterface.Valovitost_faktor = 0;
            App.MainReportInterface.Valovitost_visinaVala = 0;
            //App.MainReportInterface.Valovitost_pozicija = OdabirLimovaKontrola.LineNumber;
            points.Clear();
            //graf.InvalidatePlot(true);
        }

    }
}
