using Xunit;
using OpenRP.Framework.Client.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CitizenFX.Core;

namespace OpenRP.Framework.Client.Controllers.Tests
{
    public class VoiceControllerTests
    {
        [Fact()]
        public void VoiceZoneTest()
        {
            // https://drive.google.com/file/d/0B-zvE86DVcv2MXhVSHZnc01QWm8/view?usp=sharing
            // This map will help identify what the grid position is for the player coordinates

            var pos = Vector3.Zero; // 50, 90
            var zone = VoiceZone.GetGridCoord(pos);
            Assert.Equal(9950, zone);

            pos.X = -5000; // 0
            pos.Y = 9000; // 0
            zone = VoiceZone.GetGridCoord(pos);
            Assert.Equal(0, zone);

            pos = Vector3.Zero;
            pos.X = -1750; // 32
            pos.Y = 2250; // 67
            zone = VoiceZone.GetGridCoord(pos);
            Assert.Equal(7402, zone);
        }
    }
}