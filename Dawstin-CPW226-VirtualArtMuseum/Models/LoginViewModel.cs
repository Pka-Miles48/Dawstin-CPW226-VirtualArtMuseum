namespace Dawstin_CPW226_VirtualArtMuseum.Models
{
    /// <summary>
    /// Holds the login form data submitted by a user.
    /// </summary>
    public class LoginViewModel
    {
        /// <summary>
        /// The email address the user enters to log in.
        /// </summary>
        public string Email { get; internal set; }

        /// <summary>
        /// The password the user enters to log in.
        /// </summary>
        public string Password { get; internal set; }

        /// <summary>
        /// Whether the user wants to stay logged in after closing the browser.
        /// </summary>
        public bool RememberMe { get; internal set; }
    }
}