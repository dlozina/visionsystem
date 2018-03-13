using Microsoft.Office.Interop.Excel;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Input;
using VizijskiSustavWPF.Reports;

namespace VizijskiSustavWPF
{
    /// <summary>
    /// Interaction logic for PIzvjestaji.xaml
    /// </summary>
    public partial class PIzvjestaji
    {
        public static List<ReportInterface.DimensionLine> database = new List<ReportInterface.DimensionLine>();

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
            // Fetch data from JSON file
            // Load saved data from JSON file
            String JSONstring = File.ReadAllText(@"C:\Users\kontakt\Documents\Work\Projekti\Vision_System_OMCO\App\VisionApp\Vizijski_Omco_WPF\bin\Debug\data\savedata.JSON");
            database = JsonConvert.DeserializeObject<List<ReportInterface.DimensionLine>>(JSONstring);
            // If JSON is empty we have null
            if (database == null)
            {
                database = new List<ReportInterface.DimensionLine>();
            }

            // Open EXCEL app and template file
            Microsoft.Office.Interop.Excel.Application excel = new Microsoft.Office.Interop.Excel.Application();
            //Workbook workbook = excel.Workbooks.Open(@"C:\Users\kontakt\Documents\Work\Projekti\Vision_System_OMCO\App\VisionApp\Vizijski_Omco_WPF\bin\Debug\reporttemplate\OMCO_template.xlsx", ReadOnly: false, Editable: true);
            string TemplateFileName = "OMCO_template.xlsx";
            string path = Path.Combine(Environment.CurrentDirectory, @"reporttemplate", TemplateFileName);
            Workbook workbook = excel.Workbooks.Open(path, ReadOnly: false, Editable: true);
            // Select first and only sheet
            _Worksheet workSheet = excel.ActiveSheet;
            try
            {
                for (int i = 0; i < database.Count; i++)
                { 
                    workSheet.Cells[3, 5] = "Workpiece NO.2"; // Naziv komada
                    workSheet.Cells[11, 6 + i] = database[i].String; // String - Redni broj
                    workSheet.Cells[12, 6 + i] = database[i].MjerenoD2; // Kota A
                    workSheet.Cells[13, 6 + i] = database[i].MjerenoD3; // Kota B
                    workSheet.Cells[14, 6 + i] = database[i].MjerenoD4; // Kota C
                    workSheet.Cells[15, 6 + i] = database[i].MjerenoD5; // Kota D
                    workSheet.Cells[16, 6 + i] = database[i].MjerenoD1; // Kota E
                    workSheet.Cells[18, 6 + i] = database[i].MjerenoV3; // Kota G
                    workSheet.Cells[19, 6 + i] = database[i].MjerenoV2; // Kota H
                    workSheet.Cells[20, 6 + i] = database[i].MjerenoD1; // Kota I
                    if (database[i].Poroznost == true)
                    {
                        workSheet.Cells[23, 6 + i] = "YES"; // Poroznost
                    }
                    else
                    {
                        workSheet.Cells[23, 6 + i] = "NO";
                    } 
                }
                // Define filename
                string fileName = string.Format(@"{0}\ExcelData.xlsx", Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory));
                workSheet.SaveAs(fileName);
            }
            catch (Exception exception)
            {
                
            }
            finally
            {
                // Close Excel workbook
                workbook.Close();
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
