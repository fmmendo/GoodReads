using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace MyShelf.API.XML
{
    [XmlRoot(ElementName = "notification")]
    public class Notification
    {
        [XmlElement(ElementName = "id")]
        public string Id { get; set; }

        //[XmlArray("actors")]
        //[XmlArrayItem("actor")]
        //public List<Actor> Actors { get; set; }
        
        [XmlElement(ElementName = "new")]
        public string New { get; set; }
        
        [XmlElement(ElementName = "created_at")]
        public string Created_at { get; set; }
        
        [XmlElement(ElementName = "body")]
        public NotificationBody Body { get; set; }
        
        [XmlElement(ElementName = "url")]
        public string Url { get; set; }
        
        [XmlElement(ElementName = "resource_type")]
        public string Resource_type { get; set; }
        
        [XmlElement(ElementName = "group_resource_type")]
        public string Group_resource_type { get; set; }
        
        [XmlElement(ElementName = "group_resource")]
        public GroupResource Group_resource { get; set; }
    }
}
