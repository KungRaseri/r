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

        bool _dfl;
        bool _lastDfl;
        bool _dfr;
        bool _lastDfr;
        bool _dbl;
        bool _lastDbl;
        bool _dbr;
        bool _lastDbr;
        bool _hood;
        bool _lastHood;
        bool _trunk;
        bool _lastTrunk;

        bool _sfl;
        bool _lastSfl;
        bool _sfr;
        bool _lastSfr;
        bool _sbl;
        bool _lastSbl;
        bool _sbr;
        bool _lastSbr;

        internal VehicleController (ClientMain client) : base (client)
        {
            _vehicle = new Vehicle(0);
            _vehStates = new Dictionary<int, bool>();
            _forceEngine = false;

            Client.Event.RegisterNuiEvent(NuiEvent.TOGGLE_COMPONENT, new Action<dynamic>(ToggleComponent));
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
                _vehicle = Game.PlayerPed.VehicleTryingToEnter;
                Client.Event.TriggerServerEvent(ServerEvent.RECEIVE_VEHICLE_STATE, _vehicle.Handle, _vehicle.IsEngineRunning);
                SetVehicleEngineOn(_vehicle.Handle, _vehicle.IsEngineRunning, true, true);

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

        private void ToggleComponent(dynamic args)
        {
            if (args.type == "engine")
            {
                Client.Event.TriggerServerEvent(ServerEvent.RECEIVE_VEHICLE_STATE, _vehicle.Handle, args.status);
                _vehicle.IsEngineRunning = args.status;
            }
            else if (args.type == "dfl")
                DoorOpenClose(VehicleDoorIndex.FrontLeftDoor, args.status);
            else if (args.type == "dfr")
                DoorOpenClose(VehicleDoorIndex.FrontRightDoor, args.status);
            else if (args.type == "dbl")
                DoorOpenClose(VehicleDoorIndex.BackLeftDoor, args.status);
            else if (args.type == "dbr")
                DoorOpenClose(VehicleDoorIndex.BackRightDoor, args.status);
            else if (args.type == "hood")
                DoorOpenClose(VehicleDoorIndex.Hood, args.status);
            else if (args.type == "trunk")
                DoorOpenClose(VehicleDoorIndex.Trunk, args.status);
            else if (args.type == "wfl")
                WindowOpenClose(VehicleWindowIndex.FrontLeftWindow, args.status);
            else if (args.type == "wfr")
                WindowOpenClose(VehicleWindowIndex.FrontRightWindow, args.status);
            else if (args.type == "wbl")
                WindowOpenClose(VehicleWindowIndex.BackLeftWindow, args.status);
            else if (args.type == "wbr")
                WindowOpenClose(VehicleWindowIndex.BackRightWindow, args.status);
            else if (args.type == "sfl")
                SeatChange(VehicleSeat.Driver, args.status);
            else if (args.type == "sfr")
                SeatChange(VehicleSeat.Passenger, args.status);
            else if (args.type == "sbl")
                SeatChange(VehicleSeat.LeftRear, args.status);
            else if (args.type == "sbr")
                SeatChange(VehicleSeat.RightRear, args.status);
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

        private void SeatChange(VehicleSeat seat, bool status)
        {
            if (_vehicle.IsSeatFree(seat) && status)
                SetPedIntoVehicle(Game.PlayerPed.Handle, _vehicle.Handle, (int)seat);
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

            _dfr = _vehicle.Doors[VehicleDoorIndex.FrontRightDoor].IsOpen;

            if (_dfr != _lastDfr)
            {
                _lastDfr = _dfr;
                SendData();
            }

            _dbl = _vehicle.Doors[VehicleDoorIndex.BackLeftDoor].IsOpen;

            if (_dbl != _lastDbl)
            {
                _lastDbl = _dbl;
                SendData();
            }

            _dbr = _vehicle.Doors[VehicleDoorIndex.BackRightDoor].IsOpen;

            if (_dbr != _lastDbr)
            {
                _lastDbr = _dbr;
                SendData();
            }

            _hood = _vehicle.Doors[VehicleDoorIndex.Hood].IsOpen;

            if (_hood != _lastHood)
            {
                _lastHood = _hood;
                SendData();
            }

            _trunk = _vehicle.Doors[VehicleDoorIndex.Trunk].IsOpen;

            if (_trunk != _lastTrunk)
            {
                _lastTrunk = _trunk;
                SendData();
            }

            _sfl = !_vehicle.IsSeatFree(VehicleSeat.Driver);

            if (_sfl != _lastSfl)
            {
                _lastSfl = _sfl;
                SendData();
            }

            _sfr = !_vehicle.IsSeatFree(VehicleSeat.Passenger);

            if (_sfr != _lastSfr)
            {
                _lastSfr = _sfr;
                SendData();
            }

            _sbl = !_vehicle.IsSeatFree(VehicleSeat.LeftRear);

            if (_sbl != _lastSbl)
            {
                _lastSbl = _sbl;
                SendData();
            }

            _sbr = !_vehicle.IsSeatFree(VehicleSeat.RightRear);

            if (_sbr != _lastSbr)
            {
                _lastSbr = _sbr;
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
                _dfl,
                _dfr,
                _dbl,
                _dbr,
                _sfl,
                _sfr,
                _sbl,
                _sbr
            };
            SendNuiMessage(JsonConvert.SerializeObject(data));
        }
    }
}
