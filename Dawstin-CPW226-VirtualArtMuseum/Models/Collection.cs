namespace Dawstin_CPW226_VirtualArtMuseum.Models
{
    /// <summary>
    /// Represents a themed collection of artworks curated for the museum.
    /// Includes a title and optional theme description.
    /// </summary>
    public class Collection
    {
        /// <summary>
        /// Primary key for the Collection entity.
        /// </summary>
        public int CollectionId { get; set; }

        /// <summary>
        /// Title of the collection (e.g. “Modern Abstracts”).
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Thematic description or focus of the collection.
        /// </summary>
        public string Theme { get; set; }

        /// <summary>
        /// Artworks included in this collection.
        /// </summary>
        public ICollection<Artwork> Artworks { get; set; }
    }
}