using Microsoft.AspNetCore.Mvc;

class JsonResultConverter :
    ResultConverter<JsonResult>
{
    protected override void InnerWrite(VerifyJsonWriter writer, JsonResult result)
    {
        writer.WriteProperty(result, result.StatusCode, "StatusCode");
        writer.WriteProperty(result, result.Value, "Value");
        writer.WriteProperty(result, result.ContentType, "ContentType");
    }
}