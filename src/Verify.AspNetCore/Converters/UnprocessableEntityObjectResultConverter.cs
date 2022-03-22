using Microsoft.AspNetCore.Mvc;

class UnprocessableEntityObjectResultConverter :
    ResultConverter<UnprocessableEntityObjectResult>
{
    protected override void InnerWrite(VerifyJsonWriter writer, UnprocessableEntityObjectResult result) =>
        ObjectResultConverter.WriteObjectResult(writer, result);
}