using Newtonsoft.Json;
using Microsoft.AspNetCore.Mvc;

class RedirectToActionResultConverter :
    ResultConverter<RedirectToActionResult>
{
    protected override void InnerWrite(JsonWriter writer, RedirectToActionResult result, JsonSerializer serializer)
    {
        writer.WritePropertyName("ActionName");
        serializer.Serialize(writer, result.ActionName);
        writer.WritePropertyName("ControllerName");
        serializer.Serialize(writer, result.ControllerName);
        writer.WritePropertyName("Fragment");
        serializer.Serialize(writer, result.Fragment);
        writer.WritePropertyName("Permanent");
        serializer.Serialize(writer, result.Permanent);
        writer.WritePropertyName("PreserveMethod");
        serializer.Serialize(writer, result.PreserveMethod);
        var values = result.RouteValues;
        if (values != null && values.Any())
        {
            writer.WritePropertyName("RouteValues");
            serializer.Serialize(writer, values.ToDictionary(x => x.Value!, x => x.Value));
        }
    }
}