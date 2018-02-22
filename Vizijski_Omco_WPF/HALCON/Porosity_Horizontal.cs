using HalconDotNet;

namespace VizijskiSustavWPF.HALCON
{
    public partial class HDevelopExport
    {
        // Main procedure 
        private void porosityHorizonatal()
        {
            // Local iconic variables 
            HObject ho_Image=null, ho_Rectangle=null, ho_ImageReduced=null;
            HObject ho_ImageMean=null, ho_Region=null, ho_ConnectedRegions=null;
            HObject ho_RegionClosing=null, ho_SmallConnection=null;
            HObject ho_ContCircle=null;
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
            HOperatorSet.GenEmptyObj(out ho_ImageReduced);
            HOperatorSet.GenEmptyObj(out ho_ImageMean);
            HOperatorSet.GenEmptyObj(out ho_Region);
            HOperatorSet.GenEmptyObj(out ho_ConnectedRegions);
            HOperatorSet.GenEmptyObj(out ho_RegionClosing);
            HOperatorSet.GenEmptyObj(out ho_SmallConnection);
            HOperatorSet.GenEmptyObj(out ho_ContCircle);
            // Open camera frame
            HOperatorSet.OpenFramegrabber("GigEVision", 0, 0, 0, 0, 0, 0, "default", -1, "default", -1, "false", "default", "GC2591MP_CAM_3", 0, -1, out hv_AcqHandle);
            HOperatorSet.SetFramegrabberParam(hv_AcqHandle, "ExposureTime", 30000.0);
            // Information for PLC that frame is opened
            DetectionHorStart();
            hv_found = 0;
            hv_cnt = 0;
            hv_bol = 0;

            Porositydetectedhor = false;
            while (Porositydetectedhor == false)
            {
                ho_Image.Dispose();
                HOperatorSet.GrabImage(out ho_Image, hv_AcqHandle);
                ho_Rectangle.Dispose();
                HOperatorSet.GenRectangle1(out ho_Rectangle, 520, 200, 1050, 700);
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
                hv_area_min = 1000;
                hv_index = 0;
                HTuple end_val36 = hv_Length;
                HTuple step_val36 = 1;

                for (hv_i=1; hv_i.Continue(end_val36, step_val36); hv_i = hv_i.TupleAdd(step_val36))
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
                    //HOperatorSet.GenCircleContourXld(out ho_ContCircle, hv_Row.TupleSelect(hv_index), 
                    //    hv_Column.TupleSelect(hv_index), 50, 0, 6.28318, "positive", 1);
                    //HOperatorSet.DispObj(ho_ContCircle, hv_porosityWinHandle);
                    hv_found = 0;
                    hv_cnt = 0;
                    Porositydetectedhor = true;
                }
                hv_cnt = hv_cnt+1;
            }

            //HOperatorSet.ClearWindow(hv_porosityWinHandle);
            HOperatorSet.CloseFramegrabber(hv_AcqHandle);
            ho_Image.Dispose();
            ho_Rectangle.Dispose();
            ho_ImageReduced.Dispose();
            ho_ImageMean.Dispose();
            ho_Region.Dispose();
            ho_ConnectedRegions.Dispose();
            ho_RegionClosing.Dispose();
            ho_SmallConnection.Dispose();
            ho_ContCircle.Dispose();
        }

        //public void RunHalcon14(HTuple window)
        //{
        //    hv_porosityWinHandle = window;
        //    porosityHorizonatal();
        //}

        public void RunHalcon14()
        {
            porosityHorizonatal();
        }

    }
}

