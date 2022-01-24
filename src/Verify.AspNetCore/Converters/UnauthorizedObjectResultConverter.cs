using Microsoft.AspNetCore.Mvc;

class UnauthorizedObjectResultConverter :
    ResultConverter<UnauthorizedObjectResult>
{
    protected override void InnerWrite(VerifyJsonWriter writer, UnauthorizedObjectResult result)
    {
        ObjectResultConverter.WriteObjectResult(writer, result);
    }
}