using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace MyShelf.API.XML
{
    [XmlRoot(ElementName = "series")]
    public class Series
    {
        [XmlElement(ElementName = "id")]
        public String Id { get; set; }
        
        [XmlElement(ElementName = "title")]
        public String Title { get; set; }
        
        [XmlElement(ElementName = "description")]
        public String Description { get; set; }
        
        [XmlElement(ElementName = "note")]
        public String Note { get; set; }
        
        [XmlElement(ElementName = "series_works_count")]
        public String Series_works_count { get; set; }
        
        [XmlElement(ElementName = "primary_work_count")]
        public String Primary_work_count { get; set; }
        
        [XmlElement(ElementName = "numbered")]
        public String Numbered { get; set; }
    }
}
