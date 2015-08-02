using System;
using System.Xml.Serialization;

namespace MyShelf.API.XML
{
    [XmlRoot(ElementName = "review")]
    public class Review
    {
        [XmlElement(ElementName = "book-id")]
        public string Bookid { get; set; }

        [XmlElement(ElementName = "created-at")]
        public string Createdat { get; set; }

        [XmlElement(ElementName = "hidden-flag")]
        public string Hiddenflag { get; set; }

        [XmlElement(ElementName = "id")]
        public string Id { get; set; }

        [XmlElement(ElementName = "language-code")]
        public string LanguageCode { get; set; }

        [XmlElement(ElementName = "last-comment-at")]
        public string LastCommentAt { get; set; }

        [XmlElement(ElementName = "last-revision-at")]
        public string LastRevisionAt { get; set; }

        [XmlElement(ElementName = "non-friends-rating-count")]
        public string NonFriendsRatingCount { get; set; }

        //[XmlElement(ElementName = "notes")]
        //public Notes Notes { get; set; }

        [XmlElement(ElementName = "votes")]
        public string Votes { get; set; }

        [XmlElement(ElementName = "rating")]
        public string Rating { get; set; }

        [XmlElement(ElementName = "ratings-count")]
        public string RatingsCount { get; set; }

        [XmlElement(ElementName = "ratings-sum")]
        public string RatingsSum { get; set; }

        [XmlElement(ElementName = "read-at")]
        public string ReadAt { get; set; }

        [XmlElement(ElementName = "read-count")]
        public string ReadCount { get; set; }

        [XmlElement(ElementName = "read-status")]
        public string ReadStatus { get; set; }

        //[XmlElement(ElementName = "recommendation")]
        //public Recommendation Recommendation { get; set; }

        [XmlElement(ElementName = "recommender-user-id1")]
        public string RecommenderUserId1 { get; set; }

        [XmlElement(ElementName = "recommender-user-name1")]
        public string RecommenderUsername1 { get; set; }

        //[XmlElement(ElementName = "review")]
        //public Review GrReview { get; set; }

        [XmlElement(ElementName = "sell-flag")]
        public string SellFlag { get; set; }

        [XmlElement(ElementName = "spoiler-flag")]
        public string SpoilerFlag { get; set; }

        [XmlElement(ElementName = "started-at")]
        public string StartedAt { get; set; }

        [XmlElement(ElementName = "updated-at")]
        public DateTime UpdatedAt { get; set; }

        [XmlElement(ElementName = "user-id")]
        public string Userid { get; set; }

        [XmlElement(ElementName = "weight")]
        public string Weight { get; set; }

        [XmlElement(ElementName = "work-id")]
        public string Workid { get; set; }

        [XmlElement(ElementName = "book")]
        public Book Book { get; set; }

        [XmlElement(ElementName = "spoilers_state")]
        public string SpoilersState { get; set; }

        [XmlElement(ElementName = "shelves")]
        public Shelves Shelves { get; set; }

        [XmlElement(ElementName = "recommended_for")]
        public string RecommendedFor { get; set; }

        [XmlElement(ElementName = "recommended_by")]
        public string RecommendedBy { get; set; }

        [XmlElement(ElementName = "date_added")]
        public string DateAdded { get; set; }

        [XmlElement(ElementName = "date_updated")]
        public string DateUpdated { get; set; }

        [XmlElement(ElementName = "body")]
        public string Body { get; set; }

        [XmlElement(ElementName = "comments_count")]
        public string CommentsCount { get; set; }

        [XmlElement(ElementName = "url")]
        public string Url { get; set; }

        [XmlElement(ElementName = "link")]
        public string Link { get; set; }

        [XmlElement(ElementName = "owned")]
        public string Owned { get; set; }

        [XmlElement(ElementName = "user")]
        public User User { get; set; }
    }
}