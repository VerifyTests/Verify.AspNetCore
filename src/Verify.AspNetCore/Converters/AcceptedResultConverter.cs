class AcceptedResultConverter :
    ResultConverter<AcceptedResult>
{
    protected override void InnerWrite(VerifyJsonWriter writer, AcceptedResult result)
    {
        writer.WriteMember(result, result.Location, "Location");
        ObjectResultConverter.WriteObjectResult(writer, result);
    }
}