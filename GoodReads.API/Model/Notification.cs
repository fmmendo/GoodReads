using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace GoodReads.API.Model
{
    [XmlRoot(ElementName = "notification")]
    public class Notification
    {
        [XmlElement(ElementName = "id")]
        public String Id { get; set; }

        [XmlArray("actors")]
        [XmlArrayItem("actor")]
        public List<Actor> Actors { get; set; }
        
        [XmlElement(ElementName = "new")]
        public String New { get; set; }
        
        [XmlElement(ElementName = "created_at")]
        public String Created_at { get; set; }
        
        [XmlElement(ElementName = "body")]
        public Body Body { get; set; }
        
        [XmlElement(ElementName = "url")]
        public String Url { get; set; }
        
        [XmlElement(ElementName = "resource_type")]
        public String Resource_type { get; set; }
        
        [XmlElement(ElementName = "group_resource_type")]
        public String Group_resource_type { get; set; }
        
        [XmlElement(ElementName = "group_resource")]
        public GroupResource Group_resource { get; set; }
    }
}
