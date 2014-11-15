using System;
using System.Xml.Serialization;

namespace GoodReads.API.Model
{
    [XmlRoot(ElementName = "search")]
    public class Search
    {
        [XmlElement(ElementName = "query")]
        public String Query { get; set; }
        
        [XmlElement(ElementName = "results-start")]
        public String ResultsStart { get; set; }
        
        [XmlElement(ElementName = "results-end")]
        public String ResultsEnd { get; set; }
        
        [XmlElement(ElementName = "total-results")]
        public String TotalResults { get; set; }
        
        [XmlElement(ElementName = "source")]
        public String Source { get; set; }
        
        [XmlElement(ElementName = "query-time-seconds")]
        public String QueryTimeSeconds { get; set; }
        
        [XmlElement(ElementName = "results")]
        public Results Results { get; set; }
    }
}
