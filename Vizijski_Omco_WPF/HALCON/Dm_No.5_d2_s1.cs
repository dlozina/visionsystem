//
// File generated by HDevelop for HALCON/.NET (C#) Version 13.0.1.1
//
//  This file is intended to be used with the HDevelopTemplate or
//  HDevelopTemplateWPF projects located under %HALCONEXAMPLES%\c#

using System;
using HalconDotNet;

public partial class HDevelopExport
{

  // Main procedure 
  private void diameter2No5S1()
  {


    // Local iconic variables 
    HObject ho_Image=null, ho_Rectangle=null, ho_ImageReduced=null;
    HObject ho_EdgeAmplitude = null, ho_EdgeDirection = null, ho_ImageConverted = null;
    HObject ho_ImageMedian = null, ho_RegionOpening = null;
    HObject ho_Region=null, ho_RegionFillUp1=null, ho_Connection=null;
    HObject ho_SelectedRegions1=null, ho_Contours=null, ho_SmoothedContours=null;
    HObject ho_Edges=null, ho_Polygons=null, ho_UnionContours=null;
    HObject ho_SelectedContours=null, ho_ContEllipse=null;

    // Local control variables 
    HTuple hv_AcqHandle = new HTuple(), hv_Width = new HTuple();
    HTuple hv_UsedThreshold = new HTuple();
    HTuple hv_Height = new HTuple(), hv_SelectNumber = new HTuple();
    HTuple hv_Row = new HTuple(), hv_Col = new HTuple(), hv_TupleMax = new HTuple();
    HTuple hv_IndexMax = new HTuple(), hv_ColumMax = new HTuple();
    HTuple hv_rowToMax0 = new HTuple(), hv_colToMax0 = new HTuple();
    HTuple hv_HalfH = new HTuple(), hv_HalfW = new HTuple();
    HTuple hv_Row1 = new HTuple(), hv_Column1 = new HTuple();
    HTuple hv_Phi1 = new HTuple(), hv_Radius11 = new HTuple();
    HTuple hv_Radius21 = new HTuple(), hv_StartPhi1 = new HTuple();
    HTuple hv_EndPhi1 = new HTuple(), hv_PointOrder1 = new HTuple();
    HTuple hv_Length = new HTuple(), hv_Row2 = new HTuple();
    HTuple hv_Col2 = new HTuple(), hv_Max2 = new HTuple();
    HTuple hv_IndexMax2 = new HTuple();
    // HTuple hv_output = new HTuple(); 
    // HTuple hv_outputmm = new HTuple();
    HTuple hv_Exception = null, hv_MessageError = new HTuple();

      //************************************************************
      //KOMAD NO. 5 D2 S1
      //************************************************************

      //try
      //{
        //Camera communication - Open
        HOperatorSet.OpenFramegrabber("GigEVision", 0, 0, 0, 0, 0, 0, "default", 
            -1, "default", -1, "false", "default", "GC3851M_CAM_4", 0, -1, out hv_AcqHandle);
        HOperatorSet.SetFramegrabberParam(hv_AcqHandle, "ExposureTime", 3500.0);
        HOperatorSet.GrabImageStart(hv_AcqHandle, -1);
        //ho_Image.Dispose();
        HOperatorSet.GrabImageAsync(out ho_Image, hv_AcqHandle, -1);
        //Camera communication - Close
        HOperatorSet.CloseFramegrabber(hv_AcqHandle);

        //Find the edge conture
        HOperatorSet.GetImageSize(ho_Image, out hv_Width, out hv_Height);
        //ho_Rectangle.Dispose();
        HOperatorSet.GenRectangle1(out ho_Rectangle, hv_Height - 2600, (hv_Width / 2) - 120,
            hv_Height - 200, (hv_Width / 2) + 120);
        //ho_ImageReduced.Dispose();
        HOperatorSet.ReduceDomain(ho_Image, ho_Rectangle, out ho_ImageReduced);
        //* 10-20 za d2
        //ho_Edges.Dispose();
        HOperatorSet.EdgesSubPix(ho_ImageReduced, out ho_Edges, "canny", 1.0, 10,
            20);
        //ho_Polygons.Dispose();
        HOperatorSet.GenPolygonsXld(ho_Edges, out ho_Polygons, "ramer", 2);
        //ho_UnionContours.Dispose();
        HOperatorSet.UnionAdjacentContoursXld(ho_Edges, out ho_UnionContours, 5000,
            10, "attr_keep");
        //ho_SelectedContours.Dispose();
        HOperatorSet.SelectContoursXld(ho_UnionContours, out ho_SelectedContours,
            "contour_length", 500, 50000, -0.5, 0.5);
        HOperatorSet.GetContourXld(ho_SelectedContours, out hv_Row, out hv_Col);
        HOperatorSet.FitEllipseContourXld(ho_SelectedContours, "geometric", -1, 0,
            0, 200, 5, 2, out hv_Row1, out hv_Column1, out hv_Phi1, out hv_Radius11,
            out hv_Radius21, out hv_StartPhi1, out hv_EndPhi1, out hv_PointOrder1);
        //ho_ContEllipse.Dispose();
        HOperatorSet.GenEllipseContourXld(out ho_ContEllipse, hv_Row1, hv_Column1,
            hv_Phi1, hv_Radius11, hv_Radius21, 0, 6.28318, "positive", 1.5);
        HOperatorSet.LengthXld(ho_ContEllipse, out hv_Length);
        HOperatorSet.GetContourXld(ho_ContEllipse, out hv_Row2, out hv_Col2);

        //* Define max value from tuple
        HOperatorSet.TupleMax(hv_Col2, out hv_Max2);
        HOperatorSet.TupleFindFirst(hv_Col2, hv_Max2, out hv_IndexMax2);

        //Define constants:
        hv_HalfH = hv_Height / 2;
        hv_HalfW = hv_Width / 2;
        //Result in px
        hv_output = (-hv_HalfW) + (hv_Col2.TupleSelect(hv_IndexMax2));
        //Result in mm
        hv_outputmm = hv_output * 0.001675;

        //}

        //catch (HalconException HDevExpDefaultException1)
        //{
        //  HDevExpDefaultException1.ToHTuple(out hv_Exception);
        //  //Error handling routine
        //  hv_MessageError = new HTuple(" ERROR: Not able to analize photo, move horizontal axis");
        //}
    }


  public void RunHalcon3()
  {
    
        diameter2No5S1();
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

