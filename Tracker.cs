using System;
using OSVR.ClientKit;

namespace OSVRFreePIE
{
    public class Tracker
    {
        private PoseInterface poseInterface;
        private PositionInterface positionInterface;
        private OrientationInterface orientationInterface;
        private Vec3 positionValue;
        private Quaternion orientationValue;
        private double rollValue;
        private double pitchValue;
        private double yawValue;

        public Tracker(ClientContext context, String path)
        {
            rollValue = pitchValue = yawValue = 0.0f;

            poseInterface = context.GetPoseInterface(path);
            positionInterface = context.GetPositionInterface(path);
            orientationInterface = context.GetOrientationInterface(path);

            poseInterface.StateChanged += PoseInterface_StateChanged;
            positionInterface.StateChanged += PositionInterface_StateChanged;
            orientationInterface.StateChanged += OrientationInterface_StateChanged;
        }

        private void OrientationInterface_StateChanged(object sender, TimeValue timestamp, int sensor, Quaternion report)
        {
            orientationValue = report;
            updateRollPitchYaw();
        }

        private void PositionInterface_StateChanged(object sender, TimeValue timestamp, int sensor, Vec3 report)
        {
            positionValue = report;
        }

        private void PoseInterface_StateChanged(object sender, TimeValue timestamp, int sensor, Pose3 report)
        {
            orientationValue = report.rotation;
            positionValue = report.translation;
            updateRollPitchYaw();
        }

        private void updateRollPitchYaw()
        {
            var q = new double[] {
                orientationValue.w,
                orientationValue.z,
                orientationValue.x,
                orientationValue.y
             };

            rollValue = -Math.Atan2(2 * (q[0] * q[1] + q[2] * q[3]), 1 - 2 * (q[1] * q[1] + q[2] * q[2]));
            pitchValue = -Math.Asin(2 * (q[0] * q[2] - q[3] * q[1]));
            yawValue = -Math.Atan2(2 * (q[0] * q[3] + q[1] * q[2]), 1 - 2 * (q[2] * q[2] + q[3] * q[3]));
        }

        public Vec3 position
        {
            get
            {
                return positionValue;
            }
        }

        public Quaternion orientation
        {
            get
            {
                return orientationValue;
            }
        }

        public double x
        {
            get
            {
                return positionValue.x;
            }
        }

        public double y
        {
            get
            {
                return positionValue.y;
            }
        }

        public double z
        {
            get
            {
                return positionValue.z;
            }
        }

        public double roll
        {
            get
            {
                return rollValue;
            }
        }

        public double pitch
        {
            get
            {
                return pitchValue;
            }
        }
        
        public double yaw
        {
            get
            {
                return yawValue;
            }
        }
    }
}
