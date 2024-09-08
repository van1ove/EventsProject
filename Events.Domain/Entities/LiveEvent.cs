using System.Diagnostics.CodeAnalysis;

namespace Events.Domain.Entities
{
    public class LiveEvent
    {
        public Guid Id { get; set; }
        
        public string Title { get; set; }

        [AllowNull]
        public string Description { get; set; }
        
        public DateOnly Date { get; set; }

        public TimeOnly Time { get; set; }

        public string Place { get; set; }
        
        public string Category { get; set; }
        
        public int MaxParticipants { get; set; }

        public virtual ICollection<User> Participants { get; set; } = [];

        [AllowNull]
        public string ImageUrl { get; set; }
    }
}
