using Microsoft.AspNetCore.Mvc;

class BadRequestObjectResultConverter :
    ResultConverter<BadRequestObjectResult>
{
    protected override void InnerWrite(VerifyJsonWriter writer, BadRequestObjectResult result)
    {
        ObjectResultConverter.WriteObjectResult(writer, result);
    }
}