using System.Collections.Generic;
using JSMCodeChallenge.Exceptions;


namespace JSMCodeChallenge.Models
{
    public abstract class Location
    {
        public string Street { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public int PostalCode { get; set; }
        public Coordinates Coord { get; set; }
        public Timezone TZ { get; set; }

        public class Coordinates
        {
            public string Latitde { get; set; }
            public string Longitude { get; set; }
        }

        public class Timezone
        {
            public string Offset { get; set; }
            public string Description { get; set; }
        }

        public abstract string Region();
    }

    public class BrazilianLocation : Location
    {
        private readonly HashSet<string> NorthStates = new HashSet<string> { "acre", "amazonas", "amapá", "pará", "rondônia", "roraima", "tocantins" };
        private readonly HashSet<string> NorthEastStates = new HashSet<string> { "alagoas", "bahia", "ceará", "maranhão", "paraíba", "pernambuco", "piauí", "rio grande do norte", "sergipe" };
        private readonly HashSet<string> CenterWestStates = new HashSet<string> { "goiás", "mato grosso", "mato grosso do sul", "distrito federal" };
        private readonly HashSet<string> SouthStates = new HashSet<string> { "rio grande do sul", "paraná", "santa catarina" };
        private readonly HashSet<string> SouthEastStates = new HashSet<string> { "espírito santo", "minas gerais", "são paulo", "rio de janeiro" };
        public override string Region()
        {
            string stateLoweredString = State.ToLower();

            if (NorthStates.Contains(stateLoweredString))
                return "north";
            if (NorthEastStates.Contains(stateLoweredString))
                return "north east";
            if (CenterWestStates.Contains(stateLoweredString))
                return "center west";
            if (SouthStates.Contains(stateLoweredString))
                return "south";
            if (SouthEastStates.Contains(stateLoweredString))
                return "south east";

            throw new CannotDetermineRegionLocationException();
        }
    }
}