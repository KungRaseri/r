using CitizenFX.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenRP.Framework.Client.Controllers
{
    public class GridCoord
    {
        public int X { get; private set; }
        public int Y { get; private set; }

        public GridCoord(int x, int y)
        {
            X = x;
            Y = y;
        }
    }

    public class VoiceController : ClientAccessor
    {
        Dictionary<int, int> _grid;

        public VoiceController(ClientMain client) : base(client)
        {
            _grid = new Dictionary<int, int>();
            BuildGrid();
            Debug.WriteLine("test");
        }

        private void BuildGrid()
        {
            var minX = 5000;
            var maxX = 6000 + minX;
            var minY = 9000;
            var maxY = 5000 + minY;
            var interval = 100;
            var count = 0;

            for (var y = 0; y < maxY; y += interval)
            {
                for (var x = 0; x < maxX; x += interval)
                {
                    var quad = new GridCoord(x / interval, y / interval);
                    _grid.Add(quad.GetHashCode(), count);
                    count++;
                }
            }
        }
    }
}
