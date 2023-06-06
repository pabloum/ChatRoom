﻿using System;
using Security;
using Web.Providers;
using Web.Providers.Contracts;
using Web.Providers.Implementations;

namespace ChatBotWeb
{
    public class Startup
    {
        public IConfiguration configRoot { get; }

        public Startup(IConfiguration configuration)
        {
            configRoot = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAuthentication("MyCookieAuth").AddCookie("MyCookieAuth", options =>
            {
                options.Cookie.Name = "MyCookieAuth";
            });

            //services.AddRazorPages();

            services.AddControllers();
            services.AddEndpointsApiExplorer();

            services.AddScoped<IAuthentication, Authentication>();
            
            services.AddScoped<IMessageProvider, MessageProvider>();
            services.AddScoped<IRoomProvider, RoomProvider>();
            services.AddScoped<IStockProvider, StockProvider>();
            services.AddScoped<IServiceHandler, ServiceHandler>();

            services.AddHttpClient();

            //services.AddSignalR();
        }

        public void Configure(WebApplication app, IWebHostEnvironment env)
        {
            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseRouting();
            app.MapRazorPages();

            app.Run();
        }
    }
}