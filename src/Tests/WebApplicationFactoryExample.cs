[TestFixture]
public class WebApplicationFactoryExample :
    IDisposable
{
    WebApplicationFactory<Program> factory = new();
    HttpClient client;

    public WebApplicationFactoryExample() =>
        client = factory.CreateClient();

    [Test]
    public async Task Test()
    {
        var response = await client.GetAsync("/");
        await Verify(response);
    }

    public void Dispose()
    {
        client.Dispose();
        factory.Dispose();
    }
}