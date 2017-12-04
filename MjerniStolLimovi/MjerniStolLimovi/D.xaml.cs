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
	/// Interaction logic for D.xaml
	/// </summary>
    public partial class D : UserControl , ILimKontrola
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
            get { return TextBlockD.Text; }
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
                    pointListTranslatedAndRotated = LimoviCommon.translateAndRotate(PointList, (Point)PointList[6], (Point)PointList[1]);
                    var tempList = GetAll();
                }
            }
        }

		public D()
		{
			this.InitializeComponent();
            colorPointList.Add(Point1);
            colorPointList.Add(Point2);
            colorPointList.Add(Point3);
            colorPointList.Add(Point4);
            colorPointList.Add(Point5);
            colorPointList.Add(Point6);
            colorPointList.Add(Point7);
			colorPointList.Add(Point8);
            LimoviCommon.pointColoring(PointList, colorPointList, Purpose);
		}

        double GetOne(int measureNumber)
        {
            switch (measureNumber)
            {
                case 0: // B
                    return (Math.Abs(pointListTranslatedAndRotated[5].Y) + Math.Abs(pointListTranslatedAndRotated[2].Y)) / 2;
                case 1: // L1
                    return Math.Abs(pointListTranslatedAndRotated[4].X - pointListTranslatedAndRotated[7].X);
                case 2: // L2
                    return Math.Abs(pointListTranslatedAndRotated[3].X - pointListTranslatedAndRotated[4].X);
                case 3: // L3
                    return Math.Abs(pointListTranslatedAndRotated[0].X - pointListTranslatedAndRotated[3].X);
                case 4: // Luk
                    return Math.Abs(pointListTranslatedAndRotated[0].X - pointListTranslatedAndRotated[7].X);
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
                PointList.Add(new Point(89.2,12));
                PointList.Add(new Point(85,18));
                PointList.Add(new Point(73,1.5));
                PointList.Add(new Point(3,17));
                PointList.Add(new Point(-11,39));
                PointList.Add(new Point(63.2,13.5));
                PointList.Add(new Point(17,23.5));
                PointList = PointList;
            }
        }

	}
}