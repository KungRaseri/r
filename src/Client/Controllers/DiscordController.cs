using CitizenFX.Core;
using System;
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
            Client.RegisterTickHandler(OnDiscordPresenceTick);
        }

        private async Task OnDiscordPresenceTick()
        {
            //TODO: change to configuration values
            SetDiscordAppId("567639583219515410");

            SetDiscordRichPresenceAsset("redacted_700x700_stacked");
            SetDiscordRichPresenceAssetText("[REDACTED] Roleplay");

            //SetDiscordRichPresenceAssetSmall("redacted_700x700_mono");
            //SetDiscordRichPresenceAssetSmallText("Join us today!");

            SetDiscordRichPresenceAction(0, "Connect to Server", "fivem://connect/ca.node2.redactedgaming.gg:7000");
            SetDiscordRichPresenceAction(1, "Website", "https://redactedgaming.gg/");

            await BaseScript.Delay(TimeSpan.FromMinutes(1).Milliseconds);
        }
    }
}
