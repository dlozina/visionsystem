using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OxyPlot;
using OxyPlot.Wpf;

namespace VizijskiSustavWPF
{
    class BurrDetection
    {
        private List<int> positionList = new List<int>();
        public int Position = 0;

        private List<BurrLine> burrList;
        public List<BurrLine> BurrList
        {
            get { return burrList; }
            set 
            { 
                burrList = value;
                calculateParameters();
            }
        }

        private List<DataPoint> burrData = new List<DataPoint>();
        public List<DataPoint> BurrData
        {
            get { return burrData; }
            set 
            {
                burrData = value;
                if (((burrData.Count-1)/100)>(noOfBurrs))
                {
                    noOfBurrs=(burrData.Count-1)/100;
                    positionList.Add(Position);
                    Detect();
                }
            }
        }

        private int maxBurr;

        public int MaxBurr
        {
            get { return maxBurr; }
            set { maxBurr = value; }
        }

        private int burrPercent;

        public int BurrPercent
        {
            get { return burrPercent; }
            set { burrPercent = value; }
        }

       
        int noOfBurrs=0;
        public void Detect()
        {
            List<DataPoint> tempData = new List<DataPoint>();
            foreach (DataPoint dp in BurrData)
            {
                tempData.Add(new DataPoint(dp.X, dp.Y ));
            }
            bool stop1 = true, stop2 = true;
            List<BurrLine> tempBurrList = new List<BurrLine>();
            for (int j = 1; j <= noOfBurrs; j++)
            {
                stop1 = true;
                stop2 = true;
                double a = tempData[100 * j-1].Y;
                double b = tempData[100 * (j - 1) + 5].Y;
                for (int i = 5; i < 100; i++)
                {
                    if ((tempData[100*j - i].Y >= a) && (tempData[100*j - i].Y > tempData[100*j - i - 1].Y) && stop2 && (tempData[100 * j - i].Y > -100))
                    {
                        stop2 = false;
                        a = tempData[100*j - i].Y;
                    }
                    if ((tempData[100 * j - i].Y >= a) && stop2 && (tempData[100*j - i].Y > -100))
                    {
                        a = tempData[100*j - i].Y;
                    }
                    if (((-tempData[i + 100 * (j - 1)].Y + tempData[i + 100 * (j - 1) + 1].Y) > 2.5f) && stop1)
                     //   if ((tempData[i + 100 * (j - 1)].Y <= b) && ((-tempData[i + 100 * (j - 1)].Y + tempData[i + 100 * (j - 1) + 1].Y) > 5) && stop1)
                    {
                        stop1 = false;
                        b = tempData[i + 100 * (j - 1)].Y;
                    }
                    if (stop1)
                    {
                        b = tempData[i + 100 * (j - 1)].Y;
                    }
                }

                double burr = (a - b);
                if (b < -100)
                {
                    burr = 0;
                }
              //  tempBurrList.Add(new BurrLine(j, positionList[j], (int)((a - b) / 2)));
                tempBurrList.Add(new BurrLine(j, positionList[j-1], (int)(burr)));
                BurrList = tempBurrList;
            }
        }

        private void calculateParameters()
        {
            if (burrList.Count != 0)
            {
                maxBurr = 0;
                burrPercent = 0;
                foreach (BurrLine bl in BurrList)
                {
                    if (bl.Srh > maxBurr)
                    {
                        maxBurr = bl.Srh;
                    }
                    if (bl.Srh > 15)
                    {
                        burrPercent++;
                    }
                }
                burrPercent = (int)(100*((float)burrPercent / (float)burrList.Count));
            }
        }
        
        public void Reset()
        {
            noOfBurrs = 0;
            if (positionList!=null)
                positionList.Clear();
            if (burrList!=null)
                burrList.Clear();
            if (burrData!=null)
                burrData.Clear();
        }


        public int DetectOne(List<DataPoint> data)
        {
            List<DataPoint> tempData = new List<DataPoint>();
            for (int i = 0; i < 101; i++) 
            {
                if (data.Count>i)
                    tempData.Add(new DataPoint(data[i].X, data[i].Y*1000));
                else
                    tempData.Add(new DataPoint(-300,-300));
            }
            bool stop1 = true, stop2 = true;
           
                stop1 = true;
                stop2 = true;
                double a = tempData[99].Y;
                double b = tempData[5].Y;
                for (int i = 5; i < 100; i++)
                {
                    if ((tempData[100 - i].Y >= a) && (tempData[100 - i].Y > tempData[100 - i - 1].Y) && stop2 && (tempData[100 - i].Y > -100))
                    {
                        stop2 = false;
                        a = tempData[100 - i].Y;
                    }
                    if ((tempData[100 - i].Y >= a) && stop2 && (tempData[100 - i].Y > -100))
                    {
                        a = tempData[100 - i].Y;
                    }
                    if (((-tempData[i].Y + tempData[i + 1].Y) > 2.5f) && stop1)
                    //   if ((tempData[i + 100 * (j - 1)].Y <= b) && ((-tempData[i + 100 * (j - 1)].Y + tempData[i + 100 * (j - 1) + 1].Y) > 5) && stop1)
                    {
                        stop1 = false;
                        b = tempData[i].Y;
                    }
                    if (stop1)
                    {
                        b = tempData[i].Y;
                    }
                }

                double burr = (a - b);
                if (b < -100)
                {
                    burr = 0;
                }
                return (int)burr;
            
        }

    }
}
