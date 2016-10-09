using System;
using System.Collections.Generic;
using FreePIE.Core.Contracts;
using OSVR.ClientKit;

namespace OSVRFreePIE
{
    [GlobalType(Type = typeof(OSVRGlobal), IsIndexed = true)]
    public class OSVRFreePIE : IPlugin
    {
        private ClientContext context;

        public event EventHandler Started;

        public OSVRFreePIE()
        {
        }

        public object CreateGlobal()
        {
            context = new ClientContext("com.osvr.freepieClient");
            return new OSVRGlobal(this);
        }

        public string FriendlyName
        {
            get { return "OSVR Interface"; }
        }

        public bool GetProperty(int index, IPluginProperty property)
        {
            return false;
        }

        public bool SetProperties(Dictionary<string, object> properties)
        {
            return false;
        }

        public Action Start()
        {
            context.SetRoomRotationUsingHead();
            return null;
        }

        public void Stop()
        {
        }

        public void DoBeforeNextExecute()
        {
            context.update();
        }

        static public implicit operator ClientContext(OSVRFreePIE osvrFreePIE)
        {
            return osvrFreePIE.context;
        }

    }

    [Global(Name = "OSVR")]
    public class OSVRGlobal
    {
        private readonly OSVRFreePIE osvr;

        public OSVRGlobal(OSVRFreePIE osvr)
        {
            this.osvr = osvr;
        }

        public Analog analog(String path)
        {
            return new Analog(osvr, path);
        }

        public Button button(String path)
        {
            return new Button(osvr, path);
        }

        public Direction direction(String path)
        {
            return new Direction(osvr, path);
        }

        public EyeTracker eyeTracker(String path)
        {
            return new EyeTracker(osvr, path);
        }

        public Location2D location2D(String path)
        {
            return new Location2D(osvr, path);
        }

        public Locomotion locomotion(String path)
        {
            return new Locomotion(osvr, path);
        }

        public Tracker tracker(String path)
        {
            return new Tracker(osvr, path);
        }

        public Tracker head()
        {
            return new Tracker(osvr, "/me/head");
        }

        public Tracker leftHand()
        {
            return new Tracker(osvr, "/me/hands/left");
        }

        public Tracker rightHand()
        {
            return new Tracker(osvr, "/me/hands/right");
        }

        public Controller leftController()
        {
            return new Controller(osvr, "left");
        }

        public Controller rightController()
        {
            return new Controller(osvr, "right");
        }

    }
}