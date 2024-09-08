namespace Events.Domain.Entities
{
    public class Participant
    {
        public Guid Id { get; set; }

        public string Name { get; set; }
        
        public string Surname { get; set; }
        
        public DateOnly BirthDate { get; set; }

        public DateOnly? RegistrationDate { get; set; } = new DateOnly();

        public LiveEvent? LiveEvent { get; set; } = new LiveEvent();
        
        public string Email { get; set; }
    }
}
