using CitizenFX.Core;
using OpenRP.Framework.Common.Classes;
using System;
using System.Collections.Generic;
using System.Text;

namespace OpenRP.Framework.Server.Controllers
{
    public class EventController : ServerAccessor
    {
        internal EventController(ServerMain server) : base(server)
        {
        }

        /// <summary>
        /// Register a new server event.
        /// </summary>
        /// <param name="eventName">Event name from an enum.</param>
        /// <param name="callback">Method to call when the event is triggered.</param>
        public void RegisterEvent(Enum eventName, MulticastDelegate callback)
        {
            var name = ServerCommand.GenerateEventName(eventName);
            Server.Events[name] += callback;
        }

        /// <summary>
        /// Triggers a registered server event.
        /// </summary>
        /// <param name="eventName">Event name from an enum.</param>
        /// <param name="args">Arguments to pass to the triggered event callback.</param>
        public void TriggerEvent(Enum eventName, params object[] args)
        {
            var name = ServerCommand.GenerateEventName(eventName);
            BaseScript.TriggerEvent(name, args);
        }

        /// <summary>
        /// Triggers a registered client event and sends it to a sepcific client.
        /// </summary>
        /// <param name="player">The player object of the client to invoke the callback on.</param>
        /// <param name="eventName">Event name from an enum.</param>
        /// <param name="args">Arguments to pass to the triggered event callback.</param>
        public void TriggerClientEvent(Player player, Enum eventName, params object[] args)
        {
            var name = ServerCommand.GenerateEventName(eventName);
            BaseScript.TriggerClientEvent(player, name, args);
        }

        /// <summary>
        /// Triggers a registered client event and sends it to all clients.
        /// </summary>
        /// <param name="eventName">Event name from an enum.</param>
        /// <param name="args">Arguments to pass to the triggered event callback.</param>
        public void TriggerClientEvent(Enum eventName, params object[] args)
        {
            var name = ServerCommand.GenerateEventName(eventName);
            BaseScript.TriggerClientEvent(name, args);
        }
    }
}
