using CitizenFX.Core;
using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace OpenRP.Framework.Server.InternalPlugins
{
    public class Commands : ServerAccessor
    {
        ServerMain _server;

        internal Commands (ServerMain server) : base (server)
        {
            _server = server;

            _server.Command.Register("me", new Action<Player, List<string>>(OnMeCommand), "Emote through text.", new List<string>());
            _server.Command.Register("id", new Action<Player, List<string>>(OnIdCommand), "Get your server ID.", new List<string>());
            _server.Command.Register("tp", new Action<Player, List<string>>(OnTpCommand), "Teleport to another location.", new List<string>());
        }

        private void OnMeCommand(Player player, List<string> message)
        {
            var concat = string.Join(' ', message);
            var serverId = player.Handle;
            concat = $"{serverId}: {concat}";
            BaseScript.TriggerClientEvent("ADD_MESSAGE", 237, 188, 52, concat);
        }

        private void OnIdCommand(Player player, List<string> message)
        {
            var serverId = player.Handle;
            var concat = $"Server ID: {serverId}";
            BaseScript.TriggerClientEvent("ADD_MESSAGE", 0, 128, 0, concat);
        }

        private void OnTpCommand(Player player, List<string> message)
        {
            BaseScript.TriggerClientEvent(player, "COMMAND_TP", JsonConvert.SerializeObject(message));
        }
    }
}
