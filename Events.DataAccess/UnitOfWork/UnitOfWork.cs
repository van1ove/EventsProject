using Events.DataAccess.Contexts;
using Events.DataAccess.Repositories;
using Events.Domain.Entities;

namespace Events.DataAccess.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        public IRepository<LiveEvent> LiveEventRepository { get; private set; }
        public IRepository<Participant> ParticipantRepository { get; private set; }
        public IRepository<User> UserRepository { get; private set; }

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
            LiveEventRepository = new Repository<LiveEvent>(context);
            ParticipantRepository = new Repository<Participant>(context);
            UserRepository = new Repository<User>(context);
        }

        public async Task SaveAsync() => await _context.SaveChangesAsync();
    }
}
