using System;
using System.Xml.Serialization;

namespace GoodReads.API.Model
{
    [XmlRoot(ElementName = "body")]
    public class Body
    {
        [XmlElement(ElementName = "html")]
        public String Html { get; set; }
        
        [XmlElement(ElementName = "text")]
        public String Text { get; set; }
    }
}
