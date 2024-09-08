using System.ComponentModel.DataAnnotations;

namespace Events.Domain.Entities
{
    public class LiveEventImage
    {
        [Key]
        public Guid Id { get; set; }

        public string FileName { get; set; }

        public string FileExtention { get; set; }

        public string Url { get; set; }
    }
}
