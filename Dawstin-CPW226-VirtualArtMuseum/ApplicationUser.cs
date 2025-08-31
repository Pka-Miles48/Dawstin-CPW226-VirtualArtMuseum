using Microsoft.AspNetCore.Identity;

namespace Dawstin_CPW226_VirtualArtMuseum.Models
{
    /// <summary>
    /// Extended Identity user class for the Virtual Art Museum application.
    /// Includes custom properties to enhance user profiles beyond default authentication.
    /// </summary>
    public class ApplicationUser : IdentityUser
    {
        /// <summary>
        /// User's first name.
        /// </summary>
        public string? FirstName { get; set; }

        /// <summary>
        /// User's last name.
        /// </summary>
        public string? LastName { get; set; }

        /// <summary>
        /// Optional biography or user profile text.
        /// Useful for artists submitting work.
        /// </summary>
        public string? Bio { get; set; }

        /// <summary>
        /// Profile picture URL (optional).
        /// </summary>
        public string? ProfileImageUrl { get; set; }
    }
}