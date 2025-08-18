using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Dawstin_CPW226_VirtualArtMuseum.Models;

namespace Dawstin_CPW226_VirtualArtMuseum.Controllers
{
    /// <summary>
    /// Controller for administrative actions, including user role assignment.
    /// Accessible only to users in the "Admin" role.
    /// </summary>
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="AdminController"/> class.
        /// </summary>
        /// <param name="userManager">The user manager for accessing and modifying user data.</param>
        public AdminController(UserManager<ApplicationUser> userManager)
        {
            this.userManager = userManager;
        }

        /// <summary>
        /// Displays a list of all users for role assignment.
        /// </summary>
        /// <returns>A view showing all registered users.</returns>
        public async Task<IActionResult> AssignRoles()
        {
            var users = userManager.Users.ToList();
            return View(users);
        }

        /// <summary>
        /// Assigns a new role to a specified user, replacing any existing roles.
        /// </summary>
        /// <param name="userId">The ID of the user to modify.</param>
        /// <param name="role">The role to assign to the user.</param>
        /// <returns>Redirects to the AssignRoles view after updating.</returns>
        [HttpPost]
        public async Task<IActionResult> AssignRole(string userId, string role)
        {
            var user = await userManager.FindByIdAsync(userId);
            var currentRoles = await userManager.GetRolesAsync(user);
            await userManager.RemoveFromRolesAsync(user, currentRoles);
            await userManager.AddToRoleAsync(user, role);
            return RedirectToAction("AssignRoles");
        }
    }
}