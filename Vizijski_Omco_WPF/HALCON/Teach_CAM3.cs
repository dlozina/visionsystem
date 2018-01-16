using System;
using HalconDotNet;

public partial class HDevelopExport
{

    private void Teachcam3()
    {
        // Initialize local and output iconic variables 
        HOperatorSet.GenEmptyObj(out ho_Image);
        // Image Acquisition OPEN frame
        HOperatorSet.OpenFramegrabber("GigEVision", 0, 0, 0, 0, 0, 0, "default", -1, 
            "default", -1, "false", "default", "GC2591MP_CAM_3", 0, -1, out hv_AcqHandle);
        HOperatorSet.SetFramegrabberParam(hv_AcqHandle, "ExposureTime", 10000.0);
        HOperatorSet.GrabImageStart(hv_AcqHandle, -1);

        while (teachloop3 == false)
        {
            ho_Image.Dispose();
	        // Live image from CAM3
            HOperatorSet.GrabImageAsync(out ho_Image, hv_AcqHandle, -1);
            HOperatorSet.DispObj(ho_Image, hv_TeachWinHandle3);
            HOperatorSet.SetColor(hv_TeachWinHandle3, "spring green");
            HOperatorSet.DispRectangle1(hv_TeachWinHandle3, 690, 300, 1150, 700);
        }
	    // Image Acquisition CLOSE frame
        ho_Image.Dispose();
        HOperatorSet.ClearWindow(hv_TeachWinHandle3);
        HOperatorSet.CloseFramegrabber(hv_AcqHandle);
    }

    public void RunHalcon25(HTuple Window)
    {
        hv_TeachWinHandle3 = Window;
        HOperatorSet.ClearWindow(hv_TeachWinHandle3);
        Teachcam3();
    }

}

