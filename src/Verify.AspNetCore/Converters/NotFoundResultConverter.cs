using Microsoft.AspNetCore.Mvc;

class NotFoundResultConverter :
    ResultConverter<NotFoundResult>
{
    protected override void InnerWrite(VerifyJsonWriter writer, NotFoundResult result) =>
        writer.WriteProperty(result, result.StatusCode, "StatusCode");
}