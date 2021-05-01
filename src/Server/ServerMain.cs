using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using CitizenFX.Core;
using Microsoft.Extensions.Configuration;
using OpenRP.Framework.Database.Document;
using OpenRP.Framework.Server.Controllers;
using OpenRP.Framework.Server.InternalPlugins;
using static CitizenFX.Core.Native.API;

namespace OpenRP.Framework.Server
{
    /// <summary>
    /// 
    /// </summary>
    public class ServerMain : BaseScript
    {
        internal IConfigurationRoot Settings;
        internal PlayerList PlayersList => Players;

        public readonly DataHandler Database;

        public readonly EventController Event;
        public readonly CommandController Command;
        public EventHandlerDictionary Events => EventHandlers;

        public ServerMain()
        {
            Debug.WriteLine($"[{nameof(ServerMain)}] Server loading ...");

            // Initialize configuration settings
            Settings = LoadSettings();

            Database = new DataHandler(this);
            Event = new EventController(this);
            Command = new CommandController(this);
            new VoiceController(this);
            new VehicleController(this);

            new Commands(this);

            InitializeFiveMEvents();

            Debug.WriteLine($"[{nameof(ServerMain)}] Resources loaded ...");
        }

        private IConfigurationRoot LoadSettings()
        {
            return new ConfigurationBuilder()
                .SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), @"resources/framework"))
                .AddJsonFile("settings.json")
                .Build();
        }

        public void ReloadSettings()
        {
            Settings = LoadSettings();
        }

        private void InitializeFiveMEvents()
        {
            EventHandlers["onResourceListRefresh"] += new Action(OnResourceListRefresh);
            EventHandlers["onServerResourceStart"] += new Action<string>(OnServerResourceStart);
            EventHandlers["onServerResourceStop"] += new Action<string>(OnServerResourceStop);
            EventHandlers["entityCreating"] += new Action<int>(OnEntityCreating);
            EventHandlers["entityCreated"] += new Action<int>(OnEntityCreated);
            EventHandlers["entityRemoved"] += new Action<int>(OnEntityRemoved);
            EventHandlers["playerConnecting"] += new Action<string, CallbackDelegate, dynamic, Player>(OnPlayerConnecting);
            EventHandlers["playerJoining"] += new Action<Player, string>(OnPlayerJoining);
            EventHandlers["playerDropped"] += new Action<Player, string>(OnPlayerDropped);
            EventHandlers["playerEnteredScope"] += new Action<dynamic>(OnPlayerEnteredScope);
            EventHandlers["playerLeftScope"] += new Action<dynamic>(OnPlayerLeftScope);
        }

        private void OnPlayerDropped([FromSource] Player player, string reason)
        {

            Debug.WriteLine($"[{player.Name.ToUpper()}] LEFT | Reason: {reason}");
        }

        private async void OnPlayerJoining([FromSource] Player player, string oldId)
        {
            Debug.WriteLine($"[{player.Name.ToUpper()}] JOINING");
        }

        private void OnPlayerLeftScope(dynamic data)
        {
            Debug.WriteLine($"[SCOPE] {data.playerEnteringScope} has left the scope of {data.playerWithScope}");
        }

        private void OnPlayerEnteredScope(dynamic data)
        {
            Debug.WriteLine($"[SCOPE] {data.playerEnteringScope} has entered the scope of {data.playerWithScope}");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="playerName"></param>
        /// <param name="setKickReason"></param>
        /// <param name="deferrals"></param>
        /// <param name="player"></param>
        private async void OnPlayerConnecting(string playerName, CallbackDelegate setKickReason, dynamic deferrals, [FromSource] Player player)
        {
            Debug.WriteLine($"[{playerName.ToUpper()}] CONNECTING");

            var account = await Database.Context.Accounts.FindAsync($"discord:{player.Identifiers["discord"]}");

            if (account == null)
            {
                account = new Account()
                {
                    Identifiers = player.Identifiers.ToArray()
                };

                await Database.Context.Accounts.PostAsync(account);

                Debug.WriteLine($"[{account.Id}] New Account created");
            }
            else
            {
                Debug.WriteLine($"[{account.Id}] Existing Account found");
            }

            // This is where we do authentication, allow/deny listing, queue, etc.
            Debug.WriteLine($"[{playerName.ToUpper()}][{account.Id}] CONNECTED");
        }

        private void OnServerResourceStop(string obj)
        {
        }

        private void OnServerResourceStart(string obj)
        {
        }

        private void OnResourceListRefresh()
        {
        }

        private void OnEntityRemoved(int obj)
        {
        }

        private void OnEntityCreating(int obj)
        {
        }

        private void OnEntityCreated(int obj)
        {
        }

        public void RegisterServerCommand(ICommand command)
        {
            try
            {
                //Commands.Register(command);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
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

        public string GetPlayerIdentifierFromPrefix(int source, string prefix)
        {
            var result = string.Empty;

            for (var i = 0; i < GetNumPlayerIdentifiers(source.ToString()); i++)
            {
                if (!GetPlayerIdentifier(source.ToString(), i).Contains(prefix)) continue;

                result = GetPlayerIdentifier(source.ToString(), i);

                break;
            }

            return result;
        }
    }
}
