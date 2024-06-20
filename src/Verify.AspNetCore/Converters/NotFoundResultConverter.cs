class NotFoundResultConverter :
    ResultConverter<NotFoundResult>
{
    protected override void InnerWrite(VerifyJsonWriter writer, NotFoundResult result) =>
        writer.WriteMember(result, result.StatusCode, "StatusCode");
}