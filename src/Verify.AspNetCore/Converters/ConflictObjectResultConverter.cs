using Microsoft.AspNetCore.Mvc;

class ConflictObjectResultConverter :
    ResultConverter<ConflictObjectResult>
{
    protected override void InnerWrite(VerifyJsonWriter writer, ConflictObjectResult result)
    {
        ObjectResultConverter.WriteObjectResult(writer, result);
    }
}