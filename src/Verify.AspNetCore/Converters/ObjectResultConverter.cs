class ObjectResultConverter :
    ResultConverter<ObjectResult>
{
    protected override void InnerWrite(VerifyJsonWriter writer, ObjectResult result) =>
        WriteObjectResult(writer, result);

    public static void WriteObjectResult(VerifyJsonWriter writer, ObjectResult result)
    {
        writer.WriteMember(result, result.Value, "Value");
        writer.WriteMember(result, result.StatusCode, "StatusCode");

        writer.WriteMember(result, result.ContentTypes, "ContentTypes");
        writer.WriteMember(result, result.DeclaredType, "DeclaredType");
    }
}