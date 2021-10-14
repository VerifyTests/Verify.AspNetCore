using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using VerifyXunit;
using Westerhoff.AspNetCore.TemplateRendering;
using Xunit;

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
        return Verifier.Verify(result);
    }

    [Fact]
    public async Task MvcRazorPage()
    {
        using var host = Utility.CreateHost();
        var renderer = host.Services.GetRequiredService<RazorTemplateRenderer>();

        var rendered = await renderer.RenderAsync<MvcRazorPageModel>(
            "/MvcRazorPage.cshtml",
            new()
            {
                Value = "The Value"
            });

        await Verifier.Verify(rendered);
    }

    [Fact]
    public async Task RazorPage()
    {
        using var host = Utility.CreateHost();
        var renderer = host.Services.GetRequiredService<RazorTemplateRenderer>();
        var rendered = await renderer.RenderAsync(
            "/RazorPage.cshtml",
            new RazorPageModel
            {
                Value = "The Value"
            });

        await Verifier.Verify(rendered);
    }

    [Fact]
    public Task FileContentResult()
    {
        var result = new FileContentResult(Encoding.UTF8.GetBytes("the content"), "text/plain");
        return Verifier.Verify(result);
    }
    
    [Fact]
    public Task FileStreamResult()
    {
        var result = new FileStreamResult(new MemoryStream(Encoding.UTF8.GetBytes("the content")), "text/plain");
        return Verifier.Verify(result);
    }
    
    [Fact]
    public Task PhysicalFileResult()
    {
        var result = new PhysicalFileResult("target.txt", "text/plain");
        return Verifier.Verify(result);
    }
    
    [Fact]
    public Task VirtualFileResult()
    {
        var result = new VirtualFileResult("target.txt", "text/plain");
        return Verifier.Verify(result);
    }
}