using System;
using HalconDotNet;

public partial class HDevelopExport
{
  public HTuple hv_ExpDefaultWinHandle;

  // Main procedure 
  private void action()
  {
    // Local iconic variables 
    HObject ho_Image, ho_DerivGauss=null, ho_RegionCrossings=null;
    HObject ho_Region=null, ho_region_outer=null, ho_contour_outer=null;
    HObject ho_ContCircle=null;
    // Local control variables 
    HTuple hv_AcqHandle = null, hv_side = null;
    HTuple hv_dia = null, hv_Width = new HTuple(), hv_Height = new HTuple();
    HTuple hv_HalfH = new HTuple(), hv_HalfW = new HTuple();
    HTuple hv_row_len = new HTuple(), hv_row_outer = new HTuple();
    HTuple hv_col_outer = new HTuple(), hv_Rows = new HTuple();
    HTuple hv_Cols = new HTuple(), hv_i = new HTuple(), hv_Indices = new HTuple();
    HTuple hv_Length = new HTuple(), hv_col_min = new HTuple();
    HTuple hv_indice_min = new HTuple(), hv_col_max = new HTuple();
    HTuple hv_indice_max = new HTuple(), hv_Row = new HTuple();
    HTuple hv_Col = new HTuple(), hv_Radius = new HTuple();
    HTuple hv_StartPhi = new HTuple(), hv_EndPhi = new HTuple();
    HTuple hv_PointOrder = new HTuple(), hv_TupleMax = new HTuple();
    HTuple hv_IndexMax = new HTuple(), hv_colToMax0 = new HTuple();
    HTuple hv_output = new HTuple(), hv_outputmm = new HTuple();
    HTuple hv_TupleMin = new HTuple(), hv_IndexMin = new HTuple();
    HTuple hv_colToMin0 = new HTuple(), hv_Exception = null;
    HTuple hv_MessageError = new HTuple();
    // Initialize local and output iconic variables 
    HOperatorSet.GenEmptyObj(out ho_Image);
    HOperatorSet.GenEmptyObj(out ho_DerivGauss);
    HOperatorSet.GenEmptyObj(out ho_RegionCrossings);
    HOperatorSet.GenEmptyObj(out ho_Region);
    HOperatorSet.GenEmptyObj(out ho_region_outer);
    HOperatorSet.GenEmptyObj(out ho_contour_outer);
    HOperatorSet.GenEmptyObj(out ho_ContCircle);
    try
    {
      HOperatorSet.OpenFramegrabber("GigEVision", 0, 0, 0, 0, 0, 0, "default", -1, 
          "default", -1, "false", "default", "GC3851M_CAM_4", 0, -1, out hv_AcqHandle);
      HOperatorSet.SetFramegrabberParam(hv_AcqHandle, "ExposureTime", 3500.0);

      hv_side = 2;
      hv_dia = 1;
      HOperatorSet.GrabImageStart(hv_AcqHandle, -1);
      ho_Image.Dispose();
      HOperatorSet.GrabImageAsync(out ho_Image, hv_AcqHandle, -1);
      try
      {

        HOperatorSet.GetImageSize(ho_Image, out hv_Width, out hv_Height);
        //* Define constants and tuples:
        hv_HalfH = hv_Height/2;
        hv_HalfW = hv_Width/2;
        hv_row_len = hv_Height.Clone();
        hv_row_outer = new HTuple();
        hv_col_outer = new HTuple();

        //* Edge detection
        ho_DerivGauss.Dispose();
        HOperatorSet.DerivateGauss(ho_Image, out ho_DerivGauss, 1, "x");
        //* diameter 2 doesn't have a clean background => different values
        if ((int)(new HTuple(hv_dia.TupleEqual(2))) != 0)
        {
          ho_RegionCrossings.Dispose();
          HOperatorSet.DualThreshold(ho_DerivGauss, out ho_RegionCrossings, 20, 2, 
              2);
          ho_Region.Dispose();
          HOperatorSet.Union1(ho_RegionCrossings, out ho_Region);
        }
        else
        {
          ho_RegionCrossings.Dispose();
          HOperatorSet.DualThreshold(ho_DerivGauss, out ho_RegionCrossings, 20, 12, 
              2);
          ho_Region.Dispose();
          HOperatorSet.Union1(ho_RegionCrossings, out ho_Region);
        }

        //* Retrieve points from detected edges
        HOperatorSet.GetRegionPoints(ho_Region, out hv_Rows, out hv_Cols);

        //* Side 1 => upper side closer to probe
        if ((int)(new HTuple(hv_side.TupleEqual(1))) != 0)
        {
          //* Extract the points which define the outer or inner edge
          if ((int)((new HTuple(hv_dia.TupleEqual(3))).TupleOr(new HTuple(hv_dia.TupleEqual(
              4)))) != 0)
          {
            HTuple end_val35 = hv_row_len;
            HTuple step_val35 = 1;
            for (hv_i=1; hv_i.Continue(end_val35, step_val35); hv_i = hv_i.TupleAdd(step_val35))
            {
              HOperatorSet.TupleFind(hv_Rows, hv_i-1, out hv_Indices);
              //* if none point exists in this row replace it with an incremental interpolation
              if ((int)(new HTuple(hv_Indices.TupleEqual(-1))) != 0)
              {
                if (hv_row_outer == null)
                  hv_row_outer = new HTuple();
                hv_row_outer[hv_i-1] = (hv_row_outer.TupleSelect(hv_i-2))+1;
                if (hv_col_outer == null)
                  hv_col_outer = new HTuple();
                hv_col_outer[hv_i-1] = hv_col_outer.TupleSelect(hv_i-2);
                continue;
              }
              HOperatorSet.TupleLength(hv_Indices, out hv_Length);
              HOperatorSet.TupleMin(hv_Cols.TupleSelect(hv_Indices), out hv_col_min);
              HOperatorSet.TupleFind(hv_Cols.TupleSelect(hv_Indices), hv_col_min, 
                  out hv_indice_min);
              if (hv_row_outer == null)
                hv_row_outer = new HTuple();
              hv_row_outer[hv_i-1] = hv_Rows.TupleSelect(hv_Indices.TupleSelect(hv_indice_min));
              if (hv_col_outer == null)
                hv_col_outer = new HTuple();
              hv_col_outer[hv_i-1] = hv_Cols.TupleSelect(hv_Indices.TupleSelect(hv_indice_min));
            }
          }
          else if ((int)((new HTuple(hv_dia.TupleEqual(1))).TupleOr(new HTuple(hv_dia.TupleEqual(
              2)))) != 0)
          {
            HTuple end_val50 = hv_row_len;
            HTuple step_val50 = 1;
            for (hv_i=1; hv_i.Continue(end_val50, step_val50); hv_i = hv_i.TupleAdd(step_val50))
            {
              HOperatorSet.TupleFind(hv_Rows, hv_i-1, out hv_Indices);
              //* if none point exists in this row replace it with an incremental interpolation
              if ((int)(new HTuple(hv_Indices.TupleEqual(-1))) != 0)
              {
                if (hv_row_outer == null)
                  hv_row_outer = new HTuple();
                hv_row_outer[hv_i-1] = (hv_row_outer.TupleSelect(hv_i-2))+1;
                if (hv_col_outer == null)
                  hv_col_outer = new HTuple();
                hv_col_outer[hv_i-1] = hv_col_outer.TupleSelect(hv_i-2);
                continue;
              }
              HOperatorSet.TupleLength(hv_Indices, out hv_Length);
              HOperatorSet.TupleMax(hv_Cols.TupleSelect(hv_Indices), out hv_col_max);
              HOperatorSet.TupleFind(hv_Cols.TupleSelect(hv_Indices), hv_col_max, 
                  out hv_indice_max);
              if (hv_row_outer == null)
                hv_row_outer = new HTuple();
              hv_row_outer[hv_i-1] = hv_Rows.TupleSelect(hv_Indices.TupleSelect(hv_indice_max));
              if (hv_col_outer == null)
                hv_col_outer = new HTuple();
              hv_col_outer[hv_i-1] = hv_Cols.TupleSelect(hv_Indices.TupleSelect(hv_indice_max));
            }
          }

          //* retrieve the outer or inner edge points + cirlce fitting
          ho_region_outer.Dispose();
          HOperatorSet.GenRegionPoints(out ho_region_outer, hv_row_outer, hv_col_outer);
          ho_contour_outer.Dispose();
          HOperatorSet.GenContourPolygonXld(out ho_contour_outer, hv_row_outer, hv_col_outer);
          HOperatorSet.FitCircleContourXld(ho_contour_outer, "geotukey", -1, 0, 0, 
              3, 2, out hv_Row, out hv_Col, out hv_Radius, out hv_StartPhi, out hv_EndPhi, 
              out hv_PointOrder);
          ho_ContCircle.Dispose();
          HOperatorSet.GenCircleContourXld(out ho_ContCircle, hv_Row, hv_Col, hv_Radius, 
              0, 6.28318, "positive", 1);
          HOperatorSet.GetContourXld(ho_ContCircle, out hv_Row, out hv_Col);

          //* find the maximum of the estimated circle
          HOperatorSet.TupleMax(hv_Col, out hv_TupleMax);
          HOperatorSet.TupleFindFirst(hv_Col, hv_TupleMax, out hv_IndexMax);

          //* calculate pixel and mm outputs
          hv_colToMax0 = hv_Col.TupleSelect(hv_IndexMax);
          hv_output = (-hv_HalfW)+hv_colToMax0;
          hv_outputmm = hv_output*0.001675;
        }

        //* Side 2 => side closer to vertical moving axis
        if ((int)(new HTuple(hv_side.TupleEqual(2))) != 0)
        {
          //* Extract the points which define the outer or inner edge
          if ((int)((new HTuple(hv_dia.TupleEqual(3))).TupleOr(new HTuple(hv_dia.TupleEqual(
              4)))) != 0)
          {
            HTuple end_val87 = hv_row_len;
            HTuple step_val87 = 1;
            for (hv_i=1; hv_i.Continue(end_val87, step_val87); hv_i = hv_i.TupleAdd(step_val87))
            {
              HOperatorSet.TupleFind(hv_Rows, hv_i-1, out hv_Indices);
              //* if none point exists in this row replace it with an incremental interpolation
              if ((int)(new HTuple(hv_Indices.TupleEqual(-1))) != 0)
              {
                if (hv_row_outer == null)
                  hv_row_outer = new HTuple();
                hv_row_outer[hv_i-1] = (hv_row_outer.TupleSelect(hv_i-2))+1;
                if (hv_col_outer == null)
                  hv_col_outer = new HTuple();
                hv_col_outer[hv_i-1] = hv_col_outer.TupleSelect(hv_i-2);
                continue;
              }
              HOperatorSet.TupleLength(hv_Indices, out hv_Length);
              HOperatorSet.TupleMax(hv_Cols.TupleSelect(hv_Indices), out hv_col_max);
              HOperatorSet.TupleFind(hv_Cols.TupleSelect(hv_Indices), hv_col_max, 
                  out hv_indice_max);
              if (hv_row_outer == null)
                hv_row_outer = new HTuple();
              hv_row_outer[hv_i-1] = hv_Rows.TupleSelect(hv_Indices.TupleSelect(hv_indice_max));
              if (hv_col_outer == null)
                hv_col_outer = new HTuple();
              hv_col_outer[hv_i-1] = hv_Cols.TupleSelect(hv_Indices.TupleSelect(hv_indice_max));
            }
          }
          else if ((int)((new HTuple(hv_dia.TupleEqual(1))).TupleOr(new HTuple(hv_dia.TupleEqual(
              2)))) != 0)
          {
            HTuple end_val102 = hv_row_len;
            HTuple step_val102 = 1;
            for (hv_i=1; hv_i.Continue(end_val102, step_val102); hv_i = hv_i.TupleAdd(step_val102))
            {
              HOperatorSet.TupleFind(hv_Rows, hv_i-1, out hv_Indices);
              //* if none point exists in this row replace it with an incremental interpolation
              if ((int)(new HTuple(hv_Indices.TupleEqual(-1))) != 0)
              {
                if (hv_row_outer == null)
                  hv_row_outer = new HTuple();
                hv_row_outer[hv_i-1] = (hv_row_outer.TupleSelect(hv_i-2))+1;
                if (hv_col_outer == null)
                  hv_col_outer = new HTuple();
                hv_col_outer[hv_i-1] = hv_col_outer.TupleSelect(hv_i-2);
                continue;
              }
              HOperatorSet.TupleLength(hv_Indices, out hv_Length);
              HOperatorSet.TupleMin(hv_Cols.TupleSelect(hv_Indices), out hv_col_min);
              HOperatorSet.TupleFind(hv_Cols.TupleSelect(hv_Indices), hv_col_min, 
                  out hv_indice_min);
              if (hv_row_outer == null)
                hv_row_outer = new HTuple();
              hv_row_outer[hv_i-1] = hv_Rows.TupleSelect(hv_Indices.TupleSelect(hv_indice_min));
              if (hv_col_outer == null)
                hv_col_outer = new HTuple();
              hv_col_outer[hv_i-1] = hv_Cols.TupleSelect(hv_Indices.TupleSelect(hv_indice_min));
            }
          }

          //* retrieve the outer or inner edge points + cirlce fitting
          ho_region_outer.Dispose();
          HOperatorSet.GenRegionPoints(out ho_region_outer, hv_row_outer, hv_col_outer);
          ho_contour_outer.Dispose();
          HOperatorSet.GenContourPolygonXld(out ho_contour_outer, hv_row_outer, hv_col_outer);
          HOperatorSet.FitCircleContourXld(ho_contour_outer, "geotukey", -1, 0, 0, 
              3, 2, out hv_Row, out hv_Col, out hv_Radius, out hv_StartPhi, out hv_EndPhi, 
              out hv_PointOrder);
          ho_ContCircle.Dispose();
          HOperatorSet.GenCircleContourXld(out ho_ContCircle, hv_Row, hv_Col, hv_Radius, 
              0, 6.28318, "positive", 1);
          HOperatorSet.GetContourXld(ho_ContCircle, out hv_Row, out hv_Col);

          //* find the minimum of the estimated circle
          HOperatorSet.TupleMin(hv_Col, out hv_TupleMin);
          HOperatorSet.TupleFindFirst(hv_Col, hv_TupleMin, out hv_IndexMin);

          //* calculate pixel and mm outputs
          hv_colToMin0 = hv_Col.TupleSelect(hv_IndexMin);
          hv_output = hv_HalfW-hv_colToMin0;
          hv_outputmm = hv_output*0.001675;
        }

      }
      // catch (Exception) 
      catch (HalconException HDevExpDefaultException1)
      {
        HDevExpDefaultException1.ToHTuple(out hv_Exception);
        hv_MessageError = new HTuple(" ERROR: Not able to analize photo, move horizontal axis");
      }
      HOperatorSet.CloseFramegrabber(hv_AcqHandle);

    }
    catch (HalconException HDevExpDefaultException)
    {
      ho_Image.Dispose();
      ho_DerivGauss.Dispose();
      ho_RegionCrossings.Dispose();
      ho_Region.Dispose();
      ho_region_outer.Dispose();
      ho_contour_outer.Dispose();
      ho_ContCircle.Dispose();

      throw HDevExpDefaultException;
    }
    ho_Image.Dispose();
    ho_DerivGauss.Dispose();
    ho_RegionCrossings.Dispose();
    ho_Region.Dispose();
    ho_region_outer.Dispose();
    ho_contour_outer.Dispose();
    ho_ContCircle.Dispose();

  }

  public void InitHalcon()
  {
    // Default settings used in HDevelop 
    HOperatorSet.SetSystem("width", 512);
    HOperatorSet.SetSystem("height", 512);
  }

  public void RunHalcon(HTuple Window)
  {
    hv_ExpDefaultWinHandle = Window;
    action();
  }

}

