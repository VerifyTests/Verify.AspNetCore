namespace Westerhoff.AspNetCore.TemplateRendering
{
    /// <summary>
    /// Razor template render options.
    /// </summary>
    public class RazorTemplateRenderOptions
    {
        /// <summary>
        /// Base path.
        /// </summary>
        public string BasePath
        { get; set; } = "/Views";
    }
}
