using System;
using System.Xml.Serialization;

namespace MyShelf.API.XML
{
    [XmlRoot(ElementName = "shelf")]
    public class Shelf
    {
        [XmlAttribute(AttributeName = "name")]
        public string Name { get; set; }

        [XmlAttribute(AttributeName = "exclusive")]
        public string Exclusive { get; set; }

        [XmlAttribute(AttributeName = "count")]
        public string Count { get; set; }
    }
}
