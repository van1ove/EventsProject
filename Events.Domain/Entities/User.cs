namespace Events.Domain.Entities
{
    public class User
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Surname { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public string Role { get; set; }

        public DateOnly Birthdate { get; set; }

        public DateOnly? RegistrationDate { get; set; }

        public virtual ICollection<LiveEvent> Events { get; set; } = [];

        public string RefreshToken { get; set; }
    }
}
