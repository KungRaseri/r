using CitizenFX.Core;
using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using OpenRP.Framework.Common.Enumeration;

namespace OpenRP.Framework.Server.InternalPlugins
{
    /// <summary>
    /// Commands internal plugin.
    /// </summary>
    public class Commands : ServerAccessor
    {
        internal Commands(ServerMain server) : base(server)
        {
            Server.Command.Register("me", new Action<Player, List<string>>(OnMeCommand), "Emote through text.", new List<string>());
            Server.Command.Register("id", new Action<Player, List<string>>(OnIdCommand), "Get your server ID.", new List<string>());
            Server.Command.Register("tp", new Action<Player, List<string>>(OnTpCommand), "Teleport to another location.", new List<string>());
        }

        private void OnMeCommand(Player player, List<string> message)
        {
            var concat = string.Join(' ', message);
            var serverId = player.Handle;
            concat = $"{serverId}: {concat}";
            Server.Event.TriggerClientEvent(player, ClientEvent.ADD_MESSAGE, 237, 188, 52, concat);
        }

        private void OnIdCommand(Player player, List<string> message)
        {
            var serverId = player.Handle;
            var concat = $"Server ID: {serverId}";
            Server.Event.TriggerClientEvent(player, ClientEvent.ADD_MESSAGE, 0, 128, 0, concat);
        }

        private void OnTpCommand(Player player, List<string> message)
        {
            Server.Event.TriggerClientEvent(player, ClientEvent.COMMAND_TP, JsonConvert.SerializeObject(message));
        }
    }
}
