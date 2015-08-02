using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace MyShelf.API.XML
{
    [XmlRoot(ElementName = "shelves")]
    public class Shelves
    {
        [XmlElement(ElementName = "user_shelf")]
        public List<UserShelf> UserShelf { get; set; }

        [XmlAttribute(AttributeName = "start")]
        public string Start { get; set; }

        [XmlAttribute(AttributeName = "end")]
        public string End { get; set; }

        [XmlAttribute(AttributeName = "total")]
        public string Total { get; set; }

        [XmlElement(ElementName = "shelf")]
        public Shelf Shelf { get; set; }
    }
}
