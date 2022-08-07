using Microsoft.AspNetCore.Mvc;

class ControllerContextConverter :
    WriteOnlyJsonConverter<ControllerContext>
{
    public override void Write(VerifyJsonWriter writer, ControllerContext context)
    {
        //TODO: missing ControllerContext props
        writer.WriteStartObject();

        writer.WriteMember(context, context.HttpContext, "HttpContext");

        writer.WriteEndObject();
    }
}