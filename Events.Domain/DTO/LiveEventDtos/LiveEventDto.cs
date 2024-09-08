namespace Events.Domain.DTO.LiveEventDtos
{
    public class LiveEventDto
    {
        public Guid Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public DateOnly Date { get; set; }

        public TimeOnly Time { get; set; }

        public string Place { get; set; }

        public string Category { get; set; }

        public int MaxParticipants { get; set; }

        public string ImageUrl { get; set; }
    }
}
