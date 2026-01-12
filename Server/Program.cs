using Lib.Net.Http.WebPush;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using System.Net.Http;

var builder = WebApplication.CreateBuilder(args);

// 1. Add Controllers
builder.Services.AddControllers();

// 2. Configure PushServiceClient (Optimized & Secure)
builder.Services.AddHttpClient<PushServiceClient>();
builder.Services.AddSingleton<PushServiceClient>(sp => 
{
    var config = sp.GetRequiredService<IConfiguration>();
    var httpClient = sp.GetRequiredService<HttpClient>();
    
    // Pulls from User Secrets locally, or appsettings.json
    var publicKey = config["Vapid:PublicKey"];
    var privateKey = config["Vapid:PrivateKey"];
    var subject = config["Vapid:Subject"] ?? "mailto:admin@example.com";

    if (string.IsNullOrEmpty(publicKey) || string.IsNullOrEmpty(privateKey))
    {
        // This helps you debug if your secrets aren't loaded correctly
        throw new System.Exception("VAPID keys are missing! Run 'dotnet user-secrets set' to add them.");
    }

    var client = new PushServiceClient(httpClient);
    client.DefaultAuthentication = new Lib.Net.Http.WebPush.Authentication.VapidAuthentication(publicKey, privateKey)
    {
        Subject = subject
    };
    return client;
});

// 3. Configure CORS
builder.Services.AddCors(options => {
    options.AddPolicy("AllowClient", policy => {
        policy.WithOrigins("http://localhost:5184", "http://127.0.0.1:5184") 
              .AllowAnyMethod()
              .AllowAnyHeader()
              .AllowCredentials();
    });
});

var app = builder.Build();

// 4. Middleware Pipeline
app.UseRouting();
app.UseCors("AllowClient"); 

app.MapControllers();

app.Run();