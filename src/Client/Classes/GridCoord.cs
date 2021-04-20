namespace OpenRP.Framework.Client.Classes
{
    internal class GridCoord
    {
        public int X { get; private set; }
        public int Y { get; private set; }

        internal GridCoord(int x, int y)
        {
            X = x;
            Y = y;
        }

        /// <summary>
        /// Get a unique grid ID.
        /// </summary>
        /// <returns></returns>
        public new string GetHashCode()
        {
            var length = 3;
            var x = X.ToString("D" + length);
            var y = Y.ToString("D" + length);
            var combine = $"{x}{y}";
            return combine;
        }
    }
}
