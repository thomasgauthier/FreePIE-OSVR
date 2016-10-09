using System;
using OSVR.ClientKit;


namespace OSVRFreePIE
{
    public class Analog
    {
        private AnalogInterface analogInterface;
        private double analogValue;

        public Analog(ClientContext context, String path)
        {
            analogValue = 0.0f;
            analogInterface = context.GetAnalogInterface(path);
            analogInterface.StateChanged += AnalogInterface_StateChanged;
        }

        private void AnalogInterface_StateChanged(object sender, TimeValue timestamp, Int32 sensor, Double report)
        {
            analogValue = report;
        }

        public double value
        {
            get
            {
                return analogValue;
            }
        }

        static public implicit operator float(Analog a)
        {
            return (float)a.analogValue;
        }



        static public implicit operator double(Analog a)
        {
            return a.analogValue;
        }

    }
}
