using System.ComponentModel.DataAnnotations;

namespace API.DTOs
{
    public class CommandReadDto
    {
        public int Id { get; set; }
        public string HowTo { get; set; }
        public string Line { get; set; }
    }
}
