# <img src="/src/icon.png" height="30px"> Verify.AspNetCore

[![Discussions](https://img.shields.io/badge/Verify-Discussions-yellow?svg=true&label=)](https://github.com/orgs/VerifyTests/discussions)
[![Build status](https://ci.appveyor.com/api/projects/status/u4try12l1iimal2l?svg=true)](https://ci.appveyor.com/project/SimonCropp/verify-aspnetcore)
[![NuGet Status](https://img.shields.io/nuget/v/Verify.AspNetCore.svg)](https://www.nuget.org/packages/Verify.AspNetCore/)

Extends [Verify](https://github.com/VerifyTests/Verify) to allow verification of AspNetCore bits.<!-- singleLineInclude: intro. path: /docs/intro.include.md -->

**See [Milestones](../../milestones?state=closed) for release notes.**


## Sponsors


### Entity Framework Extensions<!-- include: zzz. path: /docs/zzz.include.md -->

[Entity Framework Extensions](https://entityframework-extensions.net/?utm_source=simoncropp&utm_medium=Verify.AspNetCore) is a major sponsor and is proud to contribute to the development this project.

[![Entity Framework Extensions](https://raw.githubusercontent.com/VerifyTests/Verify.AspNetCore/refs/heads/main/docs/zzz.png)](https://entityframework-extensions.net/?utm_source=simoncropp&utm_medium=Verify.AspNetCore)<!-- endInclude -->


## NuGet

 * https://nuget.org/packages/Verify.AspNetCore


## Usage

Enable VerifyAspNetCore once at assembly load time:

<!-- snippet: Enable -->
<a id='snippet-Enable'></a>
```cs
[ModuleInitializer]
public static void Initialize() =>
    VerifyAspNetCore.Initialize();
```
<sup><a href='/src/Tests/ModuleInitializer.cs#L3-L9' title='Snippet source file'>snippet source</a> | <a href='#snippet-Enable' title='Start of snippet'>anchor</a></sup>
<!-- endSnippet -->


### Controller

Given the following controller:

<!-- snippet: MyController.cs -->
<a id='snippet-MyController.cs'></a>
```cs
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
```
<sup><a href='/src/Tests/Snippets/MyController.cs#L1-L25' title='Snippet source file'>snippet source</a> | <a href='#snippet-MyController.cs' title='Start of snippet'>anchor</a></sup>
<!-- endSnippet -->

This test:

<!-- snippet: MyControllerTest -->
<a id='snippet-MyControllerTest'></a>
```cs
[Test]
public Task Test()
{
    var context = new ControllerContext
    {
        HttpContext = new DefaultHttpContext()
    };
    var controller = new MyController
    {
        ControllerContext = context
    };

    var result = controller.Method("inputValue");
    return Verify(
        new
        {
            result,
            context
        });
}
```
<sup><a href='/src/Tests/Snippets/MyControllerTests.cs#L4-L25' title='Snippet source file'>snippet source</a> | <a href='#snippet-MyControllerTest' title='Start of snippet'>anchor</a></sup>
<!-- endSnippet -->

Will result in the following verified file:

<!-- snippet: MyControllerTests.Test.verified.txt -->
<a id='snippet-MyControllerTests.Test.verified.txt'></a>
```txt
{
  result: [
    {
      Value: Value1
    },
    {
      Value: Value2
    }
  ],
  context: {
    HttpContext: {
      Request: {},
      Response: {
        StatusCode: OK,
        Headers: {
          headerKey: headerValue,
          receivedInput: inputValue
        },
        Cookies: {
          cookieKey: cookieValue
        }
      }
    }
  }
}
```
<sup><a href='/src/Tests/Snippets/MyControllerTests.Test.verified.txt#L1-L25' title='Snippet source file'>snippet source</a> | <a href='#snippet-MyControllerTests.Test.verified.txt' title='Start of snippet'>anchor</a></sup>
<!-- endSnippet -->


### Middleware

Given the following middleware:

<!-- snippet: MyMiddleware.cs -->
<a id='snippet-MyMiddleware.cs'></a>
```cs
public class MyMiddleware(RequestDelegate next)
{
    public Task Invoke(HttpContext context)
    {
        context.Response.Headers["headerKey"] = "headerValue";
        return next(context);
    }
}
```
<sup><a href='/src/Tests/Snippets/MyMiddleware.cs#L1-L8' title='Snippet source file'>snippet source</a> | <a href='#snippet-MyMiddleware.cs' title='Start of snippet'>anchor</a></sup>
<!-- endSnippet -->

This test:

<!-- snippet: MyMiddlewareTest -->
<a id='snippet-MyMiddlewareTest'></a>
```cs
[Test]
public async Task Test()
{
    var nextCalled = false;
    var middleware = new MyMiddleware(
        _ =>
        {
            nextCalled = true;
            return Task.CompletedTask;
        });

    var context = new DefaultHttpContext();
    await middleware.Invoke(context);

    await Verify(
        new
        {
            context.Response,
            nextCalled
        });
}
```
<sup><a href='/src/Tests/Snippets/MyMiddlewareTests.cs#L4-L26' title='Snippet source file'>snippet source</a> | <a href='#snippet-MyMiddlewareTest' title='Start of snippet'>anchor</a></sup>
<!-- endSnippet -->

Will result in the following verified file:

<!-- snippet: MyMiddlewareTests.Test.verified.txt -->
<a id='snippet-MyMiddlewareTests.Test.verified.txt'></a>
```txt
{
  Response: {
    StatusCode: OK,
    Headers: {
      headerKey: headerValue
    }
  },
  nextCalled: true
}
```
<sup><a href='/src/Tests/Snippets/MyMiddlewareTests.Test.verified.txt#L1-L9' title='Snippet source file'>snippet source</a> | <a href='#snippet-MyMiddlewareTests.Test.verified.txt' title='Start of snippet'>anchor</a></sup>
<!-- endSnippet -->


## Testing a web app with specific controller scenarios

`UseSpecificControllers` extends `IMvcBuilder` to allow integration testing of a web app using a specific controller scenario.

<!-- snippet: TestController -->
<a id='snippet-TestController'></a>
```cs
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
```
<sup><a href='/src/Tests/Tests.cs#L105-L137' title='Snippet source file'>snippet source</a> | <a href='#snippet-TestController' title='Start of snippet'>anchor</a></sup>
<!-- endSnippet -->


## ScrubAspTextResponse

The ScrubAspTextResponse feature allows modification  or scrubbing of the content of an HTTP response before verification. This is useful for scenarios where the response contains dynamic or sensitive data that needs to be replaced or removed.

<!-- snippet: ScrubAspTextResponse -->
<a id='snippet-ScrubAspTextResponse'></a>
```cs
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
```
<sup><a href='/src/Tests/Tests.cs#L30-L44' title='Snippet source file'>snippet source</a> | <a href='#snippet-ScrubAspTextResponse' title='Start of snippet'>anchor</a></sup>
<!-- endSnippet -->

Results in:

<!-- snippet: Tests.ScrubAspTextResponse.verified.txt -->
<a id='snippet-Tests.ScrubAspTextResponse.verified.txt'></a>
```txt
{
  StatusCode: OK,
  Headers: {
    Content-Type: application/json
  },
  Value: {
    key: replace
  }
}
```
<sup><a href='/src/Tests/Tests.ScrubAspTextResponse.verified.txt#L1-L9' title='Snippet source file'>snippet source</a> | <a href='#snippet-Tests.ScrubAspTextResponse.verified.txt' title='Start of snippet'>anchor</a></sup>
<!-- endSnippet -->


## Icon

[Spider](https://thenounproject.com/term/spider/904683/) designed by [marialuisa iborra](https://thenounproject.com/marialuisa.iborra/) from [The Noun Project](https://thenounproject.com).
