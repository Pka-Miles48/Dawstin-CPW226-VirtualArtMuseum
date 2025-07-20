using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Dawstin_CPW226_VirtualArtMuseum.Models;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;

namespace Dawstin_CPW226_VirtualArtMuseum.VirtualArt_Data
{
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