using System;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Input;
using CitizenFX.Core;
using Microsoft.Extensions.Configuration;
using static CitizenFX.Core.Native.API;

namespace OpenRP.Framework.Server
{
    public class ServerMain : BaseScript
    {
        public IConfigurationRoot Settings;

        public PlayerList players;

        public readonly DataHandler DB;

        public ServerMain()
        {
            Debug.WriteLine($"[{nameof(ServerMain)}] Server loading ...");

            // Initialize configuration settings

            players = Players;
            Settings = LoadSettings();

            DB = new DataHandler(this);

            Initialize();

            Debug.WriteLine($"[{nameof(ServerMain)}] Resources loaded ...");
        }

        private IConfigurationRoot LoadSettings()
        {
            return new ConfigurationBuilder()
                .SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), @"resources/framework_server"))
                .AddJsonFile("settings.json")
                .Build();
        }

        public void ReloadSettings()
        {
            Settings = LoadSettings();
        }

        private void Initialize()
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

        public void RegisterServerEvent(IEvent e)
        {
            try
            {
                //Events.RegisterEvent(e);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }

        public void UnregisterServerEvent(IEvent e)
        {
            try
            {
                //Events.UnregisterEvent(e);
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
