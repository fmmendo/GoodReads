using System.Xml.Serialization;

namespace GoodReads.API.Model
{
    [XmlRoot(ElementName = "actors")]
    public class Actors
    {
        [XmlElement(ElementName = "user")]
        public User User { get; set; }
    }
}
