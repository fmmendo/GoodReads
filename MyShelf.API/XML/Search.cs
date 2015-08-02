using System;
using System.Xml.Serialization;

namespace MyShelf.API.XML
{
    [XmlRoot(ElementName = "search")]
    public class Search
    {
        [XmlElement(ElementName = "query")]
        public string Query { get; set; }
        
        [XmlElement(ElementName = "results-start")]
        public string ResultsStart { get; set; }
        
        [XmlElement(ElementName = "results-end")]
        public string ResultsEnd { get; set; }
        
        [XmlElement(ElementName = "total-results")]
        public string TotalResults { get; set; }
        
        [XmlElement(ElementName = "source")]
        public string Source { get; set; }
        
        [XmlElement(ElementName = "query-time-seconds")]
        public string QueryTimeSeconds { get; set; }
        
        //[XmlElement(ElementName = "results")]
        //public Results Results { get; set; }
    }
}
