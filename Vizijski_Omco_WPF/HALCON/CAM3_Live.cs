using HalconDotNet;

namespace VizijskiSustavWPF.HALCON
{
    public partial class HDevelopExport
    {

        private void Livecam3(bool domainmarkup)
        {
            // Wait for CAM4 thread to be closed
            _waitHandleCam3.WaitOne();
            // Close te thread DOOR
            _waitHandleCam3.Reset();
            // Definition of ROI for Porosity
            HObject ho_Rectangle_diff = null;
            HObject ho_Rectangle_teach = null;
            HObject ho_RegionOut = null;
            // Initialize local and output iconic variables 
            HOperatorSet.GenEmptyObj(out ho_Image);
            // Image Acquisition OPEN frame
            HOperatorSet.OpenFramegrabber("GigEVision", 0, 0, 0, 0, 0, 0, "default", -1, "default", -1, "false", "default", "GC2591MP_CAM_3", 0, -1, out hv_AcqHandle);
            HOperatorSet.SetFramegrabberParam(hv_AcqHandle, "ExposureTime", 10000.0);
            HOperatorSet.GrabImageStart(hv_AcqHandle, -1);

            while (Exitloop3 == false)
            {
                ho_Image.Dispose();
                // Live image from CAM3
                HOperatorSet.GrabImageAsync(out ho_Image, hv_AcqHandle, -1);
                HOperatorSet.DispObj(ho_Image, hv_ExpDefaultWinHandle);
                if (domainmarkup == true)
                {
                    // Draw ROI
                    //if(hv_TeachRow1 == null || hv_TeachColumn1 == null || hv_TeachRow2 == null || hv_TeachColumn2 == null)
                    //{
                    //    HOperatorSet.DrawRectangle1(hv_TeachWinHandle3, out hv_TeachRow1, out hv_TeachColumn1, out hv_TeachRow2, out hv_TeachColumn2);
                    //}
                    HOperatorSet.SetColor(hv_ExpDefaultWinHandle, "spring green");
                    //HOperatorSet.GenRectangle1(out ho_Rectangle_teach, hv_TeachRow1, hv_TeachColumn1, hv_TeachRow2, hv_TeachColumn2);
                    //HOperatorSet.GenRectangle1(out ho_Rectangle_diff, hv_TeachRow1 + 10, hv_TeachColumn1 + 10, hv_TeachRow2 - 10, hv_TeachColumn2 -10);

                    //HOperatorSet.GenRectangle1(out ho_Rectangle, 520, 200, 1050, 700);

                    HOperatorSet.GenRectangle1(out ho_Rectangle_teach, 600, 200, 1160, 700); //aktivan
                                                                                             //HOperatorSet.GenRectangle1(out ho_Rectangle, 1120, 200, 1630, 700);

                    HOperatorSet.GenRectangle1(out ho_Rectangle_diff, 610, 210, 1150, 690); // 10 vise, 10 manje aktivan
                                                                                            //HOperatorSet.GenRectangle1(out ho_Rectangle_diff, 1130, 210, 1620, 690); // 10 vise, 10 manje

                    HOperatorSet.Difference(ho_Rectangle_teach, ho_Rectangle_diff, out ho_RegionOut);
                    HOperatorSet.DispObj(ho_RegionOut, hv_ExpDefaultWinHandle);
                }
            }
            // Image Acquisition CLOSE frame
            ho_Image.Dispose();
            HOperatorSet.ClearWindow(hv_ExpDefaultWinHandle);
            HOperatorSet.CloseFramegrabber(hv_AcqHandle);
            // Open the thread DOOR
            _waitHandleCam3.Set();
        }

        public void RunHalcon12(HTuple window, bool domainmarkup)
        {
            hv_ExpDefaultWinHandle = window;
            HOperatorSet.ClearWindow(hv_ExpDefaultWinHandle);
            Livecam3(domainmarkup);
        }

    }
}

