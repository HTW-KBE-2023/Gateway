using Ocelot.DependencyInjection;
using Ocelot.Middleware;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Configuration.AddJsonFile("ocelot.json", optional: false, reloadOnChange: true);
builder.Services.AddOcelot(builder.Configuration);
builder.Services.AddSwaggerForOcelot(builder.Configuration);

builder.Services.AddControllers();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwaggerForOcelotUI(option =>
    {
        option.PathToSwaggerGenerator = "/swagger/docs";
    }, uiOption =>
    {
        uiOption.DocumentTitle = "Swagger - Gateway Documentation";
        uiOption.EnableDeepLinking();
    });
}

app.UseHttpsRedirection();

app.MapControllers();

await app.UseOcelot();

app.Run();