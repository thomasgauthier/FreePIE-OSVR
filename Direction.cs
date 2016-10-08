using System;
using OSVR.ClientKit;

namespace OSVRFreePIE
{
    public class Direction
    {
        private DirectionInterface directionInterface;
        private Vec3 value;

        public Direction(ClientContext context, String path)
        {
            directionInterface = context.GetDirectionInterface(path);
            directionInterface.StateChanged += DirectionInterface_StateChanged;
        }

        private void DirectionInterface_StateChanged(object sender, TimeValue timestamp, int sensor, Vec3 report)
        {
            value = report;
        }

        public double x
        {
            get
            {
                return value.x;
            }
        }
        public double y
        {
            get
            {
                return value.y;
            }
        }
        public double z
        {
            get
            {
                return value.z;
            }
        }
    }
}
