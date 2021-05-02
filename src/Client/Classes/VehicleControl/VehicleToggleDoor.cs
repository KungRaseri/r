using CitizenFX.Core;
using OpenRP.Framework.Common.Enumeration;
using System;
using System.Threading.Tasks;
using static CitizenFX.Core.Native.API;

namespace OpenRP.Framework.Client.Classes
{
    /// <summary>
    /// Handles opening/closing and mointoring of doors in vehicles.
    /// </summary>
    public class VehicleToggleDoor : VehicleToggleComponent
    {
        int _index;

        bool _status;
        bool _lastStatus;

        bool _broken;
        bool _lastBroken;

        internal VehicleToggleDoor(int index)
        {
            _index = index;

            Client.Event.RegisterNuiEvent(NuiEvent.TOGGLE_VEHICLE_COMPONENT, new Action<dynamic>(ToggleComponent));
            Client.Event.RegisterEvent(ClientEvent.SEND_VEHILCE_STATE, new Action(OnSendVehicleState));
            Client.RegisterTickHandler(ComponentMonitor);
        }

        void OnSendVehicleState()
        {
            SendPanelState("door", _index, GetVehicleDoorAngleRatio(TrackedVehicle.Handle, _index) != 0, IsVehicleDoorDamaged(TrackedVehicle.Handle, _index));
        }

        void ToggleComponent(dynamic args)
        {
            if (args.type == "door" && args.index == _index)
            {
                _status = args.status;
                if (_status)
                    SetVehicleDoorOpen(TrackedVehicle.Handle, _index, false, false);
                else
                    SetVehicleDoorShut(TrackedVehicle.Handle, _index, false);
            }
        }

        async Task ComponentMonitor()
        {
            _status = GetVehicleDoorAngleRatio(TrackedVehicle.Handle, _index) != 0;

            if (_status != _lastStatus)
            {
                SendDoorState();
                _lastStatus = _status;
            }

            _broken = IsVehicleDoorDamaged(TrackedVehicle.Handle, _index);

            if (_broken != _lastBroken)
            {
                SendDoorState();
                _lastBroken = _broken;
            }

            if (_broken && _status)
            {
                _status = false;
                SendDoorState();
                SendPanelState("window", _index, _status, _broken);
            }

            await BaseScript.Delay(50);
        }

        private void SendDoorState()
        {
            SendPanelState("door", _index, _status, _broken);
        }
    }
}
