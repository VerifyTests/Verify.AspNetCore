using Microsoft.AspNetCore.Mvc;

class OkResultConverter :
    ResultConverter<OkResult>
{
    protected override void InnerWrite(VerifyJsonWriter writer, OkResult result) =>
        writer.WriteMember(result, result.StatusCode, "StatusCode");
}