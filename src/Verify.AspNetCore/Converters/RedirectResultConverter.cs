using Microsoft.AspNetCore.Mvc;

class RedirectResultConverter :
    ResultConverter<RedirectResult>
{
    protected override void InnerWrite(VerifyJsonWriter writer, RedirectResult result)
    {
        writer.WriteProperty(result, result.Url, "Url");
        writer.WriteProperty(result, result.PreserveMethod, "PreserveMethod");
        writer.WriteProperty(result, result.Permanent, "Permanent");
    }
}