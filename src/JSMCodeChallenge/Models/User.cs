using System;
using System.Collections.Generic;

namespace JSMCodeChallenge.Models
{
    public class User : IEquatable<User>
    {
        public string Email { get; set; } // Email is the user key!
        public string Gender { get; set; }
        public Name name { get; set; }
        public List<string> Phone { get; set; }
        public List<string> CellPhone { get; set; }
        public DateTime RegisteredDate { get; set; }
        public DateTime BirthDate { get; set; }
        public Location Location { get; set; }
        public Picture Picture { get; set; }
        public string Nationality { get; set; } = "BR";

        // public string Type() {

        // }

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