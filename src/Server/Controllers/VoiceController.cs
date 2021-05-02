using System;
using System.Collections.Generic;
using System.Text;
using static CitizenFX.Core.Native.API;

namespace OpenRP.Framework.Server.Controllers
{
    /// <summary>
    /// Handles VOIP.
    /// </summary>
    public class VoiceController : ServerAccessor
    {
        internal VoiceController (ServerMain server) : base (server)
        {
            CreateChannels();
        }

        private void CreateChannels()
        {
            for (var i = 0; i < 1024; i++)
                MumbleCreateChannel(i);
        }
    }
}
