using HalconDotNet;

namespace VizijskiSustavWPF.VisionControl
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
            HOperatorSet.GenEmptyObj(out HObject ho_GammaImage);
            // Image Acquisition OPEN frame
            //HOperatorSet.OpenFramegrabber("GigEVision", 0, 0, 0, 0, 0, 0, "default", -1, "default", -1, "false", "default", "GC3851M_CAM_4", 0, -1, out hv_AcqHandle);
            //HOperatorSet.SetFramegrabberParam(hv_AcqHandle, "ExposureTime", 3500.0);
            //HOperatorSet.GrabImageStart(hv_AcqHandle, -1);

            // New Camera
            OpenCamFrame();

            //HOperatorSet.OpenFramegrabber("GigEVision", 0, 0, 0, 0, 0, 0, "default", -1, "default", -1, "false", "default", "Diameter", 0, -1, out hv_AcqHandle);
            //HOperatorSet.SetFramegrabberParam(hv_AcqHandle, "ExposureTime", 800.0);
            //HOperatorSet.SetFramegrabberParam(hv_AcqHandle, "ExposureAuto", "Off");

            HOperatorSet.GrabImageStart(hv_AcqHandle, -1);
            //
            while (Exitloop4 == false)
            {
                ho_Image.Dispose();
                // Live image from CAM4
                HOperatorSet.GrabImageAsync(out ho_Image, hv_AcqHandle, -1);
                HOperatorSet.GetImageSize(ho_Image, out hv_Width, out hv_Height);
                hv_HalfW = hv_Width / 2;
                // Gamma Encoding
                HOperatorSet.GammaImage(ho_Image, out ho_GammaImage, 0.416667, 0.055, 0.0031308,255, "true");
                HOperatorSet.DispObj(ho_GammaImage, hv_ExpDefaultWinHandle);

                if (domainmarkup)
                {
                    // Teach parameter is passed
                    HOperatorSet.SetColor(hv_ExpDefaultWinHandle, "spring green");
                    // Old Camera
                    //HOperatorSet.DispLine(hv_ExpDefaultWinHandle, 0, 1928 - 250, 2764, 1928 - 250);
                    //HOperatorSet.DispLine(hv_ExpDefaultWinHandle, 0, 1928 + 250, 2764, 1928 + 250);
                    HOperatorSet.DispLine(hv_ExpDefaultWinHandle, 0, hv_HalfW - 250, hv_Height, hv_HalfW - 250);
                    HOperatorSet.DispLine(hv_ExpDefaultWinHandle, 0, hv_HalfW + 250, hv_Height, hv_HalfW + 250);
                }
                
            }
            // Image Acquisition CLOSE frame
            ho_Image.Dispose();
            HOperatorSet.ClearWindow(hv_ExpDefaultWinHandle);

            //CloseCamFrame();

            //HOperatorSet.CloseFramegrabber(hv_AcqHandle);
            // Open the thread DOOR
            _waitHandleCam4.Set();
        }

        public void RunCam4(HTuple window, bool domainmarkup)
        {
            hv_ExpDefaultWinHandle = window;
            HOperatorSet.ClearWindow(hv_ExpDefaultWinHandle);
            // TEST Line Display - > Test call from main thread
            Livecam4(domainmarkup);
        }

    }
}

