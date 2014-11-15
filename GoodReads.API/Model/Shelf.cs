using System;
using System.Xml.Serialization;

namespace GoodReads.API.Model
{
    [XmlRoot(ElementName = "shelf")]
    public class Shelf
    {
        [XmlAttribute(AttributeName = "name")]
        public String Name { get; set; }

        [XmlAttribute(AttributeName = "exclusive")]
        public String Exclusive { get; set; }

        [XmlAttribute(AttributeName = "count")]
        public String Count { get; set; }
    }
}
