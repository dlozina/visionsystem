using HalconDotNet;
using System.Windows;

namespace VizijskiSustavWPF.HALCON
{
    public partial class HDevelopExport
    {
        

        public void HDevelopStop()
        {
            MessageBox.Show("Press button to continue", "Program stop");
        }

        // Short Description: Creates an arrow shaped XLD contour. 
        public void gen_arrow_contour_xld(out HObject ho_Arrow, HTuple hv_Row1, HTuple hv_Column1,
            HTuple hv_Row2, HTuple hv_Column2, HTuple hv_HeadLength, HTuple hv_HeadWidth)
        {
            // Stack for temporary objects 
            HObject[] OTemp = new HObject[20];

            // Local iconic variables 

            HObject ho_TempArrow = null;

            // Local control variables 

            HTuple hv_Length = null, hv_ZeroLengthIndices = null;
            HTuple hv_DR = null, hv_DC = null, hv_HalfHeadWidth = null;
            HTuple hv_RowP1 = null, hv_ColP1 = null, hv_RowP2 = null;
            HTuple hv_ColP2 = null, hv_Index = null;
            // Initialize local and output iconic variables 
            HOperatorSet.GenEmptyObj(out ho_Arrow);
            HOperatorSet.GenEmptyObj(out ho_TempArrow);
            //This procedure generates arrow shaped XLD contours,
            ho_Arrow.Dispose();
            HOperatorSet.GenEmptyObj(out ho_Arrow);
            //
            //Calculate the arrow length
            HOperatorSet.DistancePp(hv_Row1, hv_Column1, hv_Row2, hv_Column2, out hv_Length);
            //
            //Mark arrows with identical start and end point
            //(set Length to -1 to avoid division-by-zero exception)
            hv_ZeroLengthIndices = hv_Length.TupleFind(0);
            if ((int)(new HTuple(hv_ZeroLengthIndices.TupleNotEqual(-1))) != 0)
            {
                if (hv_Length == null)
                    hv_Length = new HTuple();
                hv_Length[hv_ZeroLengthIndices] = -1;
            }
            //
            //Calculate auxiliary variables.
            hv_DR = (1.0 * (hv_Row2 - hv_Row1)) / hv_Length;
            hv_DC = (1.0 * (hv_Column2 - hv_Column1)) / hv_Length;
            hv_HalfHeadWidth = hv_HeadWidth / 2.0;
            //
            //Calculate end points of the arrow head.
            hv_RowP1 = (hv_Row1 + ((hv_Length - hv_HeadLength) * hv_DR)) + (hv_HalfHeadWidth * hv_DC);
            hv_ColP1 = (hv_Column1 + ((hv_Length - hv_HeadLength) * hv_DC)) - (hv_HalfHeadWidth * hv_DR);
            hv_RowP2 = (hv_Row1 + ((hv_Length - hv_HeadLength) * hv_DR)) - (hv_HalfHeadWidth * hv_DC);
            hv_ColP2 = (hv_Column1 + ((hv_Length - hv_HeadLength) * hv_DC)) + (hv_HalfHeadWidth * hv_DR);
            //
            //Finally create output XLD contour for each input point pair
            for (hv_Index = 0; (int)hv_Index <= (int)((new HTuple(hv_Length.TupleLength())) - 1); hv_Index = (int)hv_Index + 1)
            {
                if ((int)(new HTuple(((hv_Length.TupleSelect(hv_Index))).TupleEqual(-1))) != 0)
                {
                    //Create_ single points for arrows with identical start and end point
                    ho_TempArrow.Dispose();
                    HOperatorSet.GenContourPolygonXld(out ho_TempArrow, hv_Row1.TupleSelect(hv_Index),
                        hv_Column1.TupleSelect(hv_Index));
                }
                else
                {
                    //Create arrow contour
                    ho_TempArrow.Dispose();
                    HOperatorSet.GenContourPolygonXld(out ho_TempArrow, ((((((((((hv_Row1.TupleSelect(
                        hv_Index))).TupleConcat(hv_Row2.TupleSelect(hv_Index)))).TupleConcat(
                        hv_RowP1.TupleSelect(hv_Index)))).TupleConcat(hv_Row2.TupleSelect(hv_Index)))).TupleConcat(
                        hv_RowP2.TupleSelect(hv_Index)))).TupleConcat(hv_Row2.TupleSelect(hv_Index)),
                        ((((((((((hv_Column1.TupleSelect(hv_Index))).TupleConcat(hv_Column2.TupleSelect(
                        hv_Index)))).TupleConcat(hv_ColP1.TupleSelect(hv_Index)))).TupleConcat(
                        hv_Column2.TupleSelect(hv_Index)))).TupleConcat(hv_ColP2.TupleSelect(
                        hv_Index)))).TupleConcat(hv_Column2.TupleSelect(hv_Index)));
                }
                {
                    HObject ExpTmpOutVar_0;
                    HOperatorSet.ConcatObj(ho_Arrow, ho_TempArrow, out ExpTmpOutVar_0);
                    ho_Arrow.Dispose();
                    ho_Arrow = ExpTmpOutVar_0;
                }
            }
            ho_TempArrow.Dispose();

            return;
        }

        // Local procedures 
        public void segment(HObject ho_Image, out HObject ho_Object)
        {
            // Local iconic variables 
            HObject ho_ImaAmp, ho_ImaDir, ho_Margin;
            // Initialize local and output iconic variables 
            HOperatorSet.GenEmptyObj(out ho_Object);
            HOperatorSet.GenEmptyObj(out ho_ImaAmp);
            HOperatorSet.GenEmptyObj(out ho_ImaDir);
            HOperatorSet.GenEmptyObj(out ho_Margin);
            ho_ImaAmp.Dispose(); ho_ImaDir.Dispose();
            HOperatorSet.EdgesImage(ho_Image, out ho_ImaAmp, out ho_ImaDir, "canny", 1, "nms",
                20, 40);
            ho_Margin.Dispose();
            HOperatorSet.HysteresisThreshold(ho_ImaAmp, out ho_Margin, 20, 30, 30);
            ho_Object.Dispose();
            HOperatorSet.FillUpShape(ho_Margin, out ho_Object, "outer_circle", 1, 400);
            ho_ImaAmp.Dispose();
            ho_ImaDir.Dispose();
            ho_Margin.Dispose();
            return;
        }

        public void get_features(HObject ho_Region, out HTuple hv_Features)
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
            //, PSI1, PSI2, PSI3, PSI4]
            ho_SingleRegion.Dispose();
            ho_Contours.Dispose();
            return;
        }

        public void add_samples(HObject ho_Regions, HTuple hv_GMMHandle, HTuple hv_Class)
        {
            // Local iconic variables 
            HObject ho_ConnectedRegions = null, ho_SelectedRegions = null;
            HObject ho_Region = null;
            // Local control variables 
            HTuple hv_Number = null, hv_J = null, hv_Features = new HTuple();
            // Initialize local and output iconic variables 
            HOperatorSet.GenEmptyObj(out ho_ConnectedRegions);
            HOperatorSet.GenEmptyObj(out ho_SelectedRegions);
            HOperatorSet.GenEmptyObj(out ho_Region);
            HOperatorSet.CountObj(ho_Regions, out hv_Number);
            HTuple end_val1 = hv_Number;
            HTuple step_val1 = 1;
            for (hv_J = 1; hv_J.Continue(end_val1, step_val1); hv_J = hv_J.TupleAdd(step_val1))
            {
                ho_ConnectedRegions.Dispose();
                HOperatorSet.Connection(ho_Regions, out ho_ConnectedRegions);
                ho_SelectedRegions.Dispose();
                HOperatorSet.SelectShape(ho_ConnectedRegions, out ho_SelectedRegions, "area",
                    "and", 1000, 100000);
                ho_Region.Dispose();
                HOperatorSet.SelectObj(ho_SelectedRegions, out ho_Region, hv_J);
                get_features(ho_Region, out hv_Features);
                HOperatorSet.AddSampleClassGmm(hv_GMMHandle, hv_Features, hv_Class, 0);
            }
            ho_ConnectedRegions.Dispose();
            ho_SelectedRegions.Dispose();
            ho_Region.Dispose();
            return;
        }

        public void classify(HObject ho_Regions, HTuple hv_GMMHandle, out HTuple hv_Classes1)
        {
            // Stack for temporary objects 
            HObject[] OTemp = new HObject[20];
            // Local iconic variables 
            HObject ho_Region = null, ho_ConnectedRegions = null;
            HObject ho_SelectedRegions = null;
            // Local control variables 
            HTuple hv_Number = null, hv_J = null, hv_Features = new HTuple();
            HTuple hv_ClassID = new HTuple(), hv_ClassProb = new HTuple();
            HTuple hv_Density = new HTuple(), hv_KSigmaProb = new HTuple();
            // Initialize local and output iconic variables 
            HOperatorSet.GenEmptyObj(out ho_Region);
            HOperatorSet.GenEmptyObj(out ho_ConnectedRegions);
            HOperatorSet.GenEmptyObj(out ho_SelectedRegions);
            HOperatorSet.CountObj(ho_Regions, out hv_Number);
            hv_Classes1 = new HTuple();
            HTuple end_val2 = hv_Number;
            HTuple step_val2 = 1;
            for (hv_J = 1; hv_J.Continue(end_val2, step_val2); hv_J = hv_J.TupleAdd(step_val2))
            {
                ho_Region.Dispose();
                HOperatorSet.SelectObj(ho_Regions, out ho_Region, hv_J);
                ho_ConnectedRegions.Dispose();
                HOperatorSet.Connection(ho_Regions, out ho_ConnectedRegions);
                ho_SelectedRegions.Dispose();
                HOperatorSet.SelectShape(ho_ConnectedRegions, out ho_SelectedRegions, "area",
                    "and", 1000, 100000);
                {
                    HObject ExpTmpOutVar_0;
                    HOperatorSet.SelectObj(ho_Region, out ExpTmpOutVar_0, hv_J);
                    ho_Region.Dispose();
                    ho_Region = ExpTmpOutVar_0;
                }
                get_features(ho_SelectedRegions, out hv_Features);
                HOperatorSet.ClassifyClassGmm(hv_GMMHandle, hv_Features, 1, out hv_ClassID,
                    out hv_ClassProb, out hv_Density, out hv_KSigmaProb);
                HOperatorSet.EvaluateClassGmm(hv_GMMHandle, hv_Features, out hv_ClassProb,
                    out hv_Density, out hv_KSigmaProb);
                hv_Classes1 = hv_Classes1.TupleConcat(hv_ClassID);
            }
            ho_Region.Dispose();
            ho_ConnectedRegions.Dispose();
            ho_SelectedRegions.Dispose();
            return;
        }

        public void disp_obj_class(HObject ho_Regions, HTuple hv_Classes)
        {
            // Local iconic variables 
            HObject ho_Region = null;
            // Local control variables 
            HTuple hv_Number = null, hv_Colors = null;
            HTuple hv_J = null;
            // Initialize local and output iconic variables 
            HOperatorSet.GenEmptyObj(out ho_Region);
            HOperatorSet.CountObj(ho_Regions, out hv_Number);
            hv_Colors = new HTuple();
            hv_Colors[0] = "yellow";
            hv_Colors[1] = "magenta";
            hv_Colors[2] = "green";
            HTuple end_val2 = hv_Number;
            HTuple step_val2 = 1;
            for (hv_J = 1; hv_J.Continue(end_val2, step_val2); hv_J = hv_J.TupleAdd(step_val2))
            {
                ho_Region.Dispose();
                HOperatorSet.SelectObj(ho_Regions, out ho_Region, hv_J);
                HOperatorSet.SetColor(hv_ExpDefaultWinHandle, hv_Colors.TupleSelect(hv_Classes.TupleSelect(hv_J - 1)));
                HOperatorSet.DispObj(ho_Region, hv_ExpDefaultWinHandle);
            }
            ho_Region.Dispose();

            return;
        }


        // Main procedure 
        private void RunPick(bool picktrigger)
        {
            // Stack for temporary objects 
            HObject[] OTemp = new HObject[20];
            // Local iconic variables 
            HObject ho_Image, ho_ImageRectified = null, ho_FOV;
            HObject ho_Box, ho_ImageReduced, ho_Objects, ho_Cross = null;
            HObject ho_GripPoint = null, ho_ImageReducedGripPoint = null;
            HObject ho_ImageMedian = null, ho_ObjectGripPoint = null, ho_Cross1 = null;
            HObject ho_Arrow = null, ho_Cross2 = null, ho_ContCircle;
            // Local control variables 
            HTuple hv_AcqHandle = null, hv_pi = null, hv_angle = null;
            HTuple hv_CamParam = null, hv_CamPose = null, hv_GMMHandle = null;
            HTuple hv_Width = null, hv_Height = null, hv_CamParamOut = null;
            HTuple hv_Classes1 = null, hv_Row1 = null, hv_Column1 = null;
            HTuple hv_Row2 = null, hv_Column2 = null, hv_Diameter = null;
            HTuple hv_Area1 = null, hv_Row3 = null, hv_Column3 = null;
            HTuple hv_len_area = null, hv_MAX = new HTuple(), hv_index = new HTuple();
            HTuple hv_out = new HTuple(), hv_Length = new HTuple();
            HTuple hv_i = new HTuple(), hv_INDEX = new HTuple(), hv_j = new HTuple();
            HTuple hv_pom = new HTuple(), hv_DevDia = new HTuple();
            HTuple hv_joint = new HTuple(), hv_x_ = new HTuple(), hv_y_ = new HTuple();
            HTuple hv_w_ = new HTuple(), hv_a = new HTuple(), hv_b = new HTuple();
            HTuple hv_k = new HTuple(), hv_x_cross = new HTuple();
            HTuple hv_y_cross = new HTuple(), hv_WorldPose = new HTuple();
            HTuple hv_HomMat3D = new HTuple(), hv_HomMat3DRotate = new HTuple();
            HTuple hv_Area2 = new HTuple();
            HTuple hv_Row5 = new HTuple(), hv_Column5 = new HTuple();
            HTuple hv_SumX = new HTuple(), hv_SumY = new HTuple();
            HTuple hv_GripPointX = new HTuple(), hv_GripPointY = new HTuple();
            HTuple hv_maxdist = new HTuple(), hv_distgrippoints = new HTuple();
            HTuple hv_indexgrip = new HTuple(), hv_distgrip = new HTuple();
            HTuple hv_OrientX = new HTuple(), hv_OrientY = new HTuple();
            HTuple hv_maxX = new HTuple(), hv_diff01 = new HTuple();
            HTuple hv_diff02 = new HTuple(), hv_diff12 = new HTuple();
            HTuple hv_mindiff = new HTuple(), hv_GX = new HTuple();
            HTuple hv_GY = new HTuple(), hv_OX = new HTuple(), hv_OY = new HTuple();
            HTuple hv_DY = new HTuple(), hv_DX = new HTuple(), hv_theta = new HTuple();
            HTuple hv_anglerad = new HTuple();
            //
            HTuple hv_Pixel1Y = new HTuple(), hv_Pixel2X = new HTuple(), hv_Pixel1X = new HTuple();
            HTuple hv_Pixel2Y = new HTuple(), hv_distance = new HTuple();
            // Initialize local and output iconic variables 
            HOperatorSet.GenEmptyObj(out ho_Image);
            HOperatorSet.GenEmptyObj(out ho_ImageRectified);
            HOperatorSet.GenEmptyObj(out ho_FOV);
            HOperatorSet.GenEmptyObj(out ho_Box);
            HOperatorSet.GenEmptyObj(out ho_ImageReduced);
            HOperatorSet.GenEmptyObj(out ho_Objects);
            HOperatorSet.GenEmptyObj(out ho_Cross);
            HOperatorSet.GenEmptyObj(out ho_GripPoint);
            HOperatorSet.GenEmptyObj(out ho_ImageReducedGripPoint);
            HOperatorSet.GenEmptyObj(out ho_ImageMedian);
            HOperatorSet.GenEmptyObj(out ho_ObjectGripPoint);
            HOperatorSet.GenEmptyObj(out ho_Cross1);
            HOperatorSet.GenEmptyObj(out ho_Arrow);
            HOperatorSet.GenEmptyObj(out ho_Cross2);
            HOperatorSet.GenEmptyObj(out ho_ContCircle);
            //** GMM ***
            //Image Acquisition 01: Code generated by Image Acquisition 01
            HOperatorSet.OpenFramegrabber("GigEVision", 0, 0, 0, 0, 0, 0, "default", -1, "default", -1, "false", "default", "acA130075gm_CAM", 0, -1, out hv_AcqHandle);
            HOperatorSet.SetFramegrabberParam(hv_AcqHandle, "ExposureAuto", "Continuous");
            hv_pi = 3.14159265359;
            hv_angle = (0 * hv_pi) / 180;
            // Camera intrinsics and extrinsics
            HOperatorSet.ReadCamPar("C:/App/CamPar/intrinsics.cal", out hv_CamParam);
            HOperatorSet.ReadPose("C:/App/CamPar/extrinsics.dat", out hv_CamPose);
            HOperatorSet.ReadClassGmm("C:/App/Train/traineg_gmm_pick_1.4.ggc", out hv_GMMHandle);
            // Uzimanje slike
            ho_Image.Dispose();
            HOperatorSet.GrabImage(out ho_Image, hv_AcqHandle);
            HOperatorSet.GetImageSize(ho_Image, out hv_Width, out hv_Height);
            ho_FOV.Dispose();
            HOperatorSet.GenRectangle1(out ho_FOV, 0, 0, hv_Height, hv_Width);
            ho_Box.Dispose();
            HOperatorSet.GenRectangle1(out ho_Box, 190, 160, 870, 1130);
            HOperatorSet.ChangeRadialDistortionCamPar("adaptive", hv_CamParam, 0, out hv_CamParamOut);
            ho_ImageRectified.Dispose();
            HOperatorSet.ChangeRadialDistortionImage(ho_Image, ho_FOV, out ho_ImageRectified, hv_CamParam, hv_CamParamOut);
            ho_ImageReduced.Dispose();
            HOperatorSet.ReduceDomain(ho_ImageRectified, ho_Box, out ho_ImageReduced);
            ho_ImageRectified.Dispose();
            HOperatorSet.Illuminate(ho_ImageReduced, out ho_ImageRectified, 230, 230, 1.15);
            ho_ImageMedian.Dispose();
            HOperatorSet.MedianImage(ho_ImageRectified, out ho_ImageRectified, "circle", 3, "mirrored");
            ho_ObjectGripPoint.Dispose();
            //segmentacija i klasifikacija
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


            //prefiltriranje klasificiranih predmeta
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


            //** Carolija ***
            HOperatorSet.DiameterRegion(ho_Objects, out hv_Row1, out hv_Column1, out hv_Row2, out hv_Column2, out hv_Diameter);
            HOperatorSet.AreaCenter(ho_Objects, out hv_Area1, out hv_Row3, out hv_Column3);
            HOperatorSet.TupleLength(hv_Area1, out hv_len_area);
            if ((int)(new HTuple(hv_len_area.TupleGreater(0))) != 0)
            {
                hv_MAX = 0;
                hv_index = 0;
                hv_out = 0;
                HOperatorSet.TupleLength(hv_Diameter, out hv_Length);


                HOperatorSet.DispObj(ho_ImageRectified, hv_ExpDefaultWinHandle);
                HOperatorSet.DispLine(hv_ExpDefaultWinHandle, hv_Height / 2, 0, hv_Height / 2, hv_Width);
                HOperatorSet.DispLine(hv_ExpDefaultWinHandle, 0, hv_Width / 2, hv_Height, hv_Width / 2);

                hv_i = 1;
                HOperatorSet.TupleGenSequence(0, hv_Length - 1, 1, out hv_INDEX);
                while ((int)(new HTuple(hv_i.TupleLess(hv_Length))) != 0)
                {
                    hv_j = hv_i.Clone();
                    while ((int)(new HTuple(((hv_Diameter.TupleSelect(hv_j - 1))).TupleLess(hv_Diameter.TupleSelect(hv_j)))) != 0)
                    {
                        hv_pom = hv_Diameter.TupleSelect(hv_j);
                        if (hv_Diameter == null)
                            hv_Diameter = new HTuple();
                        hv_Diameter[hv_j] = hv_Diameter.TupleSelect(hv_j - 1);
                        if (hv_Diameter == null)
                            hv_Diameter = new HTuple();
                        hv_Diameter[hv_j - 1] = hv_pom;
                        hv_pom = hv_INDEX.TupleSelect(hv_j);
                        if (hv_INDEX == null)
                            hv_INDEX = new HTuple();
                        hv_INDEX[hv_j] = hv_INDEX.TupleSelect(hv_j - 1);
                        if (hv_INDEX == null)
                            hv_INDEX = new HTuple();
                        hv_INDEX[hv_j - 1] = hv_pom;
                        hv_j = hv_j - 1;
                        if ((int)(new HTuple(hv_j.TupleEqual(0))) != 0)
                        {
                            break;
                        }
                    }
                    hv_i = hv_i + 1;
                }

                HOperatorSet.TupleMedian(hv_Diameter, out hv_MAX);
                HOperatorSet.TupleDeviation(hv_Diameter, out hv_DevDia);

                HTuple end_val75 = hv_Length;
                HTuple step_val75 = 1;
                for (hv_i = 1; hv_i.Continue(end_val75, step_val75); hv_i = hv_i.TupleAdd(step_val75))
                {
                    if ((int)((new HTuple(((hv_Diameter.TupleSelect(hv_i - 1))).TupleGreaterEqual(
                        hv_MAX - (5 * hv_DevDia)))).TupleAnd(new HTuple(((hv_Diameter.TupleSelect(
                        hv_i - 1))).TupleLessEqual(hv_MAX + (5 * hv_DevDia))))) != 0)
                    {
                        if ((int)(new HTuple(hv_MAX.TupleGreaterEqual(2 * 300))) != 0)
                        {
                            hv_joint = 1;
                        }
                        hv_index = hv_index + 1;
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

                HTuple end_val93 = hv_index;
                HTuple step_val93 = 1;
                for (hv_k = 1; hv_k.Continue(end_val93, step_val93); hv_k = hv_k.TupleAdd(step_val93))
                {
                    if (hv_x_ == null)
                        hv_x_ = new HTuple();
                    hv_x_[hv_k - 1] = hv_Row3.TupleSelect(hv_k - 1);
                    if (hv_y_ == null)
                        hv_y_ = new HTuple();
                    hv_y_[hv_k - 1] = hv_Column3.TupleSelect(hv_k - 1);
                    if (hv_w_ == null)
                        hv_w_ = new HTuple();
                    hv_w_[hv_k - 1] = ((hv_x_.TupleSelect(hv_k - 1)) * hv_a) + ((hv_y_.TupleSelect(hv_k - 1)) * hv_b);
                }

                //* Sort objects by cost value **
                hv_i = 1;
                while ((int)(new HTuple(hv_i.TupleLess(hv_index))) != 0)
                {
                    hv_j = hv_i.Clone();
                    while ((int)(new HTuple(((hv_w_.TupleSelect(hv_j - 1))).TupleGreater(hv_w_.TupleSelect(hv_j)))) != 0)
                    {
                        hv_pom = hv_x_.TupleSelect(hv_j);
                        if (hv_x_ == null)
                            hv_x_ = new HTuple();
                        hv_x_[hv_j] = hv_x_.TupleSelect(hv_j - 1);
                        if (hv_x_ == null)
                            hv_x_ = new HTuple();
                        hv_x_[hv_j - 1] = hv_pom;
                        hv_pom = hv_y_.TupleSelect(hv_j);
                        if (hv_y_ == null)
                            hv_y_ = new HTuple();
                        hv_y_[hv_j] = hv_y_.TupleSelect(hv_j - 1);
                        if (hv_y_ == null)
                            hv_y_ = new HTuple();
                        hv_y_[hv_j - 1] = hv_pom;
                        hv_pom = hv_w_.TupleSelect(hv_j);
                        if (hv_w_ == null)
                            hv_w_ = new HTuple();
                        hv_w_[hv_j] = hv_w_.TupleSelect(hv_j - 1);
                        if (hv_w_ == null)
                            hv_w_ = new HTuple();
                        hv_w_[hv_j - 1] = hv_pom;
                        hv_j = hv_j - 1;
                        if ((int)(new HTuple(hv_j.TupleEqual(0))) != 0)
                        {
                            break;
                        }
                    }
                    hv_i = hv_i + 1;
                }
                //*******************************


                //* Display detected objects **
                HTuple end_val124 = hv_index;
                HTuple step_val124 = 1;
                for (hv_k = 1; hv_k.Continue(end_val124, step_val124); hv_k = hv_k.TupleAdd(step_val124))
                {
                    hv_x_cross = hv_x_.TupleSelect(hv_k - 1);
                    hv_y_cross = hv_y_.TupleSelect(hv_k - 1);
                    HOperatorSet.SetColor(hv_ExpDefaultWinHandle, "green");
                    ho_Cross.Dispose();
                    HOperatorSet.GenCrossContourXld(out ho_Cross, hv_x_cross, hv_y_cross, 200, 0);
                    HOperatorSet.DispObj(ho_Cross, hv_ExpDefaultWinHandle);
                    HOperatorSet.SetColor(hv_ExpDefaultWinHandle, "red");
                    HOperatorSet.SetTposition(hv_ExpDefaultWinHandle, hv_x_cross, hv_y_cross);
                    HOperatorSet.WriteString(hv_ExpDefaultWinHandle, hv_k);
                }
                //******************************

                HOperatorSet.SetOriginPose(hv_CamPose, 0, 0, 0.00, out hv_WorldPose);
                HOperatorSet.PoseToHomMat3d(hv_WorldPose, out hv_HomMat3D);
                HOperatorSet.HomMat3dRotateLocal(hv_HomMat3D, hv_angle, "z", out hv_HomMat3DRotate);
                HOperatorSet.HomMat3dToPose(hv_HomMat3DRotate, out hv_WorldPose);

                HOperatorSet.ImagePointsToWorldPlane(hv_CamParamOut, hv_WorldPose, hv_x_.TupleSelect(0), hv_y_.TupleSelect(0), "mm", out hv_X, out hv_Y);


                // Diameter check
                //HOperatorSet.ImagePointsToWorldPlane(hv_CamParamOut, hv_WorldPose, hv_Row1, hv_Column1, "mm", out hv_Pixel1X, out hv_Pixel1Y);
                //HOperatorSet.ImagePointsToWorldPlane(hv_CamParamOut, hv_WorldPose, hv_Row2, hv_Column2, "mm", out hv_Pixel2X, out hv_Pixel2Y);
                //HOperatorSet.DistancePp(hv_Pixel1X, hv_Pixel1Y, hv_Pixel2X, hv_Pixel2Y, out hv_distance);
                //HOperatorSet.TupleMean(hv_distance, out hv_distance_mean);


                HOperatorSet.SetTposition(hv_ExpDefaultWinHandle, 100, 100);
                HOperatorSet.WriteString(hv_ExpDefaultWinHandle, hv_a);
                HOperatorSet.WriteString(hv_ExpDefaultWinHandle, ", ");
                HOperatorSet.WriteString(hv_ExpDefaultWinHandle, hv_b);
                hv_angledeg = 0;
                HOperatorSet.SetTposition(hv_ExpDefaultWinHandle, 20, 20);
                HOperatorSet.WriteString(hv_ExpDefaultWinHandle, hv_Length);
            }
            else
            {
                ho_GripPoint.Dispose();
                HOperatorSet.GenRectangle1(out ho_GripPoint, 420, 520, 620, 770);
                ho_ImageReducedGripPoint.Dispose();
                HOperatorSet.ReduceDomain(ho_ImageRectified, ho_GripPoint, out ho_ImageReducedGripPoint);
                ho_ImageMedian.Dispose();
                HOperatorSet.MedianImage(ho_ImageReducedGripPoint, out ho_ImageMedian, "circle", 3, "mirrored");
                ho_ObjectGripPoint.Dispose();
                segment(ho_ImageMedian, out ho_ObjectGripPoint);
                {
                    HObject ExpTmpOutVar_0;
                    HOperatorSet.Connection(ho_ObjectGripPoint, out ExpTmpOutVar_0);
                    ho_ObjectGripPoint.Dispose();
                    ho_ObjectGripPoint = ExpTmpOutVar_0;
                }
                {
                    HObject ExpTmpOutVar_0;
                    HOperatorSet.SelectShape(ho_ObjectGripPoint, out ExpTmpOutVar_0, "circularity", "and", 0.5, 1.0);
                    ho_ObjectGripPoint.Dispose();
                    ho_ObjectGripPoint = ExpTmpOutVar_0;
                }
                {
                    HObject ExpTmpOutVar_0;
                    HOperatorSet.SelectShape(ho_ObjectGripPoint, out ExpTmpOutVar_0, "area", "and", 100, 1000);
                    ho_ObjectGripPoint.Dispose();
                    ho_ObjectGripPoint = ExpTmpOutVar_0;
                }
                HOperatorSet.AreaCenter(ho_ObjectGripPoint, out hv_Area2, out hv_Row5, out hv_Column5);
                HOperatorSet.TupleSum(hv_Row5, out hv_SumX);
                HOperatorSet.TupleSum(hv_Column5, out hv_SumY);
                hv_GripPointX = hv_SumX / 9;
                hv_GripPointY = hv_SumY / 9;
                ho_Cross1.Dispose();
                HOperatorSet.GenCrossContourXld(out ho_Cross1, hv_GripPointX, hv_GripPointY, 10, 0);
                hv_maxdist = 0;
                hv_distgrippoints = new HTuple();
                hv_indexgrip = 0;
                for (hv_i = 1; (int)hv_i <= 9; hv_i = (int)hv_i + 1)
                {
                    HOperatorSet.DistancePp(hv_Row5.TupleSelect(hv_i - 1), hv_Column5.TupleSelect(hv_i - 1), hv_GripPointX, hv_GripPointY, out hv_distgrip);
                    HOperatorSet.TupleConcat(hv_distgrippoints, hv_distgrip, out hv_distgrippoints);
                    if ((int)(new HTuple(hv_distgrip.TupleGreater(hv_maxdist))) != 0)
                    {
                        hv_indexgrip = hv_i - 1;
                        hv_maxdist = hv_distgrip.Clone();
                    }
                }
                hv_indexgrip = new HTuple();
                for (hv_i = 1; (int)hv_i <= 9; hv_i = (int)hv_i + 1)
                {
                    if ((int)(new HTuple(((hv_distgrippoints.TupleSelect(hv_i - 1))).TupleGreater(0.9 * hv_maxdist))) != 0)
                    {
                        HOperatorSet.TupleConcat(hv_indexgrip, hv_i - 1, out hv_indexgrip);
                    }
                }
                hv_OrientX = 0;
                hv_OrientY = 0;
                hv_maxX = 0;
                HOperatorSet.TupleAbs((hv_Row5.TupleSelect(hv_indexgrip.TupleSelect(0))) - (hv_Row5.TupleSelect(hv_indexgrip.TupleSelect(1))), out hv_diff01);
                HOperatorSet.TupleAbs((hv_Row5.TupleSelect(hv_indexgrip.TupleSelect(0))) - (hv_Row5.TupleSelect(hv_indexgrip.TupleSelect(2))), out hv_diff02);
                HOperatorSet.TupleAbs((hv_Row5.TupleSelect(hv_indexgrip.TupleSelect(1))) - (hv_Row5.TupleSelect(hv_indexgrip.TupleSelect(2))), out hv_diff12);
                HOperatorSet.TupleMin(((hv_diff01.TupleConcat(hv_diff02))).TupleConcat(hv_diff12), out hv_mindiff);
                if ((int)(new HTuple(hv_mindiff.TupleEqual(hv_diff01))) != 0)
                {
                    hv_OrientX = hv_Row5.TupleSelect(hv_indexgrip.TupleSelect(2));
                    hv_OrientY = hv_Column5.TupleSelect(hv_indexgrip.TupleSelect(2));
                }
                else if ((int)(new HTuple(hv_mindiff.TupleEqual(hv_diff02))) != 0)
                {
                    hv_OrientX = hv_Row5.TupleSelect(hv_indexgrip.TupleSelect(1));
                    hv_OrientY = hv_Column5.TupleSelect(hv_indexgrip.TupleSelect(1));
                }
                else if ((int)(new HTuple(hv_mindiff.TupleEqual(hv_diff12))) != 0)
                {
                    hv_OrientX = hv_Row5.TupleSelect(hv_indexgrip.TupleSelect(0));
                    hv_OrientY = hv_Column5.TupleSelect(hv_indexgrip.TupleSelect(0));
                }
                HOperatorSet.DispObj(ho_ImageReduced, hv_ExpDefaultWinHandle);
                HOperatorSet.SetColor(hv_ExpDefaultWinHandle, "red");
                ho_Arrow.Dispose();
                gen_arrow_contour_xld(out ho_Arrow, hv_GripPointX, hv_GripPointY, hv_OrientX, hv_OrientY, 15, 15);
                HOperatorSet.DispObj(ho_Arrow, hv_ExpDefaultWinHandle);
                HOperatorSet.SetColor(hv_ExpDefaultWinHandle, "green");
                ho_Cross2.Dispose();
                HOperatorSet.GenCrossContourXld(out ho_Cross2, hv_GripPointX, hv_GripPointY, 30, 0);
                HOperatorSet.DispObj(ho_Cross2, hv_ExpDefaultWinHandle);
                HOperatorSet.SetOriginPose(hv_CamPose, 0, 0, 0.00, out hv_WorldPose);
                HOperatorSet.ImagePointsToWorldPlane(hv_CamParamOut, hv_WorldPose, hv_GripPointX, hv_GripPointY, "mm", out hv_GX, out hv_GY);
                HOperatorSet.ImagePointsToWorldPlane(hv_CamParamOut, hv_WorldPose, hv_OrientX, hv_OrientY, "mm", out hv_OX, out hv_OY);
                hv_DY = hv_OY - hv_GY;
                hv_DX = hv_OX - hv_GX;
                HOperatorSet.TupleAtan2(hv_DY, hv_DX, out hv_theta);
                hv_theta = hv_theta.Clone();
                hv_anglerad = (hv_pi / 2) - hv_theta;
                hv_X = hv_GX;
                hv_Y = hv_GY;
                hv_angledeg = (hv_theta * 180) / hv_pi;
            }

            // HOperatorSet.ClearWindow(hv_ExpDefaultWinHandle);
            HOperatorSet.SetColor(hv_ExpDefaultWinHandle, "red");
            ho_ContCircle.Dispose();
            HOperatorSet.GenCircleContourXld(out ho_ContCircle, hv_Height / 2, hv_Width / 2, 100, 0, 6.28318, "positive", 10);
            ho_Image.Dispose();
            ho_ImageRectified.Dispose();
            ho_FOV.Dispose();
            ho_Box.Dispose();
            ho_ImageReduced.Dispose();
            ho_Objects.Dispose();
            ho_Cross.Dispose();
            ho_GripPoint.Dispose();
            ho_ImageReducedGripPoint.Dispose();
            ho_ImageMedian.Dispose();
            ho_ObjectGripPoint.Dispose();
            ho_Cross1.Dispose();
            ho_Arrow.Dispose();
            ho_Cross2.Dispose();
            ho_ContCircle.Dispose();
            HOperatorSet.CloseFramegrabber(hv_AcqHandle);
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
            koordinate.AngleDeg = (float)hv_angledeg.D;
            //koordinate.WorkpieceDiameter = (float)hv_distance_mean.D;
            // Chech for infinity Double to float conversion
            if (float.IsPositiveInfinity(argumenti.PXvalue))
            {
                koordinate.RXcord = float.MaxValue;
                koordinate.RYcord = float.MaxValue;
                koordinate.AngleDeg = float.MaxValue;
                koordinate.WorkpieceDiameter = float.MaxValue;
            }
            else if (float.IsNegativeInfinity(argumenti.PXvalue))
            {
                koordinate.RXcord = float.MinValue;
                koordinate.RYcord = float.MinValue;
                koordinate.AngleDeg = float.MinValue;
                koordinate.WorkpieceDiameter = float.MinValue;
            }

            if (UpdateResultPick != null)
                UpdateResultPick(this, koordinate);
        }

    }
}

