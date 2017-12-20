using System;
using HalconDotNet;


public partial class HDevelopExport
{
    // Event - after execution code we update result
    public delegate void UpdateHandler(HDevelopExport sender, HalconEventArgs e);
    public event UpdateHandler UpdateResult;
    HalconEventArgs argumenti = new HalconEventArgs();

    //Framegrabber Handle definition
    HTuple hv_AcqHandle = new HTuple();

    // Output definition for all Diameters
    private HTuple hv_output = new HTuple();
    private HTuple hv_outputmm = new HTuple();

    // Definition of variables D1 S1

    // Definition of variables D1 S2

    // Definition of variables D2 S1

    // Definition of variables D2 S2

    // Definition of variables D3 S1

    // Definition of variables D3 S2

    // Definition of variables D4 S1

    // Definition of variables D4 S2

    public void openCAMFrame(double exposure)
    {
        //Camera communication - Open
        HOperatorSet.OpenFramegrabber("GigEVision", 0, 0, 0, 0, 0, 0, "default",
            -1, "default", -1, "false", "default", "GC3851M_CAM_4", 0, -1, out hv_AcqHandle);
        HOperatorSet.SetFramegrabberParam(hv_AcqHandle, "ExposureTime", exposure);
        HOperatorSet.GrabImageStart(hv_AcqHandle, -1);
    }

    public void closeCAMFrame()
    {
        HOperatorSet.CloseFramegrabber(hv_AcqHandle);
    }



}

