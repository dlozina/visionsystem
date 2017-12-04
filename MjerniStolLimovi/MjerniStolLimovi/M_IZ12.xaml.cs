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
	/// Interaction logic for M_IZ12.xaml
	/// </summary>
	public partial class M_IZ12 : UserControl , ILimKontrola
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
            get { return TextBlockMIZ12.Text; }
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

		public M_IZ12()
		{
			this.InitializeComponent();
            colorPointList.Add(Point1);
            colorPointList.Add(Point2);
            colorPointList.Add(Point3);
            colorPointList.Add(Point4);
            colorPointList.Add(Point5);
            colorPointList.Add(Point6);
            LimoviCommon.pointColoring(PointList, colorPointList, Purpose);
		}

        double GetOne(int measureNumber)
        {
            double B_M;
            switch (measureNumber)
            {
                case 0: // B_M
                    return (Math.Abs(pointListTranslatedAndRotated[4].Y) + Math.Abs(pointListTranslatedAndRotated[1].Y)) / 2;
                case 1: // L2_M
                    B_M = (Math.Abs(pointListTranslatedAndRotated[4].Y) + Math.Abs(pointListTranslatedAndRotated[1].Y)) / 2;
                    return Math.Abs(pointListTranslatedAndRotated[0].X - pointListTranslatedAndRotated[2].X) - B_M / 2;
                case 2: // L3_M
                    B_M = (Math.Abs(pointListTranslatedAndRotated[4].Y) + Math.Abs(pointListTranslatedAndRotated[1].Y)) / 2;
                    return Math.Abs(pointListTranslatedAndRotated[3].X - pointListTranslatedAndRotated[5].X) - B_M / 2;
                case 3: // L1_M
                    B_M = (Math.Abs(pointListTranslatedAndRotated[4].Y) + Math.Abs(pointListTranslatedAndRotated[1].Y)) / 2;
                    return Math.Abs(pointListTranslatedAndRotated[2].X - pointListTranslatedAndRotated[3].X) + B_M;
                case 4: // L14+5S
                    return Math.Abs(pointListTranslatedAndRotated[0].X - pointListTranslatedAndRotated[5].X);
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
                PointList.Add(new Point(98.5,24));
                PointList.Add(new Point(95,18.5));
                PointList.Add(new Point(91,31));
                PointList.Add(new Point(14,21.5));
                PointList.Add(new Point(8,8));
                PointList.Add(new Point(5,10.3));
                PointList = PointList;
            }
        }

	}
}