using CitizenFX.Core;
using static CitizenFX.Core.Native.API;
using System.Drawing;
using System.Threading.Tasks;
using CitizenFX.Core.UI;
using OpenRP.Framework.Client.Classes;
using System.Collections.Generic;
using System;

namespace OpenRP.Framework.Client.Controllers
{
    public class VoiceController : ClientAccessor
    {
        int _grid;
        int _lastGrid;
        string _zonesList;
        Text _textGrid;
        Text _textNearby;

        readonly float[] _ranges = new float[3] { 1f, 2f, 10f }; // { Whisper, Normal, Shouting }
        int _currentRange;
        int _lastRange;

        internal VoiceController(ClientMain client) : base(client)
        {
            _grid = -1;
            _lastGrid = -1;
            _zonesList = "";
            _textGrid = new Text($"", new PointF(1f, 1f), 0.5f);
            _textNearby = new Text($"", new PointF(1.0f, 24.0f), 0.5f);

            _currentRange = 1;
            _lastRange = _currentRange;

            Client.RegisterKeyBinding("ToggleVoiceRange", "(Voice) Toggle voice range", "z", new Action(ToggleVoiceRange));

            MumbleSetAudioInputDistance(GetVoiceRange());
            MumbleSetAudioOutputDistance(10f);
            CheckMumbleLoaded();
        }

        private float GetVoiceRange()
        {
            return _ranges[_currentRange];
        }

        private void ToggleVoiceRange()
        {
            _currentRange++;
            if (_currentRange >= _ranges.Length)
                _currentRange = 0;
            MumbleSetAudioInputDistance(GetVoiceRange());
        }

        private async void CheckMumbleLoaded()
        {
            while (!MumbleIsConnected())
                await BaseScript.Delay(100);

            MumbleSetVoiceTarget(0);
            MumbleClearVoiceTarget(1);
            MumbleSetVoiceTarget(1);

            Client.RegisterTickHandler(GameTick);
            Client.RegisterTickHandler(DrawDebug);
        }

        private async Task GameTick()
        {
            var pos = Game.PlayerPed.Position;
            _grid = VoiceZone.GetGrid(pos);
            var temp = "";

            if (_lastGrid != _grid)
            {
                MumbleClearVoiceTargetChannels(1);
                NetworkSetVoiceChannel(_grid);

                for (var i = _grid - 3; i <= _grid + 3; i++)
                {
                    temp += i.ToString() + " | ";
                    MumbleAddVoiceTargetChannel(1, i);
                }

                _zonesList = temp;
                _lastGrid = _grid;

                _textGrid.Caption = $"Grid: {_grid} | Range: {GetVoiceRange()}";
                _textNearby.Caption = $"{_zonesList}";
            }

            if (_lastRange != _currentRange)
            {
                _lastRange = _currentRange;

                _textGrid.Caption = $"Grid: {_grid} | Range: {GetVoiceRange()}";
            }

            await BaseScript.Delay(50);
        }

        private async Task DrawDebug()
        {
            _textGrid.Draw();
            _textNearby.Draw();
        }
    }
}
