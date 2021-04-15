using CitizenFX.Core;
using OpenRP.Framework.Client.Classes;
using Xunit;

namespace OpenRP.Framework.Tests.Controllers
{
    public class VoiceControllerTests
    {
        [Fact()]
        public void VoiceZoneTest()
        {
            // https://drive.google.com/file/d/0B-zvE86DVcv2MXhVSHZnc01QWm8/view?usp=sharing
            // This map will help identify what the grid position is for the player coordinates

            var pos = Vector3.Zero; // 50, 90
            var zone = VoiceZone.GetZones(pos);
            Assert.Equal(89550, zone[0]);
            Assert.Equal(9, zone.Count);

            pos.X = -5000; // 0
            pos.Y = 9000; // 0
            zone = VoiceZone.GetZones(pos);
            Assert.Equal(222, zone[0]);
            Assert.Equal(4, zone.Count);

            pos.X = -1750; // 32
            pos.Y = 2250; // 67
            zone = VoiceZone.GetZones(pos);
            Assert.Equal(66618, zone[0]);
            Assert.Equal(9, zone.Count);

            pos.X = 5950; // 109
            pos.Y = 2250; // 67
            zone = VoiceZone.GetZones(pos);
            Assert.Equal(44871, zone[0]);
            Assert.Equal(6, zone.Count);
        }
    }
}