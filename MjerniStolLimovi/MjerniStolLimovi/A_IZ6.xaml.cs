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
	/// Interaction logic for A_IZ6.xaml
	/// </summary>
    public partial class A_IZ6 : UserControl , ILimKontrola
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
            get { return TextBlockAIZ6.Text; }
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

		public A_IZ6()
		{
			this.InitializeComponent();
            colorPointList.Add(Point1);
            colorPointList.Add(Point2);
            colorPointList.Add(Point3);
            colorPointList.Add(Point4);
            colorPointList.Add(Point5);
            colorPointList.Add(Point6);
            colorPointList.Add(Point7);
            LimoviCommon.pointColoring(PointList, colorPointList, Purpose);
		}

        double GetOne(int measureNumber)
        {
            double centerline2X;
            switch (measureNumber)
            {
                case 0: // B
                    return (Math.Abs(pointListTranslatedAndRotated[5].Y) + Math.Abs(pointListTranslatedAndRotated[2].Y)) / 2;
                case 1: // L4
                    centerline2X = (Math.Abs(pointListTranslatedAndRotated[6].X + pointListTranslatedAndRotated[5].X)) / 2;
                    return Math.Abs(pointListTranslatedAndRotated[4].X - centerline2X);
                case 2: // L5
                    return Math.Abs(pointListTranslatedAndRotated[3].X - pointListTranslatedAndRotated[4].X);
                case 3: // L6
                    return Math.Abs(pointListTranslatedAndRotated[0].X - pointListTranslatedAndRotated[3].X);
                case 4:
                    return (GetOne(1) + GetOne(2) + GetOne(3));
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
            if (PointList.Count < 7)
            {
                PointList.Add(new Point(11.2, 1.6));
                PointList.Add(new Point(9.75, 4.3));
                PointList.Add(new Point(8.5, 0.2));
                PointList.Add(new Point(8.3, 2.5));
                PointList.Add(new Point(4.7, 3.6));
                PointList.Add(new Point(3.35, 1.7));
                PointList.Add(new Point(0.4, 7));
                PointList = PointList;
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
            PointList = new List<object>();
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            pointListTranslatedAndRotated = LimoviCommon.translateAndRotate(PointList, (Point)PointList[6], (Point)PointList[1]);
            //pointListTranslatedAndRotated = translateAndRotate(PointList, (Point)PointList[6], (Point)PointList[1]);
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
       
        }

        private void nula_Click(object sender, RoutedEventArgs e)
        {
            purpose = 0;
            PointList = PointList;
        }

        private void jedan_Click(object sender, RoutedEventArgs e)
        {
            purpose = 1;
            PointList = PointList;
        }

        private void dva_Click(object sender, RoutedEventArgs e)
        {
            purpose = 2;
            PointList = PointList;
        }

        private void tri_Click(object sender, RoutedEventArgs e)
        {
            purpose = 3;
            PointList = PointList;
        }

        private void cetri_Click(object sender, RoutedEventArgs e)
        {
            purpose = 4;
            PointList = PointList;
        }

        private void Button_Click_5(object sender, RoutedEventArgs e)
        {
            if (PointList.Count < 8)
            {
                PointList.Add(new Point(65.5,10.8));
                PointList.Add(new Point(60,22.4));
                PointList.Add(new Point(53.5,5.5));
                PointList.Add(new Point(32.5,13));
                PointList.Add(new Point(22,36));
                PointList.Add(new Point(54,15));
                PointList.Add(new Point(39,20.5));
                PointList = PointList;
            }
        }

	}
	
}