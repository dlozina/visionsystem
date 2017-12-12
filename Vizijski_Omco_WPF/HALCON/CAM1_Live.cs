using System;
using HalconDotNet;

public partial class HDevelopExport
{
  //public HTuple hv_ExpDefaultWinHandle;

   private bool exitloop1;
   public bool Exitloop1
   {
       get { return exitloop1; }
       set { exitloop1 = value; }
   }

  // public bool exitloop = false;
  // Main procedure 
  private void livecam1()
  {

    // Local iconic variables 
    HObject ho_Image=null;

    // Local control variables 
    HTuple hv_AcqHandle = null;
    // Initialize local and output iconic variables 
    HOperatorSet.GenEmptyObj(out ho_Image);
	
    //HOperatorSet.CloseAllFramegrabbers();
    HOperatorSet.OpenFramegrabber("GigEVision", 0, 0, 0, 0, 0, 0, "default", -1, 
        "default", -1, "false", "default", "acA130075gm_CAM", 0, -1, out hv_AcqHandle);
    //HOperatorSet.SetFramegrabberParam(hv_AcqHandle, "ExposureTime", 3500.0);
    
	// Goal is to do an exit from loop
    // while ((int)(1) != 0)
    while (exitloop1 == false)
    {
      ho_Image.Dispose();
	  //Live image from CAM2
      HOperatorSet.GrabImageAsync(out ho_Image, hv_AcqHandle, -1);
      HOperatorSet.DispObj(ho_Image, hv_ExpDefaultWinHandle);
    }
	//Image Acquisition CLOSE frame
    HOperatorSet.CloseFramegrabber(hv_AcqHandle);
    ho_Image.Dispose();
    

  }

  public void RunHalcon11(HTuple Window)
  {
    hv_ExpDefaultWinHandle = Window;
    livecam1();
  }

}

