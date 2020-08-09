using System.Collections.Generic;
using JSMCodeChallenge.Exceptions;

namespace JSMCodeChallenge.Models
{
    public abstract class Location
    {
        public string Street { get; set; }
        public string City { get; set; }
        public int PostalCode { get; set; }
        public Coordinates Coordinates { get; set; }
        public Timezone Timezone { get; set; }
        public abstract string State { get; set; }
        public abstract string Region { get; }
    }

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

    public class BrazilianLocation : Location
    {
        private readonly HashSet<string> NorthStates = new HashSet<string> { "acre", "amazonas", "amapá", "pará", "rondônia", "roraima", "tocantins" };
        private readonly HashSet<string> NorthEastStates = new HashSet<string> { "alagoas", "bahia", "ceará", "maranhão", "paraíba", "pernambuco", "piauí", "rio grande do norte", "sergipe" };
        private readonly HashSet<string> CenterWestStates = new HashSet<string> { "goiás", "mato grosso", "mato grosso do sul", "distrito federal" };
        private readonly HashSet<string> SouthStates = new HashSet<string> { "rio grande do sul", "paraná", "santa catarina" };
        private readonly HashSet<string> SouthEastStates = new HashSet<string> { "espírito santo", "minas gerais", "são paulo", "rio de janeiro" };
        private string _State;
        public override string State { get => _State; set {
            string stateLoweredString = value.ToLower();

            if (!NorthStates.Contains(stateLoweredString) && !NorthEastStates.Contains(stateLoweredString) && !CenterWestStates.Contains(stateLoweredString) && !SouthStates.Contains(stateLoweredString) && !SouthEastStates.Contains(stateLoweredString))
                throw new InvalidLocationStateException();
            
            _State = stateLoweredString;
        }}
        public override string Region { get
            {
                if (NorthStates.Contains(State))
                    return "north";
                if (NorthEastStates.Contains(State))
                    return "north east";
                if (CenterWestStates.Contains(State))
                    return "center west";
                if (SouthStates.Contains(State))
                    return "south";
                return "south east";
            }
        }
    }
}