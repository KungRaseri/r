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
    /// <summary>
    /// Handles player-controlled vehicles.
    /// </summary>
    public class VehicleController : ClientAccessor
    {
        bool _dashboard;
        bool _gate;
        bool _lights;
        bool _highbeams;

        const float _gateAngle = 25;

        internal VehicleController (ClientMain client) : base (client)
        {
            VehicleToggleComponent.Client = client;

            new VehicleToggleEngine();

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
            new VehicleToggleSeatbelt();

            TrackedVehicle = new Vehicle(0);
            Seat = -1;
            Taken = new bool[] { false, false, false, false };
            _dashboard = false;
            _gate = false;
            _lights = false;
            _highbeams = false;

            Client.RegisterKeyBinding("ToggleVehiclePanel", "(HUD) Vehicle Panel", "grave", new Action(ToggleVehiclePanel));
            Client.RegisterKeyBinding("ToggleLeftSignal", "(Vehicle) Left Signal", "minus", new Action(VehicleTurnSignals.ToggleLeftSignal));
            Client.RegisterKeyBinding("ToggleRightSignal", "(Vehicle) Right Signal", "equals", new Action(VehicleTurnSignals.ToggleRightSignal));

            Client.RegisterTickHandler(WheelMonitor);
            Client.RegisterTickHandler(VehicleMonitor);
            Client.RegisterTickHandler(HeadlightsMonitor);
        }

        private async void ToggleVehiclePanel()
        {
            if (Game.PlayerPed.IsInVehicle())
            {
                TrackedVehicle = Game.PlayerPed.CurrentVehicle;
                var eventName = "TOGGLE_VEHICLE_PANEL_MODULE";
                UIElement.ToggleNuiModule(eventName, true, true, true);
                Client.Event.TriggerEvent(ClientEvent.SEND_VEHILCE_STATE);
                await SeatTaken();
            }
        }

        async Task WheelMonitor()
        {
            if (Game.PlayerPed.IsInVehicle())
            {
                var state = GetVehicleIndicatorLights(TrackedVehicle.Handle);

                if (state == 1 || state == 2)
                {
                    var angle = TrackedVehicle.SteeringAngle;

                    if ((angle >= _gateAngle || angle <= _gateAngle * -1) && !_gate)
                    {
                        await BaseScript.Delay(400);
                        angle = TrackedVehicle.SteeringAngle;
                        if ((angle >= _gateAngle || angle <= _gateAngle * -1) && !_gate)
                            _gate = true;
                    }

                    if ((angle < _gateAngle && angle > _gateAngle * -1) && _gate)
                    {
                        VehicleTurnSignals.TurnOffSignals();
                        VehicleTurnSignals.SendState(true);
                        _gate = false;
                    }
                }
            }

            await BaseScript.Delay(50);
        }

        async Task VehicleMonitor()
        {
            if (Game.PlayerPed.IsInVehicle())
            {
                if (!_dashboard)
                {
                    UIElement.ToggleNuiModule("TOGGLE_DASHBOARD_PANEL_MODULE", true, false, false);
                    _dashboard = true;
                }
                
                var eventName = "VEHICLE_SPEED_MONITOR";
                var speed = TrackedVehicle.Speed * 2.23694;
                var max = GetVehicleHandlingFloat(TrackedVehicle.Handle, "CHandlingData", "fInitialDriveMaxFlatVel") * 0.82;
                var ratio = speed / max * 100;
                var rpm = TrackedVehicle.CurrentRPM * 100;
                var gearInt = TrackedVehicle.CurrentGear;

                string gear;

                if (gearInt == 0)
                    gear = "R";
                else
                    gear = gearInt.ToString();

                var data = new
                {
                    eventName,
                    speed,
                    ratio,
                    rpm,
                    gear
                };

                SendNuiMessage(JsonConvert.SerializeObject(data));
            }
            else
            {
                if (_dashboard)
                {
                    UIElement.ToggleNuiModule("TOGGLE_DASHBOARD_PANEL_MODULE", false, false, false);
                    _dashboard = false;
                }
            }

            await BaseScript.Delay(0);
        }

        async Task HeadlightsMonitor()
        {
            var lights = false;
            var highbeams = false;

            GetVehicleLightsState(TrackedVehicle.Handle, ref lights, ref highbeams);

            if (_lights != lights || _highbeams != highbeams)
            {
                _lights = lights;
                _highbeams = highbeams;

                var eventName = "VEHICLE_LIGHTS_MONITOR";

                var data = new
                {
                    eventName,
                    _lights,
                    _highbeams
                };

                SendNuiMessage(JsonConvert.SerializeObject(data));
            }

            await BaseScript.Delay(50);
        }

        /// <summary>
        /// Checks if the provided vehicle is a car.
        /// </summary>
        /// <param name="vehicle">The vehicle to check.</param>
        /// <returns></returns>
        public static bool IsCar(Vehicle vehicle)
        {
            if (vehicle.ClassType != VehicleClass.Motorcycles && vehicle.ClassType != VehicleClass.Cycles && vehicle.ClassType != VehicleClass.Trains)
                return true;

            return false;
        }
    }
}
