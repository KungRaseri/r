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
        bool _lastStatus;

        internal VehicleToggleWindow(int index)
        {
            _index = index;

            Client.Event.RegisterNuiEvent(NuiEvent.TOGGLE_COMPONENT, new Action<dynamic>(ToggleComponent));
        }

        void ToggleComponent(dynamic args)
        {
            if (args.type == "window" && args.index == _index)
            {
                _status = args.status;
                if (_status)
                    RollDownWindow(TrackedVehicle.Handle, _index);
                else
                    RollUpWindow(TrackedVehicle.Handle, _index);
            }
        }
    }
}