using CitizenFX.Core;
using static CitizenFX.Core.Native.API;
using System;
using System.Collections;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Threading.Tasks;
using CitizenFX.Core.UI;
using OpenRP.Framework.Client.Classes;

namespace OpenRP.Framework.Client.Controllers
{
    public class VoiceController : ClientAccessor
    {
        int _lastGrid;

        public VoiceController(ClientMain client) : base(client)
        {
            _lastGrid = 0;
            MumbleSetAudioInputDistance(30f);
            MumbleSetAudioOutputDistance(30f);
            client.RegisterTickHandler(GameTick);
        }

        private async Task GameTick()
        {
            var pos = Game.PlayerPed.Position;
            var zones = VoiceZone.GetZones(pos);
            var grid = zones[0];
            var temp = "";
            var textGrid = new Text($"Grid: {grid} | {NetworkIsPlayerTalking(Game.Player.Handle)}", new PointF(1f, 1f), 0.5f);
            var textNearby = new Text($"{temp}", new PointF(1.0f, 20.0f), 0.5f);

            textGrid.Draw();
            textNearby.Draw();

            if (_lastGrid != grid)
            {
                
                MumbleClearVoiceTargetChannels(1);
                NetworkSetVoiceChannel(grid);

                foreach (var zone in zones)
                {
                    temp += zone + " | ";
                    MumbleAddVoiceTargetChannel(1, zone);
                }

                _lastGrid = grid;
            }
        }
    }
}
