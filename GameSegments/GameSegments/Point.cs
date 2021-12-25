namespace GameSegments
{
    public readonly struct Point
    {
        public readonly double X;
        public readonly double Y;
        public readonly int Index;

        public Point(double x, double y,int index = -1)
        {
            X = x;
            Y = y;
            Index = index;
        }
    }
}