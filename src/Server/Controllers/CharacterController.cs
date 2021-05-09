using CitizenFX.Core;
using Newtonsoft.Json;
using OpenRP.Framework.Common.Classes;
using OpenRP.Framework.Common.Enumeration;
using OpenRP.Framework.Database.Document;
using System;
using System.Collections.Generic;
using static CitizenFX.Core.Native.API;

namespace OpenRP.Framework.Server.Controllers
{
    public class CharacterController : ServerAccessor
    {

        internal CharacterController (ServerMain server) : base (server)
        {
            Server.Event.RegisterEvent(ServerEvent.SAVE_NEW_CHARACTER, new Action<Player, dynamic>(OnSaveNewCharacter));
            Server.Event.RegisterEvent(ServerEvent.SET_PLAYER_ROUTING_BUCKET, new Action<Player, bool>(OnSetPlayerRoutingBucket));
            Server.Event.RegisterEvent(ServerEvent.STORE_CHARACTER_CUSTOMIZATION, new Action<string, string>(OnSaveCharacterCustomization));
            Server.Event.RegisterEvent(ServerEvent.RETRIEVE_CHARACTERS, new Action<Player>(OnRetrieveCharacters));
        }

        private async void OnRetrieveCharacters([FromSource] Player player)
        {
            var temp = new List<CharacterData>();
            var account = await Server.Database.Context.Accounts.FindAsync($"discord:{player.Identifiers["discord"]}");
            var characters = await Server.Database.Context.Characters.GetAsync();
            foreach (var item in characters)
            {
                if (item.AccountId.ToString() == account.Id.ToString())
                {
                    var data = new CharacterData()
                    {
                        Id = item.Id.ToString(),
                        First = item.FirstName,
                        Last = item.LastName,
                        Customization = item.Customization
                    };

                    temp.Add(data);
                }
            }
            Server.Event.TriggerClientEvent(player, ClientEvent.RETRIEVE_CHARACTERS, JsonConvert.SerializeObject(temp));
        }

        private async void OnSaveCharacterCustomization(string id, dynamic data)
        {
            var characters = await Server.Database.Context.Characters.GetAsync();
            foreach (var item in characters)
            {
                if (item.Id.ToString() == id)
                {
                    item.Customization = data;
                    await Server.Database.Context.Characters.PatchAsync(item);
                    break;
                }
            }
        }

        private async void OnSaveNewCharacter([FromSource] Player player, dynamic args)
        {
            var account = await Server.Database.Context.Accounts.FindAsync($"discord:{player.Identifiers["discord"]}");
            Character data = JsonConvert.DeserializeObject<Character>(JsonConvert.SerializeObject(args));
            data.AccountId = account.Id;
            await Server.Database.Context.Characters.PostAsync(data);
            Server.Event.TriggerClientEvent(player, ClientEvent.GET_CHARACTER_OBJECT_ID, data.Id.ToString());
        }

        private void OnSetPlayerRoutingBucket([FromSource] Player player, bool instance)
        {
            if (instance)
            {
                if (GetPlayerRoutingBucket(player.Handle) != int.Parse(player.Handle))
                    SetPlayerRoutingBucket(player.Handle, int.Parse(player.Handle));
            }
            else
            {
                if (GetPlayerRoutingBucket(player.Handle) != 0)
                    SetPlayerRoutingBucket(player.Handle, 0);
            }
        }
    }
}
