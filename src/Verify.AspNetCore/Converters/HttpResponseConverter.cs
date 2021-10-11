using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using VerifyTests;

class HttpResponseConverter :
    WriteOnlyJsonConverter<HttpResponse>
{
    public override void WriteJson(JsonWriter writer, HttpResponse value, JsonSerializer serializer, IReadOnlyDictionary<string, object> context)
    {
        writer.WriteStartObject();

        WriteProperties(writer, serializer, value);

        writer.WriteEndObject();
    }

    public static void WriteProperties(JsonWriter writer, JsonSerializer serializer, HttpResponse response)
    {
        WriteHeaders(writer, serializer, response);

        WriteCookies(writer, serializer, response);
    }

    static void WriteCookies(JsonWriter writer, JsonSerializer serializer, HttpResponse response)
    {
        var cookies = response.Headers.Cookies();
        if (!cookies.Any())
        {
            return;
        }

        writer.WritePropertyName("Cookies");
        serializer.Serialize(writer, cookies);
    }

    static void WriteHeaders(JsonWriter writer, JsonSerializer serializer, HttpResponse response)
    {
        var headers = response.Headers.NotCookies();
        if (!headers.Any())
        {
            return;
        }

        writer.WritePropertyName("Headers");
        serializer.Serialize(writer, headers);
    }
}