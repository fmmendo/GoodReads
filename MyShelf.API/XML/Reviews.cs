using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace MyShelf.API.XML
{
    [XmlRoot(ElementName = "reviews")]
    public class Reviews
    {
        [XmlElement(ElementName = "review")]
        public List<Review> Review { get; set; }
        
        [XmlAttribute(AttributeName = "start")]
        public string Start { get; set; }
        
        [XmlAttribute(AttributeName = "end")]
        public string End { get; set; }

        [XmlAttribute(AttributeName = "total")]
        public string Total { get; set; }

        [XmlAttribute(AttributeName = "api_blocked_count")]
        public string ApliBlockedCount { get; set; }
    }
}
