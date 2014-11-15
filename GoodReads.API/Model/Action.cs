using System;
using System.Xml.Serialization;

namespace GoodReads.API.Model
{
    [XmlRoot(ElementName = "action")]
    public class GRAction
    {
        [XmlElement(ElementName = "rating")]
        public String Rating { get; set; }

        [XmlAttribute(AttributeName = "type")]
        public String Type { get; set; }
    }
}
