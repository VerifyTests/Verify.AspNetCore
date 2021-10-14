namespace Westerhoff.AspNetCore.TemplateRendering
{
    /// <summary>
    /// Razor template renderer.
    /// </summary>
    public interface IRazorTemplateRenderer
    {
        /// <summary>
        /// Render a Razor view.
        /// </summary>
        /// <param name="viewPath">Path to the view.</param>
        /// <param name="model">Model passed to the view.</param>
        /// <returns>Render result.</returns>
        Task<RazorTemplateRenderResult> RenderAsync(string viewPath, object model);

        /// <summary>
        /// Render a Razor view as a string.
        /// </summary>
        /// <param name="viewPath">Path to the view.</param>
        /// <param name="model">Model passed to the view.</param>
        /// <returns>Rendered view body.</returns>
        Task<string> RenderStringAsync(string viewPath, object model);
    }
}
