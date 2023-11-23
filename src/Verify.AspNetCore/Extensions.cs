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
            .Where(_ => _.Key != HeaderNames.SetCookie)
            .ToDictionary(_ => _.Key, _ => _.Value.ToString());

    public static Dictionary<string, string?> Cookies(this IHeaderDictionary headers) =>
        headers
            .Where(_ => _.Key == HeaderNames.SetCookie)
            .Select(_ =>
            {
                var stringSegment = _.Value.Single();
                return SetCookieHeaderValue.Parse(stringSegment);
            })
            .ToDictionary(_ => _.Name.Value!, _ => _.Value.Value);
}