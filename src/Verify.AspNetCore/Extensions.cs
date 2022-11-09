using Microsoft.Net.Http.Headers;

static class Extensions
{
    static void MoveToStart(this Stream stream)
    {
        if (stream.CanSeek)
        {
            stream.Position = 0;
        }
    }

    public static async Task<string> ReadAsString(this Stream stream)
    {
        stream.MoveToStart();
        using StreamReader reader = new(stream);
        return await reader.ReadToEndAsync();
    }

    public static Dictionary<string, string> NotCookies(this IHeaderDictionary headers) =>
        headers
            .Where(x => x.Key != HeaderNames.SetCookie)
            .ToDictionary(x => x.Key, x => x.Value.ToString());

    public static Dictionary<string, string?> Cookies(this IHeaderDictionary headers) =>
        headers
            .Where(x => x.Key == HeaderNames.SetCookie)
            .Select(x =>
            {
                var stringSegment = x.Value.Single();
                return SetCookieHeaderValue.Parse(stringSegment);
            })
            .ToDictionary(x => x.Name.Value!, x => x.Value.Value);
}