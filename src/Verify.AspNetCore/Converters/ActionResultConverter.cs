using Newtonsoft.Json;
using Microsoft.AspNetCore.Mvc;

class ActionResultConverter :
    WriteOnlyJsonConverter
{
    public override void Write(VerifyJsonWriter writer, object action, JsonSerializer serializer)
    {
        var property = action.GetType().GetProperty("Value", BindingFlags.Instance | BindingFlags.FlattenHierarchy | BindingFlags.Public);
        var value = property!.GetValue(action);
        serializer.Serialize(writer, value);
    }

    public override bool CanConvert(Type type)
    {
        if (!type.IsGenericType)
        {
            return false;
        }

        var genericType = type.GetGenericTypeDefinition();
        return genericType == typeof(ActionResult<>);
    }
}