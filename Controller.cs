using System;
using OSVR.ClientKit;

namespace OSVRFreePIE
{
    public class Controller
    {
        private Tracker tracker;
        public Button one;
        public Button two;
        public Button three;
        public Button four;
        public Button bumper;
        public Button joystick;
        public Button middle;
        public Analog joystickX;
        public Analog joystickY;
        public Analog trigger;

        public Controller(ClientContext context, String side)
        {
            tracker = new Tracker(context, "/me/hands/" + side);
            one = new Button(context, "/controller/" + side + "/1");
            two = new Button(context, "/controller/" + side + "/2");
            three = new Button(context, "/controller/" + side + "/3");
            four = new Button(context, "/controller/" + side + "/4");
            bumper = new Button(context, "/controller/" + side + "/bumper");
            joystick = new Button(context, "/controller/" + side + "/joystick/button");
            middle = new Button(context, "/controller/" + side + "/middle");
            joystickX = new Analog(context, "/controller/" + side + "/joystick/x");
            joystickY = new Analog(context, "/controller/" + side + "/joystick/y");
            trigger = new Analog(context, "/controller/" + side + "/trigger");
        }

        public Vec3 position
        {
            get
            {
                return tracker.position;
            }
        }
        
        public Quaternion orientation
        {
            get
            {
                return tracker.orientation;
            }
        }

        public double x
        {
            get
            {
                return tracker.x;
            }
        }

        public double y
        {
            get
            {
                return tracker.y;
            }
        }

        public double z
        {
            get
            {
                return tracker.z;
            }
        }

        public double roll
        {
            get
            {
                return tracker.roll;
            }
        }

        public double pitch
        {
            get
            {
                return tracker.pitch;
            }
        }

        public double yaw
        {
            get
            {
                return tracker.yaw;
            }
        }
    }
}
