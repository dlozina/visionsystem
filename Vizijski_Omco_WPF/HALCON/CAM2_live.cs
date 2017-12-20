using System;
using HalconDotNet;

public partial class HDevelopExport
{
  public HTuple hv_ExpDefaultWinHandle;

   private bool exitloop2;
   public bool Exitloop2
   {
       get { return exitloop2; }
       set { exitloop2 = value; }
   }

  // public bool exitloop = false;
  // Main procedure 
  private void livecam2()
  {

    // Local iconic variables 
    HObject ho_Image=null;

    // Local control variables 
    HTuple hv_AcqHandle = null;
    // Initialize local and output iconic variables 
    HOperatorSet.GenEmptyObj(out ho_Image);
	
    //HOperatorSet.CloseAllFramegrabbers();
    //Image Acquisition OPEN frame
    HOperatorSet.OpenFramegrabber("GigEVision", 0, 0, 0, 0, 0, 0, "default", -1, 
        "default", -1, "false", "default", "GC3851MP_CAM_2", 0, -1, out hv_AcqHandle);
    HOperatorSet.GrabImageStart(hv_AcqHandle, -1);
	
    
	// Goal is to do an exit from loop
    // while ((int)(1) != 0)
    while (exitloop2 == false)
    {
      ho_Image.Dispose();
	  //Live image from CAM2
      HOperatorSet.GrabImageAsync(out ho_Image, hv_AcqHandle, -1);
            // ho_Image.Dispose();
      HOperatorSet.DispObj(ho_Image, hv_ExpDefaultWinHandle);
    }
	//Image Acquisition CLOSE frame
    ho_Image.Dispose();
    HOperatorSet.ClearWindow(hv_ExpDefaultWinHandle);
    HOperatorSet.CloseFramegrabber(hv_AcqHandle);
  }

  public void InitHalcon()
  {
    // Default settings used in HDevelop 
    HOperatorSet.SetSystem("width", 512);
    HOperatorSet.SetSystem("height", 512);
  }

  public void RunHalcon9(HTuple Window)
  {
    hv_ExpDefaultWinHandle = Window;
    HOperatorSet.ClearWindow(hv_ExpDefaultWinHandle);
    livecam2();
  }

}

