using CitizenFX.Core;
using Newtonsoft.Json;
using OpenRP.Framework.Client.Controllers;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using OpenRP.Framework.Common.Enumeration;
using static CitizenFX.Core.Native.API;
using OpenRP.Framework.Client.Classes;

namespace OpenRP.Framework.Client.InternalPlugins
{
    /// <summary>
    /// Commands internal plugin.
    /// </summary>
    public class Commands : ClientAccessor
    {
        internal Commands(ClientMain client) : base(client)
        {
            Client.Event.RegisterEvent(ClientEvent.COMMAND_TP, new Action<string>(OnTpCommand));
        }

        private async void OnTpCommand(string args)
        {
            var list = JsonConvert.DeserializeObject<List<string>>(args);
            var length = list.Count;

            foreach (var item in list)
            {
                try
                {
                    float.Parse(item);
                }
                catch (FormatException e)
                {
                    ChatController.AddMessage(255, 0, 0, "Invalid arguments.");
                    return;
                }
            }

            if (length == 0)
            {
                if (!DoesBlipExist(GetFirstBlipInfoId(8)))
                    ChatController.AddMessage(255, 0, 0, "Waypoint not set.");
                else
                {
                    var gps = GetFirstBlipInfoId(8);
                    var pos = GetBlipCoords(gps);
                    await Position.Teleport(pos);
                }
            }
            else if (length == 1)
            {
                var found = GetPlayerFromServerId(int.Parse(list[0]));
                if (found == -1)
                    ChatController.AddMessage(255, 0, 0, "Player does not exist.");
                else
                {
                    var ped = GetPlayerPed(found);
                    var pos = GetEntityCoords(ped, true);
                    await Position.Teleport(pos);
                }
            }
            else if (length == 2)
            {
                var pos = Vector3.Zero;
                pos.X = float.Parse(list[0]);
                pos.Y = float.Parse(list[1]);
                await Position.Teleport(pos);
            }
            else
                ChatController.AddMessage(255, 0, 0, "Invalid number of arguments.");
        }
    }
}
