using CitizenFX.Core;
using static CitizenFX.Core.Native.API;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenRP.Framework.Client.Classes
{
    public class VehicleTurnSignals : VehicleToggleComponent
    {
        static readonly Dictionary<int, int> signals = new Dictionary<int, int>()
        {
            {1, 1},
            {0, 2}
        };

        public static void ToggleLeftSignal()
        {
            ToggleSignal(1);
        }

        public static void ToggleRightSignal()
        {
            ToggleSignal(0);
        }

        private static void ToggleSignal(int index)
        {
            if (Game.PlayerPed.IsInVehicle())
            {
                var state = GetVehicleIndicatorLights(Vehicle.Handle);
                TurnOffSignals();

                if (signals[index] != state)
                    SetVehicleIndicatorLights(Vehicle.Handle, index, true);
                else
                    SetVehicleIndicatorLights(Vehicle.Handle, index, false);
            }
        }

        public static void TurnOffSignals()
        {
            foreach (var signal in signals)
                SetVehicleIndicatorLights(Vehicle.Handle, signal.Key, false);
        }
    }
}
