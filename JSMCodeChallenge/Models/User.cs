using System;
using CsvHelper.Configuration;

namespace JSMCodeChallenge.Models
{
    public class User {
        public string Gender { get; set; }
        public string Title { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string CellPhone { get; set; }
        public DateTime RegisteredDate { get; set; }
        public DateTime BirthDate { get; set; }
        public int CurrentAge { get; set; }
        public int RegisteredAge { get; set; }
        public Location Location { get; set; }
        public Picture Picture { get; set; }
    }

    public class UserCSVMap : ClassMap<User> {
        public UserCSVMap() {
            Map(member => member.Gender).Name("gender");
            Map(member => member.Title).Name("name__title");
            Map(member => member.FirstName).Name("name__first");
            Map(member => member.LastName).Name("name__last");
            Map(member => member.Email).Name("email");
            Map(member => member.Phone).Name("phone");
            Map(member => member.CellPhone).Name("cell");
            Map(member => member.RegisteredDate).Name("registered__date");
            Map(member => member.BirthDate).Name("dob__date");
            Map(member => member.CurrentAge).Name("dob__age");
            Map(member => member.RegisteredAge).Name("registered__age");
            References<LocationCSVMap>(member => member.Location);
            References<PictureCSVMap>(member => member.Picture);
        }
    }
}