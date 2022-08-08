using Microsoft.AspNetCore.Mvc;

class RedirectResultConverter :
    ResultConverter<RedirectResult>
{
    protected override void InnerWrite(VerifyJsonWriter writer, RedirectResult result)
    {
        writer.WriteMember(result, result.Url, "Url");
        writer.WriteMember(result, result.PreserveMethod, "PreserveMethod");
        writer.WriteMember(result, result.Permanent, "Permanent");
    }
}