using Microsoft.AspNetCore.Mvc;

class NotFoundObjectResultConverter :
    ResultConverter<NotFoundObjectResult>
{
    protected override void InnerWrite(VerifyJsonWriter writer, NotFoundObjectResult result)
    {
        ObjectResultConverter.WriteObjectResult(writer, result);
    }
}