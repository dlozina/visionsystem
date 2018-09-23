using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace VizijskiSustavWPF.Reports
{
    public class ReportInterface
    {
        //public ReportInterface()
        //{
        //    Dimensions = new List<DimensionLine>();
        //}

        //private List<DimensionLine> dimensions;
        //public List<DimensionLine> Dimensions
        //{
        //    get { return dimensions; }
        //    set { dimensions = value; }
        //}

        public class DimensionLine : INotifyPropertyChanged
        {
            public event PropertyChangedEventHandler PropertyChanged;

            public string String { get; set; }

            public bool Poroznost { get; set; }

            // Dimenzija D1
            private float nazivnoD1;
            public float NazivnoD1
            {
                get { return nazivnoD1; }
                set
                {
                    if (nazivnoD1 != value)
                    {
                        nazivnoD1 = value;
                        DeltaD1 = MjerenoD1 - nazivnoD1;
                    }
                }
            }

            private float mjerenoD1;
            public float MjerenoD1
            {
                get { return mjerenoD1; }
                set
                {
                    if (mjerenoD1 != value)
                    {
                        mjerenoD1 = value;
                        DeltaD1 = -NazivnoD1 + mjerenoD1;
                    }
                }
            }

            private float deltaPlusD1;
            public float DeltaPlusD1
            {
                get { return deltaPlusD1; }
                set
                {
                    if (deltaPlusD1 != value)
                    {
                        deltaPlusD1 = value;
                        DeltaD1 = float.NaN;
                        DeltaD1 = -nazivnoD1 + mjerenoD1;
                    }
                }
            }

            private float deltaMinusD1;
            public float DeltaMinusD1
            {
                get { return deltaMinusD1; }
                set
                {
                    if (deltaMinusD1 != value)
                    {
                        deltaMinusD1 = value;
                        DeltaD1 = float.NaN;
                        DeltaD1 = -nazivnoD1 + mjerenoD1;
                    }
                }
            }

            private float deltaD1;
            public float DeltaD1
            {
                get { return deltaD1; }
                set
                {
                    if (deltaD1 != value)
                    {
                        deltaD1 = value;
                        OnPropertyChanged("DeltaD1");
                        OnPropertyChanged("NazivnoD1");
                        OnPropertyChanged("MjerenoD1");
                        OnPropertyChanged("DeltaPlusD1");
                        OnPropertyChanged("DeltaMinusD1");
                        OnPropertyChanged("DeltaBrushD1");
                    }
                }
            }

            public Brush DeltaBrushD1
            {
                get
                {
                    if ((DeltaD1 > DeltaPlusD1) || (DeltaD1 < DeltaMinusD1))
                        return Brushes.Red;
                    else
                        return Brushes.LightGreen;
                }
            }

            // Dimenzija D2
            private float nazivnoD2;
            public float NazivnoD2
            {
                get { return nazivnoD2; }
                set
                {
                    if (nazivnoD2 != value)
                    {
                        nazivnoD2 = value;
                        DeltaD2 = MjerenoD2 - nazivnoD2;
                    }
                }
            }

            private float mjerenoD2;
            public float MjerenoD2
            {
                get { return mjerenoD2; }
                set
                {
                    if (mjerenoD2 != value)
                    {
                        mjerenoD2 = value;
                        DeltaD2 = -NazivnoD2 + mjerenoD2;
                    }
                }
            }

            private float deltaPlusD2;
            public float DeltaPlusD2
            {
                get { return deltaPlusD2; }
                set
                {
                    if (deltaPlusD2 != value)
                    {
                        deltaPlusD2 = value;
                        DeltaD2 = float.NaN;
                        DeltaD2 = -nazivnoD2 + mjerenoD2;
                    }
                }
            }

            private float deltaMinusD2;
            public float DeltaMinusD2
            {
                get { return deltaMinusD2; }
                set
                {
                    if (deltaMinusD2 != value)
                    {
                        deltaMinusD2 = value;
                        DeltaD2 = float.NaN;
                        DeltaD2 = -nazivnoD2 + mjerenoD2;
                    }
                }
            }

            private float deltaD2;
            public float DeltaD2
            {
                get { return deltaD2; }
                set
                {
                    if (deltaD2 != value)
                    {
                        deltaD2 = value;
                        OnPropertyChanged("DeltaD2");
                        OnPropertyChanged("NazivnoD2");
                        OnPropertyChanged("MjerenoD2");
                        OnPropertyChanged("DeltaPlusD2");
                        OnPropertyChanged("DeltaMinusD2");
                        OnPropertyChanged("DeltaBrushD2");
                    }
                }
            }

            public Brush DeltaBrushD2
            {
                get
                {
                    if ((DeltaD2 > DeltaPlusD2) || (DeltaD2 < DeltaMinusD2))
                        return Brushes.Red;
                    else
                        return Brushes.LightGreen;
                }
            }

            // Dimenzija D3
            private float nazivnoD3;
            public float NazivnoD3
            {
                get { return nazivnoD3; }
                set
                {
                    if (nazivnoD3 != value)
                    {
                        nazivnoD3 = value;
                        DeltaD3 = MjerenoD3 - nazivnoD3;
                    }
                }
            }

            private float mjerenoD3;
            public float MjerenoD3
            {
                get { return mjerenoD3; }
                set
                {
                    if (mjerenoD3 != value)
                    {
                        mjerenoD3 = value;
                        DeltaD3 = -NazivnoD3 + mjerenoD3;
                    }
                }
            }

            private float deltaPlusD3;
            public float DeltaPlusD3
            {
                get { return deltaPlusD3; }
                set
                {
                    if (deltaPlusD3 != value)
                    {
                        deltaPlusD3 = value;
                        DeltaD3 = float.NaN;
                        DeltaD3 = -nazivnoD3 + mjerenoD3;
                    }
                }
            }

            private float deltaMinusD3;
            public float DeltaMinusD3
            {
                get { return deltaMinusD3; }
                set
                {
                    if (deltaMinusD3 != value)
                    {
                        deltaMinusD3 = value;
                        DeltaD3 = float.NaN;
                        DeltaD3 = -nazivnoD3 + mjerenoD3;
                    }
                }
            }

            private float deltaD3;
            public float DeltaD3
            {
                get { return deltaD3; }
                set
                {
                    if (deltaD3 != value)
                    {
                        deltaD3 = value;
                        OnPropertyChanged("DeltaD3");
                        OnPropertyChanged("NazivnoD3");
                        OnPropertyChanged("MjerenoD3");
                        OnPropertyChanged("DeltaPlusD3");
                        OnPropertyChanged("DeltaMinusD3");
                        OnPropertyChanged("DeltaBrushD3");
                    }
                }
            }

            public Brush DeltaBrushD3
            {
                get
                {
                    if ((DeltaD3 > DeltaPlusD3) || (DeltaD3 < DeltaMinusD3))
                        return Brushes.Red;
                    else
                        return Brushes.LightGreen;
                }
            }

            // Dimenzija D4
            private float nazivnoD4;
            public float NazivnoD4
            {
                get { return nazivnoD4; }
                set
                {
                    if (nazivnoD4 != value)
                    {
                        nazivnoD4 = value;
                        DeltaD4 = MjerenoD4 - nazivnoD4;
                    }
                }
            }

            private float mjerenoD4;
            public float MjerenoD4
            {
                get { return mjerenoD4; }
                set
                {
                    if (mjerenoD4 != value)
                    {
                        mjerenoD4 = value;
                        DeltaD4 = -NazivnoD4 + mjerenoD4;
                    }
                }
            }

            private float deltaPlusD4;
            public float DeltaPlusD4
            {
                get { return deltaPlusD4; }
                set
                {
                    if (deltaPlusD4 != value)
                    {
                        deltaPlusD4 = value;
                        DeltaD4 = float.NaN;
                        DeltaD4 = -nazivnoD4 + mjerenoD4;
                    }
                }
            }

            private float deltaMinusD4;
            public float DeltaMinusD4
            {
                get { return deltaMinusD4; }
                set
                {
                    if (deltaMinusD4 != value)
                    {
                        deltaMinusD4 = value;
                        DeltaD4 = float.NaN;
                        DeltaD4 = -nazivnoD4 + mjerenoD4;
                    }
                }
            }

            private float deltaD4;
            public float DeltaD4
            {
                get { return deltaD4; }
                set
                {
                    if (deltaD4 != value)
                    {
                        deltaD4 = value;
                        OnPropertyChanged("DeltaD4");
                        OnPropertyChanged("NazivnoD4");
                        OnPropertyChanged("MjerenoD4");
                        OnPropertyChanged("DeltaPlusD4");
                        OnPropertyChanged("DeltaMinusD4");
                        OnPropertyChanged("DeltaBrushD4");
                    }
                }
            }

            public Brush DeltaBrushD4
            {
                get
                {
                    if ((DeltaD4 > DeltaPlusD4) || (DeltaD4 < DeltaMinusD4))
                        return Brushes.Red;
                    else
                        return Brushes.LightGreen;
                }
            }

            // Dimenzija D5
            private float nazivnoD5;
            public float NazivnoD5
            {
                get { return nazivnoD5; }
                set
                {
                    if (nazivnoD5 != value)
                    {
                        nazivnoD5 = value;
                        DeltaD5 = MjerenoD5 - nazivnoD5;
                    }
                }
            }

            private float mjerenoD5;
            public float MjerenoD5
            {
                get { return mjerenoD5; }
                set
                {
                    if (mjerenoD5 != value)
                    {
                        mjerenoD5 = value;
                        DeltaD5 = -NazivnoD5 + mjerenoD5;
                    }
                }
            }

            private float deltaPlusD5;
            public float DeltaPlusD5
            {
                get { return deltaPlusD5; }
                set
                {
                    if (deltaPlusD5 != value)
                    {
                        deltaPlusD5 = value;
                        DeltaD5 = float.NaN;
                        DeltaD5 = -nazivnoD2 + mjerenoD5;
                    }
                }
            }

            private float deltaMinusD5;
            public float DeltaMinusD5
            {
                get { return deltaMinusD5; }
                set
                {
                    if (deltaMinusD5 != value)
                    {
                        deltaMinusD5 = value;
                        DeltaD5 = float.NaN;
                        DeltaD5 = -nazivnoD5 + mjerenoD5;
                    }
                }
            }

            private float deltaD5;
            public float DeltaD5
            {
                get { return deltaD5; }
                set
                {
                    if (deltaD5 != value)
                    {
                        deltaD5 = value;
                        OnPropertyChanged("DeltaD5");
                        OnPropertyChanged("NazivnoD5");
                        OnPropertyChanged("MjerenoD5");
                        OnPropertyChanged("DeltaPlusD5");
                        OnPropertyChanged("DeltaMinusD5");
                        OnPropertyChanged("DeltaBrushD5");
                    }
                }
            }

            public Brush DeltaBrushD5
            {
                get
                {
                    if ((DeltaD2 > DeltaPlusD2) || (DeltaD2 < DeltaMinusD2))
                        return Brushes.Red;
                    else
                        return Brushes.LightGreen;
                }
            }

            // Dimenzija V1
            private float nazivnoV1;
            public float NazivnoV1
            {
                get { return nazivnoV1; }
                set
                {
                    if (nazivnoV1 != value)
                    {
                        nazivnoV1 = value;
                        DeltaV1 = MjerenoV1 - nazivnoV1;
                    }
                }
            }

            private float mjerenoV1;
            public float MjerenoV1
            {
                get { return mjerenoV1; }
                set
                {
                    if (mjerenoV1 != value)
                    {
                        mjerenoV1 = value;
                        DeltaV1 = -NazivnoV1 + mjerenoV1;
                    }
                }
            }

            private float deltaPlusV1;
            public float DeltaPlusV1
            {
                get { return deltaPlusV1; }
                set
                {
                    if (deltaPlusV1 != value)
                    {
                        deltaPlusV1 = value;
                        DeltaV1 = float.NaN;
                        DeltaV1 = -nazivnoV1 + mjerenoV1;
                    }
                }
            }

            private float deltaMinusV1;
            public float DeltaMinusV1
            {
                get { return deltaMinusV1; }
                set
                {
                    if (deltaMinusV1 != value)
                    {
                        deltaMinusV1 = value;
                        DeltaV1 = float.NaN;
                        DeltaV1 = -nazivnoV1 + mjerenoV1;
                    }
                }
            }

            private float deltaV1;
            public float DeltaV1
            {
                get { return deltaV1; }
                set
                {
                    if (deltaV1 != value)
                    {
                        deltaV1 = value;
                        OnPropertyChanged("DeltaV1");
                        OnPropertyChanged("NazivnoV1");
                        OnPropertyChanged("MjerenoV1");
                        OnPropertyChanged("DeltaPlusV1");
                        OnPropertyChanged("DeltaMinusV1");
                        OnPropertyChanged("DeltaBrushV1");
                    }
                }
            }

            public Brush DeltaBrushV1
            {
                get
                {
                    if ((DeltaV1 > DeltaPlusV1) || (DeltaV1 < DeltaMinusV1))
                        return Brushes.Red;
                    else
                        return Brushes.LightGreen;
                }
            }

            // Dimenzija V2
            private float nazivnoV2;
            public float NazivnoV2
            {
                get { return nazivnoV2; }
                set
                {
                    if (nazivnoV2 != value)
                    {
                        nazivnoV2 = value;
                        DeltaV2 = MjerenoV2 - nazivnoV2;
                    }
                }
            }

            private float mjerenoV2;
            public float MjerenoV2
            {
                get { return mjerenoV2; }
                set
                {
                    if (mjerenoV2 != value)
                    {
                        mjerenoV2 = value;
                        DeltaV2 = -NazivnoV2 + mjerenoV2;
                    }
                }
            }

            private float deltaPlusV2;
            public float DeltaPlusV2
            {
                get { return deltaPlusV2; }
                set
                {
                    if (deltaPlusV2 != value)
                    {
                        deltaPlusV2 = value;
                        DeltaV2 = float.NaN;
                        DeltaV2 = -nazivnoV2 + mjerenoV2;
                    }
                }
            }

            private float deltaMinusV2;
            public float DeltaMinusV2
            {
                get { return deltaMinusV2; }
                set
                {
                    if (deltaMinusV2 != value)
                    {
                        deltaMinusV2 = value;
                        DeltaV2 = float.NaN;
                        DeltaV2 = -nazivnoV2 + mjerenoV2;
                    }
                }
            }

            private float deltaV2;
            public float DeltaV2
            {
                get { return deltaV2; }
                set
                {
                    if (deltaV2 != value)
                    {
                        deltaV2 = value;
                        OnPropertyChanged("DeltaV2");
                        OnPropertyChanged("NazivnoV2");
                        OnPropertyChanged("MjerenoV2");
                        OnPropertyChanged("DeltaPlusV2");
                        OnPropertyChanged("DeltaMinusV2");
                        OnPropertyChanged("DeltaBrushV2");
                    }
                }
            }

            public Brush DeltaBrushV2
            {
                get
                {
                    if ((DeltaV2 > DeltaPlusV2) || (DeltaV2 < DeltaMinusV2))
                        return Brushes.Red;
                    else
                        return Brushes.LightGreen;
                }
            }

            // Dimenzija V3
            private float nazivnoV3;
            public float NazivnoV3
            {
                get { return nazivnoV3; }
                set
                {
                    if (nazivnoV3 != value)
                    {
                        nazivnoV3 = value;
                        DeltaV3 = MjerenoV3 - nazivnoV3;
                    }
                }
            }

            private float mjerenoV3;
            public float MjerenoV3
            {
                get { return mjerenoV3; }
                set
                {
                    if (mjerenoV3 != value)
                    {
                        mjerenoV3 = value;
                        DeltaV3 = -NazivnoV3 + mjerenoV3;
                    }
                }
            }

            private float deltaPlusV3;
            public float DeltaPlusV3
            {
                get { return deltaPlusV3; }
                set
                {
                    if (deltaPlusV3 != value)
                    {
                        deltaPlusV3 = value;
                        DeltaV3 = float.NaN;
                        DeltaV3 = -nazivnoV3 + mjerenoV3;
                    }
                }
            }

            private float deltaMinusV3;
            public float DeltaMinusV3
            {
                get { return deltaMinusV3; }
                set
                {
                    if (deltaMinusV3 != value)
                    {
                        deltaMinusV3 = value;
                        DeltaV3 = float.NaN;
                        DeltaV3 = -nazivnoV3 + mjerenoV3;
                    }
                }
            }

            private float deltaV3;
            public float DeltaV3
            {
                get { return deltaV3; }
                set
                {
                    if (deltaV3 != value)
                    {
                        deltaV3 = value;
                        OnPropertyChanged("DeltaV3");
                        OnPropertyChanged("NazivnoV3");
                        OnPropertyChanged("MjerenoV3");
                        OnPropertyChanged("DeltaPlusV3");
                        OnPropertyChanged("DeltaMinusV3");
                        OnPropertyChanged("DeltaBrushV3");
                    }
                }
            }

            public Brush DeltaBrushV3
            {
                get
                {
                    if ((DeltaV3 > DeltaPlusV3) || (DeltaV3 < DeltaMinusV3))
                        return Brushes.Red;
                    else
                        return Brushes.LightGreen;
                }
            }

            // Dimenzija VB
            private float nazivnoVB;
            public float NazivnoVB
            {
                get { return nazivnoVB; }
                set
                {
                    if (nazivnoVB != value)
                    {
                        nazivnoVB = value;
                    }
                }
            }

            private float mjerenoVB;
            public float MjerenoVB
            {
                get { return mjerenoVB; }
                set
                {
                    if (mjerenoVB != value)
                    {
                        mjerenoVB = value;
                    }
                }
            }

            // Dimenzija V2 Devijacija
            private float mjerenoV2Devijacija;
            public float MjerenoV2Devijacija
            {
                get { return mjerenoV2Devijacija; }
                set
                {
                    if (mjerenoV2Devijacija != value)
                    {
                        mjerenoV2Devijacija = value;
                    }
                }
            }

            protected virtual void OnPropertyChanged(string propertyName)
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }

            public DimensionLine()
            {
            }

            //public DimensionLine(string _kote, float _nazivno, float _mjereno, float _deltaPlus, float _deltaMinus, float _delta)
            //{
            //    Kote = _kote;
            //    Nazivno = _nazivno;
            //    Mjereno = _mjereno;
            //    DeltaPlus = _deltaPlus;
            //    DeltaMinus = _deltaMinus;
            //    Delta = _delta;
            //}
        }

        public class TestData : INotifyPropertyChanged
        {   
            // Property changed implementation
            public event PropertyChangedEventHandler PropertyChanged;

            // Value hor S1
            private float valueHorS1;
            public float ValueHorS1
            {
                get { return valueHorS1; }
                set
                {
                    if (valueHorS1 != value)
                    {
                        valueHorS1 = value;
                    }
                }
            }

            // Value ver S1
            private float valueVerS1;
            public float ValueVerS1
            {
                get { return valueVerS1; }
                set
                {
                    if (valueVerS1 != value)
                    {
                        valueVerS1 = value;
                    }
                }
            }

            // Value px S1
            private float valuePxS1;
            public float ValuePxS1
            {
                get { return valuePxS1; }
                set
                {
                    if (valuePxS1 != value)
                    {
                        valuePxS1 = value;
                    }
                }
            }

            // Value hor S2
            private float valueHorS2;
            public float ValueHorS2
            {
                get { return valueHorS2; }
                set
                {
                    if (valueHorS2 != value)
                    {
                        valueHorS2 = value;
                    }
                }
            }

            // Value ver S2
            private float valueVerS2;
            public float ValueVerS2
            {
                get { return valueVerS2; }
                set
                {
                    if (valueVerS2 != value)
                    {
                        valueVerS2 = value;
                    }
                }
            }

            // Value px S1
            private float valuePxS2;
            public float ValuePxS2
            {
                get { return valuePxS2; }
                set
                {
                    if (valuePxS2 != value)
                    {
                        valuePxS2 = value;
                    }
                }
            }

            // Property changed implementation
            protected virtual void OnPropertyChanged(string propertyName)
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
