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
        List<int> _zones;
        string _zonesList;
        Text _textGrid;
        Text _textNearby;

        readonly float[] _ranges = new float[3] { 1f, 2f, 10f }; // { Whisper, Normal, Shouting }
        int _currentRange;
        int _lastRange;

        float _speakingRange;
        float _hearingRange;

        internal VoiceController(ClientMain client) : base(client)
        {
            _grid = -1;
            _lastGrid = -1;
            _zones = new List<int>();
            _zonesList = "";
            _textGrid = new Text($"", new PointF(1f, 1f), 0.5f);
            _textNearby = new Text($"", new PointF(1.0f, 24.0f), 0.5f);

            _currentRange = 1;
            _lastRange = _currentRange;

            _speakingRange = GetVoiceRange();
            _hearingRange = _ranges[_ranges.Length - 1];

            Client.RegisterKeyBinding("ToggleVoiceRange", "(Voice) Toggle voice range", "z", new Action(ToggleVoiceRange));

            MumbleSetAudioOutputDistance(_speakingRange);
            MumbleSetAudioInputDistance(_hearingRange);
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
            _speakingRange = GetVoiceRange();
            MumbleSetAudioOutputDistance(_speakingRange);
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
            _zones = VoiceZone.GetZones(pos);
            _grid = _zones[0];
            var temp = "";

            if (_lastGrid != _grid)
            {
                MumbleClearVoiceTargetChannels(1);

                foreach (var zone in _zones)
                {
                    temp += zone + " | ";
                    MumbleAddVoiceTargetChannel(1, zone);
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
