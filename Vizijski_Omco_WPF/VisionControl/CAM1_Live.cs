using HalconDotNet;

namespace VizijskiSustavWPF.VisionControl
{
    public partial class HDevelopExport
    {
   
        private void Livecam1()
        {
            // Wait for CAM4 thread to be closed
            _waitHandleCam1.WaitOne();
            // Close te thread DOOR
            _waitHandleCam1.Reset();
            // Initialize local and output iconic variables 
            HOperatorSet.GenEmptyObj(out ho_Image);
            // Image Acquisition OPEN frame
            // New Cam settings
            HOperatorSet.OpenFramegrabber("GigEVision", 0, 0, 0, 0, 0, 0, "default", -1, "default", -1, "false", "default", "RobotPick", 0, -1, out hv_AcqHandleCam1);
            //HOperatorSet.SetFramegrabberParam(hv_AcqHandle, "ExposureTime", 3500.0);
            HOperatorSet.GrabImageStart(hv_AcqHandleCam1, -1);

            while (Exitloop1 == false)
            {
                ho_Image.Dispose();
                // Live image from CAM1
                HOperatorSet.GrabImageAsync(out ho_Image, hv_AcqHandleCam1, -1);
                HOperatorSet.DispObj(ho_Image, hv_ExpDefaultWinHandle);
            }
            // Image Acquisition CLOSE frame
            ho_Image.Dispose();
            HOperatorSet.ClearWindow(hv_ExpDefaultWinHandle);
            HOperatorSet.CloseFramegrabber(hv_AcqHandleCam1);
            // Open the thread DOOR
            _waitHandleCam1.Set();
        }

        public void RunCam1(HTuple window)
        {
            hv_ExpDefaultWinHandle = window;
            HOperatorSet.ClearWindow(hv_ExpDefaultWinHandle);
            Livecam1();
        }
    }
}

