using System;
using OSVR.ClientKit;
using System.Collections.Generic;
using FreePIE.Core.Contracts;



[GlobalType(Type = typeof(OSVRGlobal))]
public class OSVRFreePIE : IPlugin
{
    public float[] data { get; set; } //float array in which we will store yaw pitch and roll
    private ClientContext context; //OSVR client context
    private OrientationInterface orientationInterface; //OSVR orientation interface

    public event EventHandler Started; //need this for IPlugin interface

    public OSVRFreePIE()
    {
        data = new float[3];
    }

    public object CreateGlobal()
    {
        return new OSVRGlobal(this); //the global instance accessible from within FreePIE script engine
    }
    
    //name for the FreePIE GUI
    public string FriendlyName
    {
        get { return "OSVR interface"; }
    }


    //method to get the pitch angle from a quaternion
    private double osvrQuatGetPitch(Quaternion p)
    {
        return Math.Asin(2.0f * (p.x * p.y - p.w * p.z));

    }

    //method to get the yaw angle from a quaternion
    private double osvrQuatGetYaw(Quaternion p)
    {
        return Math.Atan2(2.0f * p.x * p.z + 2.0f * p.y * p.w, 1.0f - 2.0f * (p.z * p.z + p.y * p.y));

    }

    //method to get the roll angle from a quaternion
    private double osvrQuatGetRoll(Quaternion p)
    {
        return Math.Atan2(2.0f * p.x * p.w + 2.0f * p.z * p.y, 1.0f - 2.0f * (p.y * p.y + p.w * p.w));
    }


    public Action Start()
    {

        //OSVR clients need an identifier
        context = new ClientContext("com.osvr.freepieClient");

        //OSVR interface for the head
        Interface head = context.getInterface("/me/head");

        //If you just want orientation
        orientationInterface = new OrientationInterface(head);
        context.SetRoomRotationUsingHead();
        return null;
    }

    //may need to have something in here, OSVR documentation is not clear on this
    public void Stop()
    {

    }

    //This method will be executed each iteration of the script
    public void DoBeforeNextExecute()
    {
        Quaternion now = orientationInterface.GetState().Value;
        data[0] = (float)osvrQuatGetYaw(now);
        data[1] = (float)osvrQuatGetPitch(now);
        data[2] = (float)osvrQuatGetRoll(now);

        context.update();
    }

    //need this for IPlugin interface
    public bool SetProperties(Dictionary<string, object> properties)
    {
        return false;
    }

    //need this for IPlugin interface
    public bool GetProperty(int index, IPluginProperty property)
    {
        return false;
    }



    [Global(Name = "OSVR")]
    public class OSVRGlobal
    {
        private readonly OSVRFreePIE osvr;

        public OSVRGlobal(OSVRFreePIE osvr)
        {
            this.osvr = osvr;
        }

        public float yaw
        {
            get { return osvr.data[0]; }
        }

        public float pitch
        {
            get { return osvr.data[1]; }
        }

        public float roll
        {
            get { return osvr.data[2]; }
        }

    }
}