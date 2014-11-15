using System.Collections.Generic;
using System.Xml.Serialization;

namespace GoodReads.API.Model
{
    [XmlRoot(ElementName = "book_links")]
    public class BookLinks
    {
        [XmlElement(ElementName = "book_link")]
        public List<BookLink> Book_link { get; set; }
    }
}
