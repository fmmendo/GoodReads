using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace GoodReads.API.Model
{
    [XmlRoot(ElementName = "notifications")]
    public class Notifications
    {
        [XmlElement(ElementName = "notification")]
        public List<Notification> Notification { get; set; }
        
        [XmlAttribute(AttributeName = "end")]
        public String End { get; set; }
        
        [XmlAttribute(AttributeName = "start")]
        public String Start { get; set; }
        
        [XmlAttribute(AttributeName = "total")]
        public String Total { get; set; }
    }
}
