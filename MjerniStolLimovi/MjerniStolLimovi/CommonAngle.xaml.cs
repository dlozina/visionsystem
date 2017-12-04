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
	/// Interaction logic for CommonAngle.xaml
	/// </summary>
	public partial class CommonAngle : UserControl, ILimKontrola
	{
        List<Arc> colorPointList = new List<Arc>();
        List<object> pointList = new List<object>();
        List<Point> pointListTranslatedAndRotated = new List<Point>();

        public CommonAngle()
        {
            this.InitializeComponent();
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
                    //izbrisati if
                    if (PointList.Count > 0)
                    pointListTranslatedAndRotated = LimoviCommon.translateAndRotate(PointList, (Point)PointList[3], (Point)PointList[0]);
                    var tempList = GetAll();
                }
            }
        }

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
            get { return ""; }
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

        public List<double> GetAll()
        {
            var tempList = new List<double>();
            for (int i = 0; i < measuresArray.Items.Count; i++)
            {
                tempList.Add(GetOne(i));
            }
            return tempList;
        }

        double GetOne(int measureNumber)
        {
            double centerline2X, centerline1X;
            switch (measureNumber)
            {
                case 0: // B
                    return (Math.Abs(pointListTranslatedAndRotated[2].Y) + Math.Abs(pointListTranslatedAndRotated[1].Y)) / 2;
                case 1: // L12
                    centerline2X = Math.Abs(pointListTranslatedAndRotated[3].X + pointListTranslatedAndRotated[2].X) / 2;
                    centerline1X = Math.Abs(pointListTranslatedAndRotated[1].X + pointListTranslatedAndRotated[0].X) / 2;
                    return Math.Abs(centerline1X - centerline2X);
                default:
                    return 0;
            }
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            //135
            angleGradientBeta.StartAngle = -90;
            angleGradientBeta.EndAngle = 45;
            RotateTransform rotate = new RotateTransform();
            rotate.Angle = -45;
            verticalLinePoint.RenderTransform = rotate;
            angleArcSmall.EndAngle = -45;
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            //90
            angleGradientBeta.StartAngle = -90;
            angleGradientBeta.EndAngle = 0;
            RotateTransform rotate = new RotateTransform();
            rotate.Angle = 0;
            verticalLinePoint.RenderTransform = rotate;
            angleArcSmall.EndAngle = 0;
        }

        private void Button_Click_5(object sender, RoutedEventArgs e)
        {
            //45
            angleGradientBeta.StartAngle = -90;
            angleGradientBeta.EndAngle = -45;
            RotateTransform rotate = new RotateTransform();
            rotate.Angle = 45;
            verticalLinePoint.RenderTransform = rotate;
            angleArcSmall.EndAngle = 45;
        }
		
	}

       
}