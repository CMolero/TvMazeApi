using Microsoft.EntityFrameworkCore;
using Polly;
using Polly.Extensions.Http;
using TvMazeApi.Repository.Data;
using TvMazeApi.Repository.Repositories;
using TvMazeApi.Repository.Services;
using TvMazeApi.Repository.Strategy;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<ScraperDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("TvMazeScraperApiContext"));
});


builder.Services.AddHttpClient("tvmaze", httpClient =>
{
    httpClient.BaseAddress = new Uri(builder.Configuration.GetValue<string>("TvMazeApiUrl"));
}).AddHttpMessageHandler(() =>
    new RateLimiterHandler(
        limitCount: 20,
        limitTime: TimeSpan.FromSeconds(10)))
    .SetHandlerLifetime(Timeout.InfiniteTimeSpan)
        .AddPolicyHandler(GetCircuitBreakerPolicy())
    .AddPolicyHandler(GetRetryPolicy());

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddScoped<IShowRepository, ShowRepository>();
builder.Services.AddScoped<IEpisodeRepository, EpisodeRepository>();
builder.Services.AddScoped<IScraperService, ScraperService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
else
{
    //Handle server errors in production and return a generic error message.
    app.UseExceptionHandler(appBuilder =>
    {
        appBuilder.Run(async context => {
            context.Response.StatusCode = 500;
            await context.Response.WriteAsync("An unexpected fault happened. Try again later.");
        });
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    var context = services.GetRequiredService<ScraperDbContext>();
    context.Database.EnsureDeleted();
    if (context.Database.ProviderName != "Microsoft.EntityFrameworkCore.InMemory")
    {
        context.Database.Migrate();
    }
}

app.Run();


static IAsyncPolicy<HttpResponseMessage> GetRetryPolicy()
{
    return HttpPolicyExtensions
        .HandleTransientHttpError()
        .OrResult(msg => msg.StatusCode == System.Net.HttpStatusCode.NotFound)
        .WaitAndRetryAsync(3, retryAttempt => TimeSpan.FromSeconds(Math.Pow(5, retryAttempt)));
}

static IAsyncPolicy<HttpResponseMessage> GetCircuitBreakerPolicy()
{
    return HttpPolicyExtensions
        .HandleTransientHttpError()
        .CircuitBreakerAsync(5, TimeSpan.FromSeconds(5));
}

public partial class Program { }