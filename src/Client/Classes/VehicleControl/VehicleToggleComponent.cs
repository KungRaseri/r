﻿using CitizenFX.Core;
using Newtonsoft.Json;
using OpenRP.Framework.Client.Controllers;
using OpenRP.Framework.Common.Enumeration;
using System.Threading.Tasks;
using static CitizenFX.Core.Native.API;

namespace OpenRP.Framework.Client.Classes
{
    /// <summary>
    /// Parent class of the vehicle component objects.
    /// </summary>
    public class VehicleToggleComponent
    {
        internal static ClientMain Client { get; set; }
        internal static Vehicle TrackedVehicle { get; set; }
        internal static int Seat { get; set; }
        internal static bool[] Taken { get; set; }
        internal static int Seats { get; set; }

        internal static void SendPanelData()
        {
            string eventName = "VEHICLE_PANEL_DATA";
            var isCar = VehicleController.IsCar(TrackedVehicle);
            var data = new
            {
                eventName,
                Seat,
                Taken,
                Seats,
                isCar
            };
            SendNuiMessage(JsonConvert.SerializeObject(data));
        }

        internal static void SendPanelState(string type, int index, bool status, bool broken)
        {
            string eventName = "VEHICLE_PANEL_STATE";
            var data = new
            {
                eventName,
                type,
                index,
                status,
                broken
            };
            SendNuiMessage(JsonConvert.SerializeObject(data));
        }

        internal static async Task SeatTaken()
        {
            Seat = -2;

            while (Seat < -1)
            {
                Seat = (int)Game.PlayerPed.SeatIndex;
                await BaseScript.Delay(50);
            }

            Taken = SeatStorage();
            Seats = GetVehicleModelNumberOfSeats((uint)TrackedVehicle.Model.Hash);
            SendPanelData();
        }

        internal static bool[] SeatStorage()
        {
            var temp = new bool[4];

            for (var i = -1; i < 3; i++)
                temp[i + 1] = !IsVehicleSeatFree(TrackedVehicle.Handle, i);

            return temp;
        }
    }
}