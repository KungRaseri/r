using CitizenFX.Core;
using MongoDB.Bson;
using Newtonsoft.Json;
using OpenRP.Framework.Common.Enumeration;
using OpenRP.Framework.Database.Document;
using System;
using System.Collections.Generic;
using System.Text;

namespace OpenRP.Framework.Server.Controllers
{
    public class CharacterController : ServerAccessor
    {
        internal CharacterController (ServerMain server) : base (server)
        {
            Server.Event.RegisterEvent(ServerEvent.SAVE_NEW_CHARACTER, new Action<Player, dynamic>(OnSaveNewCharacter));
        }

        private async void OnSaveNewCharacter([FromSource] Player player, dynamic args)
        {
            var account = await Server.Database.Context.Accounts.FindAsync($"discord:{player.Identifiers["discord"]}");
            Character data = JsonConvert.DeserializeObject<Character>(JsonConvert.SerializeObject(args));
            data.AccountId = account.Id;

            await Server.Database.Context.Characters.PostAsync(data);
        }
    }
}
