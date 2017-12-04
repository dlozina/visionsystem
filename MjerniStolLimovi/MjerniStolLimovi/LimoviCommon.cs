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
    class LimoviCommon
    {
        public static void pointColoring(List<object> listOfPoints, List<Arc> listOfPointsToColor, int purpose)
        {
                if (listOfPoints.Count == 0)
                {
                    if (purpose == 4)
                    {
                        foreach (Arc a in listOfPointsToColor)
                        {
                            a.Fill = Brushes.Red;
                        }
                    }

                    if (purpose == 0)
                    {
                        foreach (Arc a in listOfPointsToColor)
                            a.Fill = Brushes.Transparent;
                        //treperiti treba
                        //debuging
                        //izbrisati if
                        if (listOfPointsToColor.Count > 0)
                        listOfPointsToColor[0].Fill = Brushes.Red;
                    }


                }

                if (listOfPoints.Count <= listOfPointsToColor.Count)
                    for (int i = 0; i < listOfPoints.Count; i++)
                    {
                        if (listOfPoints[i] != null)
                        {
                            if (listOfPoints.Count <= listOfPointsToColor.Count)
                                listOfPointsToColor[i].Fill = Brushes.Green;
                        }
                        else
                        {
                            listOfPointsToColor[i].Fill = Brushes.Red;
                        }
                        //treperiti treba
                        if (i + 1 < listOfPointsToColor.Count)
                            listOfPointsToColor[i + 1].Fill = Brushes.Red;
                        //int isus = purpose;
                    }
        }

        public static List<Point> translateAndRotate(List<object> listOfPoints, Point origin, Point originReflection)
        {
            List<Point> pointListTranslated = new List<Point>();
            List<Point> pointListTranslatedRotated = new List<Point>();

            pointListTranslated.Clear();
            foreach (Point p in listOfPoints)
            { // for each point in list add translated point in new list
                pointListTranslated.Add(new Point(p.X - origin.X, p.Y - origin.Y));
            }

            originReflection.X -= origin.X;
            originReflection.Y -= origin.Y;

            double angle = -Math.Atan(originReflection.Y / originReflection.X);
            double cos = Math.Cos(angle);
            double sin = Math.Sin(angle);
            pointListTranslatedRotated.Clear();
            foreach (Point p in pointListTranslated)
            {
                pointListTranslatedRotated.Add(new Point(cos * p.X - sin * p.Y, sin * p.X + cos * p.Y));
            }

            return pointListTranslatedRotated;
        }

        public static void purposeLookRefresh(int purpose, Grid pointArcs, Grid lines, Grid lineArrows, Grid dimensionNames, Grid lineEnum, Grid circleEnum, Grid angleEnum, Grid circleArcs, Grid centerlines)
        {
            switch (purpose)
            {
                case 0: //default
                    pointArcs.Visibility = Visibility.Visible;
                    lines.Visibility = Visibility.Visible;
                    lineArrows.Visibility = Visibility.Visible;
                    dimensionNames.Visibility = Visibility.Visible;
                    lineEnum.Visibility = Visibility.Hidden;
                    //if (circleEnum != null)
                    circleEnum.Visibility = Visibility.Visible;
                    angleEnum.Visibility = Visibility.Visible;
                    circleArcs.Visibility = Visibility.Visible;
                    centerlines.Visibility = Visibility.Visible;
                    break;
                case 1: //valovitost
                    pointArcs.Visibility = Visibility.Hidden;
                    lines.Visibility = Visibility.Hidden;
                    lineArrows.Visibility = Visibility.Hidden;
                    dimensionNames.Visibility = Visibility.Hidden;
                    lineEnum.Visibility = Visibility.Visible;
                    //if (circleEnum != null)
                    circleEnum.Visibility = Visibility.Hidden;
                    angleEnum.Visibility = Visibility.Hidden;
                    circleArcs.Visibility = Visibility.Hidden;
                    centerlines.Visibility = Visibility.Hidden;
                    break;
                case 2: //srh
                    pointArcs.Visibility = Visibility.Hidden;
                    lines.Visibility = Visibility.Hidden;
                    lineArrows.Visibility = Visibility.Hidden;
                    dimensionNames.Visibility = Visibility.Hidden;
                    lineEnum.Visibility = Visibility.Visible;
                    //if (circleEnum != null)
                    circleEnum.Visibility = Visibility.Visible;
                    angleEnum.Visibility = Visibility.Hidden;
                    circleArcs.Visibility = Visibility.Hidden;
                    centerlines.Visibility = Visibility.Hidden;
                    break;
                case 3: //sablja
                    pointArcs.Visibility = Visibility.Hidden;
                    lines.Visibility = Visibility.Hidden;
                    lineArrows.Visibility = Visibility.Hidden;
                    dimensionNames.Visibility = Visibility.Hidden;
                    lineEnum.Visibility = Visibility.Visible;
                    //if (circleEnum != null)
                    circleEnum.Visibility = Visibility.Hidden;
                    angleEnum.Visibility = Visibility.Hidden;
                    circleArcs.Visibility = Visibility.Hidden;
                    centerlines.Visibility = Visibility.Hidden;
                    break;
                case 4: //kut
                    pointArcs.Visibility = Visibility.Visible;
                    lines.Visibility = Visibility.Hidden;
                    lineArrows.Visibility = Visibility.Hidden;
                    dimensionNames.Visibility = Visibility.Hidden;
                    lineEnum.Visibility = Visibility.Hidden;
                    //if (circleEnum != null)
                    circleEnum.Visibility = Visibility.Hidden;
                    angleEnum.Visibility = Visibility.Visible;
                    circleArcs.Visibility = Visibility.Hidden;
                    centerlines.Visibility = Visibility.Hidden;
                    break;
            }
        }
    }
    
    interface ILimKontrola
    {
        int Purpose { get; set; }
        List<object> PointList { get; set; }
        Grid MainLines { get; set; }
        Grid Circles { get; set; }
        Grid PointArcs { get; set; }
        Grid CircleArcs { get; set; }
        Grid Centerlines { get; set; }
        String SheetName { get; }
        List<string> MeasuresList { get;}
        List<double> GetAll();
        //void UserControl_MouseDown(object sender, MouseButtonEventArgs e);
    }
}
