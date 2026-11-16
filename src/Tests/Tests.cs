namespace VerifyAspNetCoreTests;

public class Tests
{
    [Test]
    public Task ChallengeResult()
    {
        var result = new ChallengeResult(
            "scheme",
            new(
                new Dictionary<string, string?>
                {
                    {
                        "key", "value"
                    }
                }));
        return Verify(result);
    }

    [Test]
    public Task HttpResponse()
    {
        var context = new DefaultHttpContext();
        var buffer = "{\"key\":\"value\"}"u8;
        var response = context.Response;
        response.Body = new MemoryStream(buffer.ToArray());
        response.ContentType = "application/json; charset=utf-8";
        return Verify(response);
    }

    #region ScrubAspTextResponse

    [Test]
    public Task ScrubAspTextResponse()
    {
        var context = new DefaultHttpContext();
        var buffer = "{\"key\":\"value\"}"u8;
        var response = context.Response;
        response.Body = new MemoryStream(buffer.ToArray());
        response.ContentType = "application/json";
        return Verify(response)
            .ScrubAspTextResponse(_ => _.Replace("value", "replace"));
    }

    #endregion

    [Test]
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

    [Test]
    public Task EmptyHttpContext() =>
        Verify(new DefaultHttpContext());

    [Test]
    public Task HeaderDictionary() =>
        Verify(new HeaderDictionary
        {
            {"key", "value"}
        });

    [Test]
    public Task FileContentResult()
    {
        var result = new FileContentResult("the content"u8.ToArray(), "text/plain");
        return Verify(result);
    }

    [Test]
    public Task FileStreamResult()
    {
        var stream = new MemoryStream("the content"u8.ToArray());
        var result = new FileStreamResult(stream, "text/plain");
        return Verify(result);
    }

    [Test]
    public Task PhysicalFileResult()
    {
        var result = new PhysicalFileResult(ProjectFiles.target_txt, "text/plain");
        return Verify(result);
    }

    [Test]
    public Task VirtualFileResult()
    {
        var result = new VirtualFileResult("target.txt", "text/plain");
        return Verify(result);
    }

    [Test]
    public Task ObjectResultSubclasses()
    {
        var value = new
        {
            Name = "item"
        };
        var routeValues = new
        {
            id = 1
        };
        return Verify(new
        {
            Ok = new OkObjectResult(value),
            Created = new CreatedResult("/items/1", value),
            Accepted = new AcceptedResult("/items/1", value),
            CreatedAtAction = new CreatedAtActionResult("Get", "Items", routeValues, value),
            AcceptedAtAction = new AcceptedAtActionResult("Get", "Items", routeValues, value),
            CreatedAtRoute = new CreatedAtRouteResult("route", routeValues, value),
            AcceptedAtRoute = new AcceptedAtRouteResult("route", routeValues, value)
        });
    }

    [Test]
    public Task StatusCodeResultSubclasses() =>
        Verify(new
        {
            Ok = new OkResult(),
            NotFound = new NotFoundResult(),
            AntiforgeryValidationFailed = new AntiforgeryValidationFailedResult(),
            StatusCode = new StatusCodeResult(418)
        });

    [Test]
    public Task ActionResultOfT()
    {
        ActionResult<string> withValue = "the value";
        ActionResult<string> withResult = new NotFoundResult();
        return Verify(new
        {
            WithValue = withValue,
            WithResult = withResult
        });
    }

    #region TestController

    [Test]
    public async Task ControllerIntegrationTest()
    {
        var builder = WebApplication.CreateBuilder();

        var controllers = builder.Services.AddControllers();
        // custom extension
        controllers.UseSpecificControllers(typeof(FooController));

        await using var app = builder.Build();
        app.MapControllers();

        await app.StartAsync();

        using var client = new HttpClient();
        var result = client.GetStringAsync($"{app.Urls.First()}/Foo");

        await Verify(result);
    }

    [ApiController]
    [Route("[controller]")]
    public class FooController :
        ControllerBase
    {
        [HttpGet]
        public string Get() =>
            "Foo";
    }

    #endregion
}