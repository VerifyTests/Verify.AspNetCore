using Newtonsoft.Json;
using Microsoft.AspNetCore.Mvc;

class ControllerContextConverter :
    WriteOnlyJsonConverter<ControllerContext>
{
    public override void Write(VerifyJsonWriter writer, ControllerContext context, JsonSerializer serializer)
    {
        var response = context.HttpContext.Response;
        writer.WriteStartObject();

        HttpResponseConverter.WriteProperties(writer, serializer, response);

        writer.WriteEndObject();
    }
}