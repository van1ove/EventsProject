using Events.Business;
using Events.Business.UseCases.AuthUseCases.Login;
using Events.Business.UseCases.AuthUseCases.RefreshToken;
using Events.Business.UseCases.AuthUseCases.Register;
using Events.Business.UseCases.LiveEventUseCases.CreateLiveEvent;
using Events.Business.UseCases.LiveEventUseCases.DeleteLiveEvent;
using Events.Business.UseCases.LiveEventUseCases.GetAllLiveEvents;
using Events.Business.UseCases.LiveEventUseCases.GetLiveEvent;
using Events.Business.UseCases.LiveEventUseCases.UpdateLiveEvent;
using Events.Business.UseCases.UserUseCases.GetAllUsers;
using Events.Business.UseCases.UserUseCases.GetEventParticipants;
using Events.Business.UseCases.UserUseCases.GetParticipant;
using Events.Business.UseCases.UserUseCases.SubscribeEvent;
using Events.Business.UseCases.UserUseCases.UnsubscribeEvent;
using Events.DataAccess.Services.TokenService;
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
            builder.Services.AddScoped<ITokenService, TokenService>();
        }

        public static void ConfigurateUserUseCases(this WebApplicationBuilder builder)
        {
            builder.Services.AddScoped<IGetAllUsersUseCase, GetAllUsersUseCase>();
            builder.Services.AddScoped<IGetEventParticipantsUseCase, GetEventParticipantsUseCase>();
            builder.Services.AddScoped<IGetParticipantUseCase, GetParticipantUseCase>();
            builder.Services.AddScoped<ISubscribeEventUseCase, SubscribeEventUseCase>();
            builder.Services.AddScoped<IUnsubscribeEventUseCase, UnsubscribeEventUseCase>();
        }

        public static void ConfigurateLiveEventUseCases(this WebApplicationBuilder builder)
        {
            builder.Services.AddScoped<ICreateLiveEventUseCase, CreateLiveEventUseCase>();
            builder.Services.AddScoped<IGetAllLiveEventsUseCase, GetAllLiveEventsUseCase>();
            builder.Services.AddScoped<IGetLiveEventUseCase, GetLiveEventUseCase>();
            builder.Services.AddScoped<IUpdateLiveEventUseCase, UpdateLiveEventUseCase>();
            builder.Services.AddScoped<IDeleteLiveEventUseCase, DeleteLiveEventUseCase>();
        }

        public static void ConfigurateAuthUseCases(this WebApplicationBuilder builder)
        {
            builder.Services.AddScoped<ILoginUseCase, LoginUseCase>();
            builder.Services.AddScoped<IRegisterUseCase, RegisterUseCase>();
            builder.Services.AddScoped<IRefreshTokenUseCase, RefreshTokenUseCase>();
        }
    }
}
