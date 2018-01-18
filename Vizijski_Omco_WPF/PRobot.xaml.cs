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
    /// Interaction logic for PRobot.xaml
    /// </summary>
    public partial class PRobot : System.Windows.Controls.Page
    {
       
        public PRobot()
        {
            InitializeComponent();
            //App.PLC.Update_1_s += new PLCInterface.UpdateHandler(updatePage);
            //App.PLC.Update_100_ms += new PLCInterface.UpdateHandler(updatePage_100);

        }

        //private void updatePage(object sender, PLCInterfaceEventArgs e)
        //{
           
        //}

        //private void updatePage_100(object sender, PLCInterfaceEventArgs e)
        //{

        //}


    }
}
