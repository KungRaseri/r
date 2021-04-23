using System;
using System.Collections.Generic;
using CitizenFX.Core;

namespace OpenRP.Framework.Client.Classes
{
    internal class VoiceZone
    {
        static int _radius;
        static int _offset;
        static VoiceZone()
        {
            _radius = 32;
            _offset = 256 / _radius;
        }

        public static int GetGrid(Vector3 pos)
        {
            return (int)(Math.Floor(31 * _offset + _offset * 6 - 6 + 5 + Math.Ceiling((pos.X + pos.Y) / _radius)));
        }
    }
}
