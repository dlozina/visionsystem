using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

using System.ComponentModel;
using System.Collections;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;
using Microsoft.VisualBasic;
using SpreadsheetLight;
using SpreadsheetLight.Drawing;
using Snap7;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;


namespace VizijskiSustavWPF
{
    public class ReportInterface : DependencyObject
    {
        // Slika odabranog lima (za dimenzije)
        public static readonly DependencyProperty sheetImageSource = DependencyProperty.Register("SheetImageSource", typeof(ImageSource), typeof(ReportInterface), new PropertyMetadata());
        public ImageSource SheetImageSource
        {
            get { return (ImageSource)GetValue(sheetImageSource); }
            set { SetValue(sheetImageSource, value); }
        }

        // Ručno izmjerena debljina lima (laserom ili ticalom)
        public static readonly DependencyProperty manualThicknessMeas = DependencyProperty.Register("ManualThicknessMeas", typeof(float), typeof(ReportInterface), new PropertyMetadata());
        public float ManualThicknessMeas
        {
            get { return (float)GetValue(manualThicknessMeas); }
            set { SetValue(manualThicknessMeas, value); }
        }

        // Slika grafa valovitosti
        public static readonly DependencyProperty ripplePlotImage = DependencyProperty.Register("RipplePlotImage", typeof(ImageSource), typeof(ReportInterface), new PropertyMetadata());
        public ImageSource RipplePlotImage
        {
            get { return (ImageSource)GetValue(ripplePlotImage); }
            set { SetValue(ripplePlotImage, value); }
        }

        // Slika grafa srha
        public static readonly DependencyProperty burrPlotImage = DependencyProperty.Register("BurrPlotImage", typeof(ImageSource), typeof(ReportInterface), new PropertyMetadata());
        public ImageSource BurrPlotImage
        {
            get { return (ImageSource)GetValue(burrPlotImage); }
            set { SetValue(burrPlotImage, value); }
        }

        // Slika grafa sablje
        public static readonly DependencyProperty sabljaPlotImage = DependencyProperty.Register("SabljaPlotImage", typeof(ImageSource), typeof(ReportInterface), new PropertyMetadata());
        public ImageSource SabljaPlotImage
        {
            get { return (ImageSource)GetValue(sabljaPlotImage); }
            set { SetValue(sabljaPlotImage, value); }
        }

        // Korisničko ime s pPostavke
        private String korisnickoIme = "";
        public String KorisnickoIme
        {
            get { return korisnickoIme; }
            set { korisnickoIme = value; }
        }

        public const int RowOffset = 25;
        public const int CollumnOffset = 2;

        private List<DimensionLine> dimensions;
        public List<DimensionLine> Dimensions
        {
            get { return dimensions; }
            set { dimensions = value; }
        }

        private String sheetName;
        public String SheetName
        {
            get { return sheetName; }
            set { sheetName = value; }
        }


        /******************** SRH ********************/
        public static readonly DependencyProperty srh_max = DependencyProperty.Register("Srh_max", typeof(int), typeof(ReportInterface), new PropertyMetadata(0));
        public static readonly DependencyProperty srh_postotak = DependencyProperty.Register("Srh_postotak", typeof(int), typeof(ReportInterface), new PropertyMetadata(0));
        public static readonly DependencyProperty srh_pozicija = DependencyProperty.Register("Srh_pozicija", typeof(int), typeof(ReportInterface), new PropertyMetadata(0));
        public static readonly DependencyProperty srh_brojUzoraka = DependencyProperty.Register("Srh_brojUzoraka", typeof(int), typeof(ReportInterface), new PropertyMetadata(0));

        private List<BurrLine> burrList;
        public List<BurrLine> BurrList
        {
            get { return burrList; }
            set { burrList = value; }
        }
        public int Srh_max
        {
            get { return (int)GetValue(srh_max); }
            set { SetValue(srh_max, value); }
        }
        public int Srh_postotak
        {
            get { return (int)GetValue(srh_postotak); }
            set { SetValue(srh_postotak, value); }
        }
        public int Srh_pozicija
        {
            get { return (int)GetValue(srh_pozicija); }
            set { SetValue(srh_pozicija, value); }
        }

        public int Srh_brojUzoraka
        {
            get { return (int)GetValue(srh_brojUzoraka); }
            set { SetValue(srh_brojUzoraka, value); }
        }

        // Slika lima za srh
        public static readonly DependencyProperty srh_limImage = DependencyProperty.Register("Srh_limImage", typeof(ImageSource), typeof(ReportInterface), new PropertyMetadata());
        public ImageSource Srh_limImage
        {
            get { return (ImageSource)GetValue(srh_limImage); }
            set { SetValue(srh_limImage, value); }
        }
        /*********************************************/

        /***************** VALOVITOST ****************/

        public static readonly DependencyProperty valovitost_debljinaLima = DependencyProperty.Register("Valovitost_debljinaLima", typeof(float), typeof(ReportInterface),new PropertyMetadata(0.3f));
        public static readonly DependencyProperty valovitost_visinaVala = DependencyProperty.Register("Valovitost_visinaVala", typeof(float), typeof(ReportInterface), new PropertyMetadata(0.0f));
        public static readonly DependencyProperty valovitost_duzinaVala = DependencyProperty.Register("Valovitost_duzinaVala", typeof(float), typeof(ReportInterface), new PropertyMetadata(0.0f));
        public static readonly DependencyProperty valovitost_faktor = DependencyProperty.Register("Valovitost_faktor", typeof(float), typeof(ReportInterface), new PropertyMetadata(0.0f));
        public static readonly DependencyProperty valovitost_brojValova = DependencyProperty.Register("Valovitost_brojValova", typeof(float), typeof(ReportInterface), new PropertyMetadata(0.0f));
        public static readonly DependencyProperty valovitost_pozicija = DependencyProperty.Register("Valovitost_pozicija", typeof(int), typeof(ReportInterface), new PropertyMetadata(0));
        public float Valovitost_debljinaLima
        {
            get { return (float)GetValue(valovitost_debljinaLima); }
            set { SetValue(valovitost_debljinaLima, value); }
        }
        public float Valovitost_visinaVala
        {
            get { return (float)GetValue(valovitost_visinaVala); }
            set { SetValue(valovitost_visinaVala, value); }
        }
        public float Valovitost_duzinaVala
        {
            get { return (float)GetValue(valovitost_duzinaVala); }
            set { SetValue(valovitost_duzinaVala, value); }
        }
        public float Valovitost_faktor
        {
            get { return (float)GetValue(valovitost_faktor); }
            set { SetValue(valovitost_faktor, value); }
        }
        public float Valovitost_brojValova
        {
            get { return (float)GetValue(valovitost_brojValova); }
            set { SetValue(valovitost_brojValova, value); }
        }
        public int Valovitost_pozicija
        {
            get { return (int)GetValue(valovitost_pozicija); }
            set { SetValue(valovitost_pozicija, value); }
        }

        // Slika lima za valovitost
        public static readonly DependencyProperty valovitost_limImage = DependencyProperty.Register("Valovitost_limImage", typeof(ImageSource), typeof(ReportInterface), new PropertyMetadata());
        public ImageSource Valovitost_limImage
        {
            get { return (ImageSource)GetValue(valovitost_limImage); }
            set { SetValue(valovitost_limImage, value); }
        }
        /*********************************************/


        /******************* SABLJA ******************/
        // Slika lima za sablju
        public static readonly DependencyProperty sablja_limImage = DependencyProperty.Register("Sablja_limImage", typeof(ImageSource), typeof(ReportInterface), new PropertyMetadata());
        public ImageSource Sablja_limImage
        {
            get { return (ImageSource)GetValue(sablja_limImage); }
            set { SetValue(sablja_limImage, value); }
        }


        public static readonly DependencyProperty sablja_duljina = DependencyProperty.Register("Sablja_duljina", typeof(float), typeof(ReportInterface), new PropertyMetadata(0.0f));
        public static readonly DependencyProperty sablja_visina = DependencyProperty.Register("Sablja_visina", typeof(float), typeof(ReportInterface), new PropertyMetadata(0.0f));
        public static readonly DependencyProperty sablja_posto = DependencyProperty.Register("Sablja_posto", typeof(float), typeof(ReportInterface), new PropertyMetadata(0.0f));
        public static readonly DependencyProperty sablja_pozicija = DependencyProperty.Register("Sablja_pozicija", typeof(int), typeof(ReportInterface), new PropertyMetadata(0));

        public float Sablja_duljina
        {
            get { return (float)GetValue(sablja_duljina); }
            set { SetValue(sablja_duljina, value); }
        }
        public float Sablja_visina
        {
            get { return (float)GetValue(sablja_visina); }
            set { SetValue(sablja_visina, value); }
        }
        public float Sablja_posto
        {
            get { return (float)GetValue(sablja_posto); }
            set { SetValue(sablja_posto, value); }
        }
        public int Sablja_pozicija
        {
            get { return (int)GetValue(sablja_pozicija); }
            set { SetValue(sablja_pozicija, value); }
        }

        /*********************************************/

        public ReportInterface()
        {
            Dimensions = new List<DimensionLine>();
            BurrList = new List<BurrLine>();
        }
        public void ReadDimensions(string filename, string sheet, int rows)
        {
            using (SLDocument sl = new SLDocument(filename, sheet))
            {
                SLWorksheetStatistics stats = sl.GetWorksheetStatistics();
                int iStartColumnIndex = CollumnOffset;

                for (int row = RowOffset; (row <= (RowOffset + rows)); ++row)
                {
                    Dimensions.Add(new DimensionLine
                    {
                        Kote = sl.GetCellValueAsString(row, iStartColumnIndex),
                        DeltaPlus = sl.GetCellValueAsInt32(row, iStartColumnIndex + 7),
                        DeltaMinus = sl.GetCellValueAsInt32(row, iStartColumnIndex + 9),
                        Nazivno = sl.GetCellValueAsInt32(row, iStartColumnIndex + 1),
                        Mjereno = sl.GetCellValueAsInt32(row, iStartColumnIndex + 4),

                        //  Delta = sl.GetCellValueAsInt32(row, iStartColumnIndex + 10)
                    });
                }
            }
        }


        public void WriteControlSheetReport(string filename, string sheet, int rows)
        {
            //if (App.pDimenzije.odabirLimova.SheetName == null)
            //{
            //    App.pIzvjestaji.b_generateReport1.Foreground = new SolidColorBrush(System.Windows.Media.Colors.Red);
            //    return;
            //}

            using (SLDocument sl = new SLDocument(filename, sheet))
            {
                //SLWorksheetStatistics stats = sl.GetWorksheetStatistics();
                int iStartColumnIndex = CollumnOffset;

                SLStyle style1 = sl.CreateStyle();
                style1.Fill.SetPattern(PatternValues.Solid, System.Drawing.Color.Red, System.Drawing.Color.Red);

                SLStyle style2 = sl.CreateStyle();
                style2.Fill.SetPattern(PatternValues.Solid, System.Drawing.Color.LightGreen, System.Drawing.Color.Green);

                // Kontrolni list broj
                int test_report_no;
                try
                {
                    String readText = File.ReadAllText("Kontrolni_list_br.txt");
                    if (int.TryParse(readText, out test_report_no))
                    {
                        test_report_no++;
                        File.WriteAllText("Kontrolni_list_br.txt", test_report_no.ToString());
                    }
                }
                catch(FileNotFoundException)
                {
                    // Stvori novi file
                    File.WriteAllText("Kontrolni_list_br.txt", "1");
                    test_report_no = 1;
                }
                sl.SetCellValue(3, 5, test_report_no);

                // Zaglavlje
                sl.SetCellValue(6, 4, App.pPostavke.tbTipTransformatora.Text);
                sl.SetCellValue(7, 4, App.pPostavke.tbObjekt.Text);
                sl.SetCellValue(8, 4, App.pPostavke.tbTvornickiBroj.Text);
                sl.SetCellValue(6, 9, App.pPostavke.tbVrstaLima.Text);
                /*sl.SetCellValue(7, 9, App.pDimenzije.odabirLimova.SheetName);*/ // Oblik lima (odabran u dimenzijama)
                sl.SetCellValue(8, 9, App.pPostavke.tbPaketBroj.Text);
                sl.SetCellValue(7, 12, App.pPostavke.tbBrojKoluta.Text);
                sl.SetCellValue(9, 12, App.pPostavke.tbCrtezBroj.Text);

                // Slika lima
                SLPicture pic = new SLPicture("Slike/dimenzije_slika_lima.png");
                pic.SetPosition(10,2);
                sl.InsertPicture(pic);

                // Datum
                String dat = String.Format("{0:D}.{1:D}.{2:D4}.", DateAndTime.Now.Day, DateAndTime.Now.Month, DateAndTime.Now.Year);
                sl.SetCellValue(48, 2, dat);

                // Operater
                sl.SetCellValue(48, 5, KorisnickoIme);

                // Širina B
                int dimensions_index = 0;
                if (Dimensions[0].Kote[0] == 'B')
                {
                    float calculatedDelta = Dimensions[0].Mjereno - Dimensions[0].Nazivno;
                    sl.SetCellValue(41, iStartColumnIndex, Dimensions[0].Kote); // ŠIRINA B WIDTH B
                    sl.SetCellValue(41, iStartColumnIndex + 7, Dimensions[0].DeltaPlus);
                    sl.SetCellValue(41, iStartColumnIndex + 9, Dimensions[0].DeltaMinus);
                    sl.SetCellValue(41, iStartColumnIndex + 1, Dimensions[0].Nazivno);
                    sl.SetCellValue(41, iStartColumnIndex + 4, Dimensions[0].Mjereno);
                    sl.SetCellValue(41, iStartColumnIndex + 10, calculatedDelta);
                    if (calculatedDelta > Dimensions[0].DeltaPlus || calculatedDelta < Dimensions[0].DeltaMinus)
                    {
                        sl.SetCellStyle(41, iStartColumnIndex + 10, style1);
                    }
                    else
                    {
                        sl.SetCellStyle(41, iStartColumnIndex + 10, style2);
                    }
                    dimensions_index = 1;
                }

                // Ostale dimenzije
                for (int row = RowOffset; (row < (RowOffset + rows - dimensions_index)); ++row)
                {
                    int ind = row - RowOffset + dimensions_index;
                    float calculatedDelta = Dimensions[ind].Mjereno - Dimensions[ind].Nazivno;
                    sl.SetCellValue(row, iStartColumnIndex, Dimensions[ind].Kote);
                    sl.SetCellValue(row, iStartColumnIndex + 7, Dimensions[ind].DeltaPlus);
                    sl.SetCellValue(row, iStartColumnIndex + 9, Dimensions[ind].DeltaMinus);
                    sl.SetCellValue(row, iStartColumnIndex + 1, Dimensions[ind].Nazivno);
                    sl.SetCellValue(row, iStartColumnIndex + 4, Dimensions[ind].Mjereno);
                    sl.SetCellValue(row, iStartColumnIndex + 10, calculatedDelta);
                    if (calculatedDelta > Dimensions[ind].DeltaPlus || calculatedDelta < Dimensions[ind].DeltaMinus)
                    {
                        sl.SetCellStyle(row, iStartColumnIndex + 10, style1);
                    }
                    else
                    {
                        sl.SetCellStyle(row, iStartColumnIndex + 10, style2);
                    }
                }

                // Prazni retci
                for (int row = RowOffset + rows - dimensions_index; row < 41; row++)
                {
                    sl.SetCellValue(row, iStartColumnIndex, "");
                }

                // Debljina lima
                sl.SetCellValue(42, 6, ManualThicknessMeas);

                const float tolerancija = 0.03f;
                sl.SetCellValue(42, 9, tolerancija); // DeltaPlus
                sl.SetCellValue(42, 11, tolerancija); // DeltaMinus

                float delta;
                if (Single.TryParse(App.pPostavke.tbNazivnaDebljina.Text.Replace(".", ","), out float nazivnaDebljina))
                {
                    sl.SetCellValue(42, 3, nazivnaDebljina);
                    delta = ManualThicknessMeas - nazivnaDebljina;
                    sl.SetCellValue(42, 12, delta);
                    if (delta > tolerancija || delta < -tolerancija)
                    {
                        sl.SetCellStyle(42, 12, style1);
                    }
                    else
                    {
                        sl.SetCellStyle(42, 12, style2);
                    }
                }

                // Srh (poprečni?)
                float srh = ((float)Srh_max)/1000.0f;
                sl.SetCellValue(43, 6, srh);

                const float tolerancijaSrh = 0.02f;
                sl.SetCellValue(43, 9, tolerancijaSrh);
                sl.SetCellValue(43, 11, tolerancijaSrh);

                float diff = srh - tolerancijaSrh;
                if (diff < 0) diff = 0;
                sl.SetCellValue(43, 12, diff);
                if (diff > 0)
                {
                    sl.SetCellStyle(43, 12, style1);
                }
                else
                {
                    sl.SetCellStyle(43, 12, style2);
                }


                //String ime_lima = App.pDimenzije.odabirLimova.SheetName.Replace("\n", " ");
                String datum = String.Format("{0:D4}-{1:D2}-{2:D2}", DateAndTime.Now.Year, DateAndTime.Now.Month, DateAndTime.Now.Day);
                String vrijeme = String.Format("{0:D2}-{1:D2}-{2:D2}", DateAndTime.Now.Hour, DateAndTime.Now.Minute, DateAndTime.Now.Second);

                if (!Directory.Exists("Izvjestaji/Kontrolni"))
                {
                    Directory.CreateDirectory("Izvjestaji/Kontrolni");
                }
                
                var objectname = string.Join("_", App.pPostavke.tbObjekt.Text.Split(System.IO.Path.GetInvalidFileNameChars()));
                if (objectname.Length!=0)
                {
                    objectname = "/" + objectname;
                }
               
                if (!Directory.Exists("Izvjestaji/Kontrolni" + objectname))
                {
                    Directory.CreateDirectory("Izvjestaji/Kontrolni" + objectname);
                }

                //sl.SaveAs("Izvjestaji/Kontrolni" + objectname + "/" + test_report_no.ToString() + "_" + ime_lima + "_" + datum + "-" + vrijeme + ".xlsx");

                // Success
                App.pIzvjestaji.b_generateReport1.Foreground = new SolidColorBrush(System.Windows.Media.Colors.Green);
            }
        }

        public void WriteReclamationReport(string filename)
        {
            if (App.MainReportInterface.SheetName == null)
            {
                App.pIzvjestaji.b_generateReport2.Foreground = new SolidColorBrush(System.Windows.Media.Colors.Red);
                return;
            }

            int test_report_no = -1;
            
            //using (SLDocument sl = new SLDocument(filename, "SRH"))
            using (SLDocument sl = new SLDocument(filename))
            {
                // Crveni
                SLStyle style1 = sl.CreateStyle();
                style1.Fill.SetPattern(PatternValues.Solid, System.Drawing.Color.Red, System.Drawing.Color.Red);

                // Zeleni
                SLStyle style2 = sl.CreateStyle();
                style2.Fill.SetPattern(PatternValues.Solid, System.Drawing.Color.LightGreen, System.Drawing.Color.Green);


                // ******************* Sheet za SRH *******************
                sl.SelectWorksheet("SRH");

                // Kontrolni list broj
                try
                {
                    String readText = File.ReadAllText("Kontrolni_list_br.txt");
                    if (int.TryParse(readText, out test_report_no))
                    {
                        test_report_no++;
                        File.WriteAllText("Kontrolni_list_br.txt", test_report_no.ToString());
                    }
                }
                catch (FileNotFoundException)
                {
                    // Stvori novi file
                    File.WriteAllText("Kontrolni_list_br.txt", "1");
                    test_report_no = 1;
                }
                sl.SetCellValue(3, 5, test_report_no);

                // Zaglavlje
                sl.SetCellValue(7, 4, App.pPostavke.tbProizvodac.Text);
                sl.SetCellValue(8, 4, App.pPostavke.tbTvornickiBroj.Text);
                sl.SetCellValue(6, 9, App.pPostavke.tbVrstaLima.Text);
                sl.SetCellValue(7, 9, App.pPostavke.tbSirina.Text);
                sl.SetCellValue(8, 9, App.pPostavke.tbBrojKoluta.Text);

                // Slika lima
                SLPicture pic = new SLPicture("Slike/srh_slika_lima.png");
                pic.ResizeInPercentage(80, 80);
                //pic.ResizeInPixels();
                //pic.SetPosition(11, 1);
                pic.SetPosition(62, 370);
                sl.InsertPicture(pic);

                // Slika grafa srha
                pic = new SLPicture("Slike/srh_graf.png");
                pic.ResizeInPixels(343, 260);
                pic.SetPosition(9, 8);
                sl.InsertPicture(pic);

                // Max. srh
                sl.SetCellValue(25, 6, ((float)Srh_max));

                int diff = Srh_max - 15;
                if (diff < 0) diff = 0;
                sl.SetCellValue(25, 12, ((float)diff));
                if (diff > 0)
                {
                    sl.SetCellStyle(25, 12, style1);
                }
                else
                {
                    sl.SetCellStyle(25, 12, style2);
                }

                // Srh postotak
                sl.SetCellValue(26, 6, Srh_postotak);
                if (Srh_postotak > 30)
                {
                    sl.SetCellStyle(26, 6, style1);
                }
                else
                {
                    sl.SetCellStyle(26, 6, style2);
                }

                // Datum
                String dat = String.Format("{0:D}.{1:D}.{2:D4}.", DateAndTime.Now.Day, DateAndTime.Now.Month, DateAndTime.Now.Year);
                sl.SetCellValue(31, 2, dat);

                // Operater
                sl.SetCellValue(31, 5, KorisnickoIme);


                // **************** Sheet za VALOVITOST ***************
                sl.SelectWorksheet("VALOVITOST");

                // Kontrolni list broj
                sl.SetCellValue(3, 5, test_report_no);

                // Zaglavlje
                sl.SetCellValue(7, 4, App.pPostavke.tbProizvodac.Text);
                sl.SetCellValue(8, 4, App.pPostavke.tbTvornickiBroj.Text);
                sl.SetCellValue(6, 9, App.pPostavke.tbVrstaLima.Text);
                sl.SetCellValue(7, 9, App.pPostavke.tbSirina.Text);
                sl.SetCellValue(8, 9, App.pPostavke.tbBrojKoluta.Text);

                // Slika lima za valovitost
                pic = new SLPicture("Slike/valovitost_slika_lima.png");
                pic.ResizeInPercentage(80, 80);
                //pic.ResizeInPixels();
                //pic.SetPosition(11, 1);
                pic.SetPosition(62, 370);
                sl.InsertPicture(pic);

                // Slika grafa valovitosti
                pic = new SLPicture("Slike/valovitost_graf.png");
                pic.ResizeInPixels(343, 260);
                pic.SetPosition(9, 8);
                sl.InsertPicture(pic);

                // Dužina vala
                sl.SetCellValue(25, 6, Valovitost_duzinaVala);

                // Visina vala
                sl.SetCellValue(26, 6, Valovitost_visinaVala);

                // Faktor
                sl.SetCellValue(27, 6, Valovitost_faktor);
                float delta = Valovitost_faktor - 1.5f;
                if (delta < 0) delta = 0;
                sl.SetCellValue(27, 12, delta);
                if (delta > 0)
                {
                    sl.SetCellStyle(27, 12, style1);
                }
                else
                {
                    sl.SetCellStyle(27, 12, style2);
                }

                // Broj valova na 1 m
                sl.SetCellValue(28, 6, Valovitost_brojValova/(Valovitost_duzinaVala/1000.0f));

                // Datum
                sl.SetCellValue(33, 2, dat);

                // Operater
                sl.SetCellValue(33, 5, KorisnickoIme);

                // ******************* Sheet za SABLJU *******************
                sl.SelectWorksheet("SABLJA");

                // Kontrolni list broj
                sl.SetCellValue(3, 5, test_report_no);

                // Zaglavlje
                sl.SetCellValue(7, 4, App.pPostavke.tbProizvodac.Text);
                sl.SetCellValue(8, 4, App.pPostavke.tbTvornickiBroj.Text);
                sl.SetCellValue(6, 9, App.pPostavke.tbVrstaLima.Text);
                sl.SetCellValue(7, 9, App.pPostavke.tbSirina.Text);
                sl.SetCellValue(8, 9, App.pPostavke.tbBrojKoluta.Text);

                // Slika lima
                pic = new SLPicture("Slike/sablja_slika_lima.png");
                pic.ResizeInPercentage(80, 80);
                //pic.ResizeInPixels();
                //pic.SetPosition(11, 1);
                pic.SetPosition(62, 370);
                sl.InsertPicture(pic);

                // Slika grafa sablje
                pic = new SLPicture("Slike/sablja_graf.png");
                pic.ResizeInPixels(343, 260);
                pic.SetPosition(9, 8);
                sl.InsertPicture(pic);

                // Duljina sablje
                sl.SetCellValue(25, 6, Sablja_duljina);

                // Visina sablje
                sl.SetCellValue(26, 6, Sablja_visina);

                // Sablja postotak
                sl.SetCellValue(27, 6, Sablja_posto);
                delta = Sablja_posto - 0.033f;
                if (delta < 0) delta = 0;
                sl.SetCellValue(27, 12, delta);
                if (delta > 0)
                {
                    sl.SetCellStyle(27, 12, style1);
                }
                else
                {
                    sl.SetCellStyle(27, 12, style2);
                }

                // Datum
                sl.SetCellValue(32, 2, dat);

                // Operater
                sl.SetCellValue(32, 5, KorisnickoIme);

                // ******************* Sheet za ŠIRINA; DEBLJINA *******************
                sl.SelectWorksheet("ŠIRINA; DEBLJINA");

                // Kontrolni list broj
                sl.SetCellValue(3, 5, test_report_no);

                // Zaglavlje
                sl.SetCellValue(7, 4, App.pPostavke.tbProizvodac.Text);
                sl.SetCellValue(8, 4, App.pPostavke.tbTvornickiBroj.Text);
                sl.SetCellValue(6, 9, App.pPostavke.tbVrstaLima.Text);
                sl.SetCellValue(7, 9, App.pPostavke.tbSirina.Text);
                sl.SetCellValue(8, 9, App.pPostavke.tbBrojKoluta.Text);

                // Širina lima (mjerenje dimenzija na test uzorku)
                if (SheetName == "TU1")
                {
                    float calculatedDelta = Dimensions[0].Mjereno - Dimensions[0].Nazivno;
                    sl.SetCellValue(25, 3, Dimensions[0].Nazivno);
                    sl.SetCellValue(25, 6, Dimensions[0].Mjereno);
                    sl.SetCellValue(25, 9, Dimensions[0].DeltaPlus);
                    sl.SetCellValue(25, 11, Dimensions[0].DeltaMinus);
                    sl.SetCellValue(25, 12, calculatedDelta);
                    if (calculatedDelta > Dimensions[0].DeltaPlus || calculatedDelta < Dimensions[0].DeltaMinus)
                    {
                        sl.SetCellStyle(25, 12, style1);
                    }
                    else
                    {
                        sl.SetCellStyle(25, 12, style2);
                    }
                }

                // Debljina lima (ručno mjerenje laserom ili ticalom)
                sl.SetCellValue(26, 6, ManualThicknessMeas);

                const float tolerancija = 0.03f;
                sl.SetCellValue(26, 9, tolerancija); // DeltaPlus
                sl.SetCellValue(26, 11, tolerancija); // DeltaMinus

                if (Single.TryParse(App.pPostavke.tbNazivnaDebljina.Text.Replace(".", ","), out float nazivnaDebljina))
                {
                    sl.SetCellValue(26, 3, nazivnaDebljina);
                    delta = ManualThicknessMeas - nazivnaDebljina;
                    sl.SetCellValue(26, 12, delta);
                    if (delta > tolerancija || delta < -tolerancija)
                    {
                        sl.SetCellStyle(26, 12, style1);
                    }
                    else
                    {
                        sl.SetCellStyle(26, 12, style2);
                    }
                }

                // Datum
                sl.SetCellValue(31, 2, dat);

                // Operater
                sl.SetCellValue(31, 5, KorisnickoIme);

                // ******************* Ime Excel datoteke *******************
                String ime_lima = App.MainReportInterface.SheetName.Replace("\n", " ");
                String datum = String.Format("{0:D4}-{1:D2}-{2:D2}", DateAndTime.Now.Year, DateAndTime.Now.Month, DateAndTime.Now.Day);
                String vrijeme = String.Format("{0:D2}-{1:D2}-{2:D2}", DateAndTime.Now.Hour, DateAndTime.Now.Minute, DateAndTime.Now.Second);

                Directory.CreateDirectory("Izvjestaji/Reklamacijski"); // Stvara novi folder samo ako već ne postoji
                sl.SaveAs("Izvjestaji/Reklamacijski/" + test_report_no.ToString() + "_" + ime_lima + "_" + datum + "-" + vrijeme + ".xlsx"); // ili xls?

                // Success
                App.pIzvjestaji.b_generateReport2.Foreground = new SolidColorBrush(System.Windows.Media.Colors.Green);

            }//using

        }

    }

    public class DimensionLine : INotifyPropertyChanged
    {
        public string Kote { get; set; }
        
        private float nazivno;
        public float Nazivno
        {
            get { return nazivno; }
            set
            {
                if (nazivno != value)
                {
                    nazivno = value;
                    Delta = Mjereno - nazivno;
                }
            }
        }
        private float mjereno;
        public float Mjereno
        {
            get { return mjereno; }
            set
            {
                if (mjereno != value)
                {
                    mjereno = value;
                    Delta = - Nazivno + mjereno;
                }
            }
        }

        private float deltaPlus;
        public float DeltaPlus 
        {
            get { return deltaPlus; }
            set
            {
                if (deltaPlus != value)
                {
                    deltaPlus = value;
                    Delta = float.NaN;
                    Delta = -nazivno + mjereno;
                }
            }
        }

        private float deltaMinus;
        public float DeltaMinus 
        {
            get { return deltaMinus; }
            set
            {
                if (deltaMinus != value)
                {
                    deltaMinus = value;
                    Delta = float.NaN;
                    Delta = -nazivno + mjereno;
                }
            }
        }

        private float delta;

        public event PropertyChangedEventHandler PropertyChanged;
        public float Delta
        {
            get { return delta; }
            set
            {
                if (delta != value)
                {
                    delta = value;
                    OnPropertyChanged("Delta");
                    OnPropertyChanged("Nazivno");
                    OnPropertyChanged("Mjereno");
                    OnPropertyChanged("DeltaPlus");
                    OnPropertyChanged("DeltaMinus");
                    
                    OnPropertyChanged("DeltaBrush");
                }
            }
        }

        public Brush DeltaBrush
        {
            get
            {
                if ((Delta > DeltaPlus) || (Delta < DeltaMinus))
                    return Brushes.Red;
                else
                    return Brushes.LightGreen;
            }
        }
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public DimensionLine()
        {
        }
        public DimensionLine(string _kote, float _nazivno, float _mjereno, float _deltaPlus, float _deltaMinus, float _delta)
        {
            Kote = _kote;
            Nazivno = _nazivno;
            Mjereno = _mjereno;
            DeltaPlus = _deltaPlus;
            DeltaMinus = _deltaMinus;
            Delta = _delta;
        }
    }

    public class BurrLine : INotifyPropertyChanged
    {
        public int RedniBroj { get; set; }
        public int BrojPozicije { get; set; }
        
        public event PropertyChangedEventHandler PropertyChanged;
        
        private int srh;
        public int Srh
        {
            get { return srh; }
            set
            {
                if (srh != value)
                {
                    srh = value;
                    OnPropertyChanged("Srh");
                    OnPropertyChanged("SrhBrush");
                }
            }
        }
        public Brush SrhBrush
        {
            get
            {
                if (Srh > 20) 
                    return Brushes.Red;
                else
                    return Brushes.LightGreen;
            }
        }
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public BurrLine()
        {
        }
        public BurrLine(int _redniBroj, int _brojPozicije, int _srh)
        {
            RedniBroj = _redniBroj;
            BrojPozicije = _brojPozicije;
            Srh = _srh;
        }
    }
}