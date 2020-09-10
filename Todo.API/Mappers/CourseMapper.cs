using System;
using Todo.API.Entities;
using Todo.API.Models;

namespace Todo.API.Mappers
{
    public static class CourseMapper
    {
        public static CourseDto ToCourseDto(Course course)
        {
            return new CourseDto()
            {
                Id = course.Id,
                Description = course.Description,
                Title = course.Title,
                AuthorId = course.AuthorId
            };
        }

        public static Course ToCourse(ManipulationCourseDto courseDto, Guid authorId, Course course = null)
        {
            if (course == null)
            {
                return new Course()
                {
                    AuthorId = authorId,
                    Description = courseDto.Description,
                    Title = courseDto.Title,
                };
            }

            course.Description = courseDto.Description;
            course.Title = courseDto.Title;
            return course;
        }
    }
}