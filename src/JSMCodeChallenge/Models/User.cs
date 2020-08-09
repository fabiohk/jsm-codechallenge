using System;
using System.Collections.Generic;
using JSMCodeChallenge.Exceptions;

namespace JSMCodeChallenge.Models
{
    public class User : IEquatable<User>
    {
        public string Email { get; set; } // Email is the user key!
        private string _Gender;
        public string Gender { get => _Gender; set {
            string loweredGenderString = value.ToLower();
            bool isFemale = loweredGenderString == "female", isMale = loweredGenderString == "male";
            
            if (!isFemale && !isMale)
                throw new InvalidGenderException();

            _Gender = isFemale ? "f" : "m";
        } }
        public Name Name { get; set; }
        public List<string> Phone { get; set; }
        public List<string> CellPhone { get; set; }
        public DateTime RegisteredDate { get; set; }
        public DateTime BirthDate { get; set; }
        public Location Location { get; set; }
        public Picture Picture { get; set; }
        public string Nationality { get; set; }
        public string Type { get; }

        public bool Equals(User anotherUser)
        {
            if (anotherUser is null)
                return false;

            return this.Email == anotherUser.Email;
        }

        public override bool Equals(object? obj) => Equals(obj as User);
        public override int GetHashCode() => Email.GetHashCode();
    }
}