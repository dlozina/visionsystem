using HalconDotNet;

namespace VizijskiSustavWPF.HALCON
{
    public partial class HDevelopExport
    {

        private void Livecam2()
        {
            // Initialize local and output iconic variables 
            HOperatorSet.GenEmptyObj(out ho_Image);
            // Image Acquisition OPEN frame
            HOperatorSet.OpenFramegrabber("GigEVision", 0, 0, 0, 0, 0, 0, "default", -1, "default", -1, "false", "default", "GC3851MP_CAM_2", 0, -1, out hv_AcqHandle);
            HOperatorSet.SetFramegrabberParam(hv_AcqHandle, "ExposureTime", 10000.0);
            HOperatorSet.GrabImageStart(hv_AcqHandle, -1);
    
            while (Exitloop2 == false)
            {
                ho_Image.Dispose();
                // Live image from CAM2
                HOperatorSet.GrabImageAsync(out ho_Image, hv_AcqHandle, -1);
                HOperatorSet.DispObj(ho_Image, hv_ExpDefaultWinHandle);
                // If teach part of the code is active
                //HOperatorSet.SetColor(hv_TeachWinHandle2, "spring green");
                //HOperatorSet.GenRectangle1(out ho_Rectangle, 716, 720, 2200, 1850);
                //HOperatorSet.GenRectangle1(out ho_Rectangle_diff, 726, 730, 2190, 1840);
                //HOperatorSet.Difference(ho_Rectangle, ho_Rectangle_diff, out ho_RegionOut);
                //HOperatorSet.DispObj(ho_RegionOut, hv_ExpDefaultWinHandle);
            }
            // Image Acquisition CLOSE frame
            ho_Image.Dispose();
            HOperatorSet.ClearWindow(hv_ExpDefaultWinHandle);
            HOperatorSet.CloseFramegrabber(hv_AcqHandle);
        }

        public void RunHalcon9(HTuple window)
        {
            hv_ExpDefaultWinHandle = window;
            HOperatorSet.ClearWindow(hv_ExpDefaultWinHandle);
            Livecam2();
        }
    }
}

