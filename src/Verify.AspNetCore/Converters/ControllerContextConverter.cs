using Microsoft.AspNetCore.Mvc;

class ControllerContextConverter :
    WriteOnlyJsonConverter<ControllerContext>
{
    public override void Write(VerifyJsonWriter writer, ControllerContext context)
    {
        var response = context.HttpContext.Response;
        writer.WriteStartObject();

        HttpResponseConverter.WriteProperties(writer, response);

        writer.WriteEndObject();
    }
}