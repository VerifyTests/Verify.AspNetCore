class JsonResultConverter :
    ResultConverter<JsonResult>
{
    protected override void InnerWrite(VerifyJsonWriter writer, JsonResult result)
    {
        writer.WriteMember(result, result.StatusCode, "StatusCode");
        writer.WriteMember(result, result.Value, "Value");
        writer.WriteMember(result, result.ContentType, "ContentType");
    }
}