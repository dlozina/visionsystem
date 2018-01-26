using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace VizijskiSustavWPF
{
    /// <summary>
    /// Interaction logic for UserControl1.xaml
    /// </summary>
    public partial class BitButton : UserControl
    {
        public buttonFunction ButtonFunction { get; set; }

        public static readonly DependencyProperty controlTag = DependencyProperty.Register("ControlTag", typeof(plcTag), typeof(BitButton), new PropertyMetadata());
        public plcTag ControlTag
        {
            get { return (plcTag)GetValue(controlTag); }
            set { SetValue(controlTag, value); }
        }

        public static readonly DependencyProperty statusTag = DependencyProperty.Register("StatusTag", typeof(plcTag), typeof(BitButton), new PropertyMetadata(null, new PropertyChangedCallback(OnTagAssign)));
        public plcTag StatusTag
        {
            get { return (plcTag)GetValue(statusTag); }
            set { SetValue(statusTag, value); }
        }

        public static readonly DependencyProperty pLCConnection = DependencyProperty.Register("PLCConnection", typeof(PLCInterface), typeof(BitButton), new PropertyMetadata(null, new PropertyChangedCallback(OnPLCAssign)));

        public PLCInterface PLCConnection
        {
            get { return (PLCInterface)GetValue(pLCConnection); }
            set { SetValue(pLCConnection, value); }
        }

        public static readonly DependencyProperty text = DependencyProperty.Register("Text", typeof(string), typeof(BitButton), new PropertyMetadata());
        public string Text
        {
            get { return (string)GetValue(text); }
            set { SetValue(text, value); }
        }

        public static readonly DependencyProperty onColor = DependencyProperty.Register("OnColor", typeof(Brush), typeof(BitButton), new PropertyMetadata());
        public Brush OnColor
        {
            get { return (Brush)GetValue(onColor); }
            set { SetValue(onColor, value); }
        }

        public static readonly DependencyProperty offColor = DependencyProperty.Register("OffColor", typeof(Brush), typeof(BitButton), new PropertyMetadata());
        public Brush OffColor
        {
            get { return (Brush)GetValue(offColor); }
            set { SetValue(offColor, value); }
        }

        public BitButton()
        {
            InitializeComponent();
        }

        private void Button_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            switch (ButtonFunction)
            {
                case buttonFunction.SetBitWhileKeyPressed:
                    PLCConnection.WriteTag(ControlTag, true);
                    break;
                case buttonFunction.InvertBit:
                    PLCConnection.WriteToggle(ControlTag);
                    break;
                case buttonFunction.SetBit:
                    PLCConnection.WriteTag(ControlTag, true);
                    break;
                case buttonFunction.ResetBit:
                    PLCConnection.WriteTag(ControlTag, false);
                    break;
            }
        }

        private void Button_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
            switch (ButtonFunction)
            {
                case buttonFunction.SetBitWhileKeyPressed:
                    PLCConnection.WriteTag(ControlTag, false);
                    break;
                case buttonFunction.InvertBit:
                    break;
                case buttonFunction.SetBit:
                    break;
                case buttonFunction.ResetBit:
                    break;
            }
        }

        public void Connect()
        {

            PLCConnection.Update_100_ms += new PLCInterface.UpdateHandler(updateStatus);
        }

        private void updateStatus(object sender, PLCInterfaceEventArgs e)
        { 
            Dispatcher.BeginInvoke((Action)(() =>
            {
                    try
                {
                    StatusTag.GetValueFromGroupBuffer(e.CyclicStatusBuffer);
                    if ((bool)StatusTag.Value)
                    {
                         Grid.Background= OnColor;
                         Button.Margin = new Thickness(3);
                    }
                    else
                    {
                        Grid.Background = OffColor;
                        Button.Margin = new Thickness(1);
                    }
                }
                catch
                {
                }
            }
            ));
        }

        private static void OnPLCAssign(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            BitButton promjenaKontrola = (BitButton)d;
            //promjenaKontrola.Connect();
        }

        private static void OnTagAssign(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            BitButton promjenaKontrola = (BitButton)d;
            promjenaKontrola.Connect();
        }
    }
}
