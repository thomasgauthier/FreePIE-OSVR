using System;
using OSVR.ClientKit;
using System.Collections.Generic;
using FreePIE.Core.Contracts;



[GlobalType(Type = typeof(OSVRGlobal))]
public class OSVRFreePIE : IPlugin
{
    public double[] data { get; set; } //float array in which we will store yaw pitch and roll
    private ClientContext context; //OSVR client context
    private OrientationInterface orientationInterface; //OSVR orientation interface
    private PositionInterface positionInterface; //OSVR position interface

    public event EventHandler Started; //need this for IPlugin interface


    public struct Euler
    {
        public double yaw;
        public double pitch;
        public double roll;

        public Euler(double yaw, double pitch, double roll)
        {
            this.yaw = yaw;
            this.pitch = pitch;
            this.roll = roll;
        }
    }

    public OSVRFreePIE()
    {
        data = new double[6];
    }

    public object CreateGlobal()
    {
        return new OSVRGlobal(this); //the global instance accessible from within FreePIE script engine
    }

    //name for the FreePIE GUI
    public string FriendlyName
    {
        get { return "OSVR Interface"; }
    }


    //obscure math to transform quaternions into euler, this code is taken from http://www.euclideanspace.com/maths/geometry/rotations/conversions/quaternionToEuler/
    private Euler quatToEuler(Quaternion q)
    {

        double sqw = q.w * q.w;
        double sqx = q.x * q.x;
        double sqy = q.y * q.y;
        double sqz = q.z * q.z;
        double unit = sqx + sqy + sqz + sqw; // if normalised is one, otherwise is correction factor
        double test = q.x * q.y + q.z * q.w;

        double yaw;
        double pitch;
        double roll;

        if (test > 0.499 * unit)
        { // singularity at north pole
            yaw = 2 * Math.Atan2(q.x, q.w);
            pitch = Math.PI / 2;
            roll = 0;
            return new Euler(yaw, pitch, roll);
        }
        if (test < -0.499 * unit)
        { // singularity at south pole
            yaw = -2 * Math.Atan2(q.x, q.w);
            pitch = -Math.PI / 2;
            roll = 0;
            return new Euler(yaw, pitch, roll);
        }
        yaw = Math.Atan2(2 * q.y * q.w - 2 * q.x * q.z, sqx - sqy - sqz + sqw);
        pitch = Math.Asin(2 * test / unit);
        roll = Math.Atan2(2 * q.x * q.w - 2 * q.y * q.z, -sqx + sqy - sqz + sqw);

        if (Double.IsNaN(yaw))
            yaw = 0d;
        if (Double.IsNaN(pitch))
            pitch = 0d;
        if (Double.IsNaN(roll))
            roll = 0d;

        return new Euler(yaw, pitch, roll);

    }


    public Action Start()
    {

        //OSVR clients need an identifier
        context = new ClientContext("com.osvr.freepieClient");

        //OSVR interface for the head
        Interface head = context.getInterface("/me/head");

        //Orientation Interface
        orientationInterface = new OrientationInterface(head);

        //Position Interface
        positionInterface = new PositionInterface(head);

        //add callbacks
        orientationInterface.StateChanged += OrientationInterface_StateChanged;
        positionInterface.StateChanged += PositionInterface_StateChanged;

        context.SetRoomRotationUsingHead(); //recenters the hmd position to the current position
        return null;
    }


    private void PositionInterface_StateChanged(object sender, TimeValue timestamp, int sensor, Vec3 report)
    {
        data[3] = report.x;
        data[4] = report.y;
        data[5] = report.z;
    }


    private void OrientationInterface_StateChanged(object sender, TimeValue timestamp, int sensor, Quaternion report)
    {
        Euler euler = quatToEuler(report);
        data[0] = euler.yaw;
        data[1] = euler.roll; // should be pitch, but for reasons I don't understand roll an pitch are mixed up
        data[2] = euler.pitch; //should be roll
    }

    //need this for IPlugin interface
    public void Stop()
    {

    }

    //This method will be executed each iteration of the script
    public void DoBeforeNextExecute()
    {
        //update OSVR context
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

        public double yaw
        {
            get { return osvr.data[0]; }
        }

        public double pitch
        {
            get { return osvr.data[1]; }
        }

        public double roll
        {
            get { return osvr.data[2]; }
        }

        public double x
        {
            get { return osvr.data[3]; }
        }
        public double y
        {
            get { return osvr.data[4]; }
        }
        public double z
        {
            get { return osvr.data[5]; }
        }

    }
}