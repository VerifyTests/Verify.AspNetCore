class HttpResponseConverter :
    WriteOnlyJsonConverter<HttpResponse>
{
    public override void Write(VerifyJsonWriter writer, HttpResponse value)
    {
        writer.WriteStartObject();

        WriteProperties(writer, value);

        writer.WriteEndObject();
    }

    static void WriteProperties(VerifyJsonWriter writer, HttpResponse response)
    {
        WriteHeaders(writer, response);

        WriteCookies(writer, response);
    }

    static void WriteCookies(VerifyJsonWriter writer, HttpResponse response)
    {
        var cookies = response.Headers.Cookies();
        writer.WriteMember(response, cookies, "Cookies");
    }

    static void WriteHeaders(VerifyJsonWriter writer, HttpResponse response)
    {
        var headers = response.Headers.NotCookies();
        writer.WriteMember(response, headers, "Headers");
    }
}