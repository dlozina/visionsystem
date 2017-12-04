using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Expression.Shapes;

namespace MjerniStolLimovi
{
	/// <summary>
	/// Interaction logic for A_IZ1_IZ6.xaml
	/// </summary>
	public partial class TU4 : UserControl , ILimKontrola
	{
        List<Arc> colorPointList = new List<Arc>();
        List<object> pointList = new List<object>();
        List<Point> pointListTranslatedAndRotated = new List<Point>();

        public Grid Circles
        {
            get { return circles; }
            set { circles = value; }
        }

        public Grid MainLines
        {
            get { return mainLines; }
            set { mainLines = value; }
        }

        public Grid PointArcs
        {
            get { return pointArcs; }
            set { pointArcs = value; }
        }

        public Grid CircleArcs
        {
            get { return circleArcs; }
            set { circleArcs = value; }
        }

        public Grid Centerlines
        {
            get { return centerlines; }
            set { centerlines = value; }
        }

        public String SheetName
        {
            get { return "TU4"; }
        }

        public List<string> MeasuresList
        {
            get
            {
                List<string> measTemp = new List<string>();
                foreach (string s in measuresArray.Items)
                {
                    measTemp.Add(s);
                }
                return measTemp;
            }
        }

        int purpose = 0;
        public int Purpose
        {
            get { return purpose; }
            set
            {
                purpose = value;
                LimoviCommon.purposeLookRefresh(purpose, pointArcs, lines, lineArrows, dimensionNames, lineEnumeration, circleEnumeration, angleEnumeration, circleArcs, centerlines);
            }
        }
        public List<object> PointList
        {
            get { return pointList; }
            set
            {
                pointList = value;
                LimoviCommon.pointColoring(PointList, colorPointList, Purpose);
                if (PointList.Count >= colorPointList.Count)
                {
                    pointListTranslatedAndRotated = LimoviCommon.translateAndRotate(PointList, (Point)PointList[3], (Point)PointList[1]);
                    var tempList = GetAll();
                }

            }
        }            
                
        //constructor
		public TU4()
		{
            this.InitializeComponent();
            colorPointList.Add(Point1);
            colorPointList.Add(Point2);
            colorPointList.Add(Point3);
            colorPointList.Add(Point4);
            LimoviCommon.pointColoring(PointList, colorPointList, Purpose);
		}

        double GetOne(int measureNumber)
        {
            double B;
            switch (measureNumber)
            {
                case 0: // B
                    return 2*Math.Abs(pointListTranslatedAndRotated[2].Y - pointListTranslatedAndRotated[1].Y);
                case 2: // L1
					B = 2*Math.Abs(pointListTranslatedAndRotated[2].Y - pointListTranslatedAndRotated[1].Y);
                    return Math.Abs(pointListTranslatedAndRotated[1].X - pointListTranslatedAndRotated[2].X) + B/2;
                case 3: // L2
                    B = 2*Math.Abs(pointListTranslatedAndRotated[2].Y - pointListTranslatedAndRotated[1].Y);
                    return Math.Abs(pointListTranslatedAndRotated[0].X - pointListTranslatedAndRotated[1].X) - B/2;
                default:
                    return 0;
            }
        }

        public List<double> GetAll()
        {
            var tempList = new List<double>();
            for (int i = 0; i < measuresArray.Items.Count; i++)
            {
                tempList.Add(GetOne(i));
            }
            return tempList;
        }

        //events
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (PointList.Count < 8)
            {

            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (PointList.Count < 7)
            {
                PointList.Add(null);
                PointList = PointList;
            }
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
           PointList=new List<object>();
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            pointListTranslatedAndRotated = LimoviCommon.translateAndRotate(PointList, (Point)PointList[6], (Point)PointList[1]);
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
          
        }

        private void Button_Click_5(object sender, RoutedEventArgs e)
        {
            PointList.Add(new Point(90, 37));
            PointList.Add(new Point(84, 40));
            PointList.Add(new Point(81.2, 20));
            PointList.Add(new Point(22.4, 1.3));
            PointList = PointList;
        }

	}
}