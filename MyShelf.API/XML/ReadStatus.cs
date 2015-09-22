using System;
using System.Xml.Serialization;

namespace MyShelf.API.XML
{
    [XmlRoot(ElementName = "read_status")]
    public class ReadStatus
    {
        [XmlElement(ElementName = "id")]
        public string Id { get; set; }

        [XmlElement(ElementName = "old_status")]
        public ReadStatus Old_status { get; set; }

        [XmlElement(ElementName = "review_id")]
        public string Review_id { get; set; }

        [XmlElement(ElementName = "status")]
        public string Status { get; set; }

        [XmlElement(ElementName = "updated_at")]
        public string Updated_at { get; set; }

        [XmlElement(ElementName = "user_id")]
        public string User_id { get; set; }

        [XmlElement(ElementName = "review")]
        public Review Review { get; set; }
    }
}
