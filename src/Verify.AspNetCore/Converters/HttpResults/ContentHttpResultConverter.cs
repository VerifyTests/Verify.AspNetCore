using Microsoft.AspNetCore.Http.HttpResults;

class ContentHttpResultConverter :
    ResultConverter<ContentHttpResult>
{
    protected override void InnerWrite(VerifyJsonWriter writer, ContentHttpResult result)
    {
        writer.WriteMember(result, result.StatusCode, "StatusCode");
        writer.WriteMember(result, result.ResponseContent, "ResponseContent");
        writer.WriteMember(result, result.ContentType, "ContentType");
    }
}
