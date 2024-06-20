class ContentResultConverter :
    ResultConverter<ContentResult>
{
    protected override void InnerWrite(VerifyJsonWriter writer, ContentResult result)
    {
        writer.WriteMember(result, result.StatusCode, "StatusCode");
        writer.WriteMember(result, result.Content, "Content");
        writer.WriteMember(result, result.ContentType, "ContentType");
    }
}