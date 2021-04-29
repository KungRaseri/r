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

        Dictionary<int, bool> _vehStates;

        bool _forceEngine;
        bool _engine;
        bool _lastEngine;

        List<VehicleToggleDoor> _components;

        int _seat;
        bool[] _taken;

        internal VehicleController (ClientMain client) : base (client)
        {
            _vehicle = new Vehicle(0);
            _vehStates = new Dictionary<int, bool>();
            _forceEngine = false;
            _taken = new bool[]{ false, false, false, false };

            _components = new List<VehicleToggleDoor>
            {
                new VehicleToggleDoor(Client, 0, false),
                new VehicleToggleDoor(Client, 1, false),
                new VehicleToggleDoor(Client, 2, false),
                new VehicleToggleDoor(Client, 3, false),
                new VehicleToggleDoor(Client, 4, false),
                new VehicleToggleDoor(Client, 5, false)
            };

            Client.Event.RegisterEvent(ClientEvent.SEND_VEHILCE_STATE, new Action<dynamic>(StoreVehicleState));

            Client.RegisterKeyBinding("ToggleVehiclePanel", "(HUD) Vehicle Panel", "grave", new Action(ToggleVehiclePanel));
            Client.RegisterTickHandler(VehicleMonitor);
            Client.RegisterTickHandler(PlayerStateMonitor);
        }

        void StoreVehicleState(dynamic state)
        {
            var temp = JsonConvert.DeserializeObject<Dictionary<int, bool>>(state);
            _vehStates = temp;
        }

        private async Task PlayerStateMonitor()
        {
            if (Game.PlayerPed.VehicleTryingToEnter != null)
            {
                SetVehicle(Game.PlayerPed.VehicleTryingToEnter);
                Client.Event.TriggerServerEvent(ServerEvent.RECEIVE_VEHICLE_STATE, _vehicle.Handle, _vehicle.IsEngineRunning);
                SetVehicleEngineOn(_vehicle.Handle, _vehicle.IsEngineRunning, true, true);
                await SeatTaken();

                while (Game.PlayerPed.VehicleTryingToEnter != null)
                    await BaseScript.Delay(50);
            }

            if (_vehStates.ContainsKey(_vehicle.Handle))
            {
                if (_vehicle.IsEngineRunning != _vehStates[_vehicle.Handle])
                    SetVehicleEngineOn(_vehicle.Handle, _vehStates[_vehicle.Handle], true, true);
            }

            await BaseScript.Delay(0);
        }

        private async Task SeatTaken()
        {
            _seat = -2;

            while (_seat < -1)
            {
                _seat = (int)Game.PlayerPed.SeatIndex;
                await BaseScript.Delay(50);
            }

            for (var i = -1; i < 3; i++)
                _taken[i + 1] = !IsVehicleSeatFree(_vehicle.Handle, i);

            foreach (var item in _taken)
                Debug.WriteLine(item.ToString());

            SendData();
        }

        private void DoorOpenClose(VehicleDoorIndex door, bool status)
        {
            if (status)
                _vehicle.Doors[door].Open();
            else
                _vehicle.Doors[door].Close();
        }

        private void WindowOpenClose(VehicleWindowIndex window, bool status)
        {
            if (status)
                _vehicle.Windows[window].RollDown();
            else
                _vehicle.Windows[window].RollUp();
        }

        private async void SeatChange(VehicleSeat seat, bool status)
        {
            if (_vehicle.IsSeatFree(seat) && status)
            {
                SetPedIntoVehicle(Game.PlayerPed.Handle, _vehicle.Handle, (int)seat);

                _seat = -2;
                while (_seat < -1)
                {
                    _seat = (int)Game.PlayerPed.SeatIndex;
                    await BaseScript.Delay(50);
                }
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

            await BaseScript.Delay(50);
        }

        private void ToggleVehiclePanel()
        {
            if (Game.PlayerPed.IsInVehicle())
            {
                SetVehicle(Game.PlayerPed.CurrentVehicle);
                var eventName = "TOGGLE_VEHICLE_PANEL_MODULE";
                UIElement.ToggleNuiModule(eventName, true, true);
                SendData();
            }
        }

        private void SetVehicle(Vehicle vehicle)
        {
            _vehicle = vehicle;
            foreach (var component in _components)
                component.Vehicle = _vehicle;
        }

        private void SendData()
        {
            string eventName = "VEHICLE_PANEL_DATA";
            var data = new
            {
                eventName,
                _seat,
                _taken
            };
            SendNuiMessage(JsonConvert.SerializeObject(data));
        }
    }
}
