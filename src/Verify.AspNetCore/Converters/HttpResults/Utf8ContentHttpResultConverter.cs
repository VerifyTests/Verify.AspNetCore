using Microsoft.AspNetCore.Http.HttpResults;

class Utf8ContentHttpResultConverter :
    ResultConverter<Utf8ContentHttpResult>
{
    protected override void InnerWrite(VerifyJsonWriter writer, Utf8ContentHttpResult result)
    {
        writer.WriteMember(result, result.StatusCode, "StatusCode");
        // The content is UTF-8 text, so show it as text rather than base64.
        writer.WriteMember(result, Encoding.UTF8.GetString(result.ResponseContent.Span), "ResponseContent");
        writer.WriteMember(result, result.ContentType, "ContentType");
    }
}
