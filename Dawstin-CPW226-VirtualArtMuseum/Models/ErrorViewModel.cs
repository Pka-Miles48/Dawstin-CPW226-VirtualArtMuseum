namespace Dawstin_CPW226_VirtualArtMuseum.Models
{
    /// <summary>
    /// Holds error details to display when something goes wrong in the app.
    /// </summary>
    public class ErrorViewModel
    {
        /// <summary>
        /// A unique ID for the current request, used to help track and debug errors.
        /// </summary>
        public string? RequestId { get; set; }

        /// <summary>
        /// Tells the view whether to show the Request ID.
        /// </summary>
        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }
}