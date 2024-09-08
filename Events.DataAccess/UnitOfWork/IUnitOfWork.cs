using Events.DataAccess.Repositories;
using Events.Domain.Entities;

namespace Events.DataAccess.UnitOfWork
{
    public interface IUnitOfWork
    {
        public IRepository<LiveEvent> LiveEventRepository { get; }
        public IRepository<Participant> ParticipantRepository { get; }
        public IRepository<User> UserRepository { get; }
        Task SaveAsync();
    }
}
