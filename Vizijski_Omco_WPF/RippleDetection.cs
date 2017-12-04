using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OxyPlot;
using OxyPlot.Wpf;

namespace VizijskiSustavWPF
{
    class RippleDetection
    {
        private int noOfWaves = 0;
        public int NoOfWaves
        {
            get { return noOfWaves; }
            set { noOfWaves = value; }
        }

        private int edgeThicknes = 500;
        public int EdgeThicknes
        {
            get { return edgeThicknes; }
            set { edgeThicknes = value; }
        }

        private int edgeLength;
        public int EdgeLength
        {
            get { return edgeLength; }
            set { edgeLength = value; }
        }

        private int edgeHeight;
        public int EdgeHeight
        {
            get { return edgeHeight; }
            set { edgeHeight = value; }
        }

        
        private int min;
        private int max;
        int X1 = 0;
        int X2 = 0;

        float ratio = 0;

        public float Ratio
        {
            get { return ratio; }
            set { ratio = value; }
        }

        private List<DataPoint> rippleData = new List<DataPoint>();
        public List<DataPoint> RippleData
        {
            get { return rippleData; }
            set { rippleData = value; }
        }
        public void Detect()
        {
            List<DataPoint> tempData = new List<DataPoint>();
            foreach (DataPoint dp in rippleData)
            {
                tempData.Add(new DataPoint(dp.X, dp.Y * 1000));
            }
            rippleData = tempData;
            int measLength = (int)rippleData[rippleData.Count-1].X;
            noOfWaves = 0;
            max=-30000;
            min=30000;
            ratio = 0;
            bool mark1 = true;
            bool mark2 = true;
            int local_max = -30000;

            for (int i=5; i<rippleData.Count-5; i++)
            {
                if (rippleData[i].Y > max)
                {
                    max = (int)rippleData[i].Y;
                }
                if (rippleData[i].Y < min)
                {
                    min = (int)rippleData[i].Y;
                }
                if (((rippleData[i].Y - rippleData[i+5].Y)< -20f)&& mark1 && mark2)
                {
                    X1 = i;
                    mark1 = false;
                    local_max = 0;
                }
                if (((rippleData[i].Y - rippleData[i + 5].Y) >= 20f) && !mark1)
                {
                    local_max = (int)rippleData[i].Y;
                    mark1 = true;
                    mark2 = false;
                }
                if (((rippleData[i].Y - rippleData[i + 5].Y) < 20f) && !mark2)
                {
                    X2 = i + 5;
                    mark1 = true;
                    mark2 = true;
                   
                    float check = (((float)(local_max-edgeThicknes) / ((float)((X2 - X1) * measLength)/(float)rippleData.Count)));
                    if (check>0.1)
                    {
                        noOfWaves += 1;
                    }
                    if (check>ratio)
                    {
                        edgeLength = (int)((float)((X2 - X1) * measLength) / (float)rippleData.Count);
                        edgeHeight = (local_max - edgeThicknes);
                        ratio = check;
                    }
                }
            }
        }


//        "Ripple_Compesation".Waves_Number:=0;
//"Ripple_Compesation".Max:=-300;
//"Ripple_Compesation".Min:=300;
//"Ripple_Compesation".Ratio:=0;
//#mark1:=1;
//#mark2:=1;
//#local_max:=-300;
//#local_min:=300;
//#out_of_tolerance:=0;
//"Ripple_Compesation".Waves_Number:=0;
//FOR #i := 5 TO REAL_TO_INT("IEC_Counter_0_DB_1".CV)-5 DO
//  IF "Graphs".Measurement_Data_Z[#i]>"Ripple_Compesation".Max THEN
//    "Ripple_Compesation".Max:="Graphs".Measurement_Data_Z[#i];
//  END_IF;
//  IF "Graphs".Measurement_Data_Z[#i]<"Ripple_Compesation".Min THEN
//    "Ripple_Compesation".Min:="Graphs".Measurement_Data_Z[#i];
//  END_IF;
//  IF "Graphs".Measurement_Data_Z[#i]-"Graphs".Measurement_Data_Z[#i+5]<0.02 AND #mark1 AND #mark2 THEN
//    #X1:=#i;
//    #mark1:=0;
//    #local_max:=0;
//  END_IF;
//  IF "Graphs".Measurement_Data_Z[#i]-"Graphs".Measurement_Data_Z[#i+5]>=0.02 AND NOT(#mark1) THEN
//    #local_max:="Graphs".Measurement_Data_Z[#i];
//    #mark1:=1;
//    #mark2:=0;
//  END_IF;
//  IF "Graphs".Measurement_Data_Z[#i]-"Graphs".Measurement_Data_Z[#i+5]<0.02 AND NOT(#mark2) THEN
//    #X2:=#i+5;
//    #mark1:=1;
//    #mark2:=1;
//    #check:=100*(#local_max-"Ripple_Compesation".edge_thicknes)/("Ripple_Compesation".edge_lenght*(#X2-#X1)/"IEC_Counter_0_DB_1".CV);
//    IF #check>0.1 THEN
//      "Ripple_Compesation".Waves_Number:= "Ripple_Compesation".Waves_Number+1;
//    END_IF;
//    IF #check>"Ripple_Compesation".Ratio THEN
//      "Ripple_Compesation".Ratio:= #check;
//    END_IF;
//  END_IF;
//   ;
// END_FOR;




        public void Smooth()
        {
            double temp;
            try
            {
                for (int i = 0; i < rippleData.Count; i++)
                {
                    if (i == 0)
                        temp = (rippleData[i].Y + rippleData[i + 1].Y + rippleData[i + 2].Y + rippleData[i + 3].Y + rippleData[i + 4].Y) / 5f;
                    else if (i == 1)
                        temp = (rippleData[i - 1].Y + rippleData[i].Y + rippleData[i + 1].Y + rippleData[i + 2].Y + rippleData[i + 3].Y + rippleData[i + 4].Y) / 6f;
                    else if (i == 2)
                        temp = (rippleData[i - 2].Y + rippleData[i - 1].Y + rippleData[i].Y + rippleData[i + 1].Y + rippleData[i + 2].Y + rippleData[i + 3].Y + rippleData[i + 4].Y) / 7f;
                    else if (i == 3)
                        temp = (rippleData[i - 3].Y + rippleData[i - 2].Y + rippleData[i - 1].Y + rippleData[i].Y + rippleData[i + 1].Y + rippleData[i + 2].Y + rippleData[i + 3].Y + rippleData[i + 4].Y) / 8f;

                    else if ((rippleData.Count - i) == 1)
                        temp = (rippleData[i - 4].Y + rippleData[i - 3].Y + rippleData[i - 2].Y + rippleData[i - 1].Y + rippleData[i].Y) / 5f;
                    else if ((rippleData.Count - i) == 2)
                        temp = (rippleData[i - 4].Y + rippleData[i - 3].Y + rippleData[i - 2].Y + rippleData[i - 1].Y + rippleData[i].Y + rippleData[i + 1].Y) / 6f;
                    else if ((rippleData.Count - i) == 3)
                        temp = (rippleData[i - 4].Y + rippleData[i - 3].Y + rippleData[i - 2].Y + rippleData[i - 1].Y + rippleData[i].Y + rippleData[i + 1].Y + rippleData[i + 2].Y) / 7f;
                    else if ((rippleData.Count - i) == 4)
                        temp = (rippleData[i - 4].Y + rippleData[i - 3].Y + rippleData[i - 2].Y + rippleData[i - 1].Y + rippleData[i].Y + rippleData[i + 1].Y + rippleData[i + 2].Y + rippleData[i + 3].Y) / 8f;

                    else
                        temp = (rippleData[i - 4].Y + rippleData[i - 3].Y + rippleData[i - 2].Y + rippleData[i - 1].Y + rippleData[i].Y + rippleData[i + 1].Y + rippleData[i + 2].Y + rippleData[i + 3].Y + rippleData[i + 4].Y) / 9f;
                    rippleData[i] = new DataPoint(rippleData[i].X, temp);
                }
            }
            catch
            {

            }
        }

        public void Reset()
        {
            rippleData.Clear();
        }
    }
}
