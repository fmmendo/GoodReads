using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace GoodReads.API.Model
{
    [XmlRoot(ElementName = "books")]
    public class Books
    {
        [XmlElement(ElementName = "book")]
        public List<Book> Book { get; set; }
        
        [XmlAttribute(AttributeName = "start")]
        public String Start { get; set; }
        
        [XmlAttribute(AttributeName = "end")]
        public String End { get; set; }
        
        [XmlAttribute(AttributeName = "total")]
        public String Total { get; set; }
    }
}
