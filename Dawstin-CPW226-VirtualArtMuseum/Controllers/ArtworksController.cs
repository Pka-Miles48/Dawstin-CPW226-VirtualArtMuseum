using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Dawstin_CPW226_VirtualArtMuseum.VirtualArt_Data;
using Dawstin_CPW226_VirtualArtMuseum.Models;
using System.Threading.Tasks;
using System.Linq;

namespace Dawstin_CPW226_VirtualArtMuseum.Controllers
{
    /// <summary>
    /// Handles interactions for submitting, viewing, and managing artwork entries.
    /// </summary>
    [Authorize]
    public class ArtworksController : Controller
    {
        private readonly VirtualArtMuseum _context;

        public ArtworksController(VirtualArtMuseum context)
        {
            _context = context;
        }

        /// <summary>
        /// Displays the public gallery of artworks.
        /// </summary>
        /// <remarks>
        /// Retrieves all artworks with an <c>Status</c> of "Approved" to ensure only
        /// reviewed and published submissions appear in the public-facing view.
        /// Includes related <c>Artist</c> and <c>Collection</c> navigation properties
        /// for display purposes.
        /// </remarks>
        /// <returns>A view showing approved artworks in the public gallery.</returns>
        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            var approvedArtworks = await _context.Artworks
                .Where(a => a.Status == "Approved")
                .Include(a => a.Artist)
                .Include(a => a.Collection)
                .ToListAsync();

            return View(approvedArtworks);
        }


        /// <summary>
        /// Returns the submission form for artists to create a new artwork entry.
        /// </summary>
        [Authorize(Roles = "Artist")]
        public IActionResult Create()
        {
            return View();
        }

        /// <summary>
        /// Handles submitted artwork data and saves it to the database.
        /// </summary>
        [Authorize(Roles = "Artist")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Artwork artwork)
        {
            if (ModelState.IsValid)
            {
                artwork.CreatedDate = DateTime.Now;
                artwork.Status = "UnderReview"; // Default for new submissions

                _context.Add(artwork);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(artwork);
        }

        /// <summary>
        /// Shows detailed info for a single artwork entry.
        /// </summary>
        [AllowAnonymous]
        public async Task<IActionResult> Details(int id)
        {
            var artwork = await _context.Artworks
                .Include(a => a.Artist)
                .Include(a => a.Collection)
                .FirstOrDefaultAsync(m => m.ArtworkId == id && m.Status == "Approved");

            if (artwork == null)
                return NotFound();

            return View(artwork);
        }

        // You can add Edit, Delete, and Admin-specific review actions later!
    }
}
