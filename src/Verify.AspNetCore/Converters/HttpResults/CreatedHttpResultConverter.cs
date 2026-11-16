using Microsoft.AspNetCore.Http.HttpResults;

class CreatedHttpResultConverter() :
    HttpResultConverter(
        typeof(Created),
        typeof(Created<>),
        typeof(Accepted),
        typeof(Accepted<>))
{
    protected override void InnerWrite(VerifyJsonWriter writer, IResult result)
    {
        writer.WriteMember(result, GetProperty(result, "Location"), "Location");
        WriteValue(writer, result);
        WriteStatusCode(writer, result);
    }
}
