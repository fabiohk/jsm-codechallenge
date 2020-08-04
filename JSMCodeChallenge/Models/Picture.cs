using CsvHelper.Configuration;

namespace JSMCodeChallenge.Models
{
    public class Picture {
        public string Large { get; set; }
        public string Medium { get; set; }
        public string Thumbnail { get; set; }
    }

    public class PictureCSVMap : ClassMap<Picture> {
        public PictureCSVMap() {
            Map(member => member.Large).Name("picture__large");
            Map(member => member.Medium).Name("picture__medium");
            Map(member => member.Thumbnail).Name("picture__thumbnail");
        }
    }
}