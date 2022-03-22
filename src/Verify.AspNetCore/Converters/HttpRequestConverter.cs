class HttpRequestConverter :
    WriteOnlyJsonConverter<HttpRequest>
{
    public override void Write(VerifyJsonWriter writer, HttpRequest value)
    {
        writer.WriteStartObject();

        WriteProperties(writer, value);

        writer.WriteEndObject();
    }

    static void WriteProperties(VerifyJsonWriter writer, HttpRequest request)
    {
        WriteHeaders(writer, request);

        WriteCookies(writer, request);
    }

    static void WriteCookies(VerifyJsonWriter writer, HttpRequest request)
    {
        var cookies = request.Headers.Cookies();
        writer.WriteProperty(request, cookies, "Cookies");
    }

    static void WriteHeaders(VerifyJsonWriter writer, HttpRequest request)
    {
        var headers = request.Headers.NotCookies();
        writer.WriteProperty(request, headers, "Headers");
    }
}