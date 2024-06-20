class StatusCodeResultConverter :
    ResultConverter<StatusCodeResult>
{
    protected override void InnerWrite(VerifyJsonWriter writer, StatusCodeResult result) =>
        writer.WriteMember(result, result.StatusCode, "StatusCode");
}