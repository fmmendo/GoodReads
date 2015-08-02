using System;
using System.Xml.Serialization;

namespace MyShelf.API.XML
{
    [XmlRoot(ElementName = "update")]
    public class Update
    {
        [XmlElement(ElementName = "action_text")]
        public string Action_text { get; set; }
        
        [XmlElement(ElementName = "link")]
        public string Link { get; set; }
        
        [XmlElement(ElementName = "image_url")]
        public string Image_url { get; set; }
        
        //[XmlElement(ElementName = "actor")]
        //public Actor Actor { get; set; }
        
        [XmlElement(ElementName = "updated_at")]
        public string Updated_at { get; set; }
        
        //[XmlElement(ElementName = "object")]
        //public GRObject Object { get; set; }
        
        [XmlAttribute(AttributeName = "type")]
        public string Type { get; set; }
        
        //[XmlElement(ElementName = "action")]
        //public GRAction Action { get; set; }
        
        [XmlElement(ElementName = "body")]
        public string Body { get; set; }
    }
}
