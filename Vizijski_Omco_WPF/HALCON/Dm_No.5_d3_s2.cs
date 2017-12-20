using System;
using HalconDotNet;

public partial class HDevelopExport
{

  // Main procedure 
  private void diameter3No5S2()
  {


        // Local iconic variables 
        HObject ho_Image=null, ho_Rectangle=null, ho_ImageReduced=null;
        HObject ho_Regions=null, ho_RegionFillUp1=null, ho_Connection=null;
        HObject ho_SelectedRegions1=null, ho_Contours=null, ho_SmoothedContours=null;
        HObject ho_ContEllipse=null;
        // Local control variables 
        HTuple hv_Width = new HTuple();
        HTuple hv_Height = new HTuple(), hv_SelectNumber = new HTuple();
        HTuple hv_Row = new HTuple(), hv_Col = new HTuple(), hv_TupleMin = new HTuple();
        HTuple hv_IndexMin = new HTuple(), hv_ColumMin = new HTuple();
        HTuple hv_rowToMin0 = new HTuple(), hv_colToMin0 = new HTuple();
        HTuple hv_HalfH = new HTuple(), hv_HalfW = new HTuple();
        HTuple hv_Row1 = new HTuple(), hv_Column1 = new HTuple();
        HTuple hv_Phi1 = new HTuple(), hv_Radius11 = new HTuple();
        HTuple hv_Radius21 = new HTuple(), hv_StartPhi1 = new HTuple();
        HTuple hv_EndPhi1 = new HTuple(), hv_PointOrder1 = new HTuple();
        HTuple hv_Length = new HTuple(), hv_Row2 = new HTuple();
        HTuple hv_Col2 = new HTuple(), hv_Max2 = new HTuple();
        HTuple hv_TupleMin2 = new HTuple(), hv_IndexMin2 = new HTuple();
        HTuple hv_IndexMax2 = new HTuple();
        HTuple hv_UsedThreshold = new HTuple();
        HTuple hv_MessageError = new HTuple();

        //************************************************************
        //KOMAD NO. 5 D3 S2
        //************************************************************

        openCAMFrame(2500.0);
        //HOperatorSet.SetFramegrabberParam(hv_AcqHandle, "ExposureTime", 2500.0);
        HOperatorSet.GrabImageAsync(out ho_Image, hv_AcqHandle, -1);
        //Camera communication - Close
        closeCAMFrame();
        //HOperatorSet.CloseFramegrabber(hv_AcqHandle);
        HOperatorSet.GetImageSize(ho_Image, out hv_Width, out hv_Height);
        HOperatorSet.GenRectangle1(out ho_Rectangle, hv_Height - 2600, (hv_Width / 2) - 120,
            hv_Height - 200, (hv_Width / 2) + 120);
        HOperatorSet.ReduceDomain(ho_Image, ho_Rectangle, out ho_ImageReduced);
        HOperatorSet.BinaryThreshold(ho_ImageReduced, out ho_Regions, "max_separability",
              "dark", out hv_UsedThreshold);
        HOperatorSet.OpeningCircle(ho_Regions, out ho_Regions, 37);
        HOperatorSet.FillUp(ho_Regions, out ho_RegionFillUp1);
        HOperatorSet.Connection(ho_RegionFillUp1, out ho_Connection);
        HOperatorSet.SelectShape(ho_Connection, out ho_SelectedRegions1, (new HTuple("area")).TupleConcat(
            "row"), "and", (new HTuple(200000)).TupleConcat(1300), (new HTuple(500000)).TupleConcat(
            1380));
        HOperatorSet.CountObj(ho_SelectedRegions1, out hv_SelectNumber);
        HOperatorSet.GenContourRegionXld(ho_SelectedRegions1, out ho_Contours, "border");
        HOperatorSet.SmoothContoursXld(ho_Contours, out ho_SmoothedContours, 29);
        HOperatorSet.GetContourXld(ho_SmoothedContours, out hv_Row, out hv_Col);
        HOperatorSet.FitEllipseContourXld(ho_SmoothedContours, "geotukey", -1, 0,
            0, 200, 5, 2, out hv_Row1, out hv_Column1, out hv_Phi1, out hv_Radius11,
            out hv_Radius21, out hv_StartPhi1, out hv_EndPhi1, out hv_PointOrder1);
        HOperatorSet.GenEllipseContourXld(out ho_ContEllipse, hv_Row1, hv_Column1,
            hv_Phi1, hv_Radius11, hv_Radius21, 0, 6.28318, "positive", 1.5);
        HOperatorSet.GetContourXld(ho_ContEllipse, out hv_Row2, out hv_Col2);

        //* Define min value from tuple
        HOperatorSet.TupleMax(hv_Col2, out hv_TupleMin);
        HOperatorSet.TupleFindFirst(hv_Col2, hv_TupleMin, out hv_IndexMin);

        //Define constants:
        hv_HalfH = hv_Height / 2;
        hv_HalfW = hv_Width / 2;
        //Result in px
        hv_output = (-hv_HalfW) + (hv_Col2.TupleSelect(hv_IndexMin));
        //Result in mm
        hv_outputmm = hv_output * 0.001675;

        // Dispose image object
        ho_Image.Dispose();
        ho_Rectangle.Dispose();
        ho_ImageReduced.Dispose();
        ho_Regions.Dispose();
        ho_RegionFillUp1.Dispose();
        ho_Connection.Dispose();
        ho_SelectedRegions1.Dispose();
        ho_Contours.Dispose();
        ho_SmoothedContours.Dispose();
        ho_ContEllipse.Dispose();

    }

  public void RunHalcon6()
  {
        diameter3No5S2();
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

