using System;
using HalconDotNet;

public partial class HDevelopExport
{

  // Main procedure 
  private void diameter4No5S1()
  {


        // Local iconic variables
        HObject ho_Image=null, ho_Rectangle=null, ho_ImageReduced=null;
        HObject ho_EdgeAmplitude=null, ho_EdgeDirection=null, ho_ImageConverted=null;
        HObject ho_ImageMedian=null, ho_Regions=null, ho_RegionFillUp1=null;
        HObject ho_Connection=null, ho_SelectedRegions1=null, ho_Contours=null;
        HObject ho_SmoothedContours=null;

        // Local control variables 
        HTuple hv_Width = new HTuple();
        HTuple hv_Height = new HTuple(), hv_SelectNumber = new HTuple();
        HTuple hv_Row = new HTuple(), hv_Col = new HTuple(), hv_TupleMax = new HTuple();
        HTuple hv_IndexMax = new HTuple(), hv_ColumMax = new HTuple();
        HTuple hv_rowToMax0 = new HTuple(), hv_colToMax0 = new HTuple();
        HTuple hv_HalfH = new HTuple(), hv_HalfW = new HTuple();
        HTuple hv_MessageError = new HTuple();

        //************************************************************
        //KOMAD NO. 5 D4 S1
        //************************************************************

        //Camera communication - Open
        openCAMFrame(1500.0);
        //HOperatorSet.SetFramegrabberParam(hv_AcqHandle, "ExposureTime", 1500.0);
        //HOperatorSet.GrabImageStart(hv_AcqHandle, -1);
        HOperatorSet.GrabImageAsync(out ho_Image, hv_AcqHandle, -1);
        //Camera communication - Close
        closeCAMFrame();
        //Find the edge conture
        HOperatorSet.GetImageSize(ho_Image, out hv_Width, out hv_Height);
        HOperatorSet.GenRectangle1(out ho_Rectangle, hv_Height - 2600, (hv_Width / 2) - 120,
            hv_Height - 200, (hv_Width / 2) + 120);
        HOperatorSet.ReduceDomain(ho_Image, ho_Rectangle, out ho_ImageReduced);
        HOperatorSet.SobelDir(ho_ImageReduced, out ho_EdgeAmplitude, out ho_EdgeDirection,
            "sum_sqrt", 13);
        HOperatorSet.ConvertImageType(ho_EdgeDirection, out ho_ImageConverted, "byte");
        HOperatorSet.MedianImage(ho_ImageConverted, out ho_ImageMedian, "square",
            7, 0);
        HOperatorSet.Threshold(ho_ImageMedian, out ho_Regions, 200, 255);
        HOperatorSet.FillUp(ho_Regions, out ho_RegionFillUp1);
        HOperatorSet.Connection(ho_Regions, out ho_Connection);
        HOperatorSet.SelectShape(ho_Connection, out ho_SelectedRegions1, "area",
            "and", 50000, 1500000);
        HOperatorSet.CountObj(ho_SelectedRegions1, out hv_SelectNumber);
        HOperatorSet.GenContourRegionXld(ho_SelectedRegions1, out ho_Contours, "border");
        HOperatorSet.SmoothContoursXld(ho_Contours, out ho_SmoothedContours, 29);
        HOperatorSet.GetContourXld(ho_SmoothedContours, out hv_Row, out hv_Col);

        //* Define max value from tuple
        HOperatorSet.TupleMin(hv_Col, out hv_TupleMax);
        HOperatorSet.TupleFindFirst(hv_Col, hv_TupleMax, out hv_IndexMax);

        //Define constants:
        hv_HalfH = hv_Height / 2;
        hv_HalfW = hv_Width / 2;

        hv_rowToMax0 = hv_Row.TupleSelect(hv_IndexMax);
        hv_colToMax0 = (hv_Col.TupleSelect(hv_IndexMax)) - 13;
        //Result in px
        hv_output = hv_HalfW - hv_colToMax0;
        //Result in mm
        hv_outputmm = hv_output * 0.001675;

        // Dispose image object
        ho_Image.Dispose();
        ho_Rectangle.Dispose();
        ho_ImageReduced.Dispose();
        ho_EdgeAmplitude.Dispose();
        ho_EdgeDirection.Dispose();
        ho_ImageConverted.Dispose();
        ho_ImageMedian.Dispose();
        ho_Regions.Dispose();
        ho_RegionFillUp1.Dispose();
        ho_Connection.Dispose();
        ho_SelectedRegions1.Dispose();
        ho_Contours.Dispose();
        ho_SmoothedContours.Dispose();
  }

  public void RunHalcon7()
  {
        diameter4No5S1();
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

