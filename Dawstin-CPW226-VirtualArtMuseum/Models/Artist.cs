using System;

namespace Dawstin_CPW226_VirtualArtMuseum.Models
{
    /// <summary>
    /// Represents an artist featured in the art museum.
    /// Stores biography and associated artworks.
    /// </summary>
    public class Artist
    {
        /// <summary>
        /// Primary key for the Artist entity.
        /// </summary>
        public int ArtistId { get; set; }

        /// <summary>
        /// Full name of the artist.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Biographical details or background of the artist.
        /// </summary>
        public string Bio { get; set; }

        /// <summary>
        /// Collection of artworks submitted by the artist.
        /// </summary>
        public ICollection<Artwork> Artworks { get; set; }
    }
}
