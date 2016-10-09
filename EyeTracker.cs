using System;
using OSVR.ClientKit;

namespace OSVRFreePIE
{
    public class EyeTracker
    {
        private EyeTracker2DInterface eyeTracker2DInterface;
        private EyeTracker3DInterface eyeTracker3DInterface;
        private EyeTrackerBlinkInterface eyeTrackerBlinkInterface;

        private Vec2 value2D;
        private EyeTracker3DState value3D;
        private bool valueBlink;

        public EyeTracker(ClientContext context, String path)
        {
            valueBlink = false;
            eyeTracker2DInterface = context.GetEyeTracker2DInterface(path);
            eyeTracker3DInterface = context.GetEyeTracker3DInterface(path);
            eyeTrackerBlinkInterface = context.GetEyeTrackerBlinkInterface(path);

            eyeTracker2DInterface.StateChanged += EyeTracker2DInterface_StateChanged;
            eyeTracker3DInterface.StateChanged += EyeTracker3DInterface_StateChanged;
            eyeTrackerBlinkInterface.StateChanged += EyeTrackerBlinkInterface_StateChanged;
        }

        private void EyeTracker2DInterface_StateChanged(object sender, TimeValue timestamp, int sensor, Vec2 report)
        {
            value2D = report;
        }

        private void EyeTracker3DInterface_StateChanged(object sender, TimeValue timestamp, int sensor, EyeTracker3DState report)
        {
            value3D = report;
        }


        private void EyeTrackerBlinkInterface_StateChanged(object sender, TimeValue timestamp, int sensor, bool report)
        {
            valueBlink = report;
        }

        public bool blink
        {
            get
            {
                return valueBlink;
            }
        }

        public Vec2 direction2D
        {
            get
            {
                return value2D;
            }
        }


        public Vec3 basePoint3D
        {
            get
            {
                return value3D.basePoint;
            }
        }

        public Vec3 direction3D
        {
            get
            {
                return value3D.direction;
            }
        }

        public bool basePoint3DValid
        {
            get
            {
                return value3D.basePointValid;
            }
        }

        public bool direction3DValid
        {
            get
            {
                return value3D.directionValid;
            }
        }
    }
}
