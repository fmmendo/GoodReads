using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace GoodReads.API.Model
{
    [XmlRoot(ElementName = "group_resource")]
    public class GroupResource
    {
        [XmlElement(ElementName = "read_status")]
        public ReadStatus Read_status { get; set; }
        
        [XmlElement(ElementName = "friend")]
        public Friend Friend { get; set; }
        
        [XmlElement(ElementName = "poll_vote")]
        public PollVote Poll_vote { get; set; }
        
        [XmlElement(ElementName = "recommendation")]
        public Recommendation Recommendation { get; set; }
        
        [XmlElement(ElementName = "user_challenge")]
        public UserChallenge User_challenge { get; set; }
        
        [XmlElement(ElementName = "user_status")]
        public UserStatus User_status { get; set; }
    }
}
