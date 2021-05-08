using CitizenFX.Core;
using MongoDB.Bson;
using Newtonsoft.Json;
using OpenRP.Framework.Common.Enumeration;
using OpenRP.Framework.Database.Document;
using System;
using System.Collections.Generic;
using System.Text;
using static CitizenFX.Core.Native.API;

namespace OpenRP.Framework.Server.Controllers
{
    public class CharacterController : ServerAccessor
    {
        internal CharacterController (ServerMain server) : base (server)
        {
            Server.Event.RegisterEvent(ServerEvent.SAVE_NEW_CHARACTER, new Action<Player, dynamic>(OnSaveNewCharacter));
            Server.Event.RegisterEvent(ServerEvent.SET_PLAYER_ROUTING_BUCKET, new Action<Player, dynamic>(OnSetPlayerRoutingBucket));
            Server.Event.RegisterEvent(ServerEvent.STORE_CHARACTER_CUSTOMIZATION, new Action<string, string>(OnSaveCharacterCustomization));
        }

        private async void OnSaveCharacterCustomization(string id, dynamic data)
        {
            var character = await Server.Database.Context.Characters.GetAsync();
            foreach (var item in character)
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

        private void OnSetPlayerRoutingBucket([FromSource] Player player, dynamic args)
        {
            SetPlayerRoutingBucket(player.Handle, int.Parse(player.Handle));
        }
    }
}
