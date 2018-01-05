using System;
using HalconDotNet;

public partial class HDevelopExport
{

    private void Teachcam4()
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

        //HOperatorSet.GenRectangle1(out ho_Rectangle, 0, hv_HalfW - 150, hv_Height, hv_HalfW + 150);

        while (teachloop == false)
        {
            ho_Image.Dispose();
	        // Live image from CAM4
            HOperatorSet.GrabImageAsync(out ho_Image, hv_AcqHandle, -1);
            HOperatorSet.DispObj(ho_Image, hv_TeachWinHandle);
        }
	    // Image Acquisition CLOSE frame
        ho_Image.Dispose();
        HOperatorSet.ClearWindow(hv_TeachWinHandle);
        HOperatorSet.CloseFramegrabber(hv_AcqHandle);
    }

    public void RunHalcon15(HTuple Window)
    {
        hv_TeachWinHandle = Window;
        HOperatorSet.ClearWindow(hv_TeachWinHandle);
        Teachcam4();
    }

}

