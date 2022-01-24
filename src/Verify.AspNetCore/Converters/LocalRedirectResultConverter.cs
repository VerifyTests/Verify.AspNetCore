using Microsoft.AspNetCore.Mvc;

class LocalRedirectResultConverter :
    ResultConverter<LocalRedirectResult>
{
    protected override void InnerWrite(VerifyJsonWriter writer, LocalRedirectResult result)
    {
        writer.WriteProperty(result, result.PreserveMethod, "PreserveMethod");
        writer.WriteProperty(result, result.Permanent, "Permanent");
        writer.WriteProperty(result, result.Url, "Url");
    }
}