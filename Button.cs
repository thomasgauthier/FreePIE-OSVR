using System;
using OSVR.ClientKit;

namespace OSVRFreePIE
{
    public class Button
    {
        private ButtonInterface buttonInterface;
        private bool buttonValue;

        public Button(ClientContext context, String path)
        {
            buttonValue = false;
            buttonInterface = context.GetButtonInterface(path);
            buttonInterface.StateChanged += ButtonInterface_StateChanged;
        }

        public bool value
        {
            get
            {
                return buttonValue;
            }
        }

        private void ButtonInterface_StateChanged(object sender, TimeValue timestamp, int sensor, byte report)
        {
            buttonValue = (report == ButtonInterface.Pressed);
        }


        static public implicit operator bool(Button b)
        {
            return b.buttonValue;
        }

        public bool __bool__()
        {
            return buttonValue;
        }

        public bool __nonzero__()
        {
            return buttonValue;
        }

    }
}
