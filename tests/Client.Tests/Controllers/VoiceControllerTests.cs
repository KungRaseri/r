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
            var zone = VoiceZone.GetGrid(pos);
            Assert.Equal(89550, zone);
            Assert.Equal(9, zone);

            pos.X = -5000; // 0
            pos.Y = 9000; // 0
            zone = VoiceZone.GetGrid(pos);
            Assert.Equal(222, zone);
            Assert.Equal(4, zone);

            pos.X = -1750; // 32
            pos.Y = 2250; // 67
            zone = VoiceZone.GetGrid(pos);
            Assert.Equal(66618, zone);
            Assert.Equal(9, zone);

            pos.X = 5950; // 109
            pos.Y = 2250; // 67
            zone = VoiceZone.GetGrid(pos);
            Assert.Equal(44871, zone);
            Assert.Equal(6, zone);
        }
    }
}