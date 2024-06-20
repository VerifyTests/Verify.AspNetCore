class ConflictResultConverter :
    ResultConverter<ConflictResult>
{
    protected override void InnerWrite(VerifyJsonWriter writer, ConflictResult result) =>
        writer.WriteMember(result, result.StatusCode, "StatusCode");
}