using System;
using System.Xml.Serialization;

namespace GoodReads.API.Model
{
    [XmlRoot(ElementName = "review")]
    public class Review
    {
        [XmlElement(ElementName = "book-id")]
        public String Bookid { get; set; }

        [XmlElement(ElementName = "created-at")]
        public String Createdat { get; set; }

        [XmlElement(ElementName = "hidden-flag")]
        public String Hiddenflag { get; set; }

        [XmlElement(ElementName = "id")]
        public String Id { get; set; }

        [XmlElement(ElementName = "language-code")]
        public String LanguageCode { get; set; }

        [XmlElement(ElementName = "last-comment-at")]
        public String LastCommentAt { get; set; }

        [XmlElement(ElementName = "last-revision-at")]
        public String LastRevisionAt { get; set; }

        [XmlElement(ElementName = "non-friends-rating-count")]
        public String NonFriendsRatingCount { get; set; }

        //[XmlElement(ElementName = "notes")]
        //public Notes Notes { get; set; }

        [XmlElement(ElementName = "votes")]
        public String Votes { get; set; }

        [XmlElement(ElementName = "rating")]
        public String Rating { get; set; }

        [XmlElement(ElementName = "ratings-count")]
        public String RatingsCount { get; set; }

        [XmlElement(ElementName = "ratings-sum")]
        public String RatingsSum { get; set; }

        [XmlElement(ElementName = "read-at")]
        public String ReadAt { get; set; }

        [XmlElement(ElementName = "read-count")]
        public String ReadCount { get; set; }

        [XmlElement(ElementName = "read-status")]
        public String ReadStatus { get; set; }

        //[XmlElement(ElementName = "recommendation")]
        //public Recommendation Recommendation { get; set; }

        [XmlElement(ElementName = "recommender-user-id1")]
        public String RecommenderUserId1 { get; set; }

        [XmlElement(ElementName = "recommender-user-name1")]
        public String RecommenderUsername1 { get; set; }

        //[XmlElement(ElementName = "review")]
        //public Review GrReview { get; set; }

        [XmlElement(ElementName = "sell-flag")]
        public String SellFlag { get; set; }

        [XmlElement(ElementName = "spoiler-flag")]
        public String SpoilerFlag { get; set; }

        [XmlElement(ElementName = "started-at")]
        public String StartedAt { get; set; }

        [XmlElement(ElementName = "updated-at")]
        public DateTime UpdatedAt { get; set; }

        [XmlElement(ElementName = "user-id")]
        public String Userid { get; set; }

        [XmlElement(ElementName = "weight")]
        public String Weight { get; set; }

        [XmlElement(ElementName = "work-id")]
        public String Workid { get; set; }

        [XmlElement(ElementName = "book")]
        public Book Book { get; set; }

        [XmlElement(ElementName = "spoilers_state")]
        public String SpoilersState { get; set; }

        [XmlElement(ElementName = "shelves")]
        public Shelves Shelves { get; set; }

        [XmlElement(ElementName = "recommended_for")]
        public String RecommendedFor { get; set; }

        [XmlElement(ElementName = "recommended_by")]
        public String RecommendedBy { get; set; }

        [XmlElement(ElementName = "date_added")]
        public String DateAdded { get; set; }

        [XmlElement(ElementName = "date_updated")]
        public String DateUpdated { get; set; }

        [XmlElement(ElementName = "body")]
        public String Body { get; set; }

        [XmlElement(ElementName = "comments_count")]
        public String CommentsCount { get; set; }

        [XmlElement(ElementName = "url")]
        public String Url { get; set; }

        [XmlElement(ElementName = "link")]
        public String Link { get; set; }

        [XmlElement(ElementName = "owned")]
        public String Owned { get; set; }

        [XmlElement(ElementName = "user")]
        public User User { get; set; }
    }
}