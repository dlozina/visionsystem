using System;

public class HalconEventArgs : EventArgs
{
    private float pXvalue;
    public float PXvalue
    {
        get { return pXvalue; }
        set { pXvalue = value; }
    }

    private float rXcord;
    public float RXcord
    {
        get { return rXcord; }
        set { rXcord = value; }
    }

    private float rYcord;
    public float RYcord
    {
        get { return rYcord; }
        set { rYcord = value; }
    }

    private float pCAM2live;
    public float PCAM2live
    {
        get { return pCAM2live; }
        set { pCAM2live = value; }
    }
}
