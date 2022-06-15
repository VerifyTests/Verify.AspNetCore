using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Testing;
using MyApp;

[UsesVerify]
public class Tests
{
    [Fact]
    public Task ChallengeResult()
    {
        var result = new ChallengeResult(
            "scheme",
            new(
                new Dictionary<string, string?>
                {
                    {"key", "value"}
                }));
        return Verify(result);
    }

    [Fact]
    public Task HttpContext()
    {
        var context = new DefaultHttpContext
        {
            Items = new Dictionary<object, object?>
            {
                {
                    "item1", "value1"
                }
            },
            Connection =
            {
                Id = "ConnectionId"
            }
        };
        return Verify(context);
    }

    [Fact]
    public Task EmptyHttpContext() =>
        Verify(new DefaultHttpContext());

    [Fact]
    public Task FileContentResult()
    {
        var result = new FileContentResult(Encoding.UTF8.GetBytes("the content"), "text/plain");
        return Verify(result);
    }

    [Fact]
    public Task FileStreamResult()
    {
        var result = new FileStreamResult(new MemoryStream(Encoding.UTF8.GetBytes("the content")), "text/plain");
        return Verify(result);
    }

    [Fact]
    public Task PhysicalFileResult()
    {
        var result = new PhysicalFileResult("target.txt", "text/plain");
        return Verify(result);
    }

    [Fact]
    public Task VirtualFileResult()
    {
        var result = new VirtualFileResult("target.txt", "text/plain");
        return Verify(result);
    }
    
    [Fact]
    public async Task ControllerIntegrationTest()
    {
        var builder = WebApplication.CreateBuilder();

        IMvcBuilder addControllers = builder.Services.AddControllers();
        addControllers.UseSpecificControllers(typeof(FooController));

        await using var app = builder.Build();
        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.MapControllers();

        var runAsync = app.RunAsync();

        await Task.Delay(10000);
        var httpClient = new HttpClient();
        var result = httpClient.GetStringAsync(app.Urls.First()+"/Foo");
        await Verify(result);
        await app.StopAsync();
    }

    [ApiController]
    [Route("[controller]")]
    public class FooController : ControllerBase
    {
        [HttpGet]
        public string Get() =>
            "Foo";
    }
}