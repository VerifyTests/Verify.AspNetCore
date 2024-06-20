class PartialViewResultConverter :
    ResultConverter<PartialViewResult>
{
    protected override void InnerWrite(VerifyJsonWriter writer, PartialViewResult result)
    {
        writer.WriteMember(result, result.StatusCode, "StatusCode");
        writer.WriteMember(result, result.ContentType, "ContentType");
        writer.WriteMember(result, result.ViewName, "ViewName");
        writer.WriteMember(result, result.Model, "Model");
    }
}