using Microsoft.AspNetCore.Http.HttpResults;

class RedirectHttpResultConverter :
    ResultConverter<RedirectHttpResult>
{
    protected override void InnerWrite(VerifyJsonWriter writer, RedirectHttpResult result)
    {
        writer.WriteMember(result, result.Url, "Url");
        writer.WriteMember(result, result.PreserveMethod, "PreserveMethod");
        writer.WriteMember(result, result.Permanent, "Permanent");
        writer.WriteMember(result, result.AcceptLocalUrlOnly, "AcceptLocalUrlOnly");
    }
}
