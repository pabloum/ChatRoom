using System.Numerics;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: "AllowSpecificOrigins",
        policy =>
        {
            var urls = builder.Configuration.GetSection("AppUrlFE").Get<string[]>();
            foreach (var appUrl in urls)
            {
                policy.WithOrigins(appUrl)
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials();
            }
        });
});

var startup = new ChatRoom.Api.Startup(builder.Configuration);
startup.ConfigureServices(builder.Services);

var app = builder.Build();
startup.Configure(app, builder.Environment);