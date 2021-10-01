using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using VerifyNUnit;
using NUnit.Framework;

[TestFixture]
public class Tests
{
    [Test]
    public Task ChallengeResult()
    {
        var result = new ChallengeResult(
            "scheme",
            new AuthenticationProperties(
                new Dictionary<string, string?>
                {
                    {"key", "value"}
                }));
        return Verifier.Verify(result);
    }

    [Test]
    public Task FileContentResult()
    {
        var result = new FileContentResult(Encoding.UTF8.GetBytes("the content"), "text/plain");
        return Verifier.Verify(result);
    }

    [Test]
    public Task FileStreamResult()
    {
        var result = new FileStreamResult(new MemoryStream(Encoding.UTF8.GetBytes("the content")), "text/plain");
        return Verifier.Verify(result);
    }

    [Test]
    public Task PhysicalFileResult()
    {
        var result = new PhysicalFileResult("target.txt", "text/plain");
        return Verifier.Verify(result);
    }

    [Test]
    public Task VirtualFileResult()
    {
        var result = new VirtualFileResult("target.txt", "text/plain");
        return Verifier.Verify(result);
    }
    
}
