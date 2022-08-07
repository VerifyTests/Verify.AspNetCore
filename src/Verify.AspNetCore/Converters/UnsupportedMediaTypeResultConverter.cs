using Microsoft.AspNetCore.Mvc;

class UnsupportedMediaTypeResultConverter :
    ResultConverter<UnsupportedMediaTypeResult>
{
    protected override void InnerWrite(VerifyJsonWriter writer, UnsupportedMediaTypeResult result) =>
        writer.WriteMember(result, result.StatusCode, "StatusCode");
}