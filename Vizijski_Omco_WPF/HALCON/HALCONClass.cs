using System;
using System.Threading;
using HalconDotNet;

namespace VizijskiSustavWPF.HALCON
{
    public partial class HDevelopExport
    {
        // Event - Update diameter result
        public delegate void UpdateHandler(VizijskiSustavWPF.HALCON.HDevelopExport sender, HalconEventArgs e);
        public event UpdateHandler UpdateResult;
        HalconEventArgs argumenti = new HalconEventArgs();
        
        // Event - Update pick result
        public delegate void UpdateHandlerPick(VizijskiSustavWPF.HALCON.HDevelopExport sender, HalconEventArgs e);
        public event UpdateHandlerPick UpdateResultPick;
        HalconEventArgs koordinate = new HalconEventArgs();
        
        // Event - Porosity detection started Vertical
        public delegate void PorosityDetectionStartEventHandler(object source, EventArgs args);
        public event PorosityDetectionStartEventHandler PorosityDetectionStart;
        
        // Event - Porosity detection started Horizontal
        public delegate void PorosityDetectionHorStartEventHandler(object source, EventArgs args);
        public event PorosityDetectionHorStartEventHandler PorosityDetectionHorStart;
        
        // Event - Porosity detected
        public delegate void PorosityDetectedEventHandler(object source, EventArgs args);
        public event PorosityDetectedEventHandler PorosityDetected;

        // Framegrabber close handle - Set to TRUE beacuse we need first thread to go trough
        static EventWaitHandle _waitHandle = new AutoResetEvent(true);

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

        //

        //

        // Output definition for all Diameters
        private HTuple hv_output = new HTuple();
        // Output from pick
        HTuple hv_x_cross = new HTuple(), hv_y_cross = new HTuple();
        HTuple hv_X = new HTuple(), hv_Y= new HTuple();
        HTuple hv_angledeg = new HTuple();
        // Layer check information for PLC
        HTuple hv_distance_mean = new HTuple();
        // Local iconic variables 
        HObject ho_Image = null, ho_Rectangle = null, ho_DerivGauss = null, ho_RegionCrossings = null;
        HObject ho_Region = null, ho_region_outer = null, ho_contour_outer = null;
        HObject ho_ContCircle = null, ho_ReducedImage = null;
        // Local control variables 
        HTuple hv_Width = new HTuple(), hv_Height = new HTuple();
        HTuple hv_HalfW = new HTuple();
        HTuple hv_row_len = new HTuple(), hv_row_outer = new HTuple();
        HTuple hv_col_outer = new HTuple(), hv_Rows = new HTuple();
        HTuple hv_Cols = new HTuple(), hv_i = new HTuple(), hv_Indices = new HTuple();
        HTuple hv_col_min = new HTuple();
        HTuple hv_indice_min = new HTuple(), hv_col_max = new HTuple();
        HTuple hv_indice_max = new HTuple(), hv_Row = new HTuple();
        HTuple hv_Col = new HTuple(), hv_Radius = new HTuple();
        HTuple hv_TupleMax = new HTuple();
        HTuple hv_IndexMax = new HTuple(), hv_colToMax0 = new HTuple();
        HTuple hv_TupleMin = new HTuple(), hv_IndexMin = new HTuple();
        HTuple hv_colToMin0 = new HTuple(), hv_Exception = null;
        HTuple hv_MessageError = new HTuple();

        // Diameter teach CAM4 image
        HObject ho_TestImage = null;

        // HDevelopExport Class properties
        public bool Exitloop1 { get; set; }

        public bool Exitloop2 { get; set; }

        public bool Exitloop3 { get; set; }

        public bool Exitloop4 { get; set; }

        public bool Teachloop { get; set; }

        public bool Teachloop2 { get; set; }

        public bool Teachloop3 { get; set; }

        public bool FramegrabberClosed4 { get; set; }

        public bool Porositydetectedver { get; set; }

        public bool Porositydetectedhor { get; set; }

        public void OpenCamFrame()
        {
            HOperatorSet.OpenFramegrabber("GigEVision", 0, 0, 0, 0, 0, 0, "default", -1, "default", -1, "false", "default", "GC3851M_CAM_4", 0, -1, out hv_AcqHandle);
            HOperatorSet.SetFramegrabberParam(hv_AcqHandle, "ExposureTime", 3500.0);
            HOperatorSet.GrabImageStart(hv_AcqHandle, -1);
        }

        public void CloseCamFrame()
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

        protected virtual void DetectionHorStart()
        {
            if (PorosityDetectionHorStart != null)
                PorosityDetectionHorStart(this, EventArgs.Empty);
        }

        public void InitHalcon()
        {
            // Default settings used in HDevelop 
            HOperatorSet.SetSystem("width", 512);
            HOperatorSet.SetSystem("height", 512);
        }


    }
}

