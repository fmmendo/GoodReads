using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace MyShelf.API.XML
{
    [XmlRoot(ElementName = "notifications")]
    public class Notifications
    {
        [XmlElement(ElementName = "notification")]
        public List<Notification> Notification { get; set; }
        
        [XmlAttribute(AttributeName = "end")]
        public string End { get; set; }
        
        [XmlAttribute(AttributeName = "start")]
        public string Start { get; set; }
        
        [XmlAttribute(AttributeName = "total")]
        public string Total { get; set; }
    }
}
