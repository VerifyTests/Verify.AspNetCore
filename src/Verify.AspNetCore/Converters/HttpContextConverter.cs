
class HttpContextConverter :
    WriteOnlyJsonConverter<HttpContext>
{
    public override void Write(VerifyJsonWriter writer, HttpContext context)
    {
        writer.WriteStartObject();

        writer.WriteProperty(context, context.Request, "Request");
        writer.WriteProperty(context, context.RequestAborted.IsCancellationRequested, "IsAbortedRequested");
        writer.WriteProperty(context, context.Response, "Response");

        writer.WriteEndObject();
    }
}