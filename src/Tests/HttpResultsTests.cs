using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Http.HttpResults;

namespace VerifyAspNetCoreTests;

public class HttpResultsTests
{
    static Item item = new("item");

    [Test]
    public Task StatusCodeResults() =>
        Verify(new
        {
            Ok = TypedResults.Ok(),
            OkOfT = TypedResults.Ok(item),
            BadRequest = TypedResults.BadRequest(),
            BadRequestOfT = TypedResults.BadRequest(item),
            Conflict = TypedResults.Conflict(item),
            NotFound = TypedResults.NotFound(),
            NotFoundOfT = TypedResults.NotFound(item),
            UnprocessableEntity = TypedResults.UnprocessableEntity(item),
            InternalServerError = TypedResults.InternalServerError(item),
            NoContent = TypedResults.NoContent(),
            Unauthorized = TypedResults.Unauthorized(),
            StatusCode = TypedResults.StatusCode(418),
            ServerSentEvents = TypedResults.ServerSentEvents(AsyncEnumerable.Empty<string>()),
            TypedResults.Empty
        });

    [Test]
    public Task CreatedResults()
    {
        var routeValues = new
        {
            id = 1
        };
        return Verify(new
        {
            Created = TypedResults.Created("/items/1"),
            CreatedOfT = TypedResults.Created("/items/1", item),
            Accepted = TypedResults.Accepted("/items/1"),
            AcceptedOfT = TypedResults.Accepted("/items/1", item),
            CreatedAtRoute = TypedResults.CreatedAtRoute("route", routeValues),
            CreatedAtRouteOfT = TypedResults.CreatedAtRoute(item, "route", routeValues),
            AcceptedAtRoute = TypedResults.AcceptedAtRoute("route", routeValues),
            AcceptedAtRouteOfT = TypedResults.AcceptedAtRoute(item, "route", routeValues)
        });
    }

    [Test]
    public Task ContentResults()
    {
        var errors = new Dictionary<string, string[]>
        {
            {
                "field", ["required"]
            }
        };
        return Verify(new
        {
            Json = TypedResults.Json(item),
            Content = TypedResults.Content("the text", "text/plain"),
            Utf8Content = TypedResults.Text("the text"u8, "text/plain"),
            Problem = TypedResults.Problem("the detail"),
            ValidationProblem = TypedResults.ValidationProblem(errors)
        });
    }

    [Test]
    public Task AuthenticationResults()
    {
        var properties = new AuthenticationProperties(
            new Dictionary<string, string?>
            {
                {
                    "key", "value"
                }
            });
        var principal = new ClaimsPrincipal(new ClaimsIdentity([new("name", "user")], "scheme"));
        return Verify(new
        {
            Challenge = TypedResults.Challenge(properties, ["scheme"]),
            Forbid = TypedResults.Forbid(properties, ["scheme1", "scheme2"]),
            SignIn = TypedResults.SignIn(principal, properties, "scheme"),
            SignOut = TypedResults.SignOut(properties, ["scheme"])
        });
    }

    [Test]
    public Task RedirectResults() =>
        Verify(new
        {
            Redirect = TypedResults.Redirect("/target"),
            PermanentRedirect = TypedResults.Redirect("/target", permanent: true, preserveMethod: true),
            LocalRedirect = TypedResults.LocalRedirect("/target"),
            RedirectToRoute = TypedResults.RedirectToRoute(
                "route",
                new
                {
                    id = 1
                },
                fragment: "section")
        });

    [Test]
    public Task FileResults() =>
        Verify(new
        {
            FileContent = TypedResults.File("the content"u8.ToArray(), "text/plain", "file.txt"),
            FileStream = TypedResults.Stream(new MemoryStream("the content"u8.ToArray()), "text/plain"),
            PushStream = TypedResults.Stream((Stream _) => Task.CompletedTask, "text/plain"),
            PhysicalFile = TypedResults.PhysicalFile(ProjectFiles.target_txt, "text/plain"),
            VirtualFile = TypedResults.VirtualFile("target.txt", "text/plain")
        });

    [Test]
    public Task FileContentHttpResult() =>
        Verify(TypedResults.File("the content"u8.ToArray(), "text/plain"));

    [Test]
    public Task FileStreamHttpResult() =>
        Verify(TypedResults.Stream(new MemoryStream("the content"u8.ToArray()), "text/plain"));

    [Test]
    public Task PhysicalFileHttpResult() =>
        Verify(TypedResults.PhysicalFile(ProjectFiles.target_txt, "text/plain"));

    [Test]
    public Task VirtualFileHttpResult() =>
        Verify(TypedResults.VirtualFile("target.txt", "text/plain"));

    [Test]
    public Task NestedResult()
    {
        Results<Ok<Item>, NotFound> result = TypedResults.Ok(item);
        return Verify(result);
    }

    [Test]
    public Task RazorComponent() =>
        Verify(
            new RazorComponentResult<TestComponent>(
                new
                {
                    Title = "the title"
                }));

    public record Item(string Name);

    public class TestComponent :
        IComponent
    {
        public void Attach(RenderHandle renderHandle)
        {
        }

        public Task SetParametersAsync(ParameterView parameters) =>
            Task.CompletedTask;
    }
}
