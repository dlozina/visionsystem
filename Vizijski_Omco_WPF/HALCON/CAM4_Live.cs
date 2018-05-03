using HalconDotNet;

namespace VizijskiSustavWPF.HALCON
{
    public partial class HDevelopExport
    {

        private void Livecam4(bool domainmarkup)
        {
            // Wait for CAM4 thread to be closed
            _waitHandleCam4.WaitOne();
            // Close te thread DOOR
            _waitHandleCam4.Reset();
            // Initialize local and output iconic variables 
            HOperatorSet.GenEmptyObj(out ho_Image);
            // Image Acquisition OPEN frame
            HOperatorSet.OpenFramegrabber("GigEVision", 0, 0, 0, 0, 0, 0, "default", -1, "default", -1, "false", "default", "GC3851M_CAM_4", 0, -1, out hv_AcqHandle);
            HOperatorSet.SetFramegrabberParam(hv_AcqHandle, "ExposureTime", 3500.0);
            HOperatorSet.GrabImageStart(hv_AcqHandle, -1);

            while (Exitloop4 == false)
            {
                ho_Image.Dispose();
                // Live image from CAM4
                HOperatorSet.GrabImageAsync(out ho_Image, hv_AcqHandle, -1);
                HOperatorSet.DispObj(ho_Image, hv_ExpDefaultWinHandle);

                if (domainmarkup == true)
                {
                    // Teach parameter is passed
                    HOperatorSet.SetColor(hv_TeachWinHandle, "spring green");
                    HOperatorSet.DispLine(hv_TeachWinHandle, 0, 1928 - 250, 2764, 1928 - 250);
                    HOperatorSet.DispLine(hv_TeachWinHandle, 0, 1928 + 250, 2764, 1928 + 250);
                }
                
            }
            // Image Acquisition CLOSE frame
            ho_Image.Dispose();
            HOperatorSet.ClearWindow(hv_ExpDefaultWinHandle);
            HOperatorSet.CloseFramegrabber(hv_AcqHandle);
            // Open the thread DOOR
            _waitHandleCam4.Set();
        }

        public void RunHalcon10(HTuple window, bool domainmarkup)
        {
            hv_ExpDefaultWinHandle = window;
            HOperatorSet.ClearWindow(hv_ExpDefaultWinHandle);
            // TEST Line Display - > Test call from main thread
            Livecam4(domainmarkup);
        }

    }
}

