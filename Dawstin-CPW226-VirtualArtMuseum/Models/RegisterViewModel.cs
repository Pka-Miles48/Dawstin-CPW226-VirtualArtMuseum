using System.ComponentModel.DataAnnotations;

namespace Dawstin_CPW226_VirtualArtMuseum.Models
{
    /// <summary>
    /// ViewModel for user registration, capturing essential credentials.
    /// </summary>
    public class RegisterViewModel
    {
        /// <summary>
        /// The user's email address. Must be a valid email format.
        /// </summary>
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        /// <summary>
        /// The username chosen by the user during registration.
        /// This will be used for display and login identification.
        /// </summary>
        [Required(ErrorMessage = "Username is required.")]
        [StringLength(20, ErrorMessage = "Username must be between 4 and 20 characters.", MinimumLength = 4)]
        [Display(Name = "Username")]
        public string Username { get; set; }

        /// <summary>
        /// The user's chosen password. Required and treated as a secure input.
        /// </summary>
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        /// <summary>
        /// Confirmation of the user's password. Must match the original password.
        /// </summary>
        [DataType(DataType.Password)]
        [Compare("Password")]
        public string ConfirmPassword { get; set; }
    }
}