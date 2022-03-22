using Microsoft.AspNetCore.Mvc;

class UnauthorizedResultConverter :
    ResultConverter<UnauthorizedResult>
{
    protected override void InnerWrite(VerifyJsonWriter writer, UnauthorizedResult result) =>
        writer.WriteProperty(result, result.StatusCode, "StatusCode");
}