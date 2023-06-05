using ChatRoom.Api.ChatHub;
using ChatRoom.Api.Installers;
using ChatRoom.Api.Middleware;

namespace ChatRoom.Api
{
    public partial class Startup
    {
        public IConfiguration configRoot { get; }

        public Startup(IConfiguration configuration)
        {
            configRoot = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            // services.AddAuthentication( ...

            services.AddControllers();

            services.AddEndpointsApiExplorer();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "Chat Room API", Version = "v1" });
            });

            services.AddSignalR();
            services.AddBusinessServices(configRoot);
            services.InjectAdditionalInterfaces();
        }

        public void Configure(WebApplication app, IWebHostEnvironment env)
        {
            // Configure the HTTP request pipeline
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            else
            {
                app.UseHttpsRedirection();
            }

            app.UseCors("AllowSpecificOrigins");
            app.UseMiddleware<ExceptionHandlingMiddleware>();

            //app.UseAuthentication();
            //app.UseAuthorization();
            //app.UseResponseLogging();

            app.UseRouting();

            app.UseEndpoints(e =>
            {
                e.MapHub<HubImplementation>("/chat");
            });

            app.MapControllers();

            app.Run();
        }
    }

}

