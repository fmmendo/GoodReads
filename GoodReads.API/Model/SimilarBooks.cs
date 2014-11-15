using System.Collections.Generic;
using System.Xml.Serialization;

namespace GoodReads.API.Model
{
    [XmlRoot(ElementName = "similar_books")]
    public class SimilarBooks
    {
        [XmlElement(ElementName = "book")]
        public List<Book> Book { get; set; }
    }
}
