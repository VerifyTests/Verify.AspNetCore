using Microsoft.AspNetCore.Mvc;

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
}