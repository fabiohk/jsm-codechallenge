using System;
using Microsoft.Extensions.Configuration;
using JSMCodeChallenge.Models;

namespace JSMCodeChallenge.Business
{
    public class BoundingBox
    {
        private readonly decimal _normalizedMinLongitude;
        private readonly decimal _normalizedMaxLongitude;
        private readonly decimal _normalizedMinLatitude;
        private readonly decimal _normalizedMaxLatitude;

        public BoundingBox(decimal minLatitude, decimal maxLatitude, decimal minLongitude, decimal maxLongitude)
        {
            _normalizedMinLatitude = minLatitude + 90;
            _normalizedMaxLatitude = maxLatitude + 90;
            _normalizedMinLongitude = minLongitude + 180;
            _normalizedMaxLongitude = maxLongitude + 180;
        }

        public bool IsCoordinatesInside(Coordinates coordinates)
        {
            decimal normalizedLatitude = coordinates.Latitude + 90, normalizedLongitude = coordinates.Longitude + 180;
            bool insideLatitudeInterval = _normalizedMinLatitude <= normalizedLatitude && normalizedLatitude <= _normalizedMaxLatitude;
            bool insideLongitudeInterval = _normalizedMinLongitude <= normalizedLongitude && normalizedLongitude <= _normalizedMaxLongitude;
            return insideLatitudeInterval && insideLongitudeInterval;
        }
    }
}