using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using CitizenFX.Core;
using Microsoft.Extensions.Configuration;
using OpenRP.Framework.Common.Interface;
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
            EventHandlers["playerJoining"] += new Action<Player, string>(OnPlayerJoining);
            EventHandlers["playerConnecting"] += new Action<string, CallbackDelegate, dynamic, string>(OnPlayerConnecting);
            EventHandlers["playerEnteredScope"] += new Action<dynamic>(OnPlayerEnteredScope);
            EventHandlers["playerLeftScope"] += new Action<dynamic>(OnPlayerLeftScope);
        }

        private async void OnPlayerJoining([FromSource] Player player, string oldId)
        {
            var account = await Database.Context.Accounts.FindAsync($"discord:{player.Identifiers["discord"]}");

            if (account == null)
            {
                account = new Account()
                {
                    Identifiers = player.Identifiers.ToArray()
                };

                await Database.Context.Accounts.PostAsync(account);

                Debug.WriteLine($"New Account [{account.Id}] created");
            }
            else
            {
                Debug.WriteLine($"Account [{account.Id}] found");
            }

            Debug.WriteLine($"[{player.Name.ToUpper()}][{account.Id}][{oldId}] is joining");
        }

        private void OnPlayerLeftScope(dynamic data)
        {
            Debug.WriteLine($"[SCOPE] {data.playerEnteringScope} has left the scope of {data.playerWithScope}");
        }

        private void OnPlayerEnteredScope(dynamic data)
        {
            Debug.WriteLine($"[SCOPE] {data.playerEnteringScope} has entered the scope of {data.playerWithScope}");
        }

        private void OnPlayerConnecting(string playerName, CallbackDelegate cb, object arg3, string arg4)
        {
            Debug.WriteLine($"[{playerName.ToUpper()}] Connected");
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
