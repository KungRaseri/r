using CitizenFX.Core;
using System;
using OpenRP.Framework.Common.Classes;
using static CitizenFX.Core.Native.API;

namespace OpenRP.Framework.Client.Controllers
{
    public class EventController : ClientAccessor
    {
        internal EventController(ClientMain client) : base(client)
        {
        }

        /// <summary>
        /// Register a new NUI event.
        /// </summary>
        /// <param name="eventName">Event name from an enum.</param>
        /// <param name="callback">Method to call when the event is triggered.</param>
        public void RegisterNuiEvent(Enum eventName, MulticastDelegate callback)
        {
            var name = eventName.ToString();
            RegisterNuiCallbackType(name);
            var nuiName = $"__cfx_nui:{name}";
            Client.Events[nuiName] += callback;
        }

        /// <summary>
        /// Register a new client event.
        /// </summary>
        /// <param name="eventName">Event name from an enum.</param>
        /// <param name="callback">Method to call when the event is triggered.</param>
        public void RegisterEvent(Enum eventName, MulticastDelegate callback)
        {
            var name = ServerCommand.GenerateEventName(eventName);
            Client.Events[name] += callback;
        }

        /// <summary>
        /// Triggers a registered client event.
        /// </summary>
        /// <param name="eventName">Event name from an enum.</param>
        /// <param name="args">Arguments to pass to the triggered event callback.</param>
        public void TriggerEvent(Enum eventName, params object[] args)
        {
            var name = ServerCommand.GenerateEventName(eventName);
            BaseScript.TriggerEvent(name, args);
        }

        /// <summary>
        /// Triggers a registered server event.
        /// </summary>
        /// <param name="eventName">Event name from an enum.</param>
        /// <param name="args">Arguments to pass to the triggered event callback.</param>

        public void TriggerServerEvent(Enum eventName, params object[] args)
        {
            var name = ServerCommand.GenerateEventName(eventName);
            BaseScript.TriggerServerEvent(name, args);
        }
    }
}
