using JSMCodeChallenge.Models;
using System.Collections.Generic;
using System.Linq;
using System;
using Serilog;

namespace JSMCodeChallenge.Business
{
    public static class UserType
    {
        private static readonly List<BoundingBox> _specialBoundingBoxes;
        private static readonly List<BoundingBox> _normalBoundingBoxes;

        static UserType()
        {
            BoundingBox specialBoundingBox = new BoundingBox(
                new Decimal(-46.361899),
                new Decimal(-34.276938),
                new Decimal(-15.411580),
                new Decimal(-2.196998)
            );
            BoundingBox anotherSpecialBoundingBox = new BoundingBox(
                new Decimal(-52.997614),
                new Decimal(-44.428305),
                new Decimal(-23.966413),
                new Decimal(-19.766959)
            );
            _specialBoundingBoxes = new List<BoundingBox>() {
                specialBoundingBox,
                anotherSpecialBoundingBox
            };
            _normalBoundingBoxes = new List<BoundingBox>() {
                new BoundingBox(
                    new Decimal(-54.777426),
                    new Decimal(-46.603598),
                    new Decimal(-34.016466),
                    new Decimal(-26.155681)
                )
            };
        }

        public static string GetUserType(User user)
        {
            Log.Debug("Trying to evaluate user with coordinates {@Coordinates}...", user.Location?.Coordinates);

            if (user.Location == null)
                return "laborious";

            Coordinates userCoordinates = user.Location.Coordinates;
            if (_specialBoundingBoxes.Any(boundingBox => boundingBox.IsCoordinatesInside(userCoordinates)))
                return "special";
            if (_normalBoundingBoxes.Any(boundingBox => boundingBox.IsCoordinatesInside(userCoordinates)))
                return "normal";
            return "laborious";
        }
    }
}