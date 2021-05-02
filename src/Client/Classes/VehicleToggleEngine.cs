using CitizenFX.Core;
using Newtonsoft.Json;
using OpenRP.Framework.Common.Enumeration;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using static CitizenFX.Core.Native.API;

namespace OpenRP.Framework.Client.Classes
{
    public class VehicleToggleEngine : VehicleToggleComponent
    {
        bool _status;
        bool _lastStatus;

        bool _broken;
        bool _lastBroken;

        Dictionary<int, bool> _vehStates;

        internal VehicleToggleEngine()
        {
            _vehStates = new Dictionary<int, bool>();

            Client.Event.RegisterNuiEvent(NuiEvent.TOGGLE_COMPONENT, new Action<dynamic>(ToggleComponent));
            Client.Event.RegisterEvent(ClientEvent.SEND_VEHILCE_STATE, new Action(OnSendVehicleState));
            Client.Event.RegisterEvent(ClientEvent.STORE_ENGINE_STATE, new Action<dynamic>(OnStoreVehicleState));
            Client.RegisterTickHandler(ComponentMonitor);
            Client.RegisterTickHandler(VehicleStateMonitor);
            Client.RegisterTickHandler(PlayerStateMonitor);
        }

        private async Task VehicleStateMonitor()
        {
            if (_vehStates.ContainsKey(TrackedVehicle.Handle))
            {
                if (TrackedVehicle.IsEngineRunning != _status)
                    SetVehicleEngineOn(TrackedVehicle.Handle, _status, true, true);
            }

            await BaseScript.Delay(0);
        }

        private async Task PlayerStateMonitor()
        {
            if (Game.PlayerPed.VehicleTryingToEnter != null)
            {
                TrackedVehicle = Game.PlayerPed.VehicleTryingToEnter;

                if (!_vehStates.ContainsKey(TrackedVehicle.Handle))
                    Client.Event.TriggerServerEvent(ServerEvent.STORE_ENGINE_STATE, TrackedVehicle.Handle, TrackedVehicle.IsEngineRunning);
                await SeatTaken();
                await BaseScript.Delay(3000);
            }

            if (!Game.PlayerPed.IsInVehicle() && TrackedVehicle.Handle != 0)
                TrackedVehicle = new Vehicle(0);

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
                Client.Event.TriggerServerEvent(ServerEvent.STORE_ENGINE_STATE, TrackedVehicle.Handle, _status);
            }
        }

        void OnSendVehicleState()
        {
            SendPanelState("engine", 0, TrackedVehicle.IsEngineRunning, TrackedVehicle.EngineHealth <= 0);
        }

        void OnStoreVehicleState(dynamic state)
        {
            var temp = JsonConvert.DeserializeObject<Dictionary<int, bool>>(state);
            _vehStates = temp;
        }

        async Task ComponentMonitor()
        {
            if (Game.PlayerPed.IsInVehicle())
            {
                if (_vehStates.ContainsKey(TrackedVehicle.Handle))
                {
                    _status = _vehStates[TrackedVehicle.Handle];

                    if (_status != _lastStatus)
                    {
                        SendEngineState();
                        _lastStatus = _status;
                    }
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
