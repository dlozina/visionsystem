using HalconDotNet;
using System.Threading;

namespace VizijskiSustavWPF.VisionControl
{
    public partial class HDevelopExport
    {
        // Main procedure 
        private void DiameterAction(HTuple hvDia, HTuple hvSide)
        {
            // Initialize local and output iconic variables 
            HOperatorSet.GenEmptyObj(out ho_Image);
            HOperatorSet.GenEmptyObj(out ho_DerivGauss);
            HOperatorSet.GenEmptyObj(out ho_RegionCrossings);
            HOperatorSet.GenEmptyObj(out ho_Region);
            HOperatorSet.GenEmptyObj(out ho_region_outer);
            HOperatorSet.GenEmptyObj(out ho_contour_outer);
            HOperatorSet.GenEmptyObj(out ho_ContCircle);
            HOperatorSet.GenEmptyObj(out ho_SelectedRegions);
            //
            HOperatorSet.GenEmptyObj(out ho_Rectangle);
            HOperatorSet.GenEmptyObj(out ho_Elipse);
            HOperatorSet.GenEmptyObj(out ho_ROIUnion);
            HOperatorSet.GenEmptyObj(out ho_ROI);
            HOperatorSet.GenEmptyObj(out ho_Contours);

            // Reset Vison output signal
            hv_output = 0;
            // Reset Error detection for PLC
            App.ResetCAM4Error();
            // Wait for CAM4 thread to be closed
            _waitHandleCam4.WaitOne();
            // Close te thread DOOR
            _waitHandleCam4.Reset();
            //try
            //{
                OpenCamFrame();
            // Exposition Change for D3 Diameter
            if ((int) (new HTuple(hvDia.TupleEqual(3))) != 0)
            {
                HOperatorSet.SetFramegrabberParam(hv_AcqHandle, "ExposureTimeAbs", 300.0);
            }
            else
            {
                HOperatorSet.SetFramegrabberParam(hv_AcqHandle, "ExposureTimeAbs", 1000.0);
            }
            //Thread pause to adjust exposition
            Thread.Sleep(1000);
            // Grab image
            HOperatorSet.GrabImageStart(hv_AcqHandle, -1);
            //HOperatorSet.GrabImageAsync(out ho_Image, hv_AcqHandle, -1);
            HOperatorSet.GrabImage(out ho_Image, hv_AcqHandle);
            //}

            //HOperatorSet.CloseFramegrabber(hv_AcqHandle);

            //catch (HalconException HDevExpDefaultExceptionCamera)
            //{
            //    //HDevExpDefaultException1.ToHTuple(out hv_Exception);
            //    //hv_MessageError = new HTuple(" ERROR: Not able to analize photo, move horizontal axis");
            //}

            _waitHandleCam4.Set();
            try
            {
                HOperatorSet.GetImageSize(ho_Image, out hv_Width, out hv_Height);
                //* Define constants and tuples:
                hv_HalfW = hv_Width/2;
                hv_row_len = hv_Height.Clone();
                hv_row_outer = new HTuple();
                hv_col_outer = new HTuple();
                // Old ROI
                if ((int) ((new HTuple(hvDia.TupleEqual(3))).TupleOr(new HTuple(hvDia.TupleEqual(4)))) != 0)
                {
                    if ((int) (new HTuple(hvSide.TupleEqual(2))) != 0)
                    {
                        ho_Rectangle.Dispose();
                        HOperatorSet.GenRectangle1(out ho_Rectangle, 0, hv_HalfW - 100, hv_Height, hv_HalfW + 250);
                        ho_Elipse.Dispose();
                        HOperatorSet.GenEllipse(out ho_Elipse, hv_HalfH, hv_HalfW - 70, (new HTuple(90)).TupleRad(), 1150, 130);
                        ho_ROIUnion.Dispose();
                        HOperatorSet.Union2(ho_Rectangle, ho_Elipse, out ho_ROIUnion);
                        ho_ROI.Dispose();
                        HOperatorSet.Difference(ho_ROIUnion, ho_Elipse, out ho_ROI);
                        HOperatorSet.ReduceDomain(ho_Image, ho_ROI, out ho_Image);
                    }
                    else
                    {
                        ho_Rectangle.Dispose();
                        HOperatorSet.GenRectangle1(out ho_Rectangle, 0, hv_HalfW - 250, hv_Height, hv_HalfW + 100);
                        ho_Elipse.Dispose();
                        HOperatorSet.GenEllipse(out ho_Elipse, hv_HalfH, hv_HalfW + 70, (new HTuple(90)).TupleRad(), 1150, 130);
                        ho_ROIUnion.Dispose();
                        HOperatorSet.Union2(ho_Rectangle, ho_Elipse, out ho_ROIUnion);
                        //* ROI
                        ho_ROI.Dispose();
                        HOperatorSet.Difference(ho_ROIUnion, ho_Elipse, out ho_ROI);
                        HOperatorSet.ReduceDomain(ho_Image, ho_ROI, out ho_Image);
                    }
                }
                else
                {
                    HOperatorSet.GenRectangle1(out ho_Rectangle, 0, hv_HalfW - 250, hv_Height, hv_HalfW + 250);
                    HOperatorSet.ReduceDomain(ho_Image, ho_Rectangle, out ho_Image);
                }
                
                // Derivate for edge detection
                HOperatorSet.DerivateGauss(ho_Image, out ho_DerivGauss, 1, "x");

                //* diameter 2 doesn't have a clean background => different values
                if ((int)(new HTuple(hvDia.TupleEqual(2))).TupleOr(new HTuple(hvDia.TupleEqual(5))) != 0)
                {
                    HOperatorSet.DualThreshold(ho_DerivGauss, out ho_RegionCrossings, 20, 2, 2);
                    HOperatorSet.Union1(ho_RegionCrossings, out ho_Region);
                }

                else
                {
                    HOperatorSet.DualThreshold(ho_DerivGauss, out ho_RegionCrossings, 20, 12, 2);
                    ho_SelectedRegions.Dispose();
                    HOperatorSet.SelectShape(ho_RegionCrossings, out ho_SelectedRegions, "area", "and", 2000, 99999);
                    ho_Region.Dispose();
                    HOperatorSet.Union1(ho_SelectedRegions, out ho_Region);
                }
                //* Retrieve points from detected edges
                HOperatorSet.GetRegionPoints(ho_Region, out hv_Rows, out hv_Cols);
                //* Side 2 => upper side closer to probe
                if ((int)(new HTuple(hvSide.TupleEqual(2))) != 0)
                {
                    //* Extract the points which define the outer or inner edge
                    if ((int)((new HTuple(hvDia.TupleEqual(3))).TupleOr(new HTuple(hvDia.TupleEqual(4))).TupleOr(new HTuple(hvDia.TupleEqual(5)))) != 0)
                    {
                        HTuple endVal35 = hv_row_len;
                        HTuple stepVal35 = 1;
                        for (hv_i=1; hv_i.Continue(endVal35, stepVal35); hv_i = hv_i.TupleAdd(stepVal35))
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

                            HOperatorSet.TupleLength(hv_Indices, out _);
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

                    else if ((int)((new HTuple(hvDia.TupleEqual(1))).TupleOr(new HTuple(hvDia.TupleEqual(2)))) != 0)
                    {
                        HTuple endVal50 = hv_row_len;
                        HTuple stepVal50 = 1;
                        for (hv_i=1; hv_i.Continue(endVal50, stepVal50); hv_i = hv_i.TupleAdd(stepVal50))
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

                            HOperatorSet.TupleLength(hv_Indices, out _);
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
                    HOperatorSet.GenRegionPoints(out ho_region_outer, hv_row_outer, hv_col_outer);
                    HOperatorSet.GenContourPolygonXld(out ho_contour_outer, hv_row_outer, hv_col_outer);
                    HOperatorSet.FitCircleContourXld(ho_contour_outer, "geotukey", -1, 0, 0, 3, 2, out hv_Row, out hv_Col, out hv_Radius, out _, out _, out _);
                    HOperatorSet.GenCircleContourXld(out ho_ContCircle, hv_Row, hv_Col, hv_Radius, 0, 6.28318, "positive", 1);
                    HOperatorSet.GetContourXld(ho_ContCircle, out hv_Row, out hv_Col);

                    //* find the maximum of the estimated circle
                    HOperatorSet.TupleMax(hv_Col, out hv_TupleMax);
                    HOperatorSet.TupleFindFirst(hv_Col, hv_TupleMax, out hv_IndexMax);

                    //* calculate pixel and mm outputs side 2
                    hv_colToMax0 = hv_Col.TupleSelect(hv_IndexMax);
                    hv_output = (-hv_HalfW)+hv_colToMax0;

                    // Display Result on side 2
                    if (hv_ExpDefaultWinHandle.Length != 0)
                    {
                        HOperatorSet.DispObj(ho_Image, hv_ExpDefaultWinHandle);
                        HOperatorSet.DispObj(ho_ContCircle, hv_ExpDefaultWinHandle);
                    }
                }

                //* Side 1 => side closer to vertical moving axis
                if ((int)(new HTuple(hvSide.TupleEqual(1))) != 0)
                {
                    //* Extract the points which define the outer or inner edge
                    if ((int)((new HTuple(hvDia.TupleEqual(3))).TupleOr(new HTuple(hvDia.TupleEqual(4))).TupleOr(new HTuple(hvDia.TupleEqual(5)))) != 0)
                    {
                        HTuple endVal87 = hv_row_len;
                        HTuple stepVal87 = 1;
                        for (hv_i=1; hv_i.Continue(endVal87, stepVal87); hv_i = hv_i.TupleAdd(stepVal87))
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

                            HOperatorSet.TupleLength(hv_Indices, out _);
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
                    else if ((int)((new HTuple(hvDia.TupleEqual(1))).TupleOr(new HTuple(hvDia.TupleEqual(2)))) != 0)
                    {
                        HTuple endVal102 = hv_row_len;
                        HTuple stepVal102 = 1;
                        for (hv_i=1; hv_i.Continue(endVal102, stepVal102); hv_i = hv_i.TupleAdd(stepVal102))
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

                            HOperatorSet.TupleLength(hv_Indices, out _);
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
                    HOperatorSet.GenRegionPoints(out ho_region_outer, hv_row_outer, hv_col_outer);
                    HOperatorSet.GenContourPolygonXld(out ho_contour_outer, hv_row_outer, hv_col_outer);
                    HOperatorSet.FitCircleContourXld(ho_contour_outer, "geotukey", -1, 0, 0, 3, 2, out hv_Row, out hv_Col, out hv_Radius, out _, out _, out _);
                    HOperatorSet.GenCircleContourXld(out ho_ContCircle, hv_Row, hv_Col, hv_Radius, 0, 6.28318, "positive", 1);
                    HOperatorSet.GetContourXld(ho_ContCircle, out hv_Row, out hv_Col);

                    //* find the minimum of the estimated circle
                    HOperatorSet.TupleMin(hv_Col, out hv_TupleMin);
                    HOperatorSet.TupleFindFirst(hv_Col, hv_TupleMin, out hv_IndexMin);

                    //* calculate pixel and mm outputs side 1
                    hv_colToMin0 = hv_Col.TupleSelect(hv_IndexMin);
                    hv_output = hv_HalfW-hv_colToMin0;

                    // Display Result on side 1
                    if (hv_ExpDefaultWinHandle.Length != 0)
                    {
                        HOperatorSet.DispObj(ho_Image, hv_ExpDefaultWinHandle);
                        HOperatorSet.DispObj(ho_ContCircle, hv_ExpDefaultWinHandle);
                    }
                }
            } //TRY

            catch (HalconException hDevExceptionVision)
            {
            // Error detection for PLC
            App.AnaliseCAM4Error();
            //    //HOperatorSet.SetColor(hv_TeachWinHandle, "spring green");
            //    HOperatorSet.SetTposition(hv_TeachWinHandle, hv_Height/2, hv_Width/4);
            //    HOperatorSet.WriteString(hv_TeachWinHandle, "NIJE PRONADEN RUB KOMADA! PONOVITI AKCIJU ILI PRESKOCITI RUB");

            }
            ho_Image.Dispose();
            ho_DerivGauss.Dispose();
            ho_RegionCrossings.Dispose();
            ho_Region.Dispose();
            ho_region_outer.Dispose();
            ho_contour_outer.Dispose();
            ho_ContCircle.Dispose();
        }

        // D1 S1 Call
        public void RunHalcon1(HTuple window)
        {
            hv_ExpDefaultWinHandle = window;
            if (hv_ExpDefaultWinHandle.Length != 0)
            {
                HOperatorSet.ClearWindow(hv_ExpDefaultWinHandle);
            }
            DiameterAction(1, 1);
            if (hv_output.Length != 0 )
            {
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
        // D1 S2 Call
        public void RunHalcon2(HTuple window)
        {
            hv_ExpDefaultWinHandle = window;
            if (hv_ExpDefaultWinHandle.Length != 0)
            {
                HOperatorSet.ClearWindow(hv_ExpDefaultWinHandle);
            }
            DiameterAction(1, 2);
            if (hv_output.Length != 0)
            {
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
        // D2 S1 Call
        public void RunHalcon3(HTuple window)
        {
            hv_ExpDefaultWinHandle = window;
            if (hv_ExpDefaultWinHandle.Length != 0)
            {
                HOperatorSet.ClearWindow(hv_ExpDefaultWinHandle);
            }
            DiameterAction(2, 1);
            if (hv_output.Length != 0)
            {
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
        // D2 S2 Call
        public void RunHalcon4(HTuple window)
        {
            hv_ExpDefaultWinHandle = window;
            if (hv_ExpDefaultWinHandle.Length != 0)
            {
                HOperatorSet.ClearWindow(hv_ExpDefaultWinHandle);
            }
            DiameterAction(2, 2);
            if (hv_output.Length != 0)
            {
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
        // D3 S1 Call
        public void RunHalcon5(HTuple window)
        {
            hv_ExpDefaultWinHandle = window;
            if (hv_ExpDefaultWinHandle.Length != 0)
            {
                HOperatorSet.ClearWindow(hv_ExpDefaultWinHandle);
            }
            DiameterAction(3, 1);
            if (hv_output.Length != 0)
            {
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
        // D3 S2 Call
        public void RunHalcon6(HTuple window)
        {
            hv_ExpDefaultWinHandle = window;
            if (hv_ExpDefaultWinHandle.Length != 0)
            {
                HOperatorSet.ClearWindow(hv_ExpDefaultWinHandle);
            }
            DiameterAction(3, 2);
            if (hv_output.Length != 0)
            {
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
        // D4 S1 Call
        public void RunHalcon7(HTuple window)
        {
            hv_ExpDefaultWinHandle = window;
            if (hv_ExpDefaultWinHandle.Length != 0)
            {
                HOperatorSet.ClearWindow(hv_ExpDefaultWinHandle);
            }
            DiameterAction(4, 1);
            if (hv_output.Length != 0)
            {
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
        // D4 S2 Call
        public void RunHalcon8(HTuple window)
        {
            hv_ExpDefaultWinHandle = window;
            if (hv_ExpDefaultWinHandle.Length != 0)
            {
                HOperatorSet.ClearWindow(hv_ExpDefaultWinHandle);
            }
            DiameterAction(4, 2);
            if (hv_output.Length != 0)
            {
                argumenti.PXvalue = (float)hv_output.D;
                //hv_output = 0;
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
        // D5 S1 Call
        public void RunDia5side1(HTuple window)
        {
            hv_ExpDefaultWinHandle = window;
            if (hv_ExpDefaultWinHandle.Length != 0)
            {
                HOperatorSet.ClearWindow(hv_ExpDefaultWinHandle);
            }
            DiameterAction(5, 1);
            if (hv_output.Length != 0)
            {
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
        // D5 S2 Call
        public void RunDia5side2(HTuple window)
        {
            hv_ExpDefaultWinHandle = window;
            if (hv_ExpDefaultWinHandle.Length != 0)
            {
                HOperatorSet.ClearWindow(hv_ExpDefaultWinHandle);
            }
            DiameterAction(5, 2);
            if (hv_output.Length != 0)
            {
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
    }
}

