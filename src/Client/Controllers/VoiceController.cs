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
using System.Collections.Generic;

namespace OpenRP.Framework.Client.Controllers
{
    public class VoiceController : ClientAccessor
    {
        int _grid;
        int _lastGrid;
        List<int> _zones;
        string _zonesList;

        public VoiceController(ClientMain client) : base(client)
        {
            _grid = 0;
            _lastGrid = 0;
            _zones = new List<int>();
            _zonesList = "";

            MumbleSetAudioInputDistance(30f);
            MumbleSetAudioOutputDistance(30f);
            client.RegisterTickHandler(GameTick);
        }

        private async Task GameTick()
        {
            var pos = Game.PlayerPed.Position;
            _zones = VoiceZone.GetZones(pos);
            _grid = _zones[0];
            var temp = "";

            if (_lastGrid != _grid)
            {
                MumbleClearVoiceTargetChannels(1);
                NetworkSetVoiceChannel(_grid);

                foreach (var zone in _zones)
                {
                    temp += zone + " | ";
                    MumbleAddVoiceTargetChannel(1, zone);
                }

                _zonesList = temp;
                _lastGrid = _grid;
            }

            var textGrid = new Text($"Grid: {_grid} | {NetworkIsPlayerTalking(Game.Player.Handle)}", new PointF(1f, 1f), 0.5f);
            var textNearby = new Text($"{_zonesList}", new PointF(1.0f, 20.0f), 0.5f);

            textGrid.Draw();
            textNearby.Draw();
        }
    }
}
