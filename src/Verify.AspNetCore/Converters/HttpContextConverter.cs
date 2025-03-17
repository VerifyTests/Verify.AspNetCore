class HttpContextConverter :
    WriteOnlyJsonConverter<HttpContext>
{
    public override void Write(VerifyJsonWriter writer, HttpContext context)
    {
        writer.WriteStartObject();

        writer.WriteMember(context, context.Request, "Request");
        writer.WriteMember(context, context.RequestAborted.IsCancellationRequested, "IsAbortedRequested", false);
        writer.WriteMember(context, context.Response, "Response");

        writer.WriteEndObject();
    }
}