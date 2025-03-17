[TestFixture]
public class MyControllerTests
{
    #region MyControllerTest
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
    #endregion
}