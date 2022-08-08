using Microsoft.AspNetCore.Mvc;

class UnprocessableEntityResultConverter :
    ResultConverter<UnprocessableEntityResult>
{
    protected override void InnerWrite(VerifyJsonWriter writer, UnprocessableEntityResult result) =>
        writer.WriteMember(result, result.StatusCode, "StatusCode");
}