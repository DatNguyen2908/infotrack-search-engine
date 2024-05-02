using FluentValidation;
using FluentValidation.AspNetCore;
using Infotrack.Sales.SearchEngine.Application;
using Infotrack.Sales.SearchEngine.Constants;
using Infotrack.Sales.SearchEngine.Contracts;
using Infotrack.Sales.SearchEngine.Domain;
using Infotrack.Sales.SearchEngine.Domain.Repositories;
using Infotrack.Sales.SearchEngine.EFCore;
using Infotrack.Sales.SearchEngine.Infrastructure;
using Infotrack.Sales.SearchEngine.WebApi.Filters;
using Infotrack.Sales.SearchEngine.WebApi.Validators;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddDbContext<SearchEngineContext>(options =>
           options.UseSqlServer(builder.Configuration.GetConnectionString("SearchEngineDatabase")));

builder.Services.AddScoped<ISearchHistoryRepository, SearchHistoryRepository>();
builder.Services.AddScoped<ISearchEngineService, SearchEngineService>();
builder.Services.AddScoped<IProviderSearchService, ProviderSearchService>();
builder.Services.AddScoped<ISearchProviderStrategy, BingSearchProvider>();
builder.Services.AddScoped<ISearchProviderStrategy, GoogleSearchProvider>();

builder.Services.Configure<GoogleSearchOptions>(builder.Configuration.GetSection("GoogleSearch"));

builder.Services.AddHttpClient(ApiClientConstants.GoogleApiClient, config =>
{
    config.BaseAddress = new Uri(builder.Configuration.GetValue<string>("GoogleBaseAddress"));
});

builder.Services.AddCors(options =>
{
    options.AddPolicy("InfotrackWebApp", policy =>
    {
        policy
            .WithOrigins(builder.Configuration.GetValue<string>("InfotrackWebAppBaseAddress"))
            .AllowAnyMethod()
            .AllowAnyHeader();
    });
});

builder.Services.AddControllers(options =>
{
    options.Filters.Add<ValidationFilter>();
})
.ConfigureApiBehaviorOptions(options =>
{
    options.SuppressModelStateInvalidFilter = true;
});
builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddValidatorsFromAssemblyContaining<SearchEngineValidator>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();

    using (var scope = app.Services.CreateScope())
    {
        var context = scope.ServiceProvider.GetRequiredService<SearchEngineContext>();
        context.Database.EnsureCreated();
    }
}

//app.UseHttpsRedirection();

// Make sure to call it before UseAuthorization method.
app.UseCors("InfotrackWebApp");

app.UseAuthorization();

app.MapControllers();

app.Run();