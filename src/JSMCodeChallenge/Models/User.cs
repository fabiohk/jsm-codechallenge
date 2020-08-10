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
        private string _Gender;
        public string Gender
        {
            get => _Gender; set
            {
                string loweredGenderString = value.ToLower();
                bool isFemale = loweredGenderString == "female", isMale = loweredGenderString == "male";

                if (!isFemale && !isMale)
                    throw new InvalidGenderException();

                _Gender = isFemale ? "f" : "m";
            }
        }
        public Name Name { get; set; }
        private List<PhoneNumber> _Phones;
        public List<string> Phones
        {
            get => _Phones.Select(PhoneUtil.ConvertToE164Format).ToList(); set => _Phones = PhoneUtil.RetrieveValidPhoneNumbers(value, "BR");
        }
        private List<PhoneNumber> _CellPhones;
        public List<string> CellPhones
        {
            get => _CellPhones.Select(PhoneUtil.ConvertToE164Format).ToList(); set => _CellPhones = PhoneUtil.RetrieveValidPhoneNumbers(value, "BR");
        }
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