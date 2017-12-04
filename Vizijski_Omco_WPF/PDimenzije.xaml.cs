using System;
using System.Collections.Generic;
using System.Collections;
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
using Microsoft.VisualBasic;
using System.Data;
using SpreadsheetLight;
using Snap7;
using System.IO;
using System.Threading;
using Microsoft.Win32;


namespace VizijskiSustavWPF
{
    /// <summary>
    /// Interaction logic for PDimenzije.xaml
    /// </summary>
    public partial class PDimenzije : System.Windows.Controls.Page
    {
        Regulator Reg = new Regulator();
       
        Thread reg_thread;


        private Point mousePosition;
        List<object> cameraPointList= new List<object>();
        byte[] buffer = new byte[2];
     
       
        public PDimenzije()
        {
            InitializeComponent();
            App.AutoSearch.OutReady += new Algoritmi.OutReadyHandler(refresh);

            //MainReportInterface.readDimensions(App.ReportPath, "Sheet1", 15);
            //this.dataGrid1.ItemsSource = MainReportInterface.Dimensions;

            MjerniStolLimovi.OdabirLimova.SheetChanged += new MjerniStolLimovi.OdabirLimova.SheetChangedHandler(odabirLimova_SheetChanged);
            MjerniStolLimovi.OdabirLimova.MeasuresCalculated += new MjerniStolLimovi.OdabirLimova.MeasuresCalculatedHandler(odabirLimova_MeasuresCalculated);
            //CameraUI.DataReady += new Processing.FinishedHandler(updateImage);
            odabirLimova.Lim = System.Type.GetType("MjerniStolLimovi.A_IZ1_IZ6, MjerniStolLimovi", true);
            App.PLC.Update_1_s += new PLCInterface.UpdateHandler(updatePage);
            App.PLC.Update_100_ms += new PLCInterface.UpdateHandler(updatePage_100);
        }

        // CameraUI event handler

        private void bDodaj_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void bObrisi_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                
            }
            catch
            {
            }
        }
        //private void bTrazi_Click(object sender, RoutedEventArgs e)
        //{
           
        //    try
        //    {
        //        int upit =Convert.ToInt32(Interaction.InputBox("Upišite upit za pretragu","Traži"));
             
        //        productViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("pROIZVODNJAViewSource")));
        //        productViewSource.View.MoveCurrentToLast();
             
                
        //    }
        //    catch {
        //        MessageBox.Show("Traženi upit nije pronađen!");
        //    }
        //}

        private void bRefresh_Click(object sender, RoutedEventArgs e)
        {
            refreshDataGrid();
        }

        private void refreshDataGrid()
        {
            try
            {
            }
            catch
            {
            }
        }

        

        

        private void WindowsFormsHost_ChildChanged(object sender, System.Windows.Forms.Integration.ChildChangedEventArgs e)
        {

        }

       

        private void b_novoMjerenje_Click(object sender, RoutedEventArgs e)
        {
            //App.PLC.WriteTag(App.PLC.CONTROL.RotacijskaOs.Reset, true);
        }

        private void dataGrid1_DragEnter(object sender, DragEventArgs e)
        {
            ScrollViewer scrollView = GetScrollbar(dataGrid1);
            scrollView.ScrollToVerticalOffset(scrollView.VerticalOffset + 1);
        }


        private static ScrollViewer GetScrollbar(DependencyObject dep)
        {
            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(dep); i++)
            {
                var child = VisualTreeHelper.GetChild(dep, i);
                if (child != null && child is ScrollViewer)
                    return child as ScrollViewer;
                else
                {
                    ScrollViewer sub = GetScrollbar(child);
                    if (sub != null)
                        return sub;
                }
            }
            return null;
        }

        private void dataGrid1_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                if ((Mouse.GetPosition(dataGrid1).Y - mousePosition.Y) > 10)
                {
                    ScrollViewer scrollView = GetScrollbar(dataGrid1);
                    scrollView.ScrollToVerticalOffset(scrollView.VerticalOffset + 1);
                    mousePosition = Mouse.GetPosition(dataGrid1);
                }
                else if ((-Mouse.GetPosition(dataGrid1).Y + mousePosition.Y) > 10)
                {
                    ScrollViewer scrollView = GetScrollbar(dataGrid1);
                    scrollView.ScrollToVerticalOffset(scrollView.VerticalOffset - 1);
                    mousePosition = Mouse.GetPosition(dataGrid1);
                }
            }
            else
            {
                mousePosition = Mouse.GetPosition(dataGrid1);
            }
        }

        private void dataGrid1_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void odabirLimova_SheetChanged(MjerniStolLimovi.OdabirLimova sender, MjerniStolLimovi.MeasuresEventArgs e)
        {
            List<DimensionLine> dimensions = App.MainReportInterface.Dimensions;
            dimensions.Clear();
            foreach (string s in e.MeasuresList)
            {
                if (e.MeasuresList.First() == s)
                    dimensions.Add(new DimensionLine(s, 0, 0, 0f, -0.6f, 0));
                else if (s.Contains(' '))
                    dimensions.Add(new DimensionLine(s, 0, 0, 0f, -1f, 0));
                else
                    dimensions.Add(new DimensionLine(s, 0, 0, 0.5f, -0.5f, 0));
                
                
            }
            this.dataGrid1.Items.Refresh();
            App.pIzvjestaji.dataGrid1.Items.Refresh();
        }

        private void odabirLimova_MeasuresCalculated(MjerniStolLimovi.OdabirLimova sender, MjerniStolLimovi.CalculatedEventArgs e)
        {
            var dimensions = (List<DimensionLine>)dataGrid1.ItemsSource;
            for (int i = 0; i < dimensions.Count;i++ )
            {
                dimensions[i].Mjereno = (float)e.CalculatedList[i];
            }
            if (dataGrid1.IsFocused)
            {
                this.dataGrid1.Items.Refresh();
                App.pIzvjestaji.dataGrid1.Items.Refresh();
            }
          


        }

        private void formsHost_ChildChanged(object sender, System.Windows.Forms.Integration.ChildChangedEventArgs e)
        {

        }


        private void updatePage(object sender, PLCInterfaceEventArgs e)
        {
            //cameraPointList.Clear();
            //for (int i = 0; i <= e.StatusData.Ticalo.POINTS.Length - 1; i++)
            //{
            //    if ((e.StatusData.Ticalo.POINTS[i].TYPE == (short)objectType.CIRCLE) || (e.StatusData.Ticalo.POINTS[i].TYPE == (short)objectType.ANGLE))
            //    {
            //        cameraPointList.Add(new Point(e.StatusData.Ticalo.POINTS[i].POINT1.X, e.StatusData.Ticalo.POINTS[i].POINT1.Y));
            //    }
            //}
            //Dispatcher.BeginInvoke(new Action(() =>
            //{
            //    odabirLimova.AddPointList(cameraPointList);
            //}));
            
        }

        private void updatePage_100(object sender, PLCInterfaceEventArgs e)
        {

            Dispatcher.BeginInvoke(new Action(() =>
            {
                //if (((bool)e.StatusData.Ticalo.AutomaticActive.Value == true) && (b_ponoviMjerenje.Content != "STOP"))
                //{
                //    b_ponoviMjerenje.Foreground = Brushes.Red;
                //    b_ponoviMjerenje.Content = "STOP";
                //}
                //else if (((bool)e.StatusData.Ticalo.AutomaticActive.Value == false) && (b_ponoviMjerenje.Foreground != Brushes.Black))
                //{
                //    b_ponoviMjerenje.Content = "PONOVI MJERENJE\n    AUTOMATSKI";
                //    b_ponoviMjerenje.Foreground = Brushes.Black;
                //}
            }));

        }

        private void b_ponoviMjerenje_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            //if (!(bool)App.PLC.STATUS.Ticalo.AutomaticActive.Value)
            //{
            //    App.PLC.WriteTag(App.PLC.CONTROL.RotacijskaOs.StopDimensionMeas, false);
            //    App.PLC.WriteTag(App.PLC.CONTROL.RotacijskaOs.StartDimensionMeas, true);
            //}
            //else
            //{
            //    App.PLC.WriteTag(App.PLC.CONTROL.RotacijskaOs.StartDimensionMeas, false);
            //    App.PLC.WriteTag(App.PLC.CONTROL.RotacijskaOs.StopDimensionMeas, true);
            //}
        }

        private void b_ponoviMjerenje_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
            //App.PLC.WriteTag(App.PLC.CONTROL.RotacijskaOs.StartDimensionMeas, false);
            //App.PLC.WriteTag(App.PLC.CONTROL.RotacijskaOs.StopDimensionMeas, false);
        }

        private void b_autoTeachIn_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void b_autoTeachIn_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        {

        }

        private void b_ponistiZadnjuTocku_Click(object sender, RoutedEventArgs e)
        {
            //App.PLC.WriteTag(App.PLC.CONTROL.RotacijskaOs.CancelPoint, true);
        }

        public void GetDimenzijeSheetImage()
        {
            App.MainReportInterface.SheetImageSource = odabirLimova.GetImageSource();
            if (App.MainReportInterface.SheetImageSource == null) return;

            // U file za Excel
            Directory.CreateDirectory("Slike"); // Ako već ne postoji, napravi folder za slike
            using (var fileStream = new System.IO.FileStream("Slike/dimenzije_slika_lima.png", System.IO.FileMode.Create))
            {
                PngBitmapEncoder encoder = new PngBitmapEncoder();
                encoder.Frames.Add(BitmapFrame.Create((BitmapSource)App.MainReportInterface.SheetImageSource));
                encoder.Save(fileStream);
            }

            // Pohrani ime lima
            App.MainReportInterface.SheetName = odabirLimova.SheetName;
        }




        private void StartRegul()
        {
            App.AutoSearch.BrojVrhova = 5;
            App.AutoSearch.CompTol = 200;
            App.AutoSearch.PosTol = 200;
            App.AutoSearch.Td = 200;
            //App.AutoSearch.VidljiveTocke = App.ObjectFound.POINTS.ToList<System.Drawing.PointF>();
            App.AutoSearch.Xdim = 2058;
            App.AutoSearch.Ydim = 2448;
            App.AutoSearch.Algoritam();
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //App.PLC.WriteTag(App.PLC.CONTROL.RotacijskaOs.Jog_plus.X, true);
            //App.PLC.WriteTag(App.PLC.CONTROL.RotacijskaOs.Jog_plus.Y, true);
            reg_thread = new Thread(StartRegul);
            reg_thread.Start();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            reg_thread.Abort();
        }

        private void refresh(object sender, OutReadyEventArgs e)
        {
        

        }

        private void b_autoTeachIn_Click(object sender, RoutedEventArgs e)
        {
        
        }

        private void saveValues_Click(object sender, RoutedEventArgs e)
        {
            string tempString="";
            foreach (DimensionLine d in App.MainReportInterface.Dimensions)
            {
                tempString += d.Nazivno.ToString()+" ";
                tempString += d.DeltaPlus.ToString()+" ";
                tempString += d.DeltaMinus.ToString()+" ";
                tempString += "\n";
 
            }
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            if (saveFileDialog.ShowDialog() == true)
                File.WriteAllText(saveFileDialog.FileName, tempString);

        }

        private void loadValues_Click(object sender, RoutedEventArgs e)
        {
            string tempString = "";
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
                tempString = File.ReadAllText(openFileDialog.FileName);
            string[] tempStringArray = tempString.Split(new Char[] {'\n'});
            for (int i = 0; i < App.MainReportInterface.Dimensions.Count; i++)
            {
                try
                {
                    App.MainReportInterface.Dimensions[i].Nazivno = (float)Convert.ToDecimal(tempStringArray[i].Split(new Char[] { ' ' })[0]);
                    App.MainReportInterface.Dimensions[i].DeltaPlus = (float)Convert.ToDecimal(tempStringArray[i].Split(new Char[] { ' ' })[1]);
                    App.MainReportInterface.Dimensions[i].DeltaMinus = (float)Convert.ToDecimal(tempStringArray[i].Split(new Char[] { ' ' })[2]);
                }
                catch
                { 
                }
            }
            dataGrid1.Items.Refresh();
            App.pIzvjestaji.dataGrid1.Items.Refresh();
        }
        
    }
}
