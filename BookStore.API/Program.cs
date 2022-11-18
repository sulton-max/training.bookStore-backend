using BookStore.API.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder
    .AddDbContexts()
    .AddEntityRepositories()
    .AddEntityServices()
    .AddCustomRouting()
    .AddCustomControllers()
    .AddOpenApiTools();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseOpenApiTools();
    app.UseSeedData();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();