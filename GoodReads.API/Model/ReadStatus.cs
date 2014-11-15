using System;
using System.Xml.Serialization;

namespace GoodReads.API.Model
{
    [XmlRoot(ElementName = "read_status")]
    public class ReadStatus
    {
        [XmlElement(ElementName = "id")]
        public String Id { get; set; }

        [XmlElement(ElementName = "old_status")]
        public ReadStatus Old_status { get; set; }

        [XmlElement(ElementName = "review_id")]
        public String Review_id { get; set; }

        [XmlElement(ElementName = "status")]
        public String Status { get; set; }

        [XmlElement(ElementName = "updated_at")]
        public String Updated_at { get; set; }

        [XmlElement(ElementName = "user_id")]
        public String User_id { get; set; }

        [XmlElement(ElementName = "review")]
        public Review Review { get; set; }
    }
}
