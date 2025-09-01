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
        /// Retrieves all artworks with a <c>Status</c> of "Approved" to ensure only
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
        /// <param name="id">The ID of the artwork to display.</param>
        /// <returns>The details view for the selected artwork, or a 404 if not found.</returns>
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

        /// <summary>
        /// Displays the admin-only review dashboard showing all artwork submissions
        /// currently marked as "UnderReview".
        /// </summary>
        /// <returns>A view populated with pending artworks for admin review.</returns>
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> ReviewDashboard()
        {
            var pendingArtworks = await _context.Artworks
                .Where(a => a.Status == "UnderReview")
                .Include(a => a.Artist)
                .ToListAsync();

            return View(pendingArtworks);
        }

        // Admin POST: approve a submission
        /// <summary>
        /// Approves an artwork submission and updates its status.
        /// </summary>
        /// <param name="id">The ID of the artwork to approve.</param>
        /// <returns>Redirects to the review dashboard.</returns>
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Approve(int id)
        {
            var art = await _context.Artworks.FindAsync(id);
            if (art != null)
            {
                art.Status = "Approved";
                await _context.SaveChangesAsync();
            }
            return RedirectToAction("ReviewDashboard");
        }

        // Admin POST: reject a submission
        /// <summary>
        /// Processes an admin rejection of an artwork submission.
        /// </summary>
        /// <param name="id">The ID of the artwork being rejected.</param>
        /// <param name="feedbackNote">Optional curator feedback explaining the rejection.</param>
        /// <returns>
        /// Redirects to the review dashboard after updating the artwork's status and saving feedback.
        /// </returns>
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Reject(int id, string feedbackNote)
        {
            var art = await _context.Artworks.FindAsync(id);
            if (art != null)
            {
                art.Status = "Rejected";
                art.FeedbackNote = feedbackNote;
                await _context.SaveChangesAsync();
            }
            return RedirectToAction("ReviewDashboard");
        }
    }
}