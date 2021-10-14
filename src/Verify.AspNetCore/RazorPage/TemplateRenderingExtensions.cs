using System;
using Westerhoff.AspNetCore.TemplateRendering;

namespace Microsoft.Extensions.DependencyInjection
{
    /// <summary>
    /// Extensions for <see cref="IServiceCollection"/> to register the Razor
    /// template rendering services.
    /// </summary>
    public static class TemplateRenderingExtensions
    {
        /// <summary>
        /// Add the Razor template rendering services.
        /// </summary>
        /// <param name="services">Service collection.</param>
        /// <returns>Service collection.</returns>
        public static IServiceCollection AddRazorTemplateRendering(this IServiceCollection services)
        {
            if (services is null)
                throw new ArgumentNullException(nameof(services));

            services.AddTransient<RazorTemplateRenderer>();

            return services;
        }
    }
}
