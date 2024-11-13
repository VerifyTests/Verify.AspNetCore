public class MyController :
    Controller
{
    public ActionResult<List<DataItem>> Method(string input)
    {
        var headers = HttpContext.Response.Headers;
        headers["headerKey"] = "headerValue";
        headers["receivedInput"] = input;

        var cookies = HttpContext.Response.Cookies;
        cookies.Append("cookieKey", "cookieValue");

        var items = new List<DataItem>
        {
            new("Value1"),
            new("Value2")
        };
        return new(items);
    }

    public class DataItem(string value)
    {
        public string Value { get; } = value;
    }
}