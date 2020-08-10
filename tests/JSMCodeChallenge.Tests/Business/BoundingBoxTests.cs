using Xunit;
using System;
using JSMCodeChallenge.Business;
using JSMCodeChallenge.Models;

namespace JSMCodeChallenge.Tests.Business
{
    public class BoundingBoxTests
    {
        [Fact(DisplayName = "Should return true if coordinates is inside the bounding box")]
        public void TestIsInsideBoundingBox()
        {
            Coordinates coordinates = new Coordinates() { Latitude = new Decimal(-40.3253), Longitude = new Decimal(20.9437) };
            decimal minLatitude = new Decimal(-46.361899), maxLatitude = new Decimal(-34.276938), minLongitude = new Decimal(-2.196998), maxLongitude = new Decimal(34.016466);
            BoundingBox boundingBox = new BoundingBox(minLatitude, maxLatitude, minLongitude, maxLongitude);

            Assert.True(boundingBox.IsCoordinatesInside(coordinates));
        }

        [Fact(DisplayName = "Should return false if coordinates isn't inside the bounding box")]
        public void TestIsOutsideBoundingBox()
        {
            Coordinates coordinates = new Coordinates() { Latitude = new Decimal(-4.3253), Longitude = new Decimal(20.9437) };
            decimal minLatitude = new Decimal(-46.361899), maxLatitude = new Decimal(-34.276938), minLongitude = new Decimal(-2.196998), maxLongitude = new Decimal(34.016466);
            BoundingBox boundingBox = new BoundingBox(minLatitude, maxLatitude, minLongitude, maxLongitude);

            Assert.False(boundingBox.IsCoordinatesInside(coordinates));
        }
    }
}