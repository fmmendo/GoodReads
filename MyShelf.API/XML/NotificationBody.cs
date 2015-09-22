using System;
using System.Xml.Serialization;

namespace MyShelf.API.XML
{
    [XmlRoot(ElementName = "body")]
    public class NotificationBody
    {
        [XmlElement(ElementName = "html")]
        public string Html { get; set; }
        
        [XmlElement(ElementName = "text")]
        public string Text { get; set; }
    }
}
