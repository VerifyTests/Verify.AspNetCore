using Microsoft.AspNetCore.Mvc;

class UnprocessableEntityResultConverter :
    ResultConverter<UnprocessableEntityResult>
{
    protected override void InnerWrite(VerifyJsonWriter writer, UnprocessableEntityResult result)
    {
        writer.WriteProperty(result, result.StatusCode, "StatusCode");
    }
}