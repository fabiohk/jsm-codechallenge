using System.Collections.Generic;
using JSMCodeChallenge.Exceptions;

namespace JSMCodeChallenge.Models
{
    public abstract class Location
    {
        public string Street { get; set; }
        public string City { get; set; }
        public int? PostalCode { get; set; }
        public Coordinates Coordinates { get; set; }
        public Timezone Timezone { get; set; }
        public abstract string State { get; set; }
        public abstract string Region { get; }
    }

    public class Coordinates
    {
        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }
    }

    public class Timezone
    {
        public string Offset { get; set; }
        public string Description { get; set; }
    }

    public class BrazilianLocation : Location
    {
        private readonly HashSet<string> _northStates = new HashSet<string> { "acre", "amazonas", "amapá", "pará", "rondônia", "roraima", "tocantins" };
        private readonly HashSet<string> _northEastStates = new HashSet<string> { "alagoas", "bahia", "ceará", "maranhão", "paraíba", "pernambuco", "piauí", "rio grande do norte", "sergipe" };
        private readonly HashSet<string> _centerWestStates = new HashSet<string> { "goiás", "mato grosso", "mato grosso do sul", "distrito federal" };
        private readonly HashSet<string> _southStates = new HashSet<string> { "rio grande do sul", "paraná", "santa catarina" };
        private readonly HashSet<string> _southEastStates = new HashSet<string> { "espírito santo", "minas gerais", "são paulo", "rio de janeiro" };
        private string _state;
        public override string State
        {
            get => _state; set
            {
                string stateLoweredString = value.ToLower();

                if (!_northStates.Contains(stateLoweredString) && !_northEastStates.Contains(stateLoweredString) && !_centerWestStates.Contains(stateLoweredString) && !_southStates.Contains(stateLoweredString) && !_southEastStates.Contains(stateLoweredString))
                    throw new InvalidLocationStateException();

                _state = stateLoweredString;
            }
        }
        public override string Region
        {
            get
            {
                if (_northStates.Contains(State))
                    return "north";
                if (_northEastStates.Contains(State))
                    return "north east";
                if (_centerWestStates.Contains(State))
                    return "center west";
                if (_southStates.Contains(State))
                    return "south";
                return "south east";
            }
        }
    }
}