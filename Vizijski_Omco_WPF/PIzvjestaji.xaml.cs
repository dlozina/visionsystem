using Microsoft.Office.Interop.Excel;
using System;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Input;

namespace VizijskiSustavWPF
{
    /// <summary>
    /// Interaction logic for PIzvjestaji.xaml
    /// </summary>
    public partial class PIzvjestaji
    {
        public PIzvjestaji()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void dataGrid1_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void saveValues_Click(object sender, RoutedEventArgs e)
        {
            // Spremanje rezultata
        }

        private void loadValues_Click(object sender, RoutedEventArgs e)
        {
            // Povlacenje rezultata
        }

        private void BIspisPodataka_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Office.Interop.Excel.Application excel = new Microsoft.Office.Interop.Excel.Application();
            Workbook workbook = excel.Workbooks.Open(@"C:\Users\kontakt\Documents\Work\Projekti\Vision_System_OMCO\App\VisionApp\Vizijski_Omco_WPF\bin\Debug\reporttemplate\OMCO_template.xlsx", ReadOnly: false, Editable: true);
            _Worksheet workSheet = excel.ActiveSheet;
            try
            {
                workSheet.Cells[3, "E"] = "Samo Hajduk";
                workSheet.Cells[11, "F"] = "1";
                workSheet.Cells[12, "F"] = "9";
                workSheet.Cells[13, "F"] = 1;
                workSheet.Cells[14, "F"] = 1;
                // Define filename
                string fileName = string.Format(@"{0}\ExcelData.xlsx", Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory));
                workSheet.SaveAs(fileName);
            }
            catch (Exception exception)
            {
                
            }
            finally
            {
                // Quit Excel application
                excel.Quit();
                // Release COM objects (very important!)
                if (excel != null)
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(excel);
                if (workSheet != null)
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(workSheet);
                // Empty variables
                excel = null;
                workSheet = null;
                // Force garbage collector cleaning
                GC.Collect();
            }

        }
    }
}
