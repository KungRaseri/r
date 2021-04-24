using CitizenFX.Core;
using OpenRP.Framework.Common.Classes;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using static CitizenFX.Core.Native.API;

namespace OpenRP.Framework.Client.Controllers
{
    /// <summary>
    /// Handles client-side Discord integration
    /// </summary>
    public class DiscordController : ClientAccessor
    {
        internal DiscordController(ClientMain client) : base(client)
        {
            //TODO: change to configuration value
            SetDiscordAppId("567639583219515410");
            Client.RegisterTickHandler(OnDiscordPresenceTick);
        }

        private async Task OnDiscordPresenceTick()
        {

            SetDiscordRichPresenceAsset("redacted_short");
            SetDiscordRichPresenceAssetText("");

            SetDiscordRichPresenceAssetSmall("");
            SetDiscordRichPresenceAssetSmallText("");

            SetDiscordRichPresenceAction(0, "Our Website!", "https://redactedgaming.gg/");
            SetDiscordRichPresenceAction(0, "Apply Now!", "https://redactedgaming.gg/index.php?/forms/4-community-allow-list-roleplay-application/");
            SetDiscordRichPresenceAction(0, "Join Server!", "fivem://");

            await BaseScript.Delay(TimeSpan.FromMinutes(1).Milliseconds);
        }
    }
}
