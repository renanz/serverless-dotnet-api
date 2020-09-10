using System.ComponentModel.DataAnnotations;
using Todo.API.Models;

namespace Todo.API.ValidationAttributes
{
    public class DifferentCourseTitleAndDescriptionAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var course = (ManipulationCourseDto) validationContext.ObjectInstance;
            if (course.Description == course.Title)
            {
                return new ValidationResult(
                    ErrorMessage ?? "The Provided description should be different from title",
                    new[] { nameof(ManipulationCourseDto) }
                );
            }
            
            return ValidationResult.Success;
        }
    }
}