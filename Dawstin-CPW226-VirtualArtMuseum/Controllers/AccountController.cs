using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Dawstin_CPW226_VirtualArtMuseum.Models;
using Microsoft.AspNetCore.Authorization;

namespace Dawstin_CPW226_VirtualArtMuseum.Controllers
{
    /// <summary>
    /// Manages user authentication, registration, and account-related actions.
    /// </summary>
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="AccountController"/> class.
        /// </summary>
        /// <param name="userManager">Manages user identity and registration.</param>
        /// <param name="signInManager">Handles user sign-in and sign-out operations.</param>
        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
        }

        /// <summary>
        /// Displays the login page.
        /// </summary>
        /// <returns>The login view.</returns>
        [AllowAnonymous]
        [HttpGet]
        public IActionResult Login(string returnUrl = "/")
        {
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }

        /// <summary>
        /// Processes login attempts and redirects users based on their assigned role.
        /// </summary>
        /// <param name="model">The login form data submitted by the user.</param>
        /// <returns>A redirect to the appropriate dashboard or an error view.</returns>
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

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

        /// <summary>
        /// Displays the registration page.
        /// </summary>
        /// <returns>The registration view.</returns>
        [AllowAnonymous]
        [HttpGet]
        public IActionResult Register() => View();

        /// <summary>
        /// Registers a new user and assigns them the Artist role.
        /// </summary>
        /// <param name="model">The registration form data submitted by the user.</param>
        /// <returns>Redirects to the login page if successful; otherwise, displays the registration view.</returns>
        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            var user = new ApplicationUser
            {
                UserName = model.Username,
                Email = model.Email
            };

            var result = await userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                await userManager.AddToRoleAsync(user, "Artist");
                return RedirectToAction("Login");
            }

            foreach (var error in result.Errors)
                ModelState.AddModelError("", error.Description);

            return View(model);
        }

        /// <summary>
        /// Logs out the currently authenticated user.
        /// </summary>
        /// <returns>Redirects to the home page after logout.</returns>
        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        /// <summary>
        /// Displays the Forgot Password page where users can request a password reset link.
        /// </summary>
        /// <returns>The ForgotPassword view.</returns>
        [HttpGet]
        public IActionResult ForgotPassword()
        {
            return View();
        }

        /// <summary>
        /// Processes the Forgot Password form submission and displays a confirmation message.
        /// </summary>
        /// <param name="model">The user's input containing their email address.</param>
        /// <returns>
        /// The ForgotPassword view with a message indicating whether a reset link was sent,
        /// regardless of email validity, to protect user privacy.
        /// </returns>
        [HttpPost]
        public IActionResult ForgotPassword(ForgotPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                ViewData["Message"] = "If your email exists, a reset link has been sent.";
            }

            return View(model);
        }
    }
}