using System;
using OSVR.ClientKit;

namespace OSVRFreePIE
{
    public class Location2D
    {
        private Location2DInterface location2DInterface;
        private Vec2 value;

        public Location2D(ClientContext context, String path)
        {
            location2DInterface = context.GetLocation2DInterface(path);
            location2DInterface.StateChanged += Location2DInterface_StateChanged;
        }

        private void Location2DInterface_StateChanged(object sender, TimeValue timestamp, int sensor, Vec2 report)
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
    }
}
