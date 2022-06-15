using Microsoft.AspNetCore.Mvc.ApplicationParts;

namespace VerifyTests;

public static partial class VerifyAspNetCore
{
    static void UseSpecificControllers(this ApplicationPartManager manager, params Type[] controllers)
    {
        manager.FeatureProviders.Add(new ControllerProvider(controllers));
        manager.ApplicationParts.Clear();
        manager.ApplicationParts.Add(new SelectedControllersParts(controllers));
    }

    /// <summary>
    /// Only allow selected controllers
    /// </summary>
    public static IMvcCoreBuilder UseSpecificControllers(this IMvcCoreBuilder builder, params Type[] controllers) =>
        builder.ConfigureApplicationPartManager(_ => _.UseSpecificControllers(controllers));

    /// <summary>
    /// Only allow selected controllers
    /// </summary>
    public static IMvcBuilder UseSpecificControllers(this IMvcBuilder builder, params Type[] controllers) =>
        builder.ConfigureApplicationPartManager(_ => _.UseSpecificControllers(controllers));

}