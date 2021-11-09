using Newtonsoft.Json;
using Microsoft.AspNetCore.Mvc;

class AcceptedAtRouteResultConverter :
    ResultConverter<AcceptedAtRouteResult>
{
    protected override void InnerWrite(JsonWriter writer, AcceptedAtRouteResult result, JsonSerializer serializer)
    {
        writer.WritePropertyName("RouteName");
        serializer.Serialize(writer, result.RouteName);
        var values = result.RouteValues;
        if (values != null && values.Any())
        {
            writer.WritePropertyName("RouteValues");
            serializer.Serialize(writer, values.ToDictionary(x => x.Key, x => x.Value));
        }
    }
}