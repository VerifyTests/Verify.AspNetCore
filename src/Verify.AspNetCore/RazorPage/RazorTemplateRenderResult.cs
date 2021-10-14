namespace Westerhoff.AspNetCore.TemplateRendering
{
    /// <summary>
    /// Razor template render result.
    /// </summary>
    public class RazorTemplateRenderResult
    {
        /// <summary>
        /// Title view data set within the Razor view (<c>ViewData["Title"])</c>).
        /// </summary>
        public string Title { get; set; } = null!;

        /// <summary>
        /// Rendered view body.
        /// </summary>
        public string Body { get; set; } = null!;
    }
}
