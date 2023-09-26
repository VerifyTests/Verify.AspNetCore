public class MyMiddleware
{
    RequestDelegate next;

    public MyMiddleware(RequestDelegate next) =>
        this.next = next;

    public Task Invoke(HttpContext context)
    {
        context.Response.Headers.Add("headerKey", "headerValue");
        return next(context);
    }
}