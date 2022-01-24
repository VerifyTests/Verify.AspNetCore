using Microsoft.AspNetCore.Mvc;

class NoContentResultConverter :
    ResultConverter<NoContentResult>
{
    protected override void InnerWrite(VerifyJsonWriter writer, NoContentResult result)
    {
        writer.WriteProperty(result, result.StatusCode, "StatusCode");
    }
}