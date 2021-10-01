using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VerifyXunit;
using Xunit;

[UsesVerify]
public class MyControllerTests
{
    #region MyControllerTest
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
        return Verifier.Verify(
            new
            {
                result,
                context
            });
    }
    #endregion
}