using CitizenFX.Core;
using static CitizenFX.Core.Native.API;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace OpenRP.Framework.Client.Classes
{
    /// <summary>
    /// Handles changing and monitoring of turn signals in vehicles.
    /// </summary>
    public class VehicleTurnSignals : VehicleToggleComponent
    {
        static int _state = 0;

        static readonly Dictionary<int, int> signals = new Dictionary<int, int>()
        {
            {1, 1},
            {0, 2}
        };

        /// <summary>
        /// Activates the left turn signal.
        /// </summary>
        public static void ToggleLeftSignal()
        {
            ToggleSignal(1);
        }

        /// <summary>
        /// Activates the right turn signal.
        /// </summary>
        public static void ToggleRightSignal()
        {
            ToggleSignal(0);
        }

        private static void ToggleSignal(int index)
        {
            if (Game.PlayerPed.IsInVehicle())
            {
                var state = GetVehicleIndicatorLights(TrackedVehicle.Handle);
                TurnOffSignals();

                if (signals[index] != state)
                    SetVehicleIndicatorLights(TrackedVehicle.Handle, index, true);
                else
                    SetVehicleIndicatorLights(TrackedVehicle.Handle, index, false);

                _state = GetVehicleIndicatorLights(TrackedVehicle.Handle);
                SendState(false);
            }
        }

        /// <summary>
        /// Deactivates both turn signals.
        /// </summary>
        public static void TurnOffSignals()
        {
            foreach (var signal in signals)
                SetVehicleIndicatorLights(TrackedVehicle.Handle, signal.Key, false);
        }

        /// <summary>
        /// Sends the current state of the turn signals to the NUI element.
        /// </summary>
        /// <param name="reset">If the turn signal state will be reset before sending the data.</param>
        public static void SendState(bool reset)
        {
            if (reset)
                _state = 0;

            var eventName = "TURN_SIGNAL_STATE";
            var data = new
            {
                eventName,
                _state
            };

            SendNuiMessage(JsonConvert.SerializeObject(data));
        }
    }
}
