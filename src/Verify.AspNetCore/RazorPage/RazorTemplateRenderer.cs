using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Routing;

namespace Westerhoff.AspNetCore.TemplateRendering
{
    /// <summary>
    /// Razor template renderer, using the existing view engine.
    /// </summary>
    public class RazorTemplateRenderer
    {
        IRazorViewEngine viewEngine;
        IServiceProvider services;
        ITempDataDictionaryFactory tempDataFactory;

        /// <summary>
        /// Create a razor template renderer.
        /// </summary>
        /// <param name="viewEngine">Razor view engine.</param>
        /// <param name="services">Service provider.</param>
        /// <param name="tempDataFactory">Temp data dictionary factory.</param>
        public RazorTemplateRenderer(IRazorViewEngine viewEngine, IServiceProvider services, ITempDataDictionaryFactory tempDataFactory)
        {
            this.viewEngine = viewEngine;
            this.services = services;
            this.tempDataFactory = tempDataFactory;
        }

        /// <inheritdoc/>
        public async Task<RazorTemplateRenderResult> RenderAsync(string viewPath, PageModel model)
        {
            // find view
            var viewResult = viewEngine.GetView(executingFilePath: null, viewPath: viewPath, isMainPage: true);
            if (!viewResult.Success)
                throw new ArgumentException("View could not be found", nameof(viewPath));

            // prepare context
            var viewData = new ViewDataDictionary(new EmptyModelMetadataProvider(), new ModelStateDictionary())
            {
                Model = model
            };
            var httpContext = new DefaultHttpContext
            {
                RequestServices = services
            };
            var actionContext = new ActionContext(httpContext, new RouteData(), new ActionDescriptor());

            // render view to string
            using var stringWriter = new StringWriter();
            var viewContext = new ViewContext(
                actionContext: actionContext,
                view: viewResult.View,
                viewData: viewData,
                tempData: tempDataFactory.GetTempData(httpContext),
                writer: stringWriter,
                htmlHelperOptions: new HtmlHelperOptions());

            await viewResult.View.RenderAsync(viewContext);

            return new RazorTemplateRenderResult
            {
                Title = viewContext.ViewData["Title"]?.ToString()!,
                Body = stringWriter.ToString(),
            };
        }

        /// <inheritdoc/>
        public async Task<string> RenderStringAsync(string viewPath, PageModel model)
            => (await RenderAsync(viewPath, model)).Body;
    }
}
