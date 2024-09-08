using Events.Business;
using Events.Business.Services.AuthService;
using Events.Business.Services.LiveEventService;
using Events.Business.Services.UserService;
using Events.DataAccess.UnitOfWork;

namespace Events_Web_Application.Configuration
{
    public static class ConfigurationExtension
    {
        public static void ConfigurateUnitOfWork(this WebApplicationBuilder builder)
        {
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
        }

        public static void ConfigurateServices(this WebApplicationBuilder builder)
        {
            builder.Services.AddAutoMapper(typeof(MapperProfile));
            builder.Services.AddScoped<ILiveEventService, LiveEventsService>();
            builder.Services.AddScoped<IUserService, UserService>();
            builder.Services.AddScoped<IAuthService, AuthService>();
        }
    }
}
