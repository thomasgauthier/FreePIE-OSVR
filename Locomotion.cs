using System;
using OSVR.ClientKit;

namespace OSVRFreePIE
{
    public class Locomotion
    {
        private NaviPositionInterface positionInterface;
        private NaviVelocityInterface velocityInterface;
        private Vec2 positionValue;
        private Vec2 velocityValue;

        public Locomotion(ClientContext context, String path)
        {
            positionInterface = context.GetNaviPositionInterface(path);
            velocityInterface = context.GetNaviVelocityInterface(path);

            positionInterface.StateChanged += PositionInterface_StateChanged;
            velocityInterface.StateChanged += VelocityInterface_StateChanged;
        }

        private void PositionInterface_StateChanged(object sender, TimeValue timestamp, int sensor, Vec2 report)
        {
            positionValue = report;
        }

        private void VelocityInterface_StateChanged(object sender, TimeValue timestamp, int sensor, Vec2 report)
        {
            velocityValue = report;
        }

        public Vec2 position
        {
            get
            {
                return positionValue;
            }
        }

        public Vec2 velocity
        {
            get
            {
                return velocityValue;
            }
        }
    }
}
