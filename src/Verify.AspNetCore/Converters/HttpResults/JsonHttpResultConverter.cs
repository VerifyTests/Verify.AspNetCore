

// JsonSerializerOptions is left out: it is configuration rather than output, and serializes as a large graph.
class JsonHttpResultConverter() :
    HttpResultConverter(typeof(JsonHttpResult<>))
{
    protected override void InnerWrite(VerifyJsonWriter writer, IResult result)
    {
        WriteStatusCode(writer, result);
        WriteValue(writer, result);
        WriteContentType(writer, result);
    }
}
