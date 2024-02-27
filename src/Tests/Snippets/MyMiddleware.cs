public class MyMiddleware(RequestDelegate next)
{
    public Task Invoke(HttpContext context)
    {
        context.Response.Headers["headerKey"] = "headerValue";
        return next(context);
    }
}