using System;
using JSMCodeChallenge.Models;

namespace JSMCodeChallenge.Business
{
    public class BoundingBox
    {
        private readonly decimal NormalizedMinLongitude;
        private readonly decimal NormalizedMaxLongitude;
        private readonly decimal NormalizedMinLatitude;
        private readonly decimal NormalizedMaxLatitude;

        public BoundingBox(decimal minLatitude, decimal maxLatitude, decimal minLongitude, decimal maxLongitude)
        {
            NormalizedMinLatitude = minLatitude + 90;
            NormalizedMaxLatitude = maxLatitude + 90;
            NormalizedMinLongitude = minLongitude + 180;
            NormalizedMaxLongitude = maxLongitude + 180;
        }

        public bool IsCoordinatesInside(Coordinates coordinates)
        {
            decimal normalizedLatitude = coordinates.Latitude + 90, normalizedLongitude = coordinates.Longitude + 180;
            bool insideLatitudeInterval = NormalizedMinLatitude <= normalizedLatitude && normalizedLatitude <= NormalizedMaxLatitude;
            bool insideLongitudeInterval = NormalizedMinLongitude <= normalizedLongitude && normalizedLongitude <= NormalizedMaxLongitude;
            return insideLatitudeInterval && insideLongitudeInterval;
        }
    }
}