using System.ComponentModel.DataAnnotations;
using Todo.API.ValidationAttributes;

namespace Todo.API.Models
{
    [DifferentCourseTitleAndDescription(ErrorMessage = "Different Course Title and Description.")]
    public abstract class ManipulationCourseDto
    {
        [Required(ErrorMessage = "Missing title")]
        [MaxLength(100)]
        public string Title { get; set; }

        [MaxLength(1500)] public virtual string Description { get; set; }
    }
}