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
	/// Interaction logic for E_IZ14.xaml
	/// </summary>
	public partial class E_IZ14 : UserControl , ILimKontrola
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
            get { return tTextBlockEIZ14.Text; }
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
                    pointListTranslatedAndRotated = LimoviCommon.translateAndRotate(PointList, (Point)PointList[3], (Point)PointList[2]);
                    var tempList = GetAll();
                }
            }
        }

		public E_IZ14()
		{
			this.InitializeComponent();
            colorPointList.Add(Point1);
            colorPointList.Add(Point2);
            colorPointList.Add(Point3);
            colorPointList.Add(Point4);
            colorPointList.Add(Point5);
            LimoviCommon.pointColoring(PointList, colorPointList, Purpose);
		}

        double GetOne(int measureNumber)
        {
            double B_E;
            switch (measureNumber)
            {
                case 0: // B_E
                    return (Math.Abs(pointListTranslatedAndRotated[4].Y) + Math.Abs(pointListTranslatedAndRotated[1].Y)) / 2;
                case 1: // L1_E
                    B_E = (Math.Abs(pointListTranslatedAndRotated[4].Y) + Math.Abs(pointListTranslatedAndRotated[1].Y)) / 2;
                    return Math.Abs(pointListTranslatedAndRotated[3].X - pointListTranslatedAndRotated[2].X) + B_E;
                case 2: // L2_E
                    B_E = (Math.Abs(pointListTranslatedAndRotated[4].Y) + Math.Abs(pointListTranslatedAndRotated[1].Y)) / 2;
                    return Math.Abs(pointListTranslatedAndRotated[0].X - pointListTranslatedAndRotated[2].X) - B_E/2;
                case 3: // L14+5S
                    return Math.Abs(pointListTranslatedAndRotated[0].X - pointListTranslatedAndRotated[4].X);
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

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (PointList.Count < 14)
            {
                PointList.Add(new Point(93,2));
                PointList.Add(new Point(92,1.3));
                PointList.Add(new Point(87.8,12.8));
                PointList.Add(new Point(14,37.5));
                PointList.Add(new Point(1.7,31.5));
                PointList = PointList;
            }
        }
	}
}