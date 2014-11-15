using System;
using System.Xml.Serialization;

namespace GoodReads.API.Model
{
    [XmlRoot(ElementName = "series_work")]
    public class SeriesWork
    {
        [XmlElement(ElementName = "id")]
        public String Id { get; set; }

        [XmlElement(ElementName = "user_position")]
        public String User_position { get; set; }

        [XmlElement(ElementName = "series")]
        public Series Series { get; set; }
    }
}
