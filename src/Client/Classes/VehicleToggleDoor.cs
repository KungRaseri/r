using CitizenFX.Core;
using OpenRP.Framework.Common.Enumeration;
using System;
using static CitizenFX.Core.Native.API;

namespace OpenRP.Framework.Client.Classes
{
    public class VehicleToggleDoor
    {
        ClientMain _client;
        int _index;
        bool _status;

        internal Vehicle Vehicle { get; set; }

        internal VehicleToggleDoor(ClientMain client, int index, bool status)
        {
            _client = client;
            _index = index;
            _status = status;

            _client.Event.RegisterNuiEvent(NuiEvent.TOGGLE_COMPONENT, new Action<dynamic>(ToggleComponent));
        }

        void ToggleComponent(dynamic args)
        {
            _status = args.status;
            if (args.type == "door" && args.index == _index)
            {
                if (_status)
                    SetVehicleDoorOpen(Vehicle.Handle, _index, false, false);
                else
                    SetVehicleDoorShut(Vehicle.Handle, _index, false);
            }
        }
    }
}
