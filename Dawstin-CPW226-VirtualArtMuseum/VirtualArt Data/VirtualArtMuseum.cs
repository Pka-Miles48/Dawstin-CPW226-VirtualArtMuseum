using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Dawstin_CPW226_VirtualArtMuseum.Models;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;

namespace Dawstin_CPW226_VirtualArtMuseum.VirtualArt_Data
{
    // ApplicationUser.cs
    /// <summary>
    /// Extended Identity user class for the Virtual Art Museum application.
    /// Includes custom properties to enhance user profiles beyond default authentication.
    /// </summary>
    public class ApplicationUser : IdentityUser
    {
        /// <summary>
        /// User's first name.
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// User's last name.
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// Optional biography or user profile text.
        /// Useful for artists submitting work.
        /// </summary>
        public string Bio { get; set; }

        /// <summary>
        /// Profile picture URL (optional).
        /// </summary>
        public string ProfileImageUrl { get; set; }
    }

    /// <summary>
    /// Virtual Art Museum database context configured for Entity Framework Core.
    /// Defines DbSets for Artworks, Artists, and Collections.
    /// </summary>
    public class VirtualArtMuseum : IdentityDbContext<ApplicationUser>
    {
        /// <summary>
        /// Initializes a new instance of the VirtualArtMuseum database context.
        /// </summary>
        /// <param name="options">The options for configuring the database context.</param>
        public VirtualArtMuseum(DbContextOptions<VirtualArtMuseum> options) : base(options) { }

        /// <summary>
        /// Database table for artwork entries.
        /// </summary>
        public DbSet<Artwork> Artworks { get; set; }

        /// <summary>
        /// Database table for featured artists.
        /// </summary>
        public DbSet<Artist> Artists { get; set; }

        /// <summary>
        /// Database table for artwork collections.
        /// </summary>
        public DbSet<Collection> Collections { get; set; }
    }
}