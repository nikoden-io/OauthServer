using Duende.IdentityServer.Services;
using Duende.IdentityServer.Validation;
using OauthServer.Domain.Interfaces;
using OauthServer.Infrastructure.Data;
using OauthServer.Infrastructure.Repositories;
using OauthServer.Presentation;
using OauthServer.Presentation.Configuration;
using OauthServer.Presentation.Services;
using OauthServer.Presentation.Validators;

var builder = WebApplication.CreateBuilder(args);

// Load configuration
var configuration = builder.Configuration;

builder.Logging.ClearProviders();
builder.Logging.AddConsole();

// Configure MongoDB settings
builder.Services.Configure<MongoDbSettings>(
    configuration.GetSection("MongoDbSettings"));

// Register MongoDB context
builder.Services.AddSingleton<MongoDbContext>();

// Register repositories
builder.Services.AddScoped<IUserRepository, UserRepository>();

// Register custom services for IdentityServer
builder.Services.AddScoped<IResourceOwnerPasswordValidator, CustomResourceOwnerPasswordValidator>();
builder.Services.AddScoped<IProfileService, CustomProfileService>();

// Configure IdentityServer
builder.Services.AddIdentityServer(options =>
    {
        options.Events.RaiseErrorEvents = true;
        options.Events.RaiseInformationEvents = true;
        options.Events.RaiseFailureEvents = true;
        options.Events.RaiseSuccessEvents = true;
    })
    .AddInMemoryIdentityResources(Config.IdentityResources)
    .AddInMemoryApiScopes(Config.ApiScopes)
    .AddInMemoryClients(Config.Clients)
    .AddDeveloperSigningCredential(); // Add developer signing credential for development


// Add controllers
builder.Services.AddControllers();

var hashedPassword = BCrypt.Net.BCrypt.HashPassword("testpassword");
Console.WriteLine(hashedPassword);

// Build the app
var app = builder.Build();

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment()) app.UseDeveloperExceptionPage();

app.UseIdentityServer();

app.UseRouting();

app.UseAuthorization();

app.MapControllers();

app.Run();