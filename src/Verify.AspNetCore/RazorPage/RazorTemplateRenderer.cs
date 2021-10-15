using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

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
        public RazorTemplateRenderer(IRazorViewEngine viewEngine, IServiceProvider services,
            ITempDataDictionaryFactory tempDataFactory)
        {
            this.viewEngine = viewEngine;
            this.services = services;
            this.tempDataFactory = tempDataFactory;
        }

        public async Task<RazorTemplateRenderResult> RenderAsync<T>(string viewPath, T model)
            where T : PageModel
        {
            var viewResult = FindView(viewPath);

            await using var writer = new StringWriter();
            var metadataProvider = new EmptyModelMetadataProvider();
            model.MetadataProvider = metadataProvider;
            var viewData = new ViewDataDictionary<T>(metadataProvider, new())
            {
                Model = model
            };
            var httpContext = new DefaultHttpContext
            {
                RequestServices = services
            };
            var actionContext = new ActionContext(httpContext, new(), new());
            var pageContext = new PageContext(actionContext)
            {
                ViewData = viewData
            };
            model.PageContext = pageContext;
            var tempData = tempDataFactory.GetTempData(httpContext);
            model.TempData = tempData;
            var viewContext = new ViewContext(
                actionContext: actionContext,
                view: viewResult.View,
                viewData: viewData,
                tempData: tempData,
                writer: writer,
                htmlHelperOptions: new());

            if (viewResult.View is RazorView {RazorPage: PageBase pageBase})
            {
                pageBase.PageContext = pageContext;
            }

            await viewResult.View.RenderAsync(viewContext);

            return new()
            {
                Title = viewContext.ViewData["Title"]?.ToString()!,
                Body = writer.ToString(),
            };
        }

        private ViewEngineResult FindView(string viewPath)
        {
            var result = viewEngine.GetView(executingFilePath: null, viewPath: viewPath, isMainPage: true);
            if (result.Success)
            {
                return result;
            }

            throw new(
                $"View could not be found:{viewPath}{string.Join(Environment.NewLine, result.SearchedLocations)}");
        }
    }
}