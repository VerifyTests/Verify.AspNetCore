using Microsoft.AspNetCore.Mvc;

class CreatedResultConverter :
    ResultConverter<CreatedResult>
{
    protected override void InnerWrite(VerifyJsonWriter writer, CreatedResult result)
    {
        writer.WriteMember(result, result.Location, "Location");
        ObjectResultConverter.WriteObjectResult(writer, result);
    }
}