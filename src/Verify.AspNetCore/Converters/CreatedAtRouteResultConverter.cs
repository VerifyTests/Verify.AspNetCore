using Newtonsoft.Json;
using Microsoft.AspNetCore.Mvc;

class CreatedAtRouteResultConverter :
    ResultConverter<CreatedAtRouteResult>
{
    protected override void InnerWrite(JsonWriter writer, CreatedAtRouteResult result, JsonSerializer serializer)
    {
        writer.WritePropertyName("RouteName");
        serializer.Serialize(writer, result.RouteName);
        var values = result.RouteValues;
        if (values != null && values.Any())
        {
            writer.WritePropertyName("RouteValues");
            serializer.Serialize(writer, values.ToDictionary(x => x.Key, x => x.Value));
        }

        ObjectResultConverter.Write(writer, result, serializer);
    }
}