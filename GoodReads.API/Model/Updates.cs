using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace GoodReads.API.Model
{
    [XmlRoot(ElementName = "updates")]
    public class Updates
    {
        [XmlElement(ElementName = "update")]
        public List<Update> Update { get; set; }

        [XmlAttribute(AttributeName = "type")]
        public String Type { get; set; }
    }
}
