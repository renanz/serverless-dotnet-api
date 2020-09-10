using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Todo.API.ValidationAttributes;

namespace Todo.API.Models
{
    [DifferentCourseTitleAndDescription(ErrorMessage = "Different Course Title and Description.")]
    public class CreateCourseDto //: IValidatableObject
    {
        [Required(ErrorMessage = "Missing title")] [MaxLength(100)] public string Title { get; set; }

        [MaxLength(1500)] public string Description { get; set; }

        // public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        // {
        //     if (Title == Description)
        //     {
        //         yield return new ValidationResult(
        //             "The Provided description should be different from title",
        //             new[] {"CreateCourseDto"}
        //         );
        //     }
        // }
    }
}