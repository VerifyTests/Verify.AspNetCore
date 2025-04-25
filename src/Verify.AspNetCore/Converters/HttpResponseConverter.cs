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
        writer.WriteMember(response, (HttpStatusCode)response.StatusCode, "StatusCode");

        WriteHeaders(writer, response);

        WriteCookies(writer, response);

        WriteIfText(writer, response);
    }

    static void WriteIfText(VerifyJsonWriter writer, HttpResponse response)
    {
        if (!EmptyFiles.ContentTypes.IsText(response.ContentType, out var subType))
        {
            if (writer.HasHttpTextResponseScrubber())
            {
                throw new("Response is not text, but scrubContent is set.");
            }

            return;
        }

        writer.WritePropertyName("Value");
        var result = response.Body.ReadAsString();
        if (writer.TryGetHttpTextResponseScrubber(out var scrubContent))
        {
            result = scrubContent(result);
        }

        if (subType == "json")
        {
            try
            {
                writer.Serialize(JToken.Parse(result));
            }
            catch
            {
                writer.WriteValue(result);
            }
        }
        else if (subType == "xml")
        {
            try
            {
                writer.Serialize(XDocument.Parse(result));
            }
            catch
            {
                writer.WriteValue(result);
            }
        }
        else
        {
            writer.WriteValue(result);
        }
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