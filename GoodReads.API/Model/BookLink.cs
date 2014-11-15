using System;
using System.Xml.Serialization;
using System.Collections.Generic;

namespace GoodReads.API.Model
{
    [XmlRoot(ElementName = "book_link")]
    public class BookLink
    {
        [XmlElement(ElementName = "id")]
        public String Id { get; set; }

        [XmlElement(ElementName = "name")]
        public String Name { get; set; }

        [XmlElement(ElementName = "link")]
        public String Link { get; set; }
    }
}
