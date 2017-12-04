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

namespace VizijskiSustavWPF.limovi
{
    /// <summary>
    /// Interaction logic for MiniMap.xaml
    /// </summary>
    public partial class MiniMap : UserControl
    {
        bool firstPointVisible;

        public bool FirstPointVisible
        {
            set 
            { 
                firstPointVisible = value;
            }
        }
        bool secondPointVisible;

        public bool SecondPointVisible
        {
            set 
            { 
                secondPointVisible = value;
            }
        }
        bool thirdPointVisible;

        public bool ThirdPointVisible
        {
            set 
            { 
                thirdPointVisible = value;
            }
        }

        Point actualPosition;
        public Point ActualPosition
        {
            set { 
                    actualPosition = value;
                    Thickness margin = e_actual.Margin;
                    margin.Bottom = actualPosition.Y;
                    margin.Left = actualPosition.X;
                    e_actual.Margin = margin;
                }
        }
        Point firstPoint;
        public Point FirstPoint
        {
            set { 
                    firstPoint = value;
                    if ((firstPoint.X == 0) && (firstPoint.Y == 0))
                    {
                        s_firstPoint.Visibility = Visibility.Hidden;
                    }
                    else
                    {
                        Thickness margin = s_firstPoint.Margin;
                        margin.Bottom = firstPoint.Y + 5;
                        margin.Left = firstPoint.X + 5;
                        s_firstPoint.Margin = margin;
                        if (firstPointVisible)
                        {
                            s_firstPoint.Visibility = Visibility.Visible;
                        }
                    }
                }
        }
        Point secondPoint;
        public Point SecondPoint
        {
            set
            {
                secondPoint = value;
                if ((secondPoint.X == 0) && (secondPoint.Y == 0))
                {
                    s_secondPoint.Visibility = Visibility.Hidden;
                }
                else
                {
                    Thickness margin = s_secondPoint.Margin;
                    margin.Bottom = secondPoint.Y + 5;
                    margin.Left = secondPoint.X + 5;
                    s_secondPoint.Margin = margin;
                    if (secondPointVisible)
                    {
                        s_secondPoint.Visibility = Visibility.Visible;
                    }
                }
            }
        }
        
        Point thirdPoint;
        public Point ThirdPoint
        {
            set
            {
                thirdPoint = value;
                if ((thirdPoint.X == 0) && (thirdPoint.Y == 0))
                {
                    s_thirdPoint.Visibility = Visibility.Hidden;
                }
                else
                {
                    Thickness margin = s_thirdPoint.Margin;
                    margin.Bottom = thirdPoint.Y + 5;
                    margin.Left = thirdPoint.X + 5;
                    s_thirdPoint.Margin = margin;
                    if (thirdPointVisible)
                    {
                        s_thirdPoint.Visibility = Visibility.Visible;
                    }
                }
            }
        }
        public MiniMap()
        {
            InitializeComponent();
            ActualPosition = new Point(0, 0);
            FirstPoint = new Point(0, 0);
            SecondPoint = new Point(0, 0);
            ThirdPoint = new Point(0, 0);
            FirstPointVisible = false;
            SecondPointVisible = false;
            ThirdPointVisible = false;

        }

    }
}
