using Microsoft.AspNetCore.Mvc;

class OkObjectResultConverter :
    ResultConverter<OkObjectResult>
{
    protected override void InnerWrite(VerifyJsonWriter writer, OkObjectResult result) =>
        ObjectResultConverter.WriteObjectResult(writer, result);
}