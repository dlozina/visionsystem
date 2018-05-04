using HalconDotNet;

namespace VizijskiSustavWPF.VisionControl
{
    public partial class HDevelopExport
    {
    
        private void porosityVertical()
        {
            // Local iconic variables 
            HObject ho_Image=null, ho_Rectangle=null, ho_Circle=null;
            HObject ho_RegionUnion=null, ho_ImageReduced=null, ho_ImageMean=null;
            HObject ho_Region=null, ho_ConnectedRegions=null, ho_RegionClosing=null;
            HObject ho_SmallConnection=null, ho_ContCircle=null;
            // Local control variables 
            HTuple hv_AcqHandle = null, hv_found = null;
            HTuple hv_cnt = null, hv_bol = null, hv_porosity_area_px = null;
            HTuple hv_porosity_area_mm = null;
            HTuple hv_UsedThreshold = new HTuple(), hv_Circularity = new HTuple();
            HTuple hv_Area = new HTuple(), hv_Row = new HTuple(), hv_Column = new HTuple();
            HTuple hv_Length = new HTuple(), hv_circ_min = new HTuple();
            HTuple hv_area_min = new HTuple(), hv_index = new HTuple();
            HTuple hv_i = new HTuple();
            // Initialize local and output iconic variables 
            HOperatorSet.GenEmptyObj(out ho_Image);
            HOperatorSet.GenEmptyObj(out ho_Rectangle);
            HOperatorSet.GenEmptyObj(out ho_Circle);
            HOperatorSet.GenEmptyObj(out ho_RegionUnion);
            HOperatorSet.GenEmptyObj(out ho_ImageReduced);
            HOperatorSet.GenEmptyObj(out ho_ImageMean);
            HOperatorSet.GenEmptyObj(out ho_Region);
            HOperatorSet.GenEmptyObj(out ho_ConnectedRegions);
            HOperatorSet.GenEmptyObj(out ho_RegionClosing);
            HOperatorSet.GenEmptyObj(out ho_SmallConnection);
            HOperatorSet.GenEmptyObj(out ho_ContCircle);
            // Wait for CAM4 thread to be closed
            _waitHandleCam2.WaitOne();
            // Close te thread DOOR
            _waitHandleCam2.Reset();
            // Open camera frame
            HOperatorSet.OpenFramegrabber("GigEVision", 0, 0, 0, 0, 0, 0, "default", -1, "default", -1, "false", "default", "GC3851MP_CAM_2", 0, -1, out hv_AcqHandle);
            HOperatorSet.SetFramegrabberParam(hv_AcqHandle, "ExposureTime", 20000.0); // 20000
            // Information for PLC that frame is opened
            DetectionStart();
            hv_found = 0;
            hv_cnt = 0;
            hv_bol = 0;

            // Detection flag
            Porositydetectedver = false;

            while (Porositydetectedver == false)
            {
                ho_Image.Dispose();
                HOperatorSet.GrabImage(out ho_Image, hv_AcqHandle);
                ho_Rectangle.Dispose();
                HOperatorSet.GenRectangle1(out ho_Rectangle, 716, 720, 2200, 1850);
                //ho_Circle.Dispose();
                //HOperatorSet.GenCircle(out ho_Circle, 900, 2000, 600);
                //ho_RegionUnion.Dispose();
                //HOperatorSet.Union2(ho_Rectangle, ho_Circle, out ho_RegionUnion);
                ho_ImageReduced.Dispose();
                HOperatorSet.ReduceDomain(ho_Image, ho_Rectangle, out ho_ImageReduced);
                ho_ImageMean.Dispose();
                HOperatorSet.MeanImage(ho_ImageReduced, out ho_ImageMean, 21, 21);
                ho_Region.Dispose();
                HOperatorSet.BinaryThreshold(ho_ImageMean, out ho_Region, "max_separability", "dark", out hv_UsedThreshold);
                ho_ConnectedRegions.Dispose();
                HOperatorSet.Connection(ho_Region, out ho_ConnectedRegions);
                ho_RegionClosing.Dispose();
                HOperatorSet.ClosingCircle(ho_ConnectedRegions, out ho_RegionClosing, 17);
                ho_SmallConnection.Dispose();
                HOperatorSet.Connection(ho_RegionClosing, out ho_SmallConnection);
                HOperatorSet.Circularity(ho_SmallConnection, out hv_Circularity);
                HOperatorSet.AreaCenter(ho_SmallConnection, out hv_Area, out hv_Row, out hv_Column);
                HOperatorSet.TupleLength(hv_Row, out hv_Length);
                // Criteria for porosity
                hv_circ_min = 0.5;
                hv_area_min = 1500; //1500; //1000;
                hv_index = 0;
                HTuple end_val44 = hv_Length;
                HTuple step_val44 = 1;

                for (hv_i=1; hv_i.Continue(end_val44, step_val44); hv_i = hv_i.TupleAdd(step_val44))
                {
                    if ((int)((new HTuple(((hv_Circularity.TupleSelect(hv_i-1))).TupleGreater(
                            hv_circ_min))).TupleAnd(new HTuple(((hv_Area.TupleSelect(hv_i-1))).TupleGreater(
                            hv_area_min)))) != 0)
                    {
                        hv_index = hv_i-1;
                        hv_found = hv_found+1;
                        hv_bol = 1;
                        break;
                    }
                    else
                    {
                        hv_bol = 0;
                    }
                }

                //HOperatorSet.ClearWindow(hv_porosityWinHandle);
                //HOperatorSet.DispObj(ho_Image, hv_porosityWinHandle);

                if ((int)((new HTuple(hv_found.TupleGreater(0))).TupleAnd(new HTuple(hv_bol.TupleEqual(
                        1)))) != 0)
                {
                    PorosityIsDetected();
                    ho_ContCircle.Dispose();
                    //HOperatorSet.GenCircleContourXld(out ho_ContCircle, hv_Row.TupleSelect(hv_index), hv_Column.TupleSelect(hv_index), 50, 0, 6.28318, "positive", 1);
                    //HOperatorSet.DispObj(ho_ContCircle, hv_porosityWinHandle);
                    hv_found = 0;
                    hv_cnt = 0;
                    Porositydetectedver = true;
                }
                hv_cnt = hv_cnt+1;
            }

            //HOperatorSet.ClearWindow(hv_porosityWinHandle);
            HOperatorSet.CloseFramegrabber(hv_AcqHandle);
            // Open the thread DOOR
            _waitHandleCam2.Set();
            // Dispose all iconic variables
            ho_Image.Dispose();
            ho_Rectangle.Dispose();
            ho_Circle.Dispose();
            ho_RegionUnion.Dispose();
            ho_ImageReduced.Dispose();
            ho_ImageMean.Dispose();
            ho_Region.Dispose();
            ho_ConnectedRegions.Dispose();
            ho_RegionClosing.Dispose();
            ho_SmallConnection.Dispose();
            ho_ContCircle.Dispose();
        }

        //public void RunPorosityVertical(HTuple Window)
        //{
        //    hv_porosityWinHandle = Window;
        //    porosityVertical();
        //}

        public void RunPorosityVertical()
        {
            porosityVertical();
        }
    }
}

