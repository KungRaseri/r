using CitizenFX.Core;
using Newtonsoft.Json;
using OpenRP.Framework.Common.Enumeration;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using static CitizenFX.Core.Native.API;

namespace OpenRP.Framework.Client.Classes
{
    /// <summary>
    /// Handles toggling on/off and monitoring of vehicle engines.
    /// </summary>
    public class VehicleToggleEngine : VehicleToggleComponent
    {
        bool _status;
        bool _lastStatus;

        bool _broken;
        bool _lastBroken;

        internal VehicleToggleEngine()
        {
            Client.Event.RegisterNuiEvent(NuiEvent.TOGGLE_VEHICLE_COMPONENT, new Action<dynamic>(ToggleComponent));
            Client.Event.RegisterEvent(ClientEvent.SEND_VEHILCE_STATE, new Action(OnSendVehicleState));
            Client.RegisterTickHandler(ComponentMonitor);
            Client.RegisterTickHandler(VehicleStateMonitor);
        }

        private async Task VehicleStateMonitor()
        {
            if (TrackedVehicle.IsEngineRunning != _status)
                SetVehicleEngineOn(TrackedVehicle.Handle, _status, true, true);

            await BaseScript.Delay(0);
        }

        async void ToggleComponent(dynamic args)
        {
            if (args.type == "engine")
            {
                if (args.status)
                {
                    TrackedVehicle.IsEngineStarting = true;
                    TrackedVehicle.AreLightsOn = false;
                    while (TrackedVehicle.IsEngineStarting)
                        await BaseScript.Delay(50);
                }

                _status = args.status;
                TrackedVehicle.IsEngineRunning = _status;
                TrackedVehicle.State.Set("engine", TrackedVehicle.IsEngineRunning, false);
            }
        }

        void OnSendVehicleState()
        {
            SendPanelState("engine", 0, TrackedVehicle.IsEngineRunning, TrackedVehicle.EngineHealth <= 0);
        }

        async Task ComponentMonitor()
        {
            if (Game.PlayerPed.IsInVehicle())
            {
                _status = TrackedVehicle.State.Get("engine");

                if (_status != _lastStatus)
                {
                    SendEngineState();
                    _lastStatus = _status;
                }

                _broken = TrackedVehicle.EngineHealth <= 0;

                if (_broken != _lastBroken)
                {
                    SendEngineState();
                    _lastBroken = _broken;
                }
            }

            await BaseScript.Delay(50);
        }

        private void SendEngineState()
        {
            SendPanelState("engine", 0, _status, _broken);
        }
    }
}
