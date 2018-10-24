using System;
using System.Threading;
using HalconDotNet;

namespace VizijskiSustavWPF.VisionControl
{
    public partial class HDevelopExport
    {
        // Class constructor
        public HDevelopExport()
        {

        }
        // Event - Update diameter result
        public delegate void UpdateHandler(HDevelopExport sender, HalconEventArgs e);
        public event UpdateHandler UpdateResult;
        HalconEventArgs argumenti = new HalconEventArgs();
        
        // Event - Update pick result
        public delegate void UpdateHandlerPick(HDevelopExport sender, HalconEventArgs e);
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
        static readonly EventWaitHandle _waitHandleCam1 = new AutoResetEvent(true);
        static readonly EventWaitHandle _waitHandleCam2 = new AutoResetEvent(true);
        static readonly EventWaitHandle _waitHandleCam3 = new AutoResetEvent(true);
        static readonly EventWaitHandle _waitHandleCam4 = new AutoResetEvent(true);

        //Framegrabber Handle definition
        // CAM1
        HTuple hv_AcqHandleCam1 = new HTuple();
        // CAM4
        HTuple hv_AcqHandle = new HTuple();
        

        // Framegrabber Handle for live CAM
        public HTuple hv_ExpDefaultWinHandle;
        // Framegrabber Handle for live Porosity
        public HTuple hv_porosityWinHandle;
        // Output definition for all Diameters
        private HTuple hv_output = new HTuple();
        // Output from pick
        HTuple hv_x_cross = new HTuple(), hv_y_cross = new HTuple();
        HTuple hv_X = new HTuple(), hv_Y= new HTuple();
        HTuple hv_angledeg = new HTuple(), hv_TempValue = null;
        // Layer check information for PLC
        HTuple hv_distance_mean = new HTuple();
        // Local iconic variables 
        HObject ho_Image = null, ho_Rectangle = null, ho_DerivGauss = null, ho_RegionCrossings = null;
        HObject ho_Region = null, ho_region_outer = null, ho_contour_outer = null;
        HObject ho_ContCircle = null, ho_ReducedImage = null, ho_SelectedRegions;
        //
        HObject ho_Elipse = null;
        HObject ho_ROIUnion = null, ho_ROI = null;
        HObject ho_Contours = null;
        // Local control variables 
        HTuple hv_Width = new HTuple(), hv_Height = new HTuple();
        HTuple hv_HalfW = new HTuple(), hv_HalfH = new HTuple();
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

        public HalconException exceptionLayerNotFound;

        // Diameter teach CAM4 image
        HObject ho_TestImage = null;

        // HDevelopExport Class properties
        public bool Exitloop1 { get; set; }

        public bool Exitloop2 { get; set; }

        public bool Exitloop3 { get; set; }

        public bool Exitloop4 { get; set; }

        public bool Porositydetectedver { get; set; }

        public bool Porositydetectedhor { get; set; }

        public void OpenCamFrame()
        {
            if (hv_AcqHandle.Length == 0)
            {
                HOperatorSet.OpenFramegrabber("GigEVision", 0, 0, 0, 0, 0, 0, "default", -1, "default", -1, "false", "default", "Diameter", 0, -1, out hv_AcqHandle);
                HOperatorSet.SetFramegrabberParam(hv_AcqHandle, "ExposureTimeAbs", 1000.0);
            }
        }

        public void CloseCamFrame()
        {
            HOperatorSet.CloseFramegrabber(hv_AcqHandle);
            //hv_AcqHandle.Type == HTupleType.EMPTY;
            //hv_AcqHandle = 0;
            //hv_AcqHandle = new HTuple();
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

