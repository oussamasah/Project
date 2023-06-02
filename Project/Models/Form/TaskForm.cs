using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace Project.Models.Form
{
    public class TaskForm
    {
        public int Id { get; set; } = 0;
        [Required]
        [MinLength(8)]
        public string Title { get; set; }

        public string Description { get; set; }

        [Required]
        [EnumDataType(typeof(enumPriority))]
        public string Priority { get; set; }
        [Required]
        [EnumDataType(typeof(enumStatus))]
        public string Status { get; set; } = "Holding";

        [Required]
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        [AllowNull]
        public DateTime ClosedAt { get; set; }

        [Required]
        public int Project { get; set; }
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

}
