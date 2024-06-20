class NoContentResultConverter :
    ResultConverter<NoContentResult>
{
    protected override void InnerWrite(VerifyJsonWriter writer, NoContentResult result) =>
        writer.WriteMember(result, result.StatusCode, "StatusCode");
}