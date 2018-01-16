using System;
using HalconDotNet;

public partial class HDevelopExport
{

    private void Teachcam2()
    {
        // Local iconic variables 
        HObject ho_Image=null;
        // Local control variables 
        HTuple hv_AcqHandle = null;
        // Initialize local and output iconic variables 
        HOperatorSet.GenEmptyObj(out ho_Image);
        // Image Acquisition OPEN frame
        HOperatorSet.OpenFramegrabber("GigEVision", 0, 0, 0, 0, 0, 0, "default", -1, 
            "default", -1, "false", "default", "GC3851MP_CAM_2", 0, -1, out hv_AcqHandle);
        HOperatorSet.SetFramegrabberParam(hv_AcqHandle, "ExposureTime", 15000.0);
        HOperatorSet.GrabImageStart(hv_AcqHandle, -1);
    
        while (teachloop2 == false)
        {
            ho_Image.Dispose();
	        // Live image from CAM2
            HOperatorSet.GrabImageAsync(out ho_Image, hv_AcqHandle, -1);
            HOperatorSet.DispObj(ho_Image, hv_TeachWinHandle2);
        }
        // Image Acquisition CLOSE frame
        ho_Image.Dispose();
        HOperatorSet.ClearWindow(hv_TeachWinHandle2);
        HOperatorSet.CloseFramegrabber(hv_AcqHandle);
    }

    public void RunHalcon24(HTuple Window)
    {
        hv_TeachWinHandle2 = Window;
        HOperatorSet.ClearWindow(hv_TeachWinHandle2);
        Teachcam2();
    }
}

