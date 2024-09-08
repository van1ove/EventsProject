using AutoMapper;
using Events.DataAccess.UnitOfWork;
using Events.Domain.DTO.ParticipantDtos;
using Events.Domain.Entities;

namespace Events.Business.Services.ParticipantService
{
    public class ParticipantService : IParticipantService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ParticipantService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ParticipantDto>> GetallParticipants()
        {
            IEnumerable<Participant> participants = await _unitOfWork.ParticipantRepository.GetAllAsync();
            List<ParticipantDto> response = new List<ParticipantDto>();

            foreach (Participant participant in participants) 
            {
                response.Add(_mapper.Map<ParticipantDto>(participant));
            }

            return response;
        }

        public async Task<ParticipantDto?> GetParticipantById(Guid id)
        {
            Participant? participant = await _unitOfWork.ParticipantRepository.GetAsync(model => model.Id == id);
            if (participant is null)
                return null;

            ParticipantDto response = _mapper.Map<ParticipantDto>(participant);
            return response; 
        }
    }
}
