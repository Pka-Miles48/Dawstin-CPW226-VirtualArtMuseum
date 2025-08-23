using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Dawstin_CPW226_VirtualArtMuseum.Models;

namespace Dawstin_CPW226_VirtualArtMuseum.Controllers
{
    /// <summary>
    /// Handles user account actions such as login and logout.
    /// </summary>
    public class AccountController : Controller
    {
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly UserManager<ApplicationUser> userManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="AccountController"/> class.
        /// </summary>
        /// <param name="signInManager">Manages user sign-in operations.</param>
        /// <param name="userManager">Manages user identity and roles.</param>
        public AccountController(SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager)
        {
            this.signInManager = signInManager;
            this.userManager = userManager;
        }

        /// <summary>
        /// Processes login attempts and redirects users based on their assigned role.
        /// </summary>
        /// <param name="model">The login form data submitted by the user.</param>
        /// <returns>A redirect to the appropriate dashboard or an error view.</returns>
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            var result = await signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, false);

            if (result.Succeeded)
            {
                var user = await userManager.FindByEmailAsync(model.Email);

                if (await userManager.IsInRoleAsync(user, "Admin"))
                    return RedirectToAction("ReviewDashboard", "Artworks");

                if (await userManager.IsInRoleAsync(user, "Artist"))
                    return RedirectToAction("Index", "Artworks");

                return RedirectToAction("Index", "Home");
            }

            ModelState.AddModelError("", "Invalid login attempt.");
            return View(model);
        }
    }
}