using System;
using System.Collections.Generic;
using JSMCodeChallenge.Exceptions;
using System.Linq;
using JSMCodeChallenge.Helpers;
using PhoneNumbers;
using JSMCodeChallenge.Business;

namespace JSMCodeChallenge.Models
{
    public class User : IEquatable<User>
    {
        public string Email { get; set; } // Email is the user key!
        private string _gender;
        public string Gender
        {
            get => _gender;
            set
            {
                string loweredGenderString = value.ToLower();
                bool isFemale = loweredGenderString == "female", isMale = loweredGenderString == "male";

                if (!isFemale && !isMale)
                    throw new InvalidGenderException();

                _gender = isFemale ? "f" : "m";
            }
        }
        public Name Name { get; set; }
        public IEnumerable<string> Phones { get; set; } = new List<string>();
        public IEnumerable<string> CellPhones { get; set; } = new List<string>();
        public DateTime? RegisteredDate { get; set; }
        public DateTime? BirthDate { get; set; }
        public Location Location { get; set; }
        public Picture Picture { get; set; }
        public string Nationality { get; set; }
        public string Type
        {
            get => UserType.GetUserType(this);
        }

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