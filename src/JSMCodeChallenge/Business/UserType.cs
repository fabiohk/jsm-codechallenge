using JSMCodeChallenge.Models;
using System.Collections.Generic;
using System.Linq;
using System;
using Serilog;
using System.IO;
using Microsoft.Extensions.Configuration;

namespace JSMCodeChallenge.Business
{
    public static class UserType
    {
        private static readonly List<BoundingBox> _specialBoundingBoxes;
        private static readonly List<BoundingBox> _normalBoundingBoxes;

        static UserType()
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false)
                .AddEnvironmentVariables()
                .Build();

            var boundingBoxesSection = configuration.GetSection("BoundingBoxes");
            var specialBoundingBoxesSection = boundingBoxesSection.GetSection("Special").GetChildren();
            var normalBoundingBoxesSection = boundingBoxesSection.GetSection("Normal").GetChildren();

            _specialBoundingBoxes = specialBoundingBoxesSection
                .Select(box => new BoundingBox(
                    Convert.ToDecimal(box["MinLatitude"]),
                    Convert.ToDecimal(box["MaxLatitude"]),
                    Convert.ToDecimal(box["MinLongitude"]),
                    Convert.ToDecimal(box["MaxLongitude"])
                ))
                .ToList();

            Log.Debug("Initialized bounding boxes with: Special (@SpecialBoundingBoxes}", _specialBoundingBoxes);
            _normalBoundingBoxes = normalBoundingBoxesSection
                .Select(box => new BoundingBox(
                    Convert.ToDecimal(box["MinLatitude"]),
                    Convert.ToDecimal(box["MaxLatitude"]),
                    Convert.ToDecimal(box["MinLongitude"]),
                    Convert.ToDecimal(box["MaxLongitude"])
                ))
                .ToList();
            Log.Debug("Initialized bounding boxes with: Normal (@NormalBoundingBoxes}", _normalBoundingBoxes);
        }

        public static string GetUserType(User user)
        {
            Log.Debug("Trying to evaluate user with coordinates {@Coordinates}...", user.Location?.Coordinates);

            if (user.Location?.Coordinates == null)
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