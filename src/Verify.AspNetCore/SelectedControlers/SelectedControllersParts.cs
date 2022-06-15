using Microsoft.AspNetCore.Mvc.ApplicationParts;

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