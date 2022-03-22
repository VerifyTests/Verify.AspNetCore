using Microsoft.AspNetCore.Mvc;

class ObjectResultConverter :
    ResultConverter<ObjectResult>
{
    protected override void InnerWrite(VerifyJsonWriter writer, ObjectResult result) =>
        WriteObjectResult(writer, result);

    public static void WriteObjectResult(VerifyJsonWriter writer, ObjectResult result)
    {
        writer.WriteProperty(result, result.Value, "Value");
        writer.WriteProperty(result, result.StatusCode, "StatusCode");

        writer.WriteProperty(result, result.ContentTypes, "ContentTypes");
        writer.WriteProperty(result, result.DeclaredType, "DeclaredType");
    }
}