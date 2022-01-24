using Microsoft.AspNetCore.Mvc;

class ConflictResultConverter :
    ResultConverter<ConflictResult>
{
    protected override void InnerWrite(VerifyJsonWriter writer, ConflictResult result)
    {
        writer.WriteProperty(result, result.StatusCode, "StatusCode");
    }
}