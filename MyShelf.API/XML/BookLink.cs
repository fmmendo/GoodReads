using System;
using System.Xml.Serialization;
using System.Collections.Generic;

namespace MyShelf.API.XML
{
    [XmlRoot(ElementName = "book_link")]
    public class BookLink
    {
        [XmlElement(ElementName = "id")]
        public string Id { get; set; }

        [XmlElement(ElementName = "name")]
        public string Name { get; set; }

        [XmlElement(ElementName = "link")]
        public string Link { get; set; }
    }
}
