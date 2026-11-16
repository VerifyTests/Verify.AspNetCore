abstract class ResultConverter<T> :
    WriteOnlyJsonConverter<T>
    where T : notnull
{
    public override void Write(VerifyJsonWriter writer, T result)
    {
        writer.WriteStartObject();
        writer.WriteMember(result, ResultTypeName.Get(result.GetType()), "ResultType");
        InnerWrite(writer, result);
        writer.WriteEndObject();
    }

    protected abstract void InnerWrite(VerifyJsonWriter writer, T result);
}
