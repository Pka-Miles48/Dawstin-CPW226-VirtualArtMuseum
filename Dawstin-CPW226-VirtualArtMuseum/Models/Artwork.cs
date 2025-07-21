namespace Dawstin_CPW226_VirtualArtMuseum.Models
{
    /// <summary>
    /// Represents an individual artwork in the museum.
    /// Includes metadata such as title, medium, description, image, and relational links to artist and collection.
    /// </summary>
    public class Artwork
    {
        /// <summary>
        /// Primary key for the Artwork entity.
        /// </summary>
        public int ArtworkId { get; set; }

        /// <summary>
        /// Title of the artwork.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Medium used for the artwork (e.g. oil, digital, watercolor).
        /// </summary>
        public string Medium { get; set; }

        /// <summary>
        /// Detailed description or commentary about the artwork.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// URL to a hosted image of the artwork.
        /// </summary>
        public string ImageUrl { get; set; }

        /// <summary>
        /// Date the artwork was created or submitted.
        /// </summary>
        public DateTime CreatedDate { get; set; }

        /// <summary>
        /// Foreign key reference to the artist who created the artwork.
        /// </summary>
        public int ArtistId { get; set; }

        /// <summary>
        /// The artist who created this artwork.
        /// </summary>
        public Artist Artist { get; set; }

        /// <summary>
        /// Foreign key reference to the collection this artwork belongs to.
        /// </summary>
        public int CollectionId { get; set; }

        /// <summary>
        /// The collection that features this artwork.
        /// </summary>
        public Collection Collection { get; set; }

        /// <summary>
        /// Indicates the current review state of the artwork submission.
        /// Internal setter restricts updates to within the application logic (e.g., admins).
        /// Possible values: "UnderReview", "Approved", "Rejected".
        /// </summary>
        public string Status { get; internal set; }
    }
}