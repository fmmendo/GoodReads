using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace GoodReads.API.Model
{
    [XmlRoot(ElementName = "shelves")]
    public class Shelves
    {
        [XmlElement(ElementName = "user_shelf")]
        public List<UserShelf> UserShelf { get; set; }

        [XmlAttribute(AttributeName = "start")]
        public String Start { get; set; }

        [XmlAttribute(AttributeName = "end")]
        public String End { get; set; }

        [XmlAttribute(AttributeName = "total")]
        public String Total { get; set; }

        [XmlElement(ElementName = "shelf")]
        public Shelf Shelf { get; set; }
    }
}
