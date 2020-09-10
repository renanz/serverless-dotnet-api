using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Todo.API.ValidationAttributes;

namespace Todo.API.Models
{
    public class UpdateCourseDto : ManipulationCourseDto
    {
        [Required(ErrorMessage = "Description should not be empty")]
        public override string Description
        {
            get => base.Description;
            set => base.Description = value;
        }
    }
}