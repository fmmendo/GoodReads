using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace MyShelf.API.XML
{
    [XmlRoot(ElementName = "updates")]
    public class Updates
    {
        [XmlElement(ElementName = "update")]
        public List<Update> Update { get; set; }

        [XmlAttribute(AttributeName = "type")]
        public string Type { get; set; }
    }
}
