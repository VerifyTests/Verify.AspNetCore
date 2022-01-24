using Microsoft.AspNetCore.Mvc;

class ContentResultConverter :
    ResultConverter<ContentResult>
{
    protected override void InnerWrite(VerifyJsonWriter writer, ContentResult result)
    {
        writer.WriteProperty(result, result.StatusCode, "StatusCode");
        writer.WriteProperty(result, result.Content, "Content");
        writer.WriteProperty(result, result.ContentType, "ContentType");
    }
}