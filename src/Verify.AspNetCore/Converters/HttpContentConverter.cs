using System.Net.Http;
using Newtonsoft.Json;
using VerifyTests;

public class HttpContentConverter : WriteOnlyJsonConverter<HttpContent>
{
    public override void WriteJson(JsonWriter writer, HttpContent content, JsonSerializer serializer, IReadOnlyDictionary<string, object> context)
    {
        writer.WriteStartObject();

        WriteHeaders(writer, serializer, content);

        writer.WritePropertyName("Body");
        serializer.Serialize(writer, content.ReadAsStringAsync().Result);

        writer.WriteEndObject();
    }

    static void WriteHeaders(JsonWriter writer, JsonSerializer serializer, HttpContent content)
    {
        var headers = content.Headers;
        if (!headers.Any())
        {
            return;
        }

        writer.WritePropertyName("Headers");
        serializer.Serialize(writer, headers);
    }
}