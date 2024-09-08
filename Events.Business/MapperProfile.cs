using AutoMapper;
using Events.Domain.DTO.LiveEventDtos;
using Events.Domain.DTO.Participant;
using Events.Domain.DTO.ParticipantDtos;
using Events.Domain.Entities;
using Events.Domain.Models.User;

namespace Events.Business
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMapForLiveEvent();
            CreateMapForParticipant();
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
        }

        private void CreateMapForParticipant() 
        {
            CreateMap<Participant, ParticipantDto>();
            CreateMap<CreateParticipantRequestDto, Participant>();
        }
    }
}
