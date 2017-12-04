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
	/// Interaction logic for D_IZ15.xaml
	/// </summary>
	public partial class D_IZ15 : UserControl , ILimKontrola
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
            get { return bTextBlockDIZ15.Text; }
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
                    pointListTranslatedAndRotated = LimoviCommon.translateAndRotate(PointList, (Point)PointList[3], (Point)PointList[0]);
                    var tempList = GetAll();
                }
            }
        }

		public D_IZ15()
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
            double centerline2X, centerline1X;
            switch (measureNumber)
            {
                case 0: // L1_D
                    centerline1X = Math.Abs(pointListTranslatedAndRotated[3].X + pointListTranslatedAndRotated[2].X) / 2;
                    centerline2X = Math.Abs(pointListTranslatedAndRotated[1].X + pointListTranslatedAndRotated[0].X) / 2;
                    return Math.Abs(centerline1X - centerline2X);
                case 1: // L14
                    return Math.Abs(pointListTranslatedAndRotated[0].X - pointListTranslatedAndRotated[3].X);
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
                PointList.Add(new Point(89.8,40));
                PointList.Add(new Point(84,27.7));
                PointList.Add(new Point(14,1.4));
                PointList.Add(new Point(2,6.8));
                PointList = PointList;
            }
        }
	}
}