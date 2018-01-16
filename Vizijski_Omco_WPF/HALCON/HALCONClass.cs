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
    // Framegrabber Handle for live Porosity
    public HTuple hv_porosityWinHandle;
    // Framegrabber Handle for teach CAM
    public HTuple hv_TeachWinHandle;
    // Framegrabber Handle for teach CAM2
    public HTuple hv_TeachWinHandle2;
    // Framegrabber Handle for teach CAM2
    public HTuple hv_TeachWinHandle3;

    // Output definition for all Diameters
    private HTuple hv_output = new HTuple();
    private HTuple hv_outputmm = new HTuple();

    
    // Diameter app variables
    // Local iconic variables 
    HObject ho_Image = null, ho_Rectangle = null, ho_DerivGauss = null, ho_RegionCrossings = null;
    HObject ho_Region = null, ho_region_outer = null, ho_contour_outer = null;
    HObject ho_ContCircle = null, ho_ReducedImage = null;
    // Local control variables 
    HTuple hv_Width = new HTuple(), hv_Height = new HTuple();
    HTuple hv_HalfH = new HTuple(), hv_HalfW = new HTuple();
    HTuple hv_row_len = new HTuple(), hv_row_outer = new HTuple();
    HTuple hv_col_outer = new HTuple(), hv_Rows = new HTuple();
    HTuple hv_Cols = new HTuple(), hv_i = new HTuple(), hv_Indices = new HTuple();
    HTuple hv_Length = new HTuple(), hv_col_min = new HTuple();
    HTuple hv_indice_min = new HTuple(), hv_col_max = new HTuple();
    HTuple hv_indice_max = new HTuple(), hv_Row = new HTuple();
    HTuple hv_Col = new HTuple(), hv_Radius = new HTuple();
    HTuple hv_StartPhi = new HTuple(), hv_EndPhi = new HTuple();
    HTuple hv_PointOrder = new HTuple(), hv_TupleMax = new HTuple();
    HTuple hv_IndexMax = new HTuple(), hv_colToMax0 = new HTuple();
    HTuple hv_TupleMin = new HTuple(), hv_IndexMin = new HTuple();
    HTuple hv_colToMin0 = new HTuple(), hv_Exception = null;
    HTuple hv_MessageError = new HTuple();

    // Diameter teach CAM4 image
    HObject ho_TestImage = null;

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

    private bool teachloop;
    public bool Teachloop
    {
        get { return teachloop; }
        set { teachloop = value; }
    }

    private bool teachloop2;
    public bool Teachloop2
    {
        get { return teachloop2; }
        set { teachloop2 = value; }
    }

    private bool teachloop3;
    public bool Teachloop3
    {
        get { return teachloop3; }
        set { teachloop3 = value; }
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

