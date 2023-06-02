using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace Project.Models.Form
{
    public class ProjectForm
    {



        public int Id { get; set; } = 0;

        [Required(AllowEmptyStrings = false)]
        [MinLength(5)]
        public string Name { get; set; }
    }
}
