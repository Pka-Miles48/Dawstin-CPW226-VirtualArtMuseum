namespace Dawstin_CPW226_VirtualArtMuseum.Models
{
    /// <summary>
    /// Represents the view model for managing user account settings, such as password updates.
    /// </summary>
    public class ManageAccountViewModel
    {
        /// <summary>
        /// Gets or sets the email address associated with the user's account.
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets the new password chosen by the user.
        /// </summary>
        public string NewPassword { get; set; }

        /// <summary>
        /// Gets or sets the confirmation of the new password to ensure accuracy.
        /// </summary>
        public string ConfirmPassword { get; set; }
    }
}
