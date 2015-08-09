using System;
using System.Xml.Serialization;

namespace MyShelf.API.XML
{
    [XmlRoot(ElementName = "action")]
    public class UpdateAction
    {
        [XmlElement(ElementName = "rating")]
        public String Rating { get; set; }

        [XmlAttribute(AttributeName = "type")]
        public String Type { get; set; }
    }
}
