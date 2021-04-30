using CitizenFX.Core;
using OpenRP.Framework.Common.Enumeration;
using System;
using System.Threading.Tasks;
using static CitizenFX.Core.Native.API;

namespace OpenRP.Framework.Client.Classes
{
    public class VehicleEngineToggle : VehicleToggleComponent
    {
        bool _status;
        bool _lastStatus;

        bool _broken;
        bool _lastBroken;

        internal VehicleEngineToggle()
        {
            Client.Event.RegisterNuiEvent(NuiEvent.TOGGLE_COMPONENT, new Action<dynamic>(ToggleComponent));
            Client.Event.RegisterEvent(ClientEvent.SEND_VEHILCE_STATE, new Action(OnSendVehicleState));
            Client.RegisterTickHandler(ComponentMonitor);
            Client.RegisterTickHandler(VehicleStateMonitor);
        }

        private async Task VehicleStateMonitor()
        {
            if (Vehicle.IsEngineRunning != _status)
                SetVehicleEngineOn(Vehicle.Handle, _status, true, true);

            await BaseScript.Delay(0);
        }

        void ToggleComponent(dynamic args)
        {
            if (args.type == "engine")
            {
                _status = args.status;
                Vehicle.IsEngineRunning = _status;
            }
        }

        void OnSendVehicleState()
        {
            SendPanelState("engine", 0, Vehicle.IsEngineRunning, Vehicle.EngineHealth <= 0);
        }

        async Task ComponentMonitor()
        {
            _status = Vehicle.IsEngineRunning;

            if (_status != _lastStatus)
            {
                SendEngineState();
                _lastStatus = _status;
            }

            _broken = Vehicle.EngineHealth <= 0;

            if (_broken != _lastBroken)
            {
                SendEngineState();
                _lastBroken = _broken;
            }

            await BaseScript.Delay(50);
        }

        private void SendEngineState()
        {
            SendPanelState("engine", 0, _status, _broken);
        }
    }
}
