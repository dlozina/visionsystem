using System;
using HalconDotNet;

public partial class HDevelopExport
{

    private void livecam4()
    {
        // Local iconic variables 
        HObject ho_Image=null;
        // Local control variables 
        HTuple hv_AcqHandle = null;
        // Initialize local and output iconic variables 
        HOperatorSet.GenEmptyObj(out ho_Image);
        // Image Acquisition OPEN frame
        HOperatorSet.OpenFramegrabber("GigEVision", 0, 0, 0, 0, 0, 0, "default", -1, 
            "default", -1, "false", "default", "GC3851M_CAM_4", 0, -1, out hv_AcqHandle);
        HOperatorSet.SetFramegrabberParam(hv_AcqHandle, "ExposureTime", 3500.0);
        HOperatorSet.GrabImageStart(hv_AcqHandle, -1);

        while (exitloop4 == false)
        {
            ho_Image.Dispose();
	        // Live image from CAM4
            HOperatorSet.GrabImageAsync(out ho_Image, hv_AcqHandle, -1);
            HOperatorSet.DispObj(ho_Image, hv_ExpDefaultWinHandle);
        }
	    // Image Acquisition CLOSE frame
        ho_Image.Dispose();
        HOperatorSet.ClearWindow(hv_ExpDefaultWinHandle);
        HOperatorSet.CloseFramegrabber(hv_AcqHandle);
    }

    public void RunHalcon10(HTuple Window)
    {
        hv_ExpDefaultWinHandle = Window;
        HOperatorSet.ClearWindow(hv_ExpDefaultWinHandle);
        livecam4();
    }

}

