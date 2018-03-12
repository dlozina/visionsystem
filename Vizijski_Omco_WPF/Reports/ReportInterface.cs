using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
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
            public string String { get; set; }

            public bool Poroznost { get; set; }

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
                        Delta = -Nazivno + mjereno;
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
    }
}
