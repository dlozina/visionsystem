using HalconDotNet;

public partial class HDevelopExport
{
    
    public void segment (HObject ho_Image, out HObject ho_Regions)
    {
        // Stack for temporary objects 
        HObject[] OTemp = new HObject[20];
        // Local iconic variables 
        HObject ho_ImaAmp, ho_ImaDir, ho_Margin, ho_ConnectedRegions;
        // Local control variables 
        HTuple hv_NumConnected = null, hv_NumHoles = null;
        // Initialize local and output iconic variables 
        HOperatorSet.GenEmptyObj(out ho_Regions);
        HOperatorSet.GenEmptyObj(out ho_ImaAmp);
        HOperatorSet.GenEmptyObj(out ho_ImaDir);
        HOperatorSet.GenEmptyObj(out ho_Margin);
        HOperatorSet.GenEmptyObj(out ho_ConnectedRegions);
        ho_ImaAmp.Dispose();ho_ImaDir.Dispose();
        HOperatorSet.EdgesImage(ho_Image, out ho_ImaAmp, out ho_ImaDir, "canny", 1, "nms", 20, 40);
        ho_Margin.Dispose();
        HOperatorSet.HysteresisThreshold(ho_ImaAmp, out ho_Margin, 20, 30, 30);
        ho_ConnectedRegions.Dispose();
        HOperatorSet.Connection(ho_Margin, out ho_ConnectedRegions);
        HOperatorSet.ConnectAndHoles(ho_ConnectedRegions, out hv_NumConnected, out hv_NumHoles);
        {
            HObject ExpTmpOutVar_0;
            HOperatorSet.ClosingCircle(ho_ConnectedRegions, out ExpTmpOutVar_0, 33);
            ho_ConnectedRegions.Dispose();
            ho_ConnectedRegions = ExpTmpOutVar_0;
        }
        ho_Regions.Dispose();
        HOperatorSet.FillUp(ho_ConnectedRegions, out ho_Regions);
        ho_ImaAmp.Dispose();
        ho_ImaDir.Dispose();
        ho_Margin.Dispose();
        ho_ConnectedRegions.Dispose();
    }

    public void get_features (HObject ho_Region, out HTuple hv_Features)
    {
        // Local iconic variables 
        HObject ho_SingleRegion, ho_Contours;
        // Local control variables 
        HTuple hv_Circularity_xld = null, hv_ContLength = null;
        HTuple hv_Circularity = null, hv_Anisometry = null, hv_Bulkiness = null;
        HTuple hv_StructureFactor = null, hv_Distance = null, hv_Sigma = null;
        HTuple hv_Roundness = null, hv_Sides = null, hv_PSI1 = null;
        HTuple hv_PSI2 = null, hv_PSI3 = null, hv_PSI4 = null;
        // Initialize local and output iconic variables 
        HOperatorSet.GenEmptyObj(out ho_SingleRegion);
        HOperatorSet.GenEmptyObj(out ho_Contours);
        ho_SingleRegion.Dispose();
        HOperatorSet.SelectObj(ho_Region, out ho_SingleRegion, 1);
        ho_Contours.Dispose();
        HOperatorSet.GenContourRegionXld(ho_SingleRegion, out ho_Contours, "border");
        HOperatorSet.CircularityXld(ho_Contours, out hv_Circularity_xld);
        HOperatorSet.Contlength(ho_SingleRegion, out hv_ContLength);
        HOperatorSet.Circularity(ho_SingleRegion, out hv_Circularity);
        HOperatorSet.Eccentricity(ho_SingleRegion, out hv_Anisometry, out hv_Bulkiness, 
            out hv_StructureFactor);
        HOperatorSet.Roundness(ho_SingleRegion, out hv_Distance, out hv_Sigma, out hv_Roundness, 
            out hv_Sides);
        HOperatorSet.MomentsRegionCentralInvar(ho_SingleRegion, out hv_PSI1, out hv_PSI2, 
            out hv_PSI3, out hv_PSI4);
        hv_Features = new HTuple();
        hv_Features = hv_Features.TupleConcat(hv_Circularity);
        hv_Features = hv_Features.TupleConcat(hv_Circularity_xld);
        hv_Features = hv_Features.TupleConcat(hv_Roundness);
        hv_Features = hv_Features.TupleConcat(hv_Anisometry);
        hv_Features = hv_Features.TupleConcat(hv_Bulkiness);
        hv_Features = hv_Features.TupleConcat(hv_StructureFactor);
        //, PSI1, PSI2, PSI3, PSI4]
        ho_SingleRegion.Dispose();
        ho_Contours.Dispose();
    }

    public void add_samples (HObject ho_Regions, HTuple hv_GMMHandle, HTuple hv_Class)
    {
        // Local iconic variables 
        HObject ho_Region=null;
        // Local control variables 
        HTuple hv_Number = null, hv_J = null, hv_Features = new HTuple();
        // Initialize local and output iconic variables 
        HOperatorSet.GenEmptyObj(out ho_Region);
        HOperatorSet.CountObj(ho_Regions, out hv_Number);
        HTuple end_val1 = hv_Number;
        HTuple step_val1 = 1;
        for (hv_J=1; hv_J.Continue(end_val1, step_val1); hv_J = hv_J.TupleAdd(step_val1))
        {
            ho_Region.Dispose();
            HOperatorSet.SelectObj(ho_Regions, out ho_Region, hv_J);
            get_features(ho_Region, out hv_Features);
            HOperatorSet.AddSampleClassGmm(hv_GMMHandle, hv_Features, hv_Class, 0);
        }
        ho_Region.Dispose();
    }

    public void classify (HObject ho_Regions, HTuple hv_GMMHandle, out HTuple hv_Classes1)
    {
        // Local iconic variables 
        HObject ho_Region=null;
        // Local control variables 
        HTuple hv_Number = null, hv_J = null, hv_Features = new HTuple();
        HTuple hv_ClassID = new HTuple(), hv_ClassProb = new HTuple();
        HTuple hv_Density = new HTuple(), hv_KSigmaProb = new HTuple();
        // Initialize local and output iconic variables 
        HOperatorSet.GenEmptyObj(out ho_Region);
        HOperatorSet.CountObj(ho_Regions, out hv_Number);
        hv_Classes1 = new HTuple();
        HTuple end_val2 = hv_Number;
        HTuple step_val2 = 1;
        for (hv_J=1; hv_J.Continue(end_val2, step_val2); hv_J = hv_J.TupleAdd(step_val2))
        {
            ho_Region.Dispose();
            HOperatorSet.SelectObj(ho_Regions, out ho_Region, hv_J);
            get_features(ho_Region, out hv_Features);
            HOperatorSet.ClassifyClassGmm(hv_GMMHandle, hv_Features, 1, out hv_ClassID, 
                out hv_ClassProb, out hv_Density, out hv_KSigmaProb);
            hv_Classes1 = hv_Classes1.TupleConcat(hv_ClassID);
        }
        ho_Region.Dispose();
    }
    // Main procedure 
    private void RunPick(bool picktrigger)
    {
        // Stack for temporary objects 
        HObject[] OTemp = new HObject[20];
        // Local iconic variables 
        HObject ho_Image=null, ho_Rectangle=null, ho_ImageRectified=null;
        HObject ho_Regions=null, ho_FOV, ho_Objects, ho_FOV_=null;
        HObject ho_ImageRectified1=null, ho_Regions1=null, ho_Cross=null;
        HObject ho_ContCircle;
        // Local control variables 
        HTuple hv_AcqHandle = null, hv_pi = null, hv_angle = null, hv_CamParam = null;
        HTuple hv_CamPose = null, hv_GMMHandle = null, hv_Classes = null;
        HTuple hv_Index1 = null, hv_Index = null, hv_Width = null;
        HTuple hv_Height = null, hv_CamParamOut = new HTuple();
        HTuple hv_Centers = null, hv_Iter = null, hv_Classes1 = null;
        HTuple hv_Row1 = null, hv_Column1 = null, hv_Row2 = null;
        HTuple hv_Column2 = null, hv_diameter = null, hv_Length = null;
        HTuple hv_Area = new HTuple(), hv_Row = new HTuple(), hv_Column = new HTuple();
        HTuple hv_MeanArea = new HTuple(), hv_Rectangularity = new HTuple();
        HTuple hv_Circularity = new HTuple(), hv_Diameter = new HTuple();
        HTuple hv_Area1 = new HTuple(), hv_Row3 = new HTuple();
        HTuple hv_Column3 = new HTuple(), hv_MAX = new HTuple();
        HTuple hv_index = new HTuple(), hv_out = new HTuple();
        HTuple hv_joint = new HTuple(), hv_index_last = new HTuple();
        HTuple hv_i = new HTuple(), hv_j = new HTuple(), hv_k = new HTuple();
        HTuple hv_x_ = new HTuple(), hv_y_ = new HTuple(), hv_IsInside = new HTuple();
        HTuple hv_INDEX = new HTuple(), hv_pom = new HTuple();
        HTuple hv_DevDia = new HTuple(), hv_w_ = new HTuple();
        HTuple hv_a = new HTuple(), hv_b = new HTuple(), hv_x_cross = new HTuple();
        HTuple hv_y_cross = new HTuple(), hv_WorldPose = new HTuple();
        HTuple hv_HomMat3D = new HTuple(), hv_HomMat3DRotate= new HTuple();
        // Initialize local and output iconic variables 
        HOperatorSet.GenEmptyObj(out ho_Image);
        HOperatorSet.GenEmptyObj(out ho_Rectangle);
        HOperatorSet.GenEmptyObj(out ho_ImageRectified);
        HOperatorSet.GenEmptyObj(out ho_Regions);
        HOperatorSet.GenEmptyObj(out ho_FOV);
        HOperatorSet.GenEmptyObj(out ho_Objects);
        HOperatorSet.GenEmptyObj(out ho_FOV_);
        HOperatorSet.GenEmptyObj(out ho_ImageRectified1);
        HOperatorSet.GenEmptyObj(out ho_Regions1);
        HOperatorSet.GenEmptyObj(out ho_Cross);
        HOperatorSet.GenEmptyObj(out ho_ContCircle);
        //** GMM ***
        //Image Acquisition 01: Code generated by Image Acquisition 01
        HOperatorSet.OpenFramegrabber("GigEVision", 0, 0, 0, 0, 0, 0, "default", -1, 
            "default", -1, "false", "default", "acA130075gm_CAM", 0, -1, out hv_AcqHandle);
        HOperatorSet.SetFramegrabberParam(hv_AcqHandle, "ExposureAuto", "Once");
        hv_pi = 3.14159265359;
        hv_angle = (  55.7308 * hv_pi) / 180 +  (0 * hv_pi);
        //* Camera Parameters
        HOperatorSet.ReadCamPar("D:/Moji Projekti/Vision_System_OMKO/App/VisionApp/Vizijski_Omco_WPF/CamPar/intrinsics.cal", out hv_CamParam);
        HOperatorSet.ReadPose("D:/Moji Projekti/Vision_System_OMKO/App/VisionApp/Vizijski_Omco_WPF/CamPar/extrinsics.dat", out hv_CamPose);
        //create GMM classifier
        HOperatorSet.CreateClassGmm(6, 1, 1, "spherical", "normalization", 10, 42, out hv_GMMHandle);
        //create class object
        hv_Classes = new HTuple();
        for (hv_Index1=1; (int)hv_Index1<=60; hv_Index1 = (int)hv_Index1 + 1)
        {
            if (hv_Classes == null)
            {
                hv_Classes = new HTuple();
            }
            hv_Classes[hv_Index1] = 0;
            if ((int)(new HTuple(hv_Index1.TupleGreaterEqual(50))) != 0)
            {
                if (hv_Classes == null)
                    hv_Classes = new HTuple();
                hv_Classes[hv_Index1] = 1;
            }
        }
        //Add training data
        for (hv_Index=1; (int)hv_Index<=7; hv_Index = (int)hv_Index + 1)
        {
            ho_Image.Dispose();
            HOperatorSet.ReadImage(out ho_Image, ("D:/Moji Projekti/Vision_System_OMKO/App/VisionApp/Vizijski_Omco_WPF/PickImage/image_No3_"+(hv_Index-1))+".ima.tif");      
            HOperatorSet.GetImageSize(ho_Image, out hv_Width, out hv_Height);
            ho_Rectangle.Dispose();
            HOperatorSet.GenRectangle1(out ho_Rectangle, 120, 50, 840, 1150);
            HOperatorSet.ChangeRadialDistortionCamPar("adaptive", hv_CamParam, 0, out hv_CamParamOut);
            ho_ImageRectified.Dispose();
            HOperatorSet.ChangeRadialDistortionImage(ho_Image, ho_Rectangle, out ho_ImageRectified, hv_CamParam, hv_CamParamOut);
            ho_Regions.Dispose();
            segment(ho_ImageRectified, out ho_Regions);
            add_samples(ho_Regions, hv_GMMHandle, hv_Classes.TupleSelect(hv_Index));
        }
        //Train GMM classifier
        HOperatorSet.TrainClassGmm(hv_GMMHandle, 100, 0.01, "training", 0.0001, out hv_Centers, out hv_Iter);
        HOperatorSet.ClearSamplesClassGmm(hv_GMMHandle);
        //Uzimanje slike
        ho_Image.Dispose();
        HOperatorSet.GrabImage(out ho_Image, hv_AcqHandle);
        HOperatorSet.GetImageSize(ho_Image, out hv_Width, out hv_Height);
        ho_FOV.Dispose();
        HOperatorSet.GenRectangle1(out ho_FOV, 0, 0, hv_Height, hv_Width);
        HOperatorSet.ChangeRadialDistortionCamPar("adaptive", hv_CamParam, 0, out hv_CamParamOut);
        ho_ImageRectified.Dispose();
        HOperatorSet.ChangeRadialDistortionImage(ho_Image, ho_FOV, out ho_ImageRectified, hv_CamParam, hv_CamParamOut);
        {
            HObject ExpTmpOutVar_0;
            HOperatorSet.Illuminate(ho_ImageRectified, out ExpTmpOutVar_0, 230, 230, 1.15);
            ho_ImageRectified.Dispose();
            ho_ImageRectified = ExpTmpOutVar_0;
        }
        {
            HObject ExpTmpOutVar_0;
            HOperatorSet.MedianImage(ho_ImageRectified, out ExpTmpOutVar_0, "circle", 3, "continued");
            ho_ImageRectified.Dispose();
            ho_ImageRectified = ExpTmpOutVar_0;
        }
        //Segmentacija i klasifikacija
        ho_Objects.Dispose();
        segment(ho_ImageRectified, out ho_Objects);
        classify(ho_Objects, hv_GMMHandle, out hv_Classes1);
        {
            HObject ExpTmpOutVar_0;
            HOperatorSet.Connection(ho_Objects, out ExpTmpOutVar_0);
            ho_Objects.Dispose();
            ho_Objects = ExpTmpOutVar_0;
        }
        //Prefiltriranje klasificiranih predmeta
        {
            HObject ExpTmpOutVar_0;
            HOperatorSet.SelectShape(ho_Objects, out ExpTmpOutVar_0, "circularity", "and", 0.5, 1.0);
            ho_Objects.Dispose();
            ho_Objects = ExpTmpOutVar_0;
        }
        {
            HObject ExpTmpOutVar_0;
            HOperatorSet.SelectShape(ho_Objects, out ExpTmpOutVar_0, "area", "and", 1000, 100000);
            ho_Objects.Dispose();
            ho_Objects = ExpTmpOutVar_0;
        }

        HOperatorSet.DiameterRegion(ho_Objects, out hv_Row1, out hv_Column1, out hv_Row2, out hv_Column2, out hv_diameter);
        HOperatorSet.TupleLength(hv_diameter, out hv_Length);
        if ((int)(new HTuple(hv_Length.TupleGreater(0))) != 0)
        {
            HOperatorSet.AreaCenter(ho_Objects, out hv_Area, out hv_Row, out hv_Column);
            HOperatorSet.TupleMean(hv_Area, out hv_MeanArea);
            {
                HObject ExpTmpOutVar_0;
                HOperatorSet.SelectShape(ho_Objects, out ExpTmpOutVar_0, "area", "and", hv_MeanArea*0.8, 100000);
                ho_Objects.Dispose();
                ho_Objects = ExpTmpOutVar_0;
            }
            HOperatorSet.Rectangularity(ho_Objects, out hv_Rectangularity);
            HOperatorSet.Circularity(ho_Objects, out hv_Circularity);
            {
                HObject ExpTmpOutVar_0;
                HOperatorSet.SelectShape(ho_Objects, out ExpTmpOutVar_0, "rectangularity", 
                    "and", 0.0, 0.85);
                ho_Objects.Dispose();
                ho_Objects = ExpTmpOutVar_0;
            }
            {
                HObject ExpTmpOutVar_0;
                HOperatorSet.SelectShape(ho_Objects, out ExpTmpOutVar_0, "max_diameter", "and", 100, 400);
                ho_Objects.Dispose();
                ho_Objects = ExpTmpOutVar_0;
            }
            //* Correct localization due to shadow **
            ho_FOV_.Dispose();
            HOperatorSet.Difference(ho_FOV, ho_Objects, out ho_FOV_);
            ho_ImageRectified1.Dispose();
            HOperatorSet.PaintRegion(ho_FOV_, ho_ImageRectified, out ho_ImageRectified1, 0, "fill");
            {
                HObject ExpTmpOutVar_0;
                HOperatorSet.MedianImage(ho_ImageRectified1, out ExpTmpOutVar_0, "circle", 7, "continued");
                ho_ImageRectified1.Dispose();
                ho_ImageRectified1 = ExpTmpOutVar_0;
            }
                ho_Regions1.Dispose();
                segment(ho_ImageRectified1, out ho_Regions1);
                classify(ho_Objects, hv_GMMHandle, out hv_Classes1);
            {
                HObject ExpTmpOutVar_0;
                HOperatorSet.Connection(ho_Objects, out ExpTmpOutVar_0);
                ho_Objects.Dispose();
                ho_Objects = ExpTmpOutVar_0;
            }
            //** Carolija ***
            HOperatorSet.DiameterRegion(ho_Objects, out hv_Row1, out hv_Column1, out hv_Row2, out hv_Column2, out hv_Diameter);
            HOperatorSet.AreaCenter(ho_Objects, out hv_Area1, out hv_Row3, out hv_Column3);
            hv_MAX = 0;
            hv_index = 0;
            hv_out = 0;
            hv_joint = 0;
            hv_index_last = 0;
            HOperatorSet.TupleLength(hv_Diameter, out hv_Length);
            hv_i = 0;
            hv_j = 0;
            while ((int)(new HTuple(hv_i.TupleLess(hv_Length))) != 0)
            {
                if ((int)((new HTuple(((hv_Diameter.TupleSelect(hv_i))).TupleGreater(400))).TupleOr(new HTuple(((hv_Diameter.TupleSelect(hv_i))).TupleLess(100)))) != 0)
                {
                    if (hv_Diameter == null)
                    hv_Diameter = new HTuple();
                    hv_Diameter[hv_i] = 0;
                    hv_j = hv_j+1;
                }
                hv_i = hv_i+1;
            }

            HTuple end_val105 = hv_Length-hv_j;
            HTuple step_val105 = 1;
            for (hv_k=1; hv_k.Continue(end_val105, step_val105); hv_k = hv_k.TupleAdd(step_val105))
            {
                hv_x_ = ((hv_Row1.TupleSelect(hv_k-1))+(hv_Row2.TupleSelect(hv_k-1)))/2;
                hv_y_ = ((hv_Column1.TupleSelect(hv_k-1))+(hv_Column2.TupleSelect(hv_k-1)))/2;
                if ((int)((new HTuple(hv_x_.TupleLess(25))).TupleOr(new HTuple(hv_x_.TupleGreater(hv_Height-25)))) != 0)
                {
                    if (hv_Diameter == null)
                    hv_Diameter = new HTuple();
                    hv_Diameter[hv_k-1] = 0;
                }
                else if ((int)((new HTuple(hv_y_.TupleLess(25))).TupleOr(new HTuple(hv_y_.TupleGreater(hv_Width-25)))) != 0)
                {
                    if (hv_Diameter == null)
                    hv_Diameter = new HTuple();
                    hv_Diameter[hv_k-1] = 0;
                }
                HOperatorSet.TestRegionPoint(ho_FOV, hv_x_, hv_y_, out hv_IsInside);
                if ((int)(new HTuple(hv_IsInside.TupleEqual(0))) != 0)
                {
                    if (hv_Diameter == null)
                    hv_Diameter = new HTuple();
                    hv_Diameter[hv_k-1] = 0;
                }
            }
            hv_i = 1;
            HOperatorSet.TupleGenSequence(0, hv_Length-1, 1, out hv_INDEX);
            while ((int)(new HTuple(hv_i.TupleLess(hv_Length))) != 0)
            {
                hv_j = hv_i.Clone();
                while ((int)(new HTuple(((hv_Diameter.TupleSelect(hv_j-1))).TupleLess(hv_Diameter.TupleSelect(hv_j)))) != 0)
                {
                    hv_pom = hv_Diameter.TupleSelect(hv_j);
                    if (hv_Diameter == null)
                    hv_Diameter = new HTuple();
                    hv_Diameter[hv_j] = hv_Diameter.TupleSelect(hv_j-1);
                    if (hv_Diameter == null)
                    hv_Diameter = new HTuple();
                    hv_Diameter[hv_j-1] = hv_pom;
                    hv_pom = hv_INDEX.TupleSelect(hv_j);
                    if (hv_INDEX == null)
                    hv_INDEX = new HTuple();
                    hv_INDEX[hv_j] = hv_INDEX.TupleSelect(hv_j-1);
                    if (hv_INDEX == null)
                    hv_INDEX = new HTuple();
                    hv_INDEX[hv_j-1] = hv_pom;
                    hv_j = hv_j-1;
                    if ((int)(new HTuple(hv_j.TupleEqual(0))) != 0)
                    {
                        break;
                    }
                }
                hv_i = hv_i+1;
            }
            HOperatorSet.TupleMedian(hv_Diameter, out hv_MAX);
            HOperatorSet.TupleDeviation(hv_Diameter, out hv_DevDia);
            HTuple end_val141 = hv_Length;
            HTuple step_val141 = 1;
            for (hv_i=1; hv_i.Continue(end_val141, step_val141); hv_i = hv_i.TupleAdd(step_val141))
            {
                if ((int)((new HTuple(((hv_Diameter.TupleSelect(hv_i-1))).TupleGreaterEqual(hv_MAX-(5*hv_DevDia)))).TupleAnd(new HTuple(((hv_Diameter.TupleSelect(hv_i-1))).TupleLessEqual(hv_MAX+(5*hv_DevDia))))) != 0)
                {
                    if ((int)(new HTuple(hv_MAX.TupleGreaterEqual(2*300))) != 0)
                    {
                        hv_joint = 1;
                    }
                    hv_index = hv_index+1;
                }
            }
            //* w_ => weight factor for L1 norm             **
            //* row component weighting a => sort by column **
            //* column component weighting b => sort by row **
            hv_x_ = new HTuple();
            hv_y_ = new HTuple();
            hv_w_ = new HTuple();
            hv_a = 0.2;
            hv_b = 1.0;
            HTuple end_val159 = hv_index;
            HTuple step_val159 = 1;
            for (hv_k=1; hv_k.Continue(end_val159, step_val159); hv_k = hv_k.TupleAdd(step_val159))
            {
                if (hv_x_ == null)
                    hv_x_ = new HTuple();
                hv_x_[hv_k-1] = ((hv_Row1.TupleSelect(hv_INDEX.TupleSelect(hv_k-1)))+(hv_Row2.TupleSelect(hv_INDEX.TupleSelect(hv_k-1))))/2;
                if (hv_y_ == null)
                    hv_y_ = new HTuple();
                hv_y_[hv_k-1] = ((hv_Column1.TupleSelect(hv_INDEX.TupleSelect(hv_k-1)))+(hv_Column2.TupleSelect(hv_INDEX.TupleSelect(hv_k-1))))/2;
                if (hv_w_ == null)
                    hv_w_ = new HTuple();
                hv_w_[hv_k-1] = ((hv_x_.TupleSelect(hv_k-1))*hv_a)+((hv_y_.TupleSelect(hv_k-1))*hv_b);
            }
            //* Sort objects by cost value **
            hv_i = 1;
            while ((int)(new HTuple(hv_i.TupleLess(hv_index))) != 0)
            {
                hv_j = hv_i.Clone();
                while ((int)(new HTuple(((hv_w_.TupleSelect(hv_j-1))).TupleGreater(hv_w_.TupleSelect(hv_j)))) != 0)
                {
                    hv_pom = hv_x_.TupleSelect(hv_j);
                    if (hv_x_ == null)
                    hv_x_ = new HTuple();
                    hv_x_[hv_j] = hv_x_.TupleSelect(hv_j-1);
                    if (hv_x_ == null)
                    hv_x_ = new HTuple();
                    hv_x_[hv_j-1] = hv_pom;
                    hv_pom = hv_y_.TupleSelect(hv_j);
                    if (hv_y_ == null)
                    hv_y_ = new HTuple();
                    hv_y_[hv_j] = hv_y_.TupleSelect(hv_j-1);
                    if (hv_y_ == null)
                    hv_y_ = new HTuple();
                    hv_y_[hv_j-1] = hv_pom;
                    hv_pom = hv_w_.TupleSelect(hv_j);
                    if (hv_w_ == null)
                    hv_w_ = new HTuple();
                    hv_w_[hv_j] = hv_w_.TupleSelect(hv_j-1);
                    if (hv_w_ == null)
                    hv_w_ = new HTuple();
                    hv_w_[hv_j-1] = hv_pom;
                    hv_j = hv_j-1;
                    if ((int)(new HTuple(hv_j.TupleEqual(0))) != 0)
                    {
                        break;
                    }
                }
                hv_i = hv_i+1;
            }
            //***********************************************
            // Display results
            //***********************************************
            HOperatorSet.DispObj(ho_ImageRectified, hv_ExpDefaultWinHandle);
            HTuple end_val192 = hv_index;
            HTuple step_val192 = 1;
            for (hv_k = 1; hv_k.Continue(end_val192, step_val192); hv_k = hv_k.TupleAdd(step_val192))
            {
                hv_x_cross = hv_x_.TupleSelect(hv_k - 1);
                hv_y_cross = hv_y_.TupleSelect(hv_k - 1);
                HOperatorSet.SetColor(hv_ExpDefaultWinHandle, "spring green");
                ho_Cross.Dispose();
                HOperatorSet.GenCrossContourXld(out ho_Cross, hv_x_cross, hv_y_cross, 200, 0);
                HOperatorSet.DispObj(ho_Cross, hv_ExpDefaultWinHandle);
            }
            //******************************
            // World cord for Robot
            //******************************
            HOperatorSet.SetOriginPose(hv_CamPose, 0, 0, 0.00003, out hv_WorldPose);
            HOperatorSet.PoseToHomMat3d(hv_WorldPose, out hv_HomMat3D);
            HOperatorSet.HomMat3dRotateLocal(hv_HomMat3D, hv_angle, "z", out hv_HomMat3DRotate);
            HOperatorSet.HomMat3dToPose(hv_HomMat3DRotate, out hv_WorldPose);
            HOperatorSet.ImagePointsToWorldPlane(hv_CamParamOut, hv_WorldPose, hv_x_.TupleSelect(0), hv_y_.TupleSelect(0), "mm", out hv_X, out hv_Y);
            // Display pick coordinates
            HOperatorSet.SetTposition(hv_ExpDefaultWinHandle, hv_x_.TupleSelect(0) + 20, hv_y_.TupleSelect(0) + 20);
            HOperatorSet.SetColor(hv_ExpDefaultWinHandle, "red");
            HOperatorSet.WriteString(hv_ExpDefaultWinHandle, hv_X);
            HOperatorSet.WriteString(hv_ExpDefaultWinHandle, ", ");
            HOperatorSet.WriteString(hv_ExpDefaultWinHandle, hv_Y);
        }

        ho_ContCircle.Dispose();
        HOperatorSet.CloseFramegrabber(hv_AcqHandle);
        // Dispose unused objects
        ho_Image.Dispose();
        ho_Rectangle.Dispose();
        ho_ImageRectified.Dispose();
        ho_Regions.Dispose();
        ho_FOV.Dispose();
        ho_Objects.Dispose();
        ho_FOV_.Dispose();
        ho_ImageRectified1.Dispose();
        ho_Regions1.Dispose();
        ho_Cross.Dispose();
        ho_ContCircle.Dispose();
    }

    public void RobotPick(HTuple window, bool trigger = false)
    {
        hv_ExpDefaultWinHandle = window;
        HOperatorSet.ClearWindow(hv_ExpDefaultWinHandle);
        if (trigger == false)
        {
            RunPick(false);
        }
        else if (trigger)
        {
            RunPick(true);
        }

        koordinate.RXcord = (float) hv_X.D;
        koordinate.RYcord = (float) hv_Y.D;
        // Chech for infinity Double to float conversion
        if (float.IsPositiveInfinity(argumenti.PXvalue))
        {
            koordinate.RXcord = float.MaxValue;
            koordinate.RYcord = float.MaxValue;
        }
        else if (float.IsNegativeInfinity(argumenti.PXvalue))
        {
            koordinate.RXcord = float.MinValue;
            koordinate.RYcord = float.MinValue;
        }

        if (UpdateResultPick != null)
            UpdateResultPick(this, koordinate);
    }

}

