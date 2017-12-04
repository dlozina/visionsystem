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

namespace VizijskiSustavWPF
{
    /// <summary>
    /// Interaction logic for PSrh.xaml
    /// </summary>
    public partial class PKut : Page
    {
        float scalex;
        float scaley;

        public PKut()
        {
            InitializeComponent();
            ReportInterface rI = new ReportInterface();
            InitializeComponent();
            //minimapa.FirstPointVisible = true;
            //minimapa.SecondPointVisible = true;
            //minimapa.ThirdPointVisible = true;
            App.PLC.Update_1_s += new PLCInterface.UpdateHandler(updatePage);
            App.PLC.Update_100_ms += new PLCInterface.UpdateHandler(updatePage_100ms);
        }

        private void p_postaviPrvuTocku_Click(object sender, RoutedEventArgs e)
        {
            //App.PLC.WriteTag(App.PLC.CONTROL.Angle.SetFirstPoint, true);
        }

        private void p_postaviDruguTocku_Click(object sender, RoutedEventArgs e)
        {
            //App.PLC.WriteTag(App.PLC.CONTROL.Angle.SetSecondPoint, true);
        }

        private void p_postaviTrecuTocku_Click(object sender, RoutedEventArgs e)
        {
            //App.PLC.WriteTag(App.PLC.CONTROL.Angle.SetThitdPoint, true);
        }

        private void p_pokreniMjerenje_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            //if (!(bool)App.PLC.STATUS.Angle.AutomaticActive.Value)
            //{
            //    App.PLC.WriteTag(App.PLC.CONTROL.Angle.Stop, false);
            //    App.PLC.WriteTag(App.PLC.CONTROL.Angle.StartAngleMeas, true);
            //}
            //else
            //{
            //    App.PLC.WriteTag(App.PLC.CONTROL.Angle.StartAngleMeas, false);
            //    App.PLC.WriteTag(App.PLC.CONTROL.Angle.Stop, true);
            //}
        }

        private void p_pokreniMjerenje_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
            //App.PLC.WriteTag(App.PLC.CONTROL.Angle.StartAngleMeas, false);
            //App.PLC.WriteTag(App.PLC.CONTROL.Angle.Stop, false);
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            //scalex = (float)minimapa.ActualWidth / 5000.0f;
            //scaley = (float)minimapa.ActualHeight / 1020.0f;
        }

        private void bestFitLine(List<AreaPoint> points, out System.Drawing.PointF avg, out float k)
        {
            // Best fit line kroz sve točke
            // Nađi srednju vrijednost X i Y
            float avgX = 0.0f, avgY = 0.0f;
            foreach (var p in points)
            {
                avgX += p.X;
                avgY += p.Y;
            }
            avgX /= (float)points.Count;
            avgY /= (float)points.Count;

            float acc1 = 0.0f, acc2 = 0.0f;
            foreach (var p in points)
            {
                acc1 += (p.X - avgX) * (p.Y - avgY);
                acc2 += (p.X - avgX) * (p.X - avgX);
            }
            avg = new System.Drawing.PointF(avgX, avgY);
            k = acc1 / acc2; // y-avgY = slope * (x-avgX) -> y = avgY + slope * (x-avgX)
        }

        private void updatePage(object sender, PLCInterfaceEventArgs e)
        {

            List<AreaPoint> l1 = new List<AreaPoint>();
            List<AreaPoint> l2 = new List<AreaPoint>();
            //if ((bool)e.StatusData.Angle.AutomaticActive.Value)
            //{
            //    for (int i = 0; i < 10; i++ )
            //    {
            //        l1.Add(e.StatusData.Angle.LINE1[i]);
            //        l2.Add(e.StatusData.Angle.LINE2[i]);
            //    }
            //    bestFitLine(l1, out avg1, out k1);
            //    bestFitLine(l2, out avg2, out k2);
            //    y1 = avg1.Y - k1 * avg1.X;
            //    y2 = avg2.Y - k2 * avg2.X;
            //    Math.Atan((k2 - k1) / (1 + k1 * k2));

            //    Dispatcher.BeginInvoke(new Action(() =>
            //    {
            //        lb_alfa.Content = Math.Abs(((180/Math.PI)*Math.Atan((k2 - k1) / (1 + k1 * k2)))).ToString();
            //        lb_beta.Content = (180.0f - (180/Math.PI)*Math.Atan((k2 - k1) / (1 + k1 * k2))).ToString();
            //    }));


            //}
            Dispatcher.BeginInvoke(new Action(() =>
            {

              
            }));

        }

        private void updatePage_100ms(object sender, PLCInterfaceEventArgs e)
        {
            Dispatcher.BeginInvoke(new Action(() =>
            {
                //minimapa.ActualPosition = new Point(Math.Abs((float)e.StatusData.HorizontalnaOs.ActualPosition.X.Value) * scalex, (float)e.StatusData.HorizontalnaOs.ActualPosition.Y.Value * scaley);
                //minimapa.FirstPoint = new Point(Math.Abs((float)e.StatusData.Angle.FirstPoint.X.Value) * scalex, (float)e.StatusData.Angle.FirstPoint.Y.Value * scaley);
                //minimapa.SecondPoint = new Point(Math.Abs((float)e.StatusData.Angle.SecondPoint.X.Value) * scalex, (float)e.StatusData.Angle.SecondPoint.Y.Value * scaley);
                //minimapa.ThirdPoint = new Point(Math.Abs((float)e.StatusData.Angle.LastPoint.X.Value) * scalex, (float)e.StatusData.Angle.LastPoint.Y.Value * scaley);

                //if (((bool)e.StatusData.Angle.AutomaticActive.Value == true) && (p_pokreniMjerenje.Content != "STOP"))
                //{
                //    p_pokreniMjerenje.Foreground = Brushes.Red;
                //    p_pokreniMjerenje.Content = "STOP";
                //}
                //else if (((bool)e.StatusData.Angle.AutomaticActive.Value == false) && (p_pokreniMjerenje.Foreground != Brushes.Black))
                //{
                //    p_pokreniMjerenje.Content = "POKRENI MJERENJE";
                //    p_pokreniMjerenje.Foreground = Brushes.Black;
                //}
            }));
        }

        private void p_pokreniMjerenje_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Page_Loaded_1(object sender, RoutedEventArgs e)
        {

        }

        public void reset()
        {
            //lb_alfa.Content = 0;
            //lb_beta.Content = 0;
        }

        private void bb_komadNo1_Loaded(object sender, RoutedEventArgs e)
        {

        }
    }
}
