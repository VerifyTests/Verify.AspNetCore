using Microsoft.AspNetCore.Mvc.ApplicationParts;

namespace VerifyTests;

public static partial class VerifyAspNetCore
{
    /// <summary>
    /// Finds the appropriate controllers
    /// </summary>
    public static void UseSpecificControllers(this ApplicationPartManager manager, params Type[] controllers)
    {
        manager.FeatureProviders.Add(new InternalControllerProvider());
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
    /// <param name="builder">The builder that configures mvc core</param>
    /// <param name="controllers">The controller types that are allowed. </param>
    public static IMvcBuilder UseSpecificControllers(this IMvcBuilder builder, params Type[] controllers) =>
        builder.ConfigureApplicationPartManager(_ => _.UseSpecificControllers(controllers));

    class SelectedControllersParts :
        ApplicationPart,
        IApplicationPartTypeProvider
    {
        public SelectedControllersParts(Type[] controllers)
        {
            var builder = new StringBuilder("Only selected controllers:");
            builder.AppendLine();
            foreach (var type in controllers)
            {
                builder.AppendLine($" * {type.FullName}");
            }

            Name = builder.ToString();
            Types = controllers.Select(_ => _.GetTypeInfo()).ToArray();
        }

        public IEnumerable<TypeInfo> Types { get; }
        public override string Name { get; }
    }
}