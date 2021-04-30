using CitizenFX.Core;
using OpenRP.Framework.Common.Enumeration;
using System;
using System.Threading.Tasks;
using static CitizenFX.Core.Native.API;

namespace OpenRP.Framework.Client.Classes
{
    public class VehicleToggleDoor : VehicleToggleComponent
    {
        int _index;
        bool _status;
        bool _lastStatus;

        internal VehicleToggleDoor(int index)
        {
            _index = index;
            _status = false;

            Client.Event.RegisterNuiEvent(NuiEvent.TOGGLE_COMPONENT, new Action<dynamic>(ToggleComponent));
            Client.Event.RegisterEvent(ClientEvent.SEND_VEHILCE_STATE, new Action(OnSendVehicleState));
            Client.RegisterTickHandler(ComponentMonitor);
        }

        void OnSendVehicleState()
        {
            SendPanelState("door", _index, GetVehicleDoorAngleRatio(Vehicle.Handle, _index) != 0);
        }

        void ToggleComponent(dynamic args)
        {
            if (args.type == "door" && args.index == _index)
            {
                _status = args.status;
                if (_status)
                    SetVehicleDoorOpen(Vehicle.Handle, _index, false, false);
                else
                    SetVehicleDoorShut(Vehicle.Handle, _index, false);

                
            }
        }

        async Task ComponentMonitor()
        {
            _status = GetVehicleDoorAngleRatio(Vehicle.Handle, _index) != 0;

            if (_status != _lastStatus)
            {
                SendPanelState("door", _index, _status);
                _lastStatus = _status;
            }

            await BaseScript.Delay(50);
        }
    }
}
