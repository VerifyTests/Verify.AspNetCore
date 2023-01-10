var builder = WebApplication.CreateBuilder(args);

builder.Services.AddMvcCore();
builder.Services.AddRazorPages();

var app = builder.Build();

app.UseRouting();
app.MapRazorPages();
app.MapGet("/", () => "Hello World!");

app.Run();