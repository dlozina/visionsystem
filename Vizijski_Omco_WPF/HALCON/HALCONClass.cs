using System;
using HalconDotNet;


public partial class HDevelopExport
{
    // Event - Update diameter result
    public delegate void UpdateHandler(HDevelopExport sender, HalconEventArgs e);
    public event UpdateHandler UpdateResult;
    HalconEventArgs argumenti = new HalconEventArgs();
    // Event - Porosity detection started
    public delegate void PorosityDetectionStartEventHandler(object source, EventArgs args);
    public event PorosityDetectionStartEventHandler PorosityDetectionStart;
    // Event - Porosity detected
    public delegate void PorosityDetectedEventHandler(object source, EventArgs args);
    public event PorosityDetectedEventHandler PorosityDetected;

    //Framegrabber Handle definition
    HTuple hv_AcqHandle = new HTuple();
    // Framegrabber Handle for live CAM
    public HTuple hv_ExpDefaultWinHandle;
    // Output definition for all Diameters
    private HTuple hv_output = new HTuple();
    private HTuple hv_outputmm = new HTuple();
    // Diameter and side definition for all parameters
    //private HTuple hv_side = null;
    //private HTuple hv_dia = null;

    // Definition of variables D1 S1

    // Definition of variables D1 S2

    // Definition of variables D2 S1

    // Definition of variables D2 S2

    // Definition of variables D3 S1

    // Definition of variables D3 S2

    // Definition of variables D4 S1

    // Definition of variables D4 S2

    // HDevelopExport Class properties
    private bool exitloop1;
    public bool Exitloop1
    {
        get { return exitloop1; }
        set { exitloop1 = value; }
    }

    private bool exitloop2;
    public bool Exitloop2
    {
        get { return exitloop2; }
        set { exitloop2 = value; }
    }

    private bool exitloop3;
    public bool Exitloop3
    {
        get { return exitloop3; }
        set { exitloop3 = value; }
    }

    private bool exitloop4;
    public bool Exitloop4
    {
        get { return exitloop4; }
        set { exitloop4 = value; }
    }

    private bool porositydetectedver;
    public bool Porositydetectedver
    {
        get { return porositydetectedver; }
        set { porositydetectedver = value; }
    }

    private bool porositydetectedhor;
    public bool Porositydetectedhor
    {
        get { return porositydetectedhor; }
        set { porositydetectedhor = value; }
    }

    public void openCAMFrame()
    {
        HOperatorSet.OpenFramegrabber("GigEVision", 0, 0, 0, 0, 0, 0, "default",
            -1, "default", -1, "false", "default", "GC3851M_CAM_4", 0, -1, out hv_AcqHandle);
        HOperatorSet.SetFramegrabberParam(hv_AcqHandle, "ExposureTime", 3500.0);
        HOperatorSet.GrabImageStart(hv_AcqHandle, -1);
    }

    public void closeCAMFrame()
    {
        HOperatorSet.CloseFramegrabber(hv_AcqHandle);
    }

    protected virtual void PorosityIsDetected()
    {
        if (PorosityDetected != null)
            PorosityDetected(this, EventArgs.Empty);
    }

    protected virtual void DetectionStart()
    {
        if (PorosityDetectionStart != null)
            PorosityDetectionStart(this, EventArgs.Empty);
    }

    public void InitHalcon()
    {
        // Default settings used in HDevelop 
        HOperatorSet.SetSystem("width", 512);
        HOperatorSet.SetSystem("height", 512);
    }


}

