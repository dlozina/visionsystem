using System;
using HalconDotNet;

public partial class HDevelopExport
{
    private void diameter2No5S2()
    {
        // Local iconic variables
        HObject ho_Image = null, ho_Rectangle = null;
        HObject ho_ImageReduced = null, ho_Edges = null;
        HObject ho_SelectedContours = null, ho_ContEllipse = null;
        // Local control variables
        HTuple hv_Width = new HTuple(), hv_Height = new HTuple();
        HTuple hv_Row = new HTuple(), hv_Col = new HTuple();
        HTuple hv_HalfH = new HTuple(), hv_HalfW = new HTuple();
        HTuple hv_Row1 = new HTuple(), hv_Column1 = new HTuple();
        HTuple hv_Phi1 = new HTuple(), hv_Radius11 = new HTuple();
        HTuple hv_Radius21 = new HTuple(), hv_StartPhi1 = new HTuple();
        HTuple hv_EndPhi1 = new HTuple(), hv_PointOrder1 = new HTuple();
        HTuple hv_Length = new HTuple(), hv_Row2 = new HTuple();
        // Result variables
        HTuple hv_Col2 = new HTuple();
        HTuple hv_TupleMax = new HTuple();
        HTuple hv_IndexMax = new HTuple();
        //************************************************************
        //KOMAD NO. 5 D2 S2
        //************************************************************
        //Camera communication - Open
        // Exposure time 3500.0
        openCAMFrame(3500.0);
        HOperatorSet.GrabImageAsync(out ho_Image, hv_AcqHandle, -1);
        //Camera communication - Close
        closeCAMFrame();
        HOperatorSet.GetImageSize(ho_Image, out hv_Width, out hv_Height);
        HOperatorSet.GenRectangle1(out ho_Rectangle, hv_Height - 2600, (hv_Width / 2) - 120,
            hv_Height - 200, (hv_Width / 2) + 120);
        HOperatorSet.ReduceDomain(ho_Image, ho_Rectangle, out ho_ImageReduced);
        HOperatorSet.EdgesSubPix(ho_ImageReduced, out ho_Edges, "canny", 1.9, 10,
            20);
        HOperatorSet.UnionAdjacentContoursXld(ho_Edges, out ho_SelectedContours, 2000,
            1, "attr_keep");
        HOperatorSet.SelectContoursXld(ho_SelectedContours, out ho_SelectedContours,
            "contour_length", 500, 50000, -0.5, 0.5);
        HOperatorSet.GetContourXld(ho_SelectedContours, out hv_Row, out hv_Col);
        HOperatorSet.FitEllipseContourXld(ho_SelectedContours, "geotukey", -1, 0,
            0, 200, 5, 2, out hv_Row1, out hv_Column1, out hv_Phi1, out hv_Radius11,
            out hv_Radius21, out hv_StartPhi1, out hv_EndPhi1, out hv_PointOrder1);
        HOperatorSet.GenEllipseContourXld(out ho_ContEllipse, hv_Row1, hv_Column1,
            hv_Phi1, hv_Radius11, hv_Radius21, 0, 6.28318, "positive", 1.5);
        HOperatorSet.LengthXld(ho_ContEllipse, out hv_Length);
        HOperatorSet.GetContourXld(ho_ContEllipse, out hv_Row2, out hv_Col2);
        // Define max value from tuple
        HOperatorSet.TupleMax(hv_Col2, out hv_TupleMax);
        HOperatorSet.TupleFindFirst(hv_Col2, hv_TupleMax, out hv_IndexMax);
        // Define constants:
        hv_HalfH = hv_Height / 2;
        hv_HalfW = hv_Width / 2;
        // Result in px
        hv_output = (-hv_HalfW) + (hv_Col2.TupleSelect(hv_IndexMax));
        // Result in mm
        hv_outputmm = hv_output * 0.001675;
        // Dispose image object
        ho_Image.Dispose();
        ho_Rectangle.Dispose();
        ho_ImageReduced.Dispose();
        ho_Edges.Dispose();
        ho_SelectedContours.Dispose();
        ho_ContEllipse.Dispose();
    }

    public void RunHalcon4()
    {
        diameter2No5S2();
        argumenti.PXvalue = (float)hv_output.D;

        // Chech for infinity Double to float conversion
        if (float.IsPositiveInfinity(argumenti.PXvalue))
        {
            argumenti.PXvalue = float.MaxValue;
        }
        else if (float.IsNegativeInfinity(argumenti.PXvalue))
        {
            argumenti.PXvalue = float.MinValue;
        }

        if (UpdateResult != null)
            UpdateResult(this, argumenti);
    }
}

