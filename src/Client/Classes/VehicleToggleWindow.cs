using CitizenFX.Core;
using OpenRP.Framework.Common.Enumeration;
using System;
using static CitizenFX.Core.Native.API;

namespace OpenRP.Framework.Client.Classes
{
    public class VehicleToggleWindow : VehicleToggleComponent
    {
        int _index;
        bool _status;

        internal VehicleToggleWindow(int index)
        {
            _index = index;
            _status = false;

            Client.Event.RegisterNuiEvent(NuiEvent.TOGGLE_COMPONENT, new Action<dynamic>(ToggleComponent));
        }

        void ToggleComponent(dynamic args)
        {
            if (args.type == "window" && args.index == _index)
            {
                _status = args.status;
                if (_status)
                    RollDownWindow(Vehicle.Handle, _index);
                else
                    RollUpWindow(Vehicle.Handle, _index);
            }
        }
    }
}
