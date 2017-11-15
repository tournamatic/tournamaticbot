using System;
using System.Collections.Generic;

namespace TournamaticBot
{
    public enum AttachmentType
    {
        //TODO: what type of file extensions are valid?

        Unknown = 0,
        Image = 1,
        Icon = 2,
        TileImage = 3,
        FeaturedTileImage = 4,
        Video = 5,

        /// <summary>
        /// .txt, .PDF, .doc
        /// </summary>
        Document = 6,
        Zip = 7,

        /// <summary>
        /// A hyper-link.
        /// </summary>
        Url = 8,

        /// <summary>
        /// Google map link.
        /// <example>
        /// google.com/maps/@[lat],[lon],[zoom-level]z
        /// google.com/maps?q=[lat],[lon] (shows the red marker)
        /// </example>
        /// </summary>
        Map = 9
    }

    public class AttachmentSimple
    {
        public int AttachmentId { get; set; }
        public int DisplayOrder { get; set; }
        public AttachmentType Type { get; set; }
        public string AttachmentTitle { get; set; }
        public string Link { get; set; }
        public string ContentType { get; set; }
    }

    public class Tournament
    {
        public int TournamentId { get; set; }
        public bool IsPublished { get; set; }
        public string OwnerId { get; set; }
        public string OwnerName { get; set; }
        public bool IsFeatured { get; set; }
        public bool HasMatchPlan { get; set; }
        public int CategoryId { get; set; }
        public string CategoryTitle { get; set; }
        public string Title { get; set; }
        public string HashTag { get; set; }
        public string TwitterHandle { get; set; }
        public bool ShowSocialNetworksEvent { get; set; }
        public string ShortDescription { get; set; }
        public string Description { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public DateTime RegistrationDeadline { get; set; }
        public DateTime? WithdrawalDeadline { get; set; }
        public string Location { get; set; }
        public string TimeZoneId { get; set; }
        public DateTime ModificationDate { get; set; }

        public string ImageUrl
        {
            get
            {
                var imageUrl = string.Empty;
                if (Attachments != null && Attachments.Count > 0)
                {
                    for (var i = Attachments.Count - 1; i >= 0; i--)
                    {
                        var atta = Attachments[i];
                        // If the attachment is a tile image.
                        if (atta.Type == AttachmentType.Unknown || atta.Type == AttachmentType.TileImage)
                        {
                            if (!string.IsNullOrEmpty(atta.Link))
                            {
                                imageUrl = "https://tournamatic.com/" + atta.Link.Replace("\\", "/");
                            }
                        }
                    }
                }
                if (string.IsNullOrEmpty(imageUrl))
                {
                    var category = CategoryTitle;
                    if (!string.IsNullOrEmpty(category))
                    {
                        imageUrl = "https://tournamatic.com/Content/img/stock/" + category.Replace(" ", "-") + ".s.jpg";
                    }
                    else
                    {
                        imageUrl = "https://placehold.it/300x200";
                    }
                }
                return imageUrl;
            }
        }

        public virtual IList<AttachmentSimple> Attachments { get; set; }
    }
}