using System.Net.Http.Headers;
using System.Text.Json;
using TestAPI;
using TestAPI.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add CORS services and configure the policy
builder.Services.AddCors(options =>
{
  options.AddPolicy("AllowAll",
    policy => policy.AllowAnyOrigin()
      .AllowAnyMethod()
      .AllowAnyHeader());
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
  app.UseSwagger();
  app.UseSwaggerUI();
}
app.UseCors("AllowAll");
app.UseHttpsRedirection();

var weatherForecastService = new WeatherForecastService();
app.MapGet("/weatherforecast", () =>
  {
    var forecast = weatherForecastService.GenerateForecast();
    return Results.Ok(forecast);
  })
  .WithName("GetWeatherForecast")
  .WithOpenApi();

var gitHubService = new GitHubService();
app.MapGet("/github", async () =>
{
  using HttpClient client = gitHubService.CreateHttpClient();
  var repositories = await gitHubService.FetchRepositoriesAsync(client);
  return Results.Ok(repositories);
});

app.Run();