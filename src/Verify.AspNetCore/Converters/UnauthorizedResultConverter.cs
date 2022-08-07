using Microsoft.AspNetCore.Mvc;

class UnauthorizedResultConverter :
    ResultConverter<UnauthorizedResult>
{
    protected override void InnerWrite(VerifyJsonWriter writer, UnauthorizedResult result) =>
        writer.WriteMember(result, result.StatusCode, "StatusCode");
}