using CitizenFX.Core;
using Newtonsoft.Json;
using OpenRP.Framework.Client.Classes;
using OpenRP.Framework.Common.Enumeration;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using static CitizenFX.Core.Native.API;

namespace OpenRP.Framework.Client.Controllers
{
    public class VehicleController : ClientAccessor
    {
        Vehicle _vehicle;

        Dictionary<Vehicle, bool> _vehStates;

        bool _forceEngine;
        bool _engine;
        bool _lastEngine;

        bool _dfl;
        bool _lastDfl;

        internal VehicleController (ClientMain client) : base (client)
        {
            _vehicle = new Vehicle(0);
            _vehStates = new Dictionary<Vehicle, bool>();
            _forceEngine = false;

            Client.Event.RegisterNuiEvent(NuiEvent.TOGGLE_COMPONENT, new Action<dynamic>(ToggleComponent));

            Client.RegisterKeyBinding("ToggleVehiclePanel", "(HUD) Vehicle Panel", "grave", new Action(ToggleVehiclePanel));
            Client.RegisterTickHandler(VehicleMonitor);
            Client.RegisterTickHandler(PlayerStateMonitor);
        }

        private async Task PlayerStateMonitor()
        {
            if (Game.PlayerPed.VehicleTryingToEnter != null)
            {
                _vehicle = Game.PlayerPed.VehicleTryingToEnter;
                if (!_vehStates.ContainsKey(_vehicle))
                    _vehStates[_vehicle] = _vehicle.IsEngineRunning;
                SetVehicleEngineOn(_vehicle.Handle, _vehStates[_vehicle], true, true);

                while (Game.PlayerPed.VehicleTryingToEnter != null)
                    await BaseScript.Delay(50);
            }

            if (_vehStates.ContainsKey(_vehicle))
            {
                if (_vehicle.IsEngineRunning != _vehStates[_vehicle])
                    SetVehicleEngineOn(_vehicle.Handle, _vehStates[_vehicle], true, true);
            }

            await BaseScript.Delay(0);
        }

        private void ToggleComponent(dynamic args)
        {
            if (args.type == "engine")
            {
                _vehStates[_vehicle] = args.status;
                _vehicle.IsEngineRunning = args.status;
            }
            else if (args.type == "dfl")
            {
                if (args.status)
                    _vehicle.Doors[VehicleDoorIndex.FrontLeftDoor].Open();
                else
                    _vehicle.Doors[VehicleDoorIndex.FrontLeftDoor].Close();
            }
        }

        private async Task VehicleMonitor()
        {
            _engine = _vehicle.IsEngineRunning;

            if (_engine != _lastEngine)
            {
                _lastEngine = _engine;
                SendData();
            }

            _dfl = _vehicle.Doors[VehicleDoorIndex.FrontLeftDoor].IsOpen;

            if (_dfl != _lastDfl)
            {
                _lastDfl = _dfl;
                SendData();
            }

            await BaseScript.Delay(50);
        }

        private void ToggleVehiclePanel()
        {
            if (Game.PlayerPed.IsInVehicle())
            {
                _vehicle = Game.PlayerPed.CurrentVehicle;
                var eventName = "TOGGLE_VEHICLE_PANEL_MODULE";
                UIElement.ToggleNuiModule(eventName, true, true);
                SendData();
            }
        }

        private void SendData()
        {
            string eventName = "VEHICLE_PANEL_DATA";
            var data = new
            {
                eventName,
                _engine,
                _dfl
            };
            SendNuiMessage(JsonConvert.SerializeObject(data));
        }
    }
}
