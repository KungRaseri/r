using CitizenFX.Core;
using static CitizenFX.Core.Native.API;
using System;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenRP.Framework.Client.Controllers
{
    public class VoiceController : ClientAccessor
    {
        int _lastGrid;

        public VoiceController(ClientMain client) : base(client)
        {
            _lastGrid = 0;
            client.RegisterTickHandler(GameTick);
        }

        private async Task GameTick()
        {
            var pos = Game.PlayerPed.Position;
            var zones = VoiceZone.GetZones(pos);
            var grid = VoiceZone.GetGrid(zones);

            if (_lastGrid != grid)
            {
                Debug.WriteLine($"Grid: {grid}");
                NetworkSetVoiceChannel(grid);

                foreach (var zone in zones)
                {
                    MumbleAddVoiceTargetChannel(1, zone);
                }

                _lastGrid = grid;
            }
        }
    }
}
