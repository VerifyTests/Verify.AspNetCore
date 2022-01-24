using Microsoft.AspNetCore.Mvc;

class BadRequestResultConverter :
    ResultConverter<BadRequestResult>
{
    protected override void InnerWrite(VerifyJsonWriter writer, BadRequestResult result)
    {
        writer.WriteProperty(result, result.StatusCode, "StatusCode");
    }
}