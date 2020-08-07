using System;

namespace JSMCodeChallenge.Models
{
    public class User : IEquatable<User>
    {
        public string Gender { get; set; }

        public string Title { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public string Email { get; set; } // Email is the user key!
        public string Phone { get; set; }

        public string CellPhone { get; set; }

        public DateTime RegisteredDate { get; set; }

        public DateTime BirthDate { get; set; }

        public int CurrentAge { get; set; }

        public int RegisteredAge { get; set; }

        public Location Location { get; set; }

        public Picture Picture { get; set; }
        public string Nationality { get; set; } = "BR";

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