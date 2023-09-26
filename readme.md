# <img src="/src/icon.png" height="30px"> Verify.AspNetCore

[![Discussions](https://img.shields.io/badge/Verify-Discussions-yellow?svg=true&label=)](https://github.com/orgs/VerifyTests/discussions)
[![Build status](https://ci.appveyor.com/api/projects/status/u4try12l1iimal2l?svg=true)](https://ci.appveyor.com/project/SimonCropp/verify-aspnetcore)
[![NuGet Status](https://img.shields.io/nuget/v/Verify.AspNetCore.svg)](https://www.nuget.org/packages/Verify.AspNetCore/)

Extends [Verify](https://github.com/VerifyTests/Verify) to allow verification of AspNetCore bits.

**See [Milestones](../../milestones?state=closed) for release notes.**



## NuGet package

https://nuget.org/packages/Verify.AspNetCore/


## Usage

Enable VerifyAspNetCore once at assembly load time:

<!-- snippet: Enable -->
<a id='snippet-enable'></a>
```cs
[ModuleInitializer]
public static void Initialize() =>
    VerifyAspNetCore.Initialize();
```
<sup><a href='/src/Tests/ModuleInitializer.cs#L3-L9' title='Snippet source file'>snippet source</a> | <a href='#snippet-enable' title='Start of snippet'>anchor</a></sup>
<!-- endSnippet -->


### Controller

Given the following controller:

<!-- snippet: MyController.cs -->
<a id='snippet-MyController.cs'></a>
```cs
using Microsoft.AspNetCore.Mvc;

public class MyController :
    Controller
{
    public ActionResult<List<DataItem>> Method(string input)
    {
        var headers = HttpContext.Response.Headers;
        headers.Add("headerKey", "headerValue");
        headers.Add("receivedInput", input);

        var cookies = HttpContext.Response.Cookies;
        cookies.Append("cookieKey", "cookieValue");

        var items = new List<DataItem>
        {
            new("Value1"),
            new("Value2")
        };
        return new(items);
    }

    public class DataItem
    {
        public string Value { get; }

        public DataItem(string value) =>
            Value = value;
    }
}
```
<sup><a href='/src/Tests/Snippets/MyController.cs#L1-L30' title='Snippet source file'>snippet source</a> | <a href='#snippet-MyController.cs' title='Start of snippet'>anchor</a></sup>
<!-- endSnippet -->

This test:

<!-- snippet: MyControllerTest -->
<a id='snippet-mycontrollertest'></a>
```cs
[Fact]
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
<sup><a href='/src/Tests/Snippets/MyControllerTests.cs#L6-L27' title='Snippet source file'>snippet source</a> | <a href='#snippet-mycontrollertest' title='Start of snippet'>anchor</a></sup>
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
      IsAbortedRequested: false,
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
<sup><a href='/src/Tests/Snippets/MyControllerTests.Test.verified.txt#L1-L26' title='Snippet source file'>snippet source</a> | <a href='#snippet-MyControllerTests.Test.verified.txt' title='Start of snippet'>anchor</a></sup>
<!-- endSnippet -->


### Middleware

Given the following middleware:

<!-- snippet: MyMiddleware.cs -->
<a id='snippet-MyMiddleware.cs'></a>
```cs
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
```
<sup><a href='/src/Tests/Snippets/MyMiddleware.cs#L1-L13' title='Snippet source file'>snippet source</a> | <a href='#snippet-MyMiddleware.cs' title='Start of snippet'>anchor</a></sup>
<!-- endSnippet -->

This test:

<!-- snippet: MyMiddlewareTest -->
<a id='snippet-mymiddlewaretest'></a>
```cs
[Fact]
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
<sup><a href='/src/Tests/Snippets/MyMiddlewareTests.cs#L4-L26' title='Snippet source file'>snippet source</a> | <a href='#snippet-mymiddlewaretest' title='Start of snippet'>anchor</a></sup>
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
<a id='snippet-testcontroller'></a>
```cs
[Fact]
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
<sup><a href='/src/Tests/Tests.cs#L79-L111' title='Snippet source file'>snippet source</a> | <a href='#snippet-testcontroller' title='Start of snippet'>anchor</a></sup>
<!-- endSnippet -->


## Icon

[Spider](https://thenounproject.com/term/spider/904683/) designed by [marialuisa iborra](https://thenounproject.com/marialuisa.iborra/) from [The Noun Project](https://thenounproject.com).
