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
using Microsoft.Expression.Shapes;
using System.Timers;

namespace MjerniStolLimovi
{
    /// <summary>
    /// Interaction logic for OdabirLimova.xaml
    /// </summary>
    
    public class MeasuresEventArgs : EventArgs
    {
        private List<string> measuresList;
        public List<string> MeasuresList
        {
            get { return measuresList; }
            set { measuresList = value; }
        }

        public MeasuresEventArgs(List<string> _measuresList)
        {
            MeasuresList = new List<string>(_measuresList);
        }
    }

    public class CalculatedEventArgs : EventArgs
    {
        private List<double> calculatedList;
        public List<double> CalculatedList
        {
            get { return calculatedList; }
            set { calculatedList = value; }
        }

        public CalculatedEventArgs(List<double> _calculatedList)
        {
            CalculatedList = new List<double>(_calculatedList);
        }
    }

    public partial class OdabirLimova : UserControl
    {
        System.Timers.Timer blinkTimer = new System.Timers.Timer(500);
        public static readonly DependencyProperty LimProperty = DependencyProperty.Register("Lim", typeof(Type), typeof(OdabirLimova), new PropertyMetadata(null, new PropertyChangedCallback(OnLimChanged)));
        public static readonly DependencyProperty lineNumber = DependencyProperty.Register("LineNumber", typeof(int), typeof(OdabirLimova), new PropertyMetadata(0));
        public static readonly DependencyProperty arcNumber = DependencyProperty.Register("ArcNumber", typeof(int), typeof(OdabirLimova), new PropertyMetadata(0));
        public static readonly DependencyProperty verticalCutting = DependencyProperty.Register("VerticalCutting", typeof(string), typeof(OdabirLimova), new PropertyMetadata(""));
        public static readonly DependencyProperty sheetImageSource = DependencyProperty.Register("SheetImageSource", typeof(ImageSource), typeof(OdabirLimova), new PropertyMetadata());

        public delegate void SheetChangedHandler(OdabirLimova sender, MeasuresEventArgs e);
        public static event SheetChangedHandler SheetChanged;

        public delegate void MeasuresCalculatedHandler(OdabirLimova sender, CalculatedEventArgs e);
        public static event MeasuresCalculatedHandler MeasuresCalculated;

       
        public int LineNumber
        {
            get { return (int)GetValue(lineNumber); }
            set { SetValue(lineNumber, value); }
        }

        public int ArcNumber
        {
            get { return (int)GetValue(arcNumber); }
            set { SetValue(arcNumber, value); }
        }

        public string VerticalCutting
        {
            get { return (string)GetValue(verticalCutting); }
            set { SetValue(verticalCutting, value); }
        }

        public ImageSource SheetImageSource
        {
            get { return (ImageSource)GetValue(sheetImageSource); }
            set { SetValue(sheetImageSource, value); }
        }

        private String sheetName;
        public String SheetName
        {
            get { return sheetName; }
            set { sheetName = value; }
        }

        public ImageSource SheetImage
        {
            get
            {
                return getSheetImage();
            }
        }

        private BitmapSource getSheetImage()
        {
            double actualHeight = vb_limovi.ActualHeight;
            double actualWidth = vb_limovi.ActualWidth;

            double renderHeight = vb_limovi.ActualHeight;
            double renderWidth = vb_limovi.ActualWidth;

            RenderTargetBitmap renderTarget = new RenderTargetBitmap((int)renderWidth, (int)renderHeight, 96, 96, PixelFormats.Pbgra32);
            VisualBrush sourceBrush = new VisualBrush(vb_limovi);

            DrawingVisual drawingVisual = new DrawingVisual();
            //DrawingContext drawingContext = drawingVisual.RenderOpen();

            //using (drawingContext)
            //{
            //    drawingContext.PushTransform(new ScaleTransform(1, 1));
            //    drawingContext.DrawRectangle(sourceBrush, null, new Rect(new Point(0, 0), new Point(actualWidth, actualHeight)));
            //}
            renderTarget.Render(drawingVisual);

            // Test - save to file
            //using (var fileStream = new System.IO.FileStream("dimenzije_slika_lima.png", System.IO.FileMode.Create))
            //{
            //    PngBitmapEncoder encoder = new PngBitmapEncoder();
            //    encoder.Frames.Add(BitmapFrame.Create(renderTarget));
            //    encoder.Save(fileStream);
            //}

            return (BitmapSource)renderTarget;
        }

        private int purpose;
        public int Purpose
        {
            get { return purpose; }
            set 
            {

                purpose = value;
                ((ILimKontrola)vb_limovi.Child).Purpose = purpose;
                if (purpose == 0)
                {
                    //blinkTimer.Start();
                    blinkTimer.Start();
                }
                else
                {
                    blinkTimer.Stop();
                }

            }
        }
        public Type Lim
        {
            get { return (Type)GetValue(LimProperty); }
            set { SetValue(LimProperty, value);
            //refreshImageSource();
            }
        }

        private static void OnLimChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            OdabirLimova odabirKontrola = (OdabirLimova)d;
            ILimKontrola limKontrola = (ILimKontrola)odabirKontrola.vb_limovi.Child;

            odabirKontrola.vb_limovi.Child = (UIElement)Activator.CreateInstance(odabirKontrola.Lim);
            ((ILimKontrola)odabirKontrola.vb_limovi.Child).Purpose = ((OdabirLimova)d).Purpose;
            ((ILimKontrola)odabirKontrola.vb_limovi.Child).PointList = ((ILimKontrola)odabirKontrola.vb_limovi.Child).PointList; // za refrešanje na početku 
            SheetChanged(odabirKontrola, new MeasuresEventArgs(((ILimKontrola)odabirKontrola.vb_limovi.Child).MeasuresList));
        }



        public OdabirLimova()
        {
            InitializeComponent();
            blinkTimer.Elapsed += new ElapsedEventHandler(OdabirLimova_blinkTimerTick);
    //        blinkTimer.Start();
            
        }

        public void AddPoint(Point _point)
        {
            var limKontrola = (ILimKontrola)this.vb_limovi.Child;
            limKontrola.PointList.Add(_point);
            limKontrola.PointList = limKontrola.PointList;
            if (limKontrola.PointList.Count>=(limKontrola.PointArcs.Children.Count+limKontrola.CircleArcs.Children.Count))
            {
                var measures = limKontrola.GetAll();
                MeasuresCalculated(this, new CalculatedEventArgs(measures));
            }

        }

        public void AddPointList(List<object> _pointList)
        {
            var limKontrola = (ILimKontrola)this.vb_limovi.Child;
            limKontrola.PointList=_pointList;
            
            if (limKontrola.PointList.Count >= (limKontrola.PointArcs.Children.Count + limKontrola.CircleArcs.Children.Count))
            {
                var measures = limKontrola.GetAll();
                MeasuresCalculated(this, new CalculatedEventArgs(measures));
            }
        }

        public BitmapSource GetImageSource()
        {
            try
            {                
                double actualHeight = vb_limovi.ActualHeight;
                double actualWidth = vb_limovi.ActualWidth;

                double renderHeight = vb_limovi.ActualHeight;
                double renderWidth = vb_limovi.ActualWidth;

                RenderTargetBitmap renderTarget = new RenderTargetBitmap((int)renderWidth, (int)renderHeight, 96, 96, PixelFormats.Pbgra32);
                VisualBrush sourceBrush = new VisualBrush(vb_limovi);

                DrawingVisual drawingVisual = new DrawingVisual();
                DrawingContext drawingContext = drawingVisual.RenderOpen();

                using (drawingContext)
                {
                    drawingContext.PushTransform(new ScaleTransform(1, 1));
                    drawingContext.DrawRectangle(sourceBrush, null, new Rect(new Point(0, 0), new Point(actualWidth, actualHeight)));
                }
                renderTarget.Render(drawingVisual);

                //using (var fileStream = new System.IO.FileStream("dimenzije_slika_lima.png", System.IO.FileMode.Create))
                //{
                //    PngBitmapEncoder encoder = new PngBitmapEncoder();
                //    encoder.Frames.Add(BitmapFrame.Create(renderTarget));
                //    encoder.Save(fileStream);
                //}

                //SheetImageSource = (BitmapSource)renderTarget;

                SheetName = ((ILimKontrola)vb_limovi.Child).SheetName;
                return (BitmapSource)renderTarget;
            }
            catch
            {
                return null;
            }
        }

        public void GetJpgImage(UIElement source, double scale, int quality)
        {
            double actualHeight = source.RenderSize.Height;
            double actualWidth = source.RenderSize.Width;

            double renderHeight = actualHeight * scale;
            double renderWidth = actualWidth * scale;

            RenderTargetBitmap renderTarget = new RenderTargetBitmap((int)renderWidth, (int)renderHeight, 96, 96, PixelFormats.Pbgra32);
            VisualBrush sourceBrush = new VisualBrush(source);

            DrawingVisual drawingVisual = new DrawingVisual();
            DrawingContext drawingContext = drawingVisual.RenderOpen();

            using (drawingContext)
            {
                drawingContext.PushTransform(new ScaleTransform(scale, scale));
                drawingContext.DrawRectangle(sourceBrush, null, new Rect(new Point(0, 0), new Point(actualWidth, actualHeight)));
            }
            renderTarget.Render(drawingVisual);

            using (var fileStream = new System.IO.FileStream("ss.jpg", System.IO.FileMode.Create))
            {
                JpegBitmapEncoder encoder = new JpegBitmapEncoder();
                encoder.QualityLevel = quality;
                encoder.Frames.Add(BitmapFrame.Create(renderTarget));
                encoder.Save(fileStream);
            }
        }

        private void b_A_Click(object sender, RoutedEventArgs e)
        {
            Lim = System.Type.GetType("MjerniStolLimovi.A");
        }

        private void b_AIZ6_Click(object sender, RoutedEventArgs e)
        {
            Lim = System.Type.GetType("MjerniStolLimovi.A_IZ6");
        }

        private void b_AIZ1IZ6_Click(object sender, RoutedEventArgs e)
        {
            Lim = System.Type.GetType("MjerniStolLimovi.A_IZ1_IZ6");
        }

        private void b_BIZ5_Click(object sender, RoutedEventArgs e)
        {
            Lim = System.Type.GetType("MjerniStolLimovi.B_IZ5");
        }

        private void b_BIZ6_Click(object sender, RoutedEventArgs e)
        {
            Lim = System.Type.GetType("MjerniStolLimovi.B_IZ6");
        }

        private void b_BIZ2IZ7_Click(object sender, RoutedEventArgs e)
        {
            Lim = System.Type.GetType("MjerniStolLimovi.B_IZ2_IZ7");
        }
        private void b_BIZ10_Click(object sender, RoutedEventArgs e)
        {
            Lim = System.Type.GetType("MjerniStolLimovi.B_IZ10");
        }
        private void b_CIZ10_Click(object sender, RoutedEventArgs e)
        {
            Lim = System.Type.GetType("MjerniStolLimovi.C_IZ10");
        }
        private void b_CIZ11_Click(object sender, RoutedEventArgs e)
        {
            Lim = System.Type.GetType("MjerniStolLimovi.C_IZ11");
        }
        private void b_D_Click(object sender, RoutedEventArgs e)
        {
            Lim = System.Type.GetType("MjerniStolLimovi.D");
        }
        private void b_DIZ11_Click(object sender, RoutedEventArgs e)
        {
            Lim = System.Type.GetType("MjerniStolLimovi.D_IZ11");
        }

        private void b_DIZ15_Click(object sender, RoutedEventArgs e)
        {
            Lim = System.Type.GetType("MjerniStolLimovi.D_IZ15");
        }

        private void b_EIZ14_Click(object sender, RoutedEventArgs e)
        {
            Lim = System.Type.GetType("MjerniStolLimovi.E_IZ14");
        }

        private void b_KIZ5IZ8_Click(object sender, RoutedEventArgs e)
        {
            Lim = System.Type.GetType("MjerniStolLimovi.K_IZ5_IZ8");
        }

        private void b_MIZ12_Click(object sender, RoutedEventArgs e)
        {
            Lim = System.Type.GetType("MjerniStolLimovi.M_IZ12");
        }

        private void b_NIZ13_Click(object sender, RoutedEventArgs e)
        {
            Lim = System.Type.GetType("MjerniStolLimovi.N_IZ13");
        }

        private void TEST_Click(object sender, RoutedEventArgs e)
        {
            Lim = System.Type.GetType("MjerniStolLimovi.TestUzorak");
        }

        private void b_TU2_Click(object sender, RoutedEventArgs e)
        {
            Lim = System.Type.GetType("MjerniStolLimovi.TU2");
        }
        private void b_TU3_Click(object sender, RoutedEventArgs e)
        {
            Lim = System.Type.GetType("MjerniStolLimovi.TU3");
        }
        private void b_TU4_Click(object sender, RoutedEventArgs e)
        {
            Lim = System.Type.GetType("MjerniStolLimovi.TU4");
        }

        private void b_TU5_Click(object sender, RoutedEventArgs e)
        {
            Lim = System.Type.GetType("MjerniStolLimovi.TU5");
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            sbr.ScrollToVerticalOffset(30 + sbr.VerticalOffset);
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            sbr.ScrollToVerticalOffset(-30 + sbr.VerticalOffset);
        }

        private void UserControl_MouseDown(object sender, MouseButtonEventArgs e)
        {
            //im.Source = SheetImageSource;
            Path tempLine;
            Ellipse tempCircle;
            Arc tempArc;

            if ((Mouse.DirectlyOver is Path) && ((purpose == 1) || (purpose == 2) || (purpose == 3)))
            {
                tempLine = ((Path)Mouse.DirectlyOver);
                if (tempLine.Name.Substring(0,5).Contains("Line"))
                {
                    for (int i = 0; i < ((ILimKontrola)this.vb_limovi.Child).MainLines.Children.Count; i++)
                    {
                        ((Path)((ILimKontrola)this.vb_limovi.Child).MainLines.Children[i]).Stroke = Brushes.Black;
                    }
                    for (int i = 0; i < ((ILimKontrola)this.vb_limovi.Child).Circles.Children.Count; i++)
                    {
                        ((Ellipse)((ILimKontrola)this.vb_limovi.Child).Circles.Children[i]).Stroke = Brushes.Black;
                    }
                    for (int i = 0; i < ((ILimKontrola)this.vb_limovi.Child).PointArcs.Children.Count; i++)
                    {
                        ((Arc)((ILimKontrola)this.vb_limovi.Child).PointArcs.Children[i]).Fill = Brushes.Red;
                    }
                    tempLine.Stroke = Brushes.Blue;
                    string[] tempStringArray = tempLine.Name.Split(new string[] { "Line" }, StringSplitOptions.None);
                    LineNumber = Convert.ToInt16(tempStringArray[1]);
                    VerticalCutting = (tempStringArray[0] == "p") ? "Poprečno" : "Uzdužno";
                }
            }

            if ((Mouse.DirectlyOver is Ellipse) && (purpose == 2))
            {
                tempCircle = ((Ellipse)Mouse.DirectlyOver);
                if (tempCircle.Name.Substring(0, 6).Contains("Circle"))
                {
                    for (int i = 0; i < ((ILimKontrola)this.vb_limovi.Child).MainLines.Children.Count; i++)
                    {
                        ((Path)((ILimKontrola)this.vb_limovi.Child).MainLines.Children[i]).Stroke = Brushes.Black;
                    }
                    for (int i = 0; i < ((ILimKontrola)this.vb_limovi.Child).Circles.Children.Count; i++)
                    {
                        ((Ellipse)((ILimKontrola)this.vb_limovi.Child).Circles.Children[i]).Stroke = Brushes.Black;
                    }
                    for (int i = 0; i < ((ILimKontrola)this.vb_limovi.Child).PointArcs.Children.Count; i++)
                    {
                        ((Arc)((ILimKontrola)this.vb_limovi.Child).PointArcs.Children[i]).Fill = Brushes.Red;
                    }
                    tempCircle.Stroke = Brushes.Blue;
                    LineNumber = Convert.ToInt16(tempCircle.Name.Split(new string[] { "Circle" }, StringSplitOptions.None)[1]);
                }
            }

            if ((Mouse.DirectlyOver is Arc) && (purpose == 4))
            {
                tempArc = ((Arc)Mouse.DirectlyOver);
                if (tempArc.Name.Substring(0, 5).Contains("Point"))
                {
                    for (int i = 0; i < ((ILimKontrola)this.vb_limovi.Child).MainLines.Children.Count; i++)
                    {
                        ((Path)((ILimKontrola)this.vb_limovi.Child).MainLines.Children[i]).Stroke = Brushes.Black;
                    }
                    for (int i = 0; i < ((ILimKontrola)this.vb_limovi.Child).Circles.Children.Count; i++)
                    {
                        ((Ellipse)((ILimKontrola)this.vb_limovi.Child).Circles.Children[i]).Stroke = Brushes.Black;
                    }
                    for (int i = 0; i < ((ILimKontrola)this.vb_limovi.Child).PointArcs.Children.Count; i++)
                    {
                        ((Arc)((ILimKontrola)this.vb_limovi.Child).PointArcs.Children[i]).Fill = Brushes.Red;
                    }
                    tempArc.Fill = Brushes.Blue;
                    ArcNumber = Convert.ToInt16(tempArc.Name.Split(new string[] { "Point" }, StringSplitOptions.None)[1]);
                }
            }

        }

        private void OdabirLimova_blinkTimerTick(object sender, ElapsedEventArgs e)
        {
            this.Dispatcher.BeginInvoke(new Action(() =>
            {
                var circArcList = ((ILimKontrola)this.vb_limovi.Child).CircleArcs.Children;
                var pointArcList = ((ILimKontrola)this.vb_limovi.Child).PointArcs.Children;
                foreach (Arc a in circArcList)
                {
                    if (a.Fill == Brushes.Red)
                    {
                        if (a.Visibility == Visibility.Hidden)
                            a.Visibility = Visibility.Visible;
                        else
                            a.Visibility = Visibility.Hidden;
                    }
                    else
                        a.Visibility = Visibility.Visible;
                }
                foreach (Arc a in pointArcList)
                {
                    if (a.Fill == Brushes.Red)
                    {
                        if (a.Visibility == Visibility.Hidden)
                            a.Visibility = Visibility.Visible;
                        else
                            a.Visibility = Visibility.Hidden;
                    }
                    else
                        a.Visibility = Visibility.Visible;
                }
            }));
        }

        private void CommonAngle_Click(object sender, RoutedEventArgs e)
        {
            Lim = System.Type.GetType("MjerniStolLimovi.CommonAngle");
        }

        

    }
}
