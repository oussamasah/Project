using System.ComponentModel.DataAnnotations;

namespace Project.Models
{
    public class Projects
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MinLength(5)]
        public string Name { get; set; }
        [Required]
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
