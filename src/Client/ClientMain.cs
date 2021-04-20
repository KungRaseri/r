using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CitizenFX.Core;
using OpenRP.Framework.Client.Controllers;
using OpenRP.Framework.Client.InternalPlugins;
using static CitizenFX.Core.Native.API;

namespace OpenRP.Framework.Client
{
    /// <summary>
    /// Main client-side entry point for the OpenRP Framework
    /// </summary>
    public class ClientMain : BaseScript
    {
        public PlayerList players;

        /// <summary>
        /// Controller that handles event registration and invocation.
        /// </summary>
        public readonly EventController Event;

        /// <summary>
        /// Controller that handles the Message Box.
        /// </summary>
        public readonly ChatController Chat;

        internal EventHandlerDictionary Events => EventHandlers;

        public ClientMain()
        {
            players = Players;
            new VoiceController(this);

            Event = new EventController(this);
            Chat = new ChatController(this);

            InitializeFiveMEvents();
            InitializeInternalPlugins();

            Debug.WriteLine($"[{nameof(ClientMain)}] resources loaded");
        }

        private void InitializeInternalPlugins()
        {
            new Commands(this);
        }


        /// <summary>
        /// Initializes Event Handlers for FiveM provided events.
        /// </summary>
        private void InitializeFiveMEvents()
        {

            //EventHandlers["gameEventTriggered"] += new Action<string, List<int>>(OnGameEventTriggered);
            EventHandlers["onClientResourceStart"] += new Action<string>(OnClientResourceStart);
            EventHandlers["onClientResourceStop"] += new Action<string>(OnClientResourceStop);
            EventHandlers["onResourceStarting"] += new Action<string>(OnResourceStarting);
            //EventHandlers["populationPedCreating"] += new Action<float, float, float, uint, dynamic>(OnPopulationPedCreating);
        }

        private void OnPopulationPedCreating(float x, float y, float z, uint model, dynamic overrideCalls)
        {
            Debug.WriteLine($"[PED] Created Ped ({model}) @ vector3({x},{y},{z})");
        }

        private void OnResourceStarting(string resourceName)
        {
            Debug.WriteLine($"[{resourceName}] Resource is Starting");
        }

        private void OnClientResourceStart(string resourceName)
        {
            if (!GetCurrentResourceName().Equals(resourceName))
                return;
            Debug.WriteLine($"[{resourceName}] Resource has Started");
        }

        private void OnClientResourceStop(string resourceName)
        {
            if (!GetCurrentResourceName().Equals(resourceName))
                return;

            Debug.WriteLine($"[{resourceName}] Resource has Stopped");
        }

        private void OnGameEventTriggered(string name, List<int> data)
        {
            Debug.WriteLine($"[NETWORK] {name}: {string.Join(",", data)}");
        }

        public void RegisterKeyBinding(string commandString, string description, string key, MulticastDelegate callback)
        {
            RegisterKeyMapping(commandString, description, "keyboard", key);
            RegisterCommand(commandString, callback, true);
        }

        public void RegisterTickHandler(Func<Task> action)
        {
            try
            {
                Tick += action;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }

        public void UnregisterTickHandler(Func<Task> action)
        {
            try
            {
                Tick -= action;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }

        public void RegisterExport(string exportName, Delegate callback)
        {
            Exports.Add(exportName, callback);
        }

        public dynamic GetExport(string resourceName)
        {
            return Exports[resourceName];
        }
    }
}