using HalconDotNet;

public partial class HDevelopExport
{
    // Local procedures 
    public void segment (HObject ho_Image, out HObject ho_Regions)
    {
        // Local iconic variables 
        HObject ho_EdgeAmplitude, ho_Region, ho_ConnectedRegions;

        // Initialize local and output iconic variables 
        HOperatorSet.GenEmptyObj(out ho_Regions);
        HOperatorSet.GenEmptyObj(out ho_EdgeAmplitude);
        HOperatorSet.GenEmptyObj(out ho_Region);
        HOperatorSet.GenEmptyObj(out ho_ConnectedRegions);
        try
        {
            ho_EdgeAmplitude.Dispose();
            HOperatorSet.SobelAmp(ho_Image, out ho_EdgeAmplitude, "sum_abs", 13);
            ho_Region.Dispose();
            HOperatorSet.BinaryThreshold(ho_EdgeAmplitude, out ho_Region, "max_separability", 
                "light", out _);
            {
                HObject ExpTmpOutVar_0;
                HOperatorSet.CloseEdgesLength(ho_Region, ho_EdgeAmplitude, out ExpTmpOutVar_0, 
                    10, 127);
                ho_Region.Dispose();
                ho_Region = ExpTmpOutVar_0;
            }
                ho_ConnectedRegions.Dispose();
                HOperatorSet.Connection(ho_Region, out ho_ConnectedRegions);
                HOperatorSet.ConnectAndHoles(ho_ConnectedRegions, out _, out _);
            {
                HObject ExpTmpOutVar_0;
                HOperatorSet.ClosingCircle(ho_ConnectedRegions, out ExpTmpOutVar_0, 33);
                ho_ConnectedRegions.Dispose();
                ho_ConnectedRegions = ExpTmpOutVar_0;
            }

            ho_Regions.Dispose();
            HOperatorSet.FillUp(ho_ConnectedRegions, out ho_Regions);
            ho_EdgeAmplitude.Dispose();
            ho_Region.Dispose();
            ho_ConnectedRegions.Dispose();
        }
    catch (HalconException HDevExpDefaultException)
    {
        ho_EdgeAmplitude.Dispose();
        ho_Region.Dispose();
        ho_ConnectedRegions.Dispose();

        throw HDevExpDefaultException;
    }
    }

    public void get_features (HObject ho_Region, out HTuple hv_Features)
    {
        // Local iconic variables 
        HObject ho_SingleRegion, ho_Contours;

        // Local control variables 
        HTuple hv_Circularity_xld = null;
        HTuple hv_Circularity = null, hv_Anisometry = null, hv_Bulkiness = null;
        HTuple hv_StructureFactor = null;
        HTuple hv_Roundness = null;
        // Initialize local and output iconic variables 
        HOperatorSet.GenEmptyObj(out ho_SingleRegion);
        HOperatorSet.GenEmptyObj(out ho_Contours);
        try
        {
            ho_SingleRegion.Dispose();
            HOperatorSet.SelectObj(ho_Region, out ho_SingleRegion, 1);
            ho_Contours.Dispose();
            HOperatorSet.GenContourRegionXld(ho_SingleRegion, out ho_Contours, "border");
            HOperatorSet.CircularityXld(ho_Contours, out hv_Circularity_xld);
            HOperatorSet.Contlength(ho_SingleRegion, out _);
            HOperatorSet.Circularity(ho_SingleRegion, out hv_Circularity);
            HOperatorSet.Eccentricity(ho_SingleRegion, out hv_Anisometry, out hv_Bulkiness, 
                out hv_StructureFactor);
            HOperatorSet.Roundness(ho_SingleRegion, out _, out _, out hv_Roundness, 
                out _);
            HOperatorSet.MomentsRegionCentralInvar(ho_SingleRegion, out _, out _, 
                out _, out _);
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
        catch (HalconException HDevExpDefaultException)
        {
            ho_SingleRegion.Dispose();
            ho_Contours.Dispose();

            throw HDevExpDefaultException;
        }
    }

    public void add_samples (HObject ho_Regions, HTuple hv_GMMHandle, HTuple hv_Class)
    {
        // Local iconic variables 
        HObject ho_Region=null;

        // Local control variables 
        HTuple hv_Number = null, hv_J = null, hv_Features;
        // Initialize local and output iconic variables 
        HOperatorSet.GenEmptyObj(out ho_Region);
        try
        {
            HOperatorSet.CountObj(ho_Regions, out hv_Number);
            HTuple endVal1 = hv_Number;
            HTuple stepVal1 = 1;
            for (hv_J=1; hv_J.Continue(endVal1, stepVal1); hv_J = hv_J.TupleAdd(stepVal1))
            {
                ho_Region.Dispose();
                HOperatorSet.SelectObj(ho_Regions, out ho_Region, hv_J);
                get_features(ho_Region, out hv_Features);
                HOperatorSet.AddSampleClassGmm(hv_GMMHandle, hv_Features, hv_Class, 0);
            }
            ho_Region.Dispose();
        }
        catch (HalconException HDevExpDefaultException)
        {
            ho_Region.Dispose();

            throw HDevExpDefaultException;
        }
    }

    public void classify (HObject ho_Regions, HTuple hv_GMMHandle, out HTuple hv_Classes1)
    {
        // Local iconic variables 
        HObject ho_Region=null;

        // Local control variables 
        HTuple hv_Number = null, hv_J = null, hv_Features;
        HTuple hv_ClassID;

        // Initialize local and output iconic variables 
        HOperatorSet.GenEmptyObj(out ho_Region);
        try
        {
            HOperatorSet.CountObj(ho_Regions, out hv_Number);
            hv_Classes1 = new HTuple();
            HTuple endVal2 = hv_Number;
            HTuple stepVal2 = 1;
            for (hv_J=1; hv_J.Continue(endVal2, stepVal2); hv_J = hv_J.TupleAdd(stepVal2))
            {
                ho_Region.Dispose();
                HOperatorSet.SelectObj(ho_Regions, out ho_Region, hv_J);
                get_features(ho_Region, out hv_Features);
                HOperatorSet.ClassifyClassGmm(hv_GMMHandle, hv_Features, 1, out hv_ClassID, 
                    out _, out _, out _);
                hv_Classes1 = hv_Classes1.TupleConcat(hv_ClassID);
            }
            ho_Region.Dispose();
        }
        catch (HalconException HDevExpDefaultException)
        {
            ho_Region.Dispose();
            throw HDevExpDefaultException;
        }
    }

    public void disp_obj_class (HObject ho_Regions, HTuple hv_Classes)
    {
        // Local iconic variables 
        HObject ho_Region=null;

        // Local control variables 
        HTuple hv_Number = null, hv_Colors = null;
        HTuple hv_J = null;
        // Initialize local and output iconic variables 
        HOperatorSet.GenEmptyObj(out ho_Region);
        try
        {
            HOperatorSet.CountObj(ho_Regions, out hv_Number);
            hv_Colors = new HTuple();
            hv_Colors[0] = "yellow";
            hv_Colors[1] = "magenta";
            hv_Colors[2] = "green";
            HTuple endVal2 = hv_Number;
            HTuple stepVal2 = 1;
            for (hv_J=1; hv_J.Continue(endVal2, stepVal2); hv_J = hv_J.TupleAdd(stepVal2))
            {
                ho_Region.Dispose();
                HOperatorSet.SelectObj(ho_Regions, out ho_Region, hv_J);
                HOperatorSet.SetColor(hv_ExpDefaultWinHandle, hv_Colors.TupleSelect(hv_Classes.TupleSelect(
                    hv_J-1)));
                HOperatorSet.DispObj(ho_Region, hv_ExpDefaultWinHandle);

            }
            ho_Region.Dispose();
        }
        catch (HalconException HDevExpDefaultException)
        {
            ho_Region.Dispose();
            throw HDevExpDefaultException;
        }
    }

    // Main procedure 
    private void RunPick()
    {
    // Local iconic variables 
    HObject ho_Image=null, ho_Rectangle=null, ho_ImageRectified=null;
    HObject ho_Regions=null, ho_FOV=null, ho_Objects=null, ho_Cross=null;

    // Local control variables 
    HTuple hv_AcqHandle = null, hv_pi = null, hv_CamParam = null;
    HTuple hv_CamPose = null;
    HTuple hv_GMMHandle = null, hv_Classes = null, hv_Index1 = null;
    HTuple hv_Index = null, hv_Width = new HTuple(), hv_Height = new HTuple();
    HTuple hv_CamParamOut = new HTuple(), hv_Centers = null;
    HTuple hv_Iter = null, hv_Classes1 = new HTuple(), hv_Row1 = new HTuple();
    HTuple hv_Column1 = new HTuple(), hv_Row2 = new HTuple();
    HTuple hv_Column2 = new HTuple(), hv_diameter = new HTuple();
    HTuple hv_Length = new HTuple(), hv_Area = new HTuple();
    HTuple hv_Row = new HTuple(), hv_Column = new HTuple();
    HTuple hv_MeanArea = new HTuple(), hv_Rectangularity = new HTuple();
    HTuple hv_Diameter = new HTuple(), hv_MAX = new HTuple();
    HTuple hv_index = new HTuple(), hv_out = new HTuple();
    HTuple hv_joint = new HTuple(), hv_index_last = new HTuple();
    HTuple hv_i = new HTuple(), hv_j = new HTuple(), hv_k = new HTuple();
    HTuple hv_x_ = new HTuple(), hv_y_ = new HTuple(), hv_IsInside = new HTuple();
    HTuple hv_dia_max = new HTuple(), hv_INDEX = new HTuple();
    HTuple hv_pom = new HTuple(), hv_DevDia = new HTuple();
    HTuple hv_o = new HTuple(), hv_Pow_x = new HTuple(), hv_Pow_y = new HTuple();
    HTuple hv_d_eukl = new HTuple(), hv_x_pom = new HTuple();
    HTuple hv_y_pom = new HTuple(), hv_coor_len = new HTuple();
    HTuple hv_Exception = null;

    // Initialize local and output iconic variables 
    HOperatorSet.GenEmptyObj(out ho_Image);
    HOperatorSet.GenEmptyObj(out ho_Rectangle);
    HOperatorSet.GenEmptyObj(out ho_ImageRectified);
    HOperatorSet.GenEmptyObj(out ho_Regions);
    HOperatorSet.GenEmptyObj(out ho_FOV);
    HOperatorSet.GenEmptyObj(out ho_Objects);
    HOperatorSet.GenEmptyObj(out ho_Cross);
    try
    {
        //** GMM ***
        //OpenCam1
        HOperatorSet.OpenFramegrabber("GigEVision", 0, 0, 0, 0, 0, 0, "default", -1, 
            "default", -1, "false", "default", "acA130075gm_CAM", 0, -1, out hv_AcqHandle);
        HOperatorSet.SetFramegrabberParam(hv_AcqHandle, "ExposureAuto", "Continuous");
        hv_pi = 3.14159265359;

        //* Read camera intrinsics
        HOperatorSet.ReadCamPar("D:/Moji Projekti/Vision_System_OMKO/App/VisionApp/Vizijski_Omco_WPF/CamPar/intrinsics.cal", out hv_CamParam);
        HOperatorSet.ReadPose("D:/Moji Projekti/Vision_System_OMKO/App/VisionApp/Vizijski_Omco_WPF/CamPar/extrinsics.dat", out hv_CamPose);
        //* create GMM classifier
        HOperatorSet.CreateClassGmm(6, 1, 1, "spherical", "normalization", 10, 42, 
            out hv_GMMHandle);

        //* create class object
        hv_Classes = new HTuple();
        for (hv_Index1=1; (int)hv_Index1<=60; hv_Index1 = (int)hv_Index1 + 1)
        {
        if (hv_Classes == null)
            hv_Classes = new HTuple();
        hv_Classes[hv_Index1] = 0;
        if ((int)(new HTuple(hv_Index1.TupleGreaterEqual(50))) != 0)
        {
            if (hv_Classes == null)
            hv_Classes = new HTuple();
            hv_Classes[hv_Index1] = 1;
        }
        }

        //* add training data
        for (hv_Index=1; (int)hv_Index<=7; hv_Index = (int)hv_Index + 1)
        {
        ho_Image.Dispose();
        HOperatorSet.ReadImage(out ho_Image, ("D:/Moji Projekti/Vision_System_OMKO/App/VisionApp/Vizijski_Omco_WPF/PickImage/image_No3_"+(hv_Index-1))+".ima.tif");
        HOperatorSet.GetImageSize(ho_Image, out hv_Width, out hv_Height);
        ho_Rectangle.Dispose();
        HOperatorSet.GenRectangle1(out ho_Rectangle, 0, 0, hv_Height, hv_Width);
        HOperatorSet.ChangeRadialDistortionCamPar("adaptive", hv_CamParam, 0, out hv_CamParamOut);
        ho_ImageRectified.Dispose();
        HOperatorSet.ChangeRadialDistortionImage(ho_Image, ho_Rectangle, out ho_ImageRectified, 
            hv_CamParam, hv_CamParamOut);
        ho_Regions.Dispose();
        segment(ho_ImageRectified, out ho_Regions);
        add_samples(ho_Regions, hv_GMMHandle, hv_Classes.TupleSelect(hv_Index));
        }

        //Train GMM classifier
        HOperatorSet.TrainClassGmm(hv_GMMHandle, 100, 0.01, "training", 0.0001, out hv_Centers, 
            out hv_Iter);
        HOperatorSet.ClearSamplesClassGmm(hv_GMMHandle);

        dev_update_off();

        try
        {
        //* Image acqusition
        ho_Image.Dispose();
        HOperatorSet.GrabImage(out ho_Image, hv_AcqHandle);
        HOperatorSet.GetImageSize(ho_Image, out hv_Width, out hv_Height);
        ho_FOV.Dispose();
        HOperatorSet.GenRectangle1(out ho_FOV, 0, 0, hv_Height, hv_Width);
        HOperatorSet.ChangeRadialDistortionCamPar("adaptive", hv_CamParam, 0, out hv_CamParamOut);
        ho_ImageRectified.Dispose();
        HOperatorSet.ChangeRadialDistortionImage(ho_Image, ho_FOV, out ho_ImageRectified, 
            hv_CamParam, hv_CamParamOut);


        //* Segmentation and classification
        ho_Objects.Dispose();
        segment(ho_ImageRectified, out ho_Objects);
        classify(ho_Objects, hv_GMMHandle, out hv_Classes1);
        disp_obj_class(ho_Objects, hv_Classes1);
        {
        HObject ExpTmpOutVar_0;
        HOperatorSet.Connection(ho_Objects, out ExpTmpOutVar_0);
        ho_Objects.Dispose();
        ho_Objects = ExpTmpOutVar_0;
        }


        //* Exit routine, because palette part empty
        HOperatorSet.DiameterRegion(ho_Objects, out hv_Row1, out hv_Column1, out hv_Row2, 
            out hv_Column2, out hv_diameter);
        HOperatorSet.TupleLength(hv_diameter, out hv_Length);
        if ((int)(new HTuple(hv_Length.TupleGreater(450))) != 0)
        {
            //*         break
        }


        //* prefiltriranje klasificiranih predmeta
        {
        HObject ExpTmpOutVar_0;
        HOperatorSet.SelectShape(ho_Objects, out ExpTmpOutVar_0, "circularity", "and", 
            0.5, 1.0);
        ho_Objects.Dispose();
        ho_Objects = ExpTmpOutVar_0;
        }
        {
        HObject ExpTmpOutVar_0;
        HOperatorSet.SelectShape(ho_Objects, out ExpTmpOutVar_0, "area", "and", 1000, 
            100000);
        ho_Objects.Dispose();
        ho_Objects = ExpTmpOutVar_0;
        }
        HOperatorSet.AreaCenter(ho_Objects, out hv_Area, out hv_Row, out hv_Column);
        HOperatorSet.TupleMean(hv_Area, out hv_MeanArea);
        {
        HObject ExpTmpOutVar_0;
        HOperatorSet.SelectShape(ho_Objects, out ExpTmpOutVar_0, "area", "and", hv_MeanArea*0.8, 
            100000);
        ho_Objects.Dispose();
        ho_Objects = ExpTmpOutVar_0;
        }
        HOperatorSet.Rectangularity(ho_Objects, out hv_Rectangularity);
        {
        HObject ExpTmpOutVar_0;
        HOperatorSet.SelectShape(ho_Objects, out ExpTmpOutVar_0, "rectangularity", 
            "and", 0.0, 0.85);
        ho_Objects.Dispose();
        ho_Objects = ExpTmpOutVar_0;
        }
        {
        HObject ExpTmpOutVar_0;
        HOperatorSet.SelectShape(ho_Objects, out ExpTmpOutVar_0, "max_diameter", 
            "and", 100, 400);
        ho_Objects.Dispose();
        ho_Objects = ExpTmpOutVar_0;
        }


        //* Carolija
        HOperatorSet.DiameterRegion(ho_Objects, out hv_Row1, out hv_Column1, out hv_Row2, out hv_Column2, out hv_Diameter);
        hv_index = 0;
        HOperatorSet.TupleLength(hv_Diameter, out hv_Length);
        if ((int)(new HTuple(hv_Length.TupleGreater(450))) != 0)
        {
            //*         break
        }

        hv_i = 0;
        hv_j = 0;
        while ((int)(new HTuple(hv_i.TupleLess(hv_Length))) != 0)
        {
            if ((int)((new HTuple(((hv_Diameter.TupleSelect(hv_i))).TupleGreater(400))).TupleOr(
                new HTuple(((hv_Diameter.TupleSelect(hv_i))).TupleLess(100)))) != 0)
            {
            if (hv_Diameter == null)
                hv_Diameter = new HTuple();
            hv_Diameter[hv_i] = 0;
            hv_j = hv_j+1;
            }
            hv_i = hv_i+1;
        }

        //* Test if object is in safe zone
        HTuple endVal98 = hv_Length-hv_j;
        HTuple stepVal98 = 1;
        for (hv_k=1; hv_k.Continue(endVal98, stepVal98); hv_k = hv_k.TupleAdd(stepVal98))
        {
            hv_x_ = ((hv_Row1.TupleSelect(hv_k-1))+(hv_Row2.TupleSelect(hv_k-1)))/2;
            hv_y_ = ((hv_Column1.TupleSelect(hv_k-1))+(hv_Column2.TupleSelect(hv_k-1)))/2;
            if ((int)((new HTuple(hv_x_.TupleLess(25))).TupleOr(new HTuple(hv_x_.TupleGreater(
                hv_Height-25)))) != 0)
            {
            if (hv_Diameter == null)
                hv_Diameter = new HTuple();
            hv_Diameter[hv_k-1] = 0;
            }
            else if ((int)((new HTuple(hv_y_.TupleLess(25))).TupleOr(new HTuple(hv_y_.TupleGreater(
                hv_Width-25)))) != 0)
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

        //* Exit routine, because palette part empty
        HOperatorSet.TupleMax(hv_Diameter, out hv_dia_max);
        if ((int)(new HTuple(hv_dia_max.TupleLess(100))) != 0)
        {
            //*         break
        }

        //* Sort objects by diameter
        hv_i = 1;
        HOperatorSet.TupleGenSequence(0, hv_Length-1, 1, out hv_INDEX);
        while ((int)(new HTuple(hv_i.TupleLess(hv_Length))) != 0)
        {
            hv_j = hv_i.Clone();
            while ((int)(new HTuple(((hv_Diameter.TupleSelect(hv_j-1))).TupleLess(hv_Diameter.TupleSelect(
                hv_j)))) != 0)
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

        //* Cluster objects
        HOperatorSet.TupleMedian(hv_Diameter, out hv_MAX);
        HOperatorSet.TupleDeviation(hv_Diameter, out hv_DevDia);
        HTuple endVal141 = hv_Length;
        HTuple stepVal141 = 1;
        for (hv_i=1; hv_i.Continue(endVal141, stepVal141); hv_i = hv_i.TupleAdd(stepVal141))
        {
            if ((int)((new HTuple(((hv_Diameter.TupleSelect(hv_i-1))).TupleGreater(
                hv_MAX-(3*hv_DevDia)))).TupleAnd(new HTuple(((hv_Diameter.TupleSelect(
                hv_i-1))).TupleLess(hv_MAX+(3*hv_DevDia))))) != 0)
            {
            if ((int)(new HTuple(hv_MAX.TupleGreaterEqual(2*300))) != 0)
            {
            }
            hv_index = hv_index+1;
            }
        }

        hv_x_ = new HTuple();
        hv_y_ = new HTuple();

        HTuple endVal153 = hv_index;
        HTuple stepVal153 = 1;
        for (hv_k=1; hv_k.Continue(endVal153, stepVal153); hv_k = hv_k.TupleAdd(stepVal153))
        {
            if (hv_x_ == null)
            hv_x_ = new HTuple();
            hv_x_[hv_k] = ((hv_Row1.TupleSelect(hv_INDEX.TupleSelect(hv_k-1)))+(hv_Row2.TupleSelect(
                hv_INDEX.TupleSelect(hv_k-1))))/2;
            if (hv_y_ == null)
            hv_y_ = new HTuple();
            hv_y_[hv_k] = ((hv_Column1.TupleSelect(hv_INDEX.TupleSelect(hv_k-1)))+(hv_Column2.TupleSelect(
                hv_INDEX.TupleSelect(hv_k-1))))/2;
        }

        HTuple endVal158 = hv_index;
        HTuple stepVal158 = 1;
        for (hv_k=1; hv_k.Continue(endVal158, stepVal158); hv_k = hv_k.TupleAdd(stepVal158))
        {
            HTuple endVal159 = hv_index;
            HTuple stepVal159 = 1;
            for (hv_o=hv_k+1; hv_o.Continue(endVal159, stepVal159); hv_o = hv_o.TupleAdd(stepVal159))
            {
            HOperatorSet.TuplePow((hv_x_.TupleSelect(hv_k))-(hv_x_.TupleSelect(hv_o)), 
                2, out hv_Pow_x);
            HOperatorSet.TuplePow((hv_y_.TupleSelect(hv_k))-(hv_y_.TupleSelect(hv_o)), 
                2, out hv_Pow_y);
            HOperatorSet.TupleSqrt(hv_Pow_x+hv_Pow_y, out hv_d_eukl);
            if ((int)(new HTuple(hv_d_eukl.TupleLess(100))) != 0)
            {
                hv_x_pom = ((hv_x_.TupleSelect(hv_k))+(hv_x_.TupleSelect(hv_o)))/2;
                hv_y_pom = ((hv_y_.TupleSelect(hv_k))+(hv_y_.TupleSelect(hv_o)))/2;
                if (hv_x_ == null)
                hv_x_ = new HTuple();
                hv_x_[hv_k] = hv_x_pom;
                if (hv_x_ == null)
                hv_x_ = new HTuple();
                hv_x_[hv_o] = hv_x_pom;
                if (hv_y_ == null)
                hv_y_ = new HTuple();
                hv_y_[hv_k] = hv_y_pom;
                if (hv_y_ == null)
                hv_y_ = new HTuple();
                hv_y_[hv_o] = hv_y_pom;
            }
            }
        }

        //* Exit routine, because palette part empty
        HOperatorSet.TupleLength(hv_x_, out hv_coor_len);
        if ((int)(new HTuple(hv_coor_len.TupleEqual(0))) != 0)
        {
            //*         break
        }

        HTuple endVal180 = hv_index;
        HTuple stepVal180 = 1;
        for (hv_k=1; hv_k.Continue(endVal180, stepVal180); hv_k = hv_k.TupleAdd(stepVal180))
        {
            hv_x_cross = hv_x_.TupleSelect(hv_k);
            hv_y_cross = hv_y_.TupleSelect(hv_k);
            //* Display cross of kth object
            HOperatorSet.SetColor(hv_ExpDefaultWinHandle, "green");
            ho_Cross.Dispose();
            HOperatorSet.GenCrossContourXld(out ho_Cross, hv_x_cross, hv_y_cross, 200, 
                0);
            HOperatorSet.DispObj(ho_Cross, hv_ExpDefaultWinHandle);
            //* Display coordinates of kth object
            HOperatorSet.SetColor(hv_ExpDefaultWinHandle, "red");
            HOperatorSet.SetTposition(hv_ExpDefaultWinHandle, hv_x_cross, hv_y_cross);
            HOperatorSet.WriteString(hv_ExpDefaultWinHandle, hv_x_cross.TupleInt());
            HOperatorSet.WriteString(hv_ExpDefaultWinHandle, new HTuple(", "));
            HOperatorSet.WriteString(hv_ExpDefaultWinHandle, hv_y_cross.TupleInt());
        }

        HOperatorSet.ImagePointsToWorldPlane(hv_CamParam, hv_CamPose, hv_x_cross,hv_y_cross,"mm", out hv_X, out hv_Y);
        //HOperatorSet.ClearWindow(hv_ExpDefaultWinHandle);

        }
        // catch (Exception) 
        catch (HalconException HDevExpDefaultException1)
        {
        HDevExpDefaultException1.ToHTuple(out hv_Exception);
        }

        HOperatorSet.CloseFramegrabber(hv_AcqHandle);

    }
    catch (HalconException HDevExpDefaultException)
    {
        ho_Image.Dispose();
        ho_Rectangle.Dispose();
        ho_ImageRectified.Dispose();
        ho_Regions.Dispose();
        ho_FOV.Dispose();
        ho_Objects.Dispose();
        ho_Cross.Dispose();

        throw HDevExpDefaultException;
    }
    ho_Image.Dispose();
    ho_Rectangle.Dispose();
    ho_ImageRectified.Dispose();
    ho_Regions.Dispose();
    ho_FOV.Dispose();
    ho_Objects.Dispose();
    ho_Cross.Dispose();

    }

    public void RobotPick(HTuple window)
    {
        hv_ExpDefaultWinHandle = window;
        RunPick();
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

