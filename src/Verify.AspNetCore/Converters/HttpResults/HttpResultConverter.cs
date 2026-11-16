// For minimal API results that come in a plain and a generic form, such as Ok and Ok<TValue>.
// Generic types are given as their definition (typeof(Ok<>)).
abstract class HttpResultConverter(params Type[] types) :
    WriteOnlyJsonConverter
{
    public override bool CanConvert(Type type)
    {
        if (type.IsGenericType)
        {
            type = type.GetGenericTypeDefinition();
        }

        return types.Contains(type);
    }

    public override void Write(VerifyJsonWriter writer, object value)
    {
        var result = (IResult) value;
        writer.WriteStartObject();
        writer.WriteMember(result, ResultTypeName.Get(result.GetType()), "ResultType");
        InnerWrite(writer, result);
        writer.WriteEndObject();
    }

    protected abstract void InnerWrite(VerifyJsonWriter writer, IResult result);

    protected static void WriteValue(VerifyJsonWriter writer, IResult result)
    {
        if (result is IValueHttpResult valueResult)
        {
            writer.WriteMember(result, valueResult.Value, "Value");
        }
    }

    protected static void WriteStatusCode(VerifyJsonWriter writer, IResult result)
    {
        if (result is IStatusCodeHttpResult statusCodeResult)
        {
            writer.WriteMember(result, statusCodeResult.StatusCode, "StatusCode");
        }
    }

    protected static void WriteContentType(VerifyJsonWriter writer, IResult result)
    {
        if (result is IContentTypeHttpResult contentTypeResult)
        {
            writer.WriteMember(result, contentTypeResult.ContentType, "ContentType");
        }
    }

    // Members with no interface to read them through, such as Location on Created and Created<TValue>.
    protected static object? GetProperty(IResult result, string name) =>
        result.GetType().GetProperty(name)!.GetValue(result);
}
