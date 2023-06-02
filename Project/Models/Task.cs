using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Project.Models
{
    public class Tasks
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MinLength(8)]
        public string Title { get; set; }

        public string Description { get; set; }

        [Required]
        [EnumDataType(typeof(enumPriority))]
        public string Priority { get; set; }
        [Required]
        [EnumDataType(typeof(enumStatus))]
        public string Status { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime ClosedAt { get; set; } 


        [Required]
        [ForeignKey(nameof(Projects))]
        public int Project { get; set; }
    }
}

enum enumPriority
{
    Hight,
    Medium,
    Low

}

enum enumStatus
{
    Holding,
    Processing,
    Pending,
    Done

}
