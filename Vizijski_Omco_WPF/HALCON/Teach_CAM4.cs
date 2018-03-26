using HalconDotNet;

namespace VizijskiSustavWPF.HALCON
{
    public partial class HDevelopExport
    {

        private void Teachcam4()
        {
            // Initialize local and output iconic variables 
            HOperatorSet.GenEmptyObj(out ho_TestImage);
            // Image Acquisition OPEN frame
            HOperatorSet.OpenFramegrabber("GigEVision", 0, 0, 0, 0, 0, 0, "default", -1, 
                "default", -1, "false", "default", "GC3851M_CAM_4", 0, -1, out hv_AcqHandle);
            HOperatorSet.SetFramegrabberParam(hv_AcqHandle, "ExposureTime", 3500.0);
            HOperatorSet.GrabImageStart(hv_AcqHandle, -1);
            // HOperatorSet.GenRectangle1(out ho_Rectangle, 0, 1928 - 150, 2764, 1928 + 150);
            //HOperatorSet.GenRectangle1(out ho_Rectangle, 0, 1928 - 250, 2764, 1928 + 250);

            while (Teachloop == false)
            {
                ho_TestImage.Dispose();
                // Live image from CAM4
                HOperatorSet.GrabImageAsync(out ho_TestImage, hv_AcqHandle, -1);
                HOperatorSet.DispObj(ho_TestImage, hv_TeachWinHandle);
                HOperatorSet.SetColor(hv_TeachWinHandle, "spring green");
                HOperatorSet.DispLine(hv_TeachWinHandle, 0, 1928 - 250, 2764, 1928 - 250);
                HOperatorSet.DispLine(hv_TeachWinHandle, 0, 1928 + 250, 2764, 1928 + 250);
            }
            // Image Acquisition CLOSE frame
            ho_TestImage.Dispose();
            HOperatorSet.ClearWindow(hv_TeachWinHandle);
            HOperatorSet.CloseFramegrabber(hv_AcqHandle);
            //Framegrabber closed signal
            FramegrabberClosed4 = true;
        }

        public void RunHalcon15(HTuple window)
        {
            hv_TeachWinHandle = window;
            HOperatorSet.ClearWindow(hv_TeachWinHandle);
            Teachcam4();
        }

    }
}

