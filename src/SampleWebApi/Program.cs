#region sample_web_api

var builder = WebApplication.CreateBuilder(args);

var app = builder.Build();

app.MapGet("/", () => "Hello World!");
app.MapGet("/blowup", _ => throw new("Boo!"));
app.MapPost("/json", (MyEntity entity) => entity);

app.Run();

public record MyEntity(Guid Id, string MyValue);

#endregion

