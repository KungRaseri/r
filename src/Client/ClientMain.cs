using System;
using System.Threading.Tasks;
using CitizenFX.Core;
using OpenRP.Framework.Client.Controllers;
using OpenRP.Framework.Common.Interface;
using static CitizenFX.Core.Native.API;

namespace OpenRP.Framework.Client
{
    /// <summary>
    /// Main client-side entry point for the OpenRP Framework
    /// </summary>
    public class ClientMain : BaseScript
    {
        public PlayerList players;
        public readonly VoiceController Voice;
        public readonly EventController Event;
        public readonly ChatController Chat;
        public EventHandlerDictionary Events => EventHandlers;

        public ClientMain()
        {
            players = Players;
            Voice = new VoiceController(this);
            Event = new EventController(this);
            Chat = new ChatController(this);

            Initialize();

            Debug.WriteLine($"[{nameof(ClientMain)}] resources loaded");
        }

        private void OnClientResourceStart(string resourceName)
        {
            if (!GetCurrentResourceName().Equals(resourceName))
                return;
            Debug.WriteLine($"[{resourceName}] start");
        }

        private void OnClientResourceStop(string resourceName)
        {
            if (!GetCurrentResourceName().Equals(resourceName))
                return;
            Debug.WriteLine($"[{resourceName}] stop");
        }

        private void Initialize()
        {
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