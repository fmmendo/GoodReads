using System;
using System.Xml.Serialization;

namespace GoodReads.API.Model
{
    [XmlRoot(ElementName = "update")]
    public class Update
    {
        [XmlElement(ElementName = "action_text")]
        public String Action_text { get; set; }
        
        [XmlElement(ElementName = "link")]
        public String Link { get; set; }
        
        [XmlElement(ElementName = "image_url")]
        public String Image_url { get; set; }
        
        [XmlElement(ElementName = "actor")]
        public Actor Actor { get; set; }
        
        [XmlElement(ElementName = "updated_at")]
        public String Updated_at { get; set; }
        
        [XmlElement(ElementName = "object")]
        public GRObject Object { get; set; }
        
        [XmlAttribute(AttributeName = "type")]
        public String Type { get; set; }
        
        [XmlElement(ElementName = "action")]
        public GRAction Action { get; set; }
        
        [XmlElement(ElementName = "body")]
        public String Body { get; set; }
    }
}
