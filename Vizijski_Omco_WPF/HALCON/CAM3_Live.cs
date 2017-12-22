using System;
using HalconDotNet;

public partial class HDevelopExport
{

    private void livecam3()
    {
        // Local iconic variables 
        HObject ho_Image=null;
        // Local control variables 
        HTuple hv_AcqHandle = null;
        // Initialize local and output iconic variables 
        HOperatorSet.GenEmptyObj(out ho_Image);
        // Image Acquisition OPEN frame
        HOperatorSet.OpenFramegrabber("GigEVision", 0, 0, 0, 0, 0, 0, "default", -1, 
            "default", -1, "false", "default", "GC2591MP_CAM_3", 0, -1, out hv_AcqHandle);
        HOperatorSet.SetFramegrabberParam(hv_AcqHandle, "ExposureTime", 3500.0);
        HOperatorSet.GrabImageStart(hv_AcqHandle, -1);

        while (exitloop3 == false)
        {
            ho_Image.Dispose();
	        // Live image from CAM3
            HOperatorSet.GrabImageAsync(out ho_Image, hv_AcqHandle, -1);
            HOperatorSet.DispObj(ho_Image, hv_ExpDefaultWinHandle);
        }
	    // Image Acquisition CLOSE frame
        ho_Image.Dispose();
        HOperatorSet.ClearWindow(hv_ExpDefaultWinHandle);
        HOperatorSet.CloseFramegrabber(hv_AcqHandle);
    }

    public void RunHalcon12(HTuple Window)
    {
        hv_ExpDefaultWinHandle = Window;
        HOperatorSet.ClearWindow(hv_ExpDefaultWinHandle);
        livecam3();
    }

}

