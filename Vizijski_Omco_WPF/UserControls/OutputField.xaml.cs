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
    /// Interaction logic for UserControl1.xaml
    /// </summary>
    public partial class OutputField : UserControl
    {

        public static readonly DependencyProperty controlTag = DependencyProperty.Register("ControlTag", typeof(plcTag), typeof(OutputField), new PropertyMetadata());
        public plcTag ControlTag
        {
            get { return (plcTag)GetValue(controlTag); }
            set { SetValue(controlTag, value); }
        }

        public static readonly DependencyProperty pLCConnection = DependencyProperty.Register("PLCConnection", typeof(PLCInterface), typeof(OutputField), new PropertyMetadata(null, new PropertyChangedCallback(OnPLCAssign)));

        public PLCInterface PLCConnection
        {
            get { return (PLCInterface)GetValue(pLCConnection); }
            set { SetValue(pLCConnection, value); }
        } 

        public static readonly DependencyProperty text = DependencyProperty.Register("Text", typeof(string), typeof(OutputField), new PropertyMetadata());
        public string Text
        {
            get { return (string)GetValue(text); }
            set { SetValue(text, value); }
        }

        public static readonly DependencyProperty setpointX888 = DependencyProperty.Register("SetpointX888", typeof(float), typeof(OutputField), new PropertyMetadata());
        public float SetpointX888
        {
            get { return (float)GetValue(setpointX888); }
            set { SetValue(setpointX888, value); }
        }

        public OutputField()
        {
            InitializeComponent();
           
        }

        public void Connect()
        {
            PLCConnection.Update_100_ms += new PLCInterface.UpdateHandler(updatePage123);
            PLCConnection.Update_1_s += new PLCInterface.UpdateHandler(updatePage1234);
        }

        private void updatePage123(object sender, PLCInterfaceEventArgs e)
        {

            Dispatcher.BeginInvoke((Action)(() =>
            {
                try
                {
                    ControlTag.GetValueFromGroupBuffer(e.CyclicStatusBuffer);
                    SetpointX888 = (float)ControlTag.Value;
                }
                catch
                {
                }
            }
            ));
        }

        private void updatePage1234(object sender, PLCInterfaceEventArgs e)
        {


          
        }

        private static void OnPLCAssign(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            OutputField promjenaKontrola = (OutputField)d;
            promjenaKontrola.Connect();
        }
    }
}
