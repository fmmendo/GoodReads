using System.Xml.Serialization;

namespace MyShelf.API.XML
{
    [XmlRoot(ElementName = "update")]
    public class Update
    {
        [XmlElement(ElementName = "action_text")]
        public string ActionText { get; set; }
        
        [XmlElement(ElementName = "link")]
        public string Link { get; set; }
        
        [XmlElement(ElementName = "image_url")]
        public string ImageUrl { get; set; }

        [XmlElement(ElementName = "actor")]
        public User Actor { get; set; }

        [XmlElement(ElementName = "updated_at")]
        public string UpdatedAt { get; set; }

        [XmlElement(ElementName = "object")]
        public UpdateObject Object { get; set; }

        [XmlAttribute(AttributeName = "type")]
        public string Type { get; set; }

        [XmlElement(ElementName = "action")]
        public UpdateAction Action { get; set; }

        [XmlElement(ElementName = "body")]
        public string Body { get; set; }
    }
}
