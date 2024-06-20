class BadRequestResultConverter :
    ResultConverter<BadRequestResult>
{
    protected override void InnerWrite(VerifyJsonWriter writer, BadRequestResult result) =>
        writer.WriteMember(result, result.StatusCode, "StatusCode");
}