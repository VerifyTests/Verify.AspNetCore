class ProblemHttpResultConverter() :
    HttpResultConverter(
        typeof(ProblemHttpResult),
        typeof(ValidationProblem))
{
    protected override void InnerWrite(VerifyJsonWriter writer, IResult result)
    {
        WriteStatusCode(writer, result);
        WriteContentType(writer, result);
        // Both expose their ProblemDetails as the IValueHttpResult value.
        writer.WriteMember(result, ((IValueHttpResult) result).Value, "ProblemDetails");
    }
}
