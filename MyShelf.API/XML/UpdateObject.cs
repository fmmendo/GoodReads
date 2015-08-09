using System.Xml.Serialization;

namespace MyShelf.API.XML
{
    [XmlRoot(ElementName = "object")]
    public class UpdateObject
    {
        [XmlElement(ElementName = "read_status")]
        public ReadStatus Read_status { get; set; }

        [XmlElement(ElementName = "user_status")]
        public UserStatus User_status { get; set; }

        [XmlElement(ElementName = "book")]
        public Book Book { get; set; }
    }
}
