namespace Events.Domain.DTO.LiveEventDtos
{
    public class CreateLiveEventRequestDto
    {
        public string Title { get; set; }

        public string Description { get; set; }

        public string Date { get; set; }

        public string Time { get; set; }

        public string Place { get; set; }

        public string Category { get; set; }

        public int MaxParticipants { get; set; }

        public string ImageUrl { get; set; }
    }
}
