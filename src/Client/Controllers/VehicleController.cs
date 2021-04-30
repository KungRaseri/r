using CitizenFX.Core;
using Newtonsoft.Json;
using OpenRP.Framework.Client.Classes;
using OpenRP.Framework.Common.Enumeration;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using static CitizenFX.Core.Native.API;
using static OpenRP.Framework.Client.Classes.VehicleToggleComponent;

namespace OpenRP.Framework.Client.Controllers
{
    public class VehicleController : ClientAccessor
    {
        bool _gate;
        const float _gateAngle = 25;

        internal VehicleController (ClientMain client) : base (client)
        {
            VehicleToggleComponent.Client = client;

            new VehicleEngineToggle();

            new VehicleToggleDoor(0);
            new VehicleToggleDoor(1);
            new VehicleToggleDoor(2);
            new VehicleToggleDoor(3);
            new VehicleToggleDoor(4);
            new VehicleToggleDoor(5);

            new VehicleToggleWindow(0);
            new VehicleToggleWindow(1);
            new VehicleToggleWindow(2);
            new VehicleToggleWindow(3);

            new VehicleSwitchSeats();

            VehicleToggleComponent.Vehicle = new Vehicle(0);
            Seat = -1;
            Taken = new bool[] { false, false, false, false };
            _gate = false;

            Client.RegisterKeyBinding("ToggleVehiclePanel", "(HUD) Vehicle Panel", "grave", new Action(ToggleVehiclePanel));
            Client.RegisterKeyBinding("ToggleLeftSignal", "(Vehicle) Left Signal", "minus", new Action(VehicleTurnSignals.ToggleLeftSignal));
            Client.RegisterKeyBinding("ToggleRightSignal", "(Vehicle) Right Signal", "equals", new Action(VehicleTurnSignals.ToggleRightSignal));

            Client.RegisterTickHandler(PlayerStateMonitor);
            Client.RegisterTickHandler(WheelMonitor);
        }

        private async void ToggleVehiclePanel()
        {
            if (Game.PlayerPed.IsInVehicle())
            {
                VehicleToggleComponent.Vehicle = Game.PlayerPed.CurrentVehicle;
                var eventName = "TOGGLE_VEHICLE_PANEL_MODULE";
                UIElement.ToggleNuiModule(eventName, true, true);
                Client.Event.TriggerEvent(ClientEvent.SEND_VEHILCE_STATE);
                await SeatTaken();
            }
        }

        private async Task PlayerStateMonitor()
        {
            if (Game.PlayerPed.VehicleTryingToEnter != null)
            {
                VehicleToggleComponent.Vehicle = Game.PlayerPed.VehicleTryingToEnter;
                Client.Event.TriggerServerEvent(ServerEvent.STORE_ENGINE_STATE, VehicleToggleComponent.Vehicle.Handle, VehicleToggleComponent.Vehicle.IsEngineRunning);
                SetVehicleEngineOn(VehicleToggleComponent.Vehicle.Handle, VehicleToggleComponent.Vehicle.IsEngineRunning, true, true);
                await SeatTaken();
                await BaseScript.Delay(3000);
            }

            if (!Game.PlayerPed.IsInVehicle() && VehicleToggleComponent.Vehicle.Handle != 0)
                VehicleToggleComponent.Vehicle = new Vehicle(0);

            await BaseScript.Delay(0);
        }

        async Task WheelMonitor()
        {
            if (Game.PlayerPed.IsInVehicle())
            {
                var state = GetVehicleIndicatorLights(VehicleToggleComponent.Vehicle.Handle);

                if (state == 1 || state == 2)
                {
                    var angle = VehicleToggleComponent.Vehicle.SteeringAngle;

                    if ((angle >= _gateAngle || angle <= _gateAngle * -1) && !_gate)
                    {
                        await BaseScript.Delay(400);
                        angle = VehicleToggleComponent.Vehicle.SteeringAngle;
                        if ((angle >= _gateAngle || angle <= _gateAngle * -1) && !_gate)
                            _gate = true;
                    }

                    if ((angle < _gateAngle && angle > _gateAngle * -1) && _gate)
                    {
                        VehicleTurnSignals.TurnOffSignals();
                        _gate = false;
                    }
                }
            }

            await BaseScript.Delay(50);
        }
    }
}
