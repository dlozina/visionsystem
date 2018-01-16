﻿using System;
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
using System.ComponentModel;
using OxyPlot;
using OxyPlot.Wpf;
using System.IO;
using HalconDotNet;
using System.Threading;

namespace VizijskiSustavWPF
{
    /// <summary>
    /// Interaction logic for PSrh.xaml
    /// </summary>
    public partial class PSrh : Page
    {
        
        public PSrh()
        {
            InitializeComponent();
            App.PLC.Update_1_s += new PLCInterface.UpdateHandler(updatePage);
            App.PLC.Update_100_ms += new PLCInterface.UpdateHandler(updatePage_100ms);
        }

        public void PorosityHorWindow()
        {
            HTuple WindowID = hwindowPorsity.HalconID;
            App.HDevExp.RunHalcon14(WindowID);
        }

        public void PorosityVerWindow()
        {
            HTuple WindowID = hwindowPorsity.HalconID;
            App.HDevExp.RunHalcon13(WindowID);
        }

        private void TeachCAM2()
        {
            App.HDevExp.InitHalcon();
            HTuple WindowID = hwindowPorsity.HalconID;
            App.HDevExp.RunHalcon24(WindowID);
        }

        private void TeachCAM3()
        {
            App.HDevExp.InitHalcon();
            HTuple WindowID = hwindowPorsity.HalconID;
            App.HDevExp.RunHalcon25(WindowID);
        }

        private void updatePage(object sender, PLCInterfaceEventArgs e)
        {
            
        }

        private void updatePage_100ms(object sender, PLCInterfaceEventArgs e)
        {
            
        }


        private void Page_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void b_pstartKamere1_Click(object sender, RoutedEventArgs e)
        {
            App.HDevExp.Teachloop = false;
            Thread TeachCAM2Thread = new Thread(new ThreadStart(this.TeachCAM2));
            TeachCAM2Thread.Name = "TeachCAM2Thread";
            TeachCAM2Thread.Start();
        }

        private void b_pstartKamere2_Click(object sender, RoutedEventArgs e)
        {
            App.HDevExp.Teachloop = false;
            Thread TeachCAM3Thread = new Thread(new ThreadStart(this.TeachCAM3));
            TeachCAM3Thread.Name = "TeachCAM3Thread";
            TeachCAM3Thread.Start();
        }
    }
}
