namespace OpenRP.Framework.Client.Classes
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

        public string GetHashCode()
        {
            var length = 3;
            var x = X.ToString("D" + length);
            var y = Y.ToString("D" + length);
            var combine = $"{x}{y}";
            return combine;
        }
    }
}
