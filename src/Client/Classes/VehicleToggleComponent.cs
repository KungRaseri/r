using CitizenFX.Core;
using Newtonsoft.Json;
using OpenRP.Framework.Common.Enumeration;
using System.Threading.Tasks;
using static CitizenFX.Core.Native.API;

namespace OpenRP.Framework.Client.Classes
{
    public class VehicleToggleComponent
    {
        internal static ClientMain Client { get; set; }
        internal static Vehicle Vehicle { get; set; }
        internal static int Seat { get; set; }
        internal static bool[] Taken { get; set; }
        internal static int Seats { get; set; }

        internal static void SendPanelData()
        {
            string eventName = "VEHICLE_PANEL_DATA";
            var data = new
            {
                eventName,
                Seat,
                Taken,
                Seats
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
            Seats = GetVehicleModelNumberOfSeats((uint)Vehicle.Model.Hash);
            SendPanelData();
        }

        internal static bool[] SeatStorage()
        {
            var temp = new bool[4];

            for (var i = -1; i < 3; i++)
                temp[i + 1] = !IsVehicleSeatFree(Vehicle.Handle, i);

            return temp;
        }
    }
}