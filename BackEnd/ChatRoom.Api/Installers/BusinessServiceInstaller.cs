using ChatRoom.Common.FileParsers;
using ChatRoom.Common.HttpRequest;
using ChatRoom.Persistence.Context;
using ChatRoom.Persistence.InMemoryData;
using ChatRoom.Repository.Base;
using ChatRoom.Security;
using ChatRoom.Services.Base;
using ChatRoom.Services.Providers.Contracts;
using Microsoft.EntityFrameworkCore;

namespace ChatRoom.Api.Installers
{
    public static class BusinessServiceInstaller
    {
        public static void AddBusinessServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<IConfiguration>(configuration);

            services.RegisterAllDirectImplementations<IService>(ServiceLifetime.Scoped);
            services.RegisterAllDirectImplementations<IRepository>(ServiceLifetime.Scoped);
            services.RegisterAllDirectImplementations<IProvider>(ServiceLifetime.Scoped);

            services.AddDbContext<ChatRoomDbContext>(options =>
                options.UseSqlServer(configuration.GetValue<string>("ConnectionStrings:HotelBookinDatabase")));

            if (configuration.GetValue<bool>("UseDataBase"))
            {
                using (var context = services.BuildServiceProvider().GetRequiredService<ChatRoomDbContext>())
                {
                    context.Database.EnsureCreated();
                }
            }
        }

        public static void InjectAdditionalInterfaces(this IServiceCollection services)
        {
            services.AddSingleton<IInMemoryData, InMemoryData>();
            services.AddScoped<IAuthentication, Authentication>();
            services.AddScoped<IRequestHandler, RequestHandler>();
            services.AddScoped<ICsvParser, CsvParser>();
        }
    }
}