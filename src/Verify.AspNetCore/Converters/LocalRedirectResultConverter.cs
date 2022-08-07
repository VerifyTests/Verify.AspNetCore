using Microsoft.AspNetCore.Mvc;

class LocalRedirectResultConverter :
    ResultConverter<LocalRedirectResult>
{
    protected override void InnerWrite(VerifyJsonWriter writer, LocalRedirectResult result)
    {
        writer.WriteMember(result, result.PreserveMethod, "PreserveMethod");
        writer.WriteMember(result, result.Permanent, "Permanent");
        writer.WriteMember(result, result.Url, "Url");
    }
}