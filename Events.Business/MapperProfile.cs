using AutoMapper;
using Events.Business.Utility;
using Events.Domain.DTO.AuthDtos;
using Events.Domain.DTO.LiveEventDtos;
using Events.Domain.Entities;
using Events.Domain.Models.User;

namespace Events.Business
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMapForLiveEvent();
            CreateMapForUser();
        }

        private void CreateMapForLiveEvent()
        {
            CreateMap<LiveEvent, LiveEventDto>();

            CreateMap<CreateLiveEventRequestDto, LiveEvent>()
                .ForMember(dest => dest.Date, opt => opt.MapFrom(src => DateOnly.Parse(src.Date)))
                .ForMember(dest => dest.Time, opt => opt.MapFrom(src => TimeOnly.Parse(src.Time)));
            CreateMap<UpdateLiveEventRequestDto, LiveEvent>();
        }

        private void CreateMapForUser() 
        {
            CreateMap<User, UserModel>();

            CreateMap<RegisterRequestDto, User>()
                .ForMember(dest => dest.Password, opt => opt.MapFrom(src => src.Password.GetHash()))
                .ForMember(dest => dest.Role, opt => opt.MapFrom(src => "User"))
                .ForMember(dest => dest.RegistrationDate, opt => opt.MapFrom(src => DateTime.UtcNow.Date.ToDateOnly()));
        }
    }
}
