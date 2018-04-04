using HalconDotNet;

namespace VizijskiSustavWPF.HALCON
{
    public partial class HDevelopExport
    {
        HTuple hv_TeachRow1 = null;
        HTuple hv_TeachRow2 = null;
        HTuple hv_TeachColumn1 = null;
        HTuple hv_TeachColumn2 = null;

        private void Teachcam3()
        {
            HObject ho_ContRectangle = null;
            HObject ho_Rectangle_diff = null;
            HObject ho_Rectangle_teach = null;
            HObject ho_RegionOut = null;

            // Definition of ROI for Porosity
            
            // Initialize local and output iconic variables 
            HOperatorSet.GenEmptyObj(out ho_Image);
            // Image Acquisition OPEN frame
            HOperatorSet.OpenFramegrabber("GigEVision", 0, 0, 0, 0, 0, 0, "default", -1, "default", -1, "false", "default", "GC2591MP_CAM_3", 0, -1, out hv_AcqHandle);
            HOperatorSet.SetFramegrabberParam(hv_AcqHandle, "ExposureTime", 35000.0);
            HOperatorSet.GrabImageStart(hv_AcqHandle, -1);

            while (Teachloop3 == false)
            {
                ho_Image.Dispose();
                // Live image from CAM3
                HOperatorSet.GrabImageAsync(out ho_Image, hv_AcqHandle, -1);
                HOperatorSet.DispObj(ho_Image, hv_TeachWinHandle3);
                // Draw ROI
                //if(hv_TeachRow1 == null || hv_TeachColumn1 == null || hv_TeachRow2 == null || hv_TeachColumn2 == null)
                //{
                //    HOperatorSet.DrawRectangle1(hv_TeachWinHandle3, out hv_TeachRow1, out hv_TeachColumn1, out hv_TeachRow2, out hv_TeachColumn2);
                //}
                HOperatorSet.SetColor(hv_TeachWinHandle3, "spring green");
                //HOperatorSet.GenRectangle1(out ho_Rectangle_teach, hv_TeachRow1, hv_TeachColumn1, hv_TeachRow2, hv_TeachColumn2);
                //HOperatorSet.GenRectangle1(out ho_Rectangle_diff, hv_TeachRow1 + 10, hv_TeachColumn1 + 10, hv_TeachRow2 - 10, hv_TeachColumn2 -10);

                //HOperatorSet.GenRectangle1(out ho_Rectangle, 520, 200, 1050, 700);

                HOperatorSet.GenRectangle1(out ho_Rectangle_teach, 900, 200, 1460, 700); //aktivan
                //HOperatorSet.GenRectangle1(out ho_Rectangle, 1120, 200, 1630, 700);

                HOperatorSet.GenRectangle1(out ho_Rectangle_diff, 910, 210, 1450, 690); // 10 vise, 10 manje aktivan
                //HOperatorSet.GenRectangle1(out ho_Rectangle_diff, 1130, 210, 1620, 690); // 10 vise, 10 manje

                HOperatorSet.Difference(ho_Rectangle_teach, ho_Rectangle_diff, out ho_RegionOut);
                HOperatorSet.DispObj(ho_RegionOut, hv_TeachWinHandle3);
            }
            // Image Acquisition CLOSE frame
            ho_Image.Dispose();
            HOperatorSet.ClearWindow(hv_TeachWinHandle3);
            HOperatorSet.CloseFramegrabber(hv_AcqHandle);
        }

        public void RunHalcon25(HTuple window)
        {
            hv_TeachWinHandle3 = window;
            HOperatorSet.ClearWindow(hv_TeachWinHandle3);
            Teachcam3();
        }

    }
}

