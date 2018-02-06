using HalconDotNet;

namespace VizijskiSustavWPF.HALCON
{
    public partial class HDevelopExport
    {

        private void Teachcam2()
        {
            HObject ho_ContRectangle = null;
            HObject ho_Rectangle_diff = null;
            HObject ho_RegionOut = null;
            bool first = true;
            HTuple ho_row_1 = new HTuple(), ho_col_1 = new HTuple(), ho_row_2 = new HTuple(), ho_col_2 = new HTuple(); 
            // Initialize local and output iconic variables 
            HOperatorSet.GenEmptyObj(out ho_Image);

            // Image Acquisition OPEN frame
            HOperatorSet.OpenFramegrabber("GigEVision", 0, 0, 0, 0, 0, 0, "default", -1, 
                "default", -1, "false", "default", "GC3851MP_CAM_2", 0, -1, out hv_AcqHandle);
            HOperatorSet.SetFramegrabberParam(hv_AcqHandle, "ExposureTime", 15000.0);
            HOperatorSet.GrabImageStart(hv_AcqHandle, -1);
            
            while (Teachloop2 == false)
            {
                ho_Image.Dispose();
                // Live image from CAM2
                HOperatorSet.GrabImageAsync(out ho_Image, hv_AcqHandle, -1);
                HOperatorSet.DispObj(ho_Image, hv_TeachWinHandle2);
                HOperatorSet.SetColor(hv_TeachWinHandle2, "spring green");
                //if (first)
                //{
                //    HOperatorSet.DrawRectangle1(hv_TeachWinHandle2, out ho_row_1, out ho_col_1, out ho_row_2, out ho_col_2);
                //    first = false;
                //}
                //HOperatorSet.GenRectangle1(out ho_Rectangle, ho_row_1, ho_col_1, ho_row_2, ho_col_2);
                HOperatorSet.GenRectangle1(out ho_Rectangle, 716, 720, 2200, 1850);
                HOperatorSet.GenRectangle1(out ho_Rectangle_diff, 726, 730, 2190, 1840);
                HOperatorSet.Difference(ho_Rectangle, ho_Rectangle_diff, out ho_RegionOut);
                HOperatorSet.DispObj(ho_RegionOut, hv_TeachWinHandle2);
            }
            // Image Acquisition CLOSE frame
            ho_Image.Dispose();
            HOperatorSet.ClearWindow(hv_TeachWinHandle2);
            HOperatorSet.CloseFramegrabber(hv_AcqHandle);
        }

        public void RunHalcon24(HTuple window)
        {
            hv_TeachWinHandle2 = window;
            HOperatorSet.ClearWindow(hv_TeachWinHandle2);
            Teachcam2();
        }
    }
}

