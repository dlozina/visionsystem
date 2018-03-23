using Microsoft.Office.Interop.Excel;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Threading;
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

        private void saveValues_Click(object sender, RoutedEventArgs e)
        {
            // Spremanje rezultata
        }

        private void loadValues_Click(object sender, RoutedEventArgs e)
        {
            // Povlacenje rezultata
        }

        private void ExcelExport()
        {
            // Fetch data from JSON file
            // Load saved data from JSON file
            string DataBaseFileName = "savedata.JSON";
            string DataBasePath = Path.Combine(Environment.CurrentDirectory, @"database", DataBaseFileName);
            String JSONstring = File.ReadAllText(DataBasePath);
            database = JsonConvert.DeserializeObject<List<ReportInterface.DimensionLine>>(JSONstring);
            // If JSON is empty we have null
            if (database == null)
            {
                database = new List<ReportInterface.DimensionLine>();
            }

            // Open EXCEL app and template file
            Microsoft.Office.Interop.Excel.Application excel = new Microsoft.Office.Interop.Excel.Application();
            string TemplateFileName = "OMCO_template.xlsx";
            string TemplatePath = Path.Combine(Environment.CurrentDirectory, @"reporttemplate", TemplateFileName);
            Workbook workbook = excel.Workbooks.Open(TemplatePath, ReadOnly: false, Editable: true);
            // Select first and only sheet
            Worksheet workSheet = excel.ActiveSheet;
            //Worksheet workSheet = excel.ActiveWorkbook.Worksheets[1];

            try
            {
                // Upisivanje zaglavlja
                workSheet.Cells[3, 5] = "Workpiece NO.HAJ";        // Naziv komada
                // Upisivanje Nazivnih vrijednosti
                workSheet.Cells[12, 3] = database[0].NazivnoD2;  // Kota A
                workSheet.Cells[13, 3] = database[0].NazivnoD3;  // Kota B
                workSheet.Cells[14, 3] = database[0].NazivnoD4;  // Kota C
                workSheet.Cells[15, 3] = database[0].NazivnoD5;  // Kota D
                workSheet.Cells[16, 3] = database[0].NazivnoD1;  // Kota E
                workSheet.Cells[18, 3] = database[0].NazivnoV3;  // Kota G
                workSheet.Cells[19, 3] = database[0].NazivnoV2;  // Kota H
                workSheet.Cells[20, 3] = database[0].NazivnoV1;  // Kota I
                // Tolerance Delta +
                workSheet.Cells[12, 4] = database[0].DeltaPlusD2;  // Kota A
                workSheet.Cells[13, 4] = database[0].DeltaPlusD3;  // Kota B
                workSheet.Cells[14, 4] = database[0].DeltaPlusD4;  // Kota C
                workSheet.Cells[15, 4] = database[0].DeltaPlusD5;  // Kota D
                workSheet.Cells[16, 4] = database[0].DeltaPlusD1;  // Kota E
                workSheet.Cells[18, 4] = database[0].DeltaPlusV3;  // Kota G
                workSheet.Cells[19, 4] = database[0].DeltaPlusV2;  // Kota H
                workSheet.Cells[20, 4] = database[0].DeltaPlusV1;  // Kota I
                // Tolerance Delta -
                workSheet.Cells[12, 5] = database[0].DeltaMinusD2;  // Kota A
                workSheet.Cells[13, 5] = database[0].DeltaMinusD3;  // Kota B
                workSheet.Cells[14, 5] = database[0].DeltaMinusD4;  // Kota C
                workSheet.Cells[15, 5] = database[0].DeltaMinusD5;  // Kota D
                workSheet.Cells[16, 5] = database[0].DeltaMinusD1;  // Kota E
                workSheet.Cells[18, 5] = database[0].DeltaMinusV3;  // Kota G
                workSheet.Cells[19, 5] = database[0].DeltaMinusV2;  // Kota H
                workSheet.Cells[20, 5] = database[0].DeltaMinusV1;  // Kota I

                int j = 0;
                int filenumber = 1;
                for (int i = 0; i < database.Count; i++)
                {
                    workSheet.Cells[11, 7 + j] = database[i].String;    // String - Redni broj

                    workSheet.Cells[12, 7 + j] = database[i].MjerenoD2; // Kota A
                    if (database[i].DeltaBrushD2 == Brushes.Red)
                    {
                        workSheet.Cells[12, 7 + j].Interior.Color = ColorTranslator.ToOle(Color.Red);
                    }

                    workSheet.Cells[13, 7 + j] = database[i].MjerenoD3; // Kota B
                    if (database[i].DeltaBrushD3 == Brushes.Red)
                    {
                        workSheet.Cells[13, 7 + j].Interior.Color = ColorTranslator.ToOle(Color.Red);
                    }

                    workSheet.Cells[14, 7 + j] = database[i].MjerenoD4; // Kota C
                    if (database[i].DeltaBrushD4 == Brushes.Red)
                    {
                        workSheet.Cells[14, 7 + j].Interior.Color = ColorTranslator.ToOle(Color.Red);
                    }

                    workSheet.Cells[15, 7 + j] = database[i].MjerenoD5; // Kota D
                    if (database[i].DeltaBrushD5 == Brushes.Red)
                    {
                        workSheet.Cells[15, 7 + j].Interior.Color = ColorTranslator.ToOle(Color.Red);
                    }

                    workSheet.Cells[16, 7 + j] = database[i].MjerenoD1; // Kota E
                    if (database[i].DeltaBrushD1 == Brushes.Red)
                    {
                        workSheet.Cells[16, 7 + j].Interior.Color = ColorTranslator.ToOle(Color.Red);
                    }

                    workSheet.Cells[18, 7 + j] = database[i].MjerenoV3; // Kota G
                    if (database[i].DeltaBrushV3 == Brushes.Red)
                    {
                         workSheet.Cells[18, 7 + j].Interior.Color = ColorTranslator.ToOle(Color.Red);
                    }

                    workSheet.Cells[19, 7 + j] = database[i].MjerenoV2; // Kota H
                    if (database[i].DeltaBrushV2 == Brushes.Red)
                    {
                        workSheet.Cells[19, 7 + j].Interior.Color = ColorTranslator.ToOle(Color.Red);
                    }

                    workSheet.Cells[20, 7 + j] = database[i].MjerenoV1; // Kota I
                    if (database[i].DeltaBrushV1 == Brushes.Red)
                    {
                        workSheet.Cells[20, 7 + j].Interior.Color = ColorTranslator.ToOle(Color.Red);
                    }

                    if (database[i].Poroznost == true)
                    {
                        workSheet.Cells[23, 7 + j] = "YES";             // Poroznost
                        workSheet.Cells[23, 7 + j].Interior.Color = ColorTranslator.ToOle(Color.Red);
                    }
                    else
                    {
                        workSheet.Cells[23, 7 + j] = "NO";
                    }

                    if (j == 11)
                    {
                        string fileName2 = string.Format(@"{0}\Reports\ExcelData" + filenumber + ".xlsx", Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory));
                        workSheet.SaveAs(fileName2);
                        for (int k = 0; k < 12; k++)
                        {
                            // Nuliranje Template file-a
                            workSheet.Cells[11, 7 + k] = null; // String - Redni broj
                            workSheet.Cells[12, 7 + k] = null; // Kota A
                            workSheet.Cells[13, 7 + k] = null; // Kota B
                            workSheet.Cells[14, 7 + k] = null; // Kota C
                            workSheet.Cells[15, 7 + k] = null; // Kota D
                            workSheet.Cells[16, 7 + k] = null; // Kota E
                            workSheet.Cells[18, 7 + k] = null; // Kota G
                            workSheet.Cells[19, 7 + k] = null; // Kota H
                            workSheet.Cells[20, 7 + k] = null; // Kota I
                            workSheet.Cells[23, 7 + k] = null; // Poroznost
                        }
                        filenumber++;
                        j = -1;
                    }
                    j++;
                }
                // Define filename and save
                string fileName = string.Format(@"{0}\Reports\ExcelData" + filenumber + ".xlsx", Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory));
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


        private void BIspisPodataka_Click(object sender, RoutedEventArgs e)
        {
            Thread excelExportThread = new Thread(ExcelExport);
            excelExportThread.Name = "Thread ExcelExport";
            excelExportThread.Start();
        }
    }
}
