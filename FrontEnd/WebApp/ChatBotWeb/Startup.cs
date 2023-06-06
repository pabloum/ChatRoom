using System;
using System.Net.Http;
using System.Net.Http.Headers;
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
                options.LoginPath = "/Login";
                options.AccessDeniedPath = "/AccesDenied";
            });

            services.AddAuthorization(options =>
            {
                options.AddPolicy("MustBeEvaluator",
                    policy => policy.RequireClaim("Department", "Evaluator"));
            });

            //services.AddRazorPages();

            services.AddControllers();
            services.AddEndpointsApiExplorer();

            services.AddSingleton<IAuthentication, Authentication>();
            
            services.AddScoped<IMessageProvider, MessageProvider>();
            services.AddScoped<IRoomProvider, RoomProvider>();
            services.AddScoped<IStockProvider, StockProvider>();
            services.AddScoped<IServiceHandler, ServiceHandler>();

            services.AddHttpClient();
            // services.AddHttpClient("BackendApi", client =>
            // {
            //     client.BaseAddress = new Uri("https://localhost:2701/");
            //     client.DefaultRequestHeaders.Accept.Clear();
            //     client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            // });

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

            app.UseRouting();
            app.MapRazorPages();

            app.UseAuthentication();
            app.UseAuthorization();

            app.Run();
        }
    }
}