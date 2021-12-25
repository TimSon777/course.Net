using XProtocol.Serializator;

namespace GameSegments
{
    public class GameSettings
    {
        public int PointsCount { get; set; }

        public GameSettings(int pointsCount)
        {
            PointsCount = pointsCount;
        }
    }
}