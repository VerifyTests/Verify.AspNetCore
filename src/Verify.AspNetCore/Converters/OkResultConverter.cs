using Microsoft.AspNetCore.Mvc;

class OkResultConverter :
    ResultConverter<OkResult>
{
    protected override void InnerWrite(VerifyJsonWriter writer, OkResult result)
    {
        writer.WriteProperty(result, result.StatusCode, "StatusCode");
    }
}