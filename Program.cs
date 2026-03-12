//var builder = WebApplication.CreateBuilder(args);

//// Add services to the container.

//builder.Services.AddControllers();

//var app = builder.Build();

//// Configure the HTTP request pipeline.

////app.UseHttpsRedirection();

//app.UseAuthorization();

//app.MapControllers();

//app.Run();

using API_Books.api.Service;
using API_Books.api.Services;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

// Явно добавляем MVC API
builder.Services.AddControllers(options =>
{
    options.SuppressAsyncSuffixInActionNames = false;
});
builder.Services.AddScoped<IBookService, InMemoryBookService>();

var app = builder.Build();

// Логируем все endpoints при старте
app.Services.GetRequiredService<EndpointDataSource>().Endpoints
    .Where(e => e is RouteEndpoint re && re.RoutePattern.RawText.Contains("api"))
    .ToList()
    .ForEach(e => Console.WriteLine($"✅ Registered route: {e}"));

app.UseAuthorization();
app.MapControllers();

Console.WriteLine("🚀 Server starting...");
app.Run();
