using System.ComponentModel.DataAnnotations;

namespace Dawstin_CPW226_VirtualArtMuseum.Models
{
    /// <summary>
    /// ViewModel for initiating a password reset request.
    /// </summary>
    public class ForgotPasswordViewModel
    {
        /// <summary>
        /// The email address associated with the user's account.
        /// </summary>
        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Please enter a valid email address.")]
        public string Email { get; set; }
    }
}