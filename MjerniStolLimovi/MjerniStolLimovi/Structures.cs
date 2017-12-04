using System;

namespace MjerniStolLimovi
{

    public struct AreaPoint
    {
        public extern AreaPoint(float X, float Y);
        public float X { get; set; }
        public float Y { get; set; }
    }

    public struct VelocityPoint
    {
        public extern VelocityPoint(float X, float Y);
        public float X { get; set; }
        public float Y { get; set; }
    }

    public struct CameraOutput
    {
        public extern CameraOutput(AreaPoint POINT1, AreaPoint POINT2, float PARAMETER, short TYPE);
        public AreaPoint POINT1 { get; set; }
        public AreaPoint POINT2 { get; set; }
        public float PARAMETER { get; set; }
        public short TYPE { get; set; }
    }
}