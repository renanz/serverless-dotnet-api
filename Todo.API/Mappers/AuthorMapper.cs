using System;
using System.Linq;
using Todo.API.Entities;
using Todo.API.Helpers;
using Todo.API.Models;

namespace Todo.API.Mappers
{
    public static class AuthorMapper
    {
        public static AuthorDto ToAuthorDto(Author author)
        {
            return new AuthorDto()
            {
                Age = author.DateOfBirth.GetCurrentAge(),
                Id = author.Id,
                Name = $"{author.FirstName} {author.LastName}",
                MainCategory = author.MainCategory
            };
        }

        public static Author ToAuthor(CreateAuthorDto author)
        {
            return new Author()
            {
                FirstName = author.FirstName,
                LastName = author.LastName,
                MainCategory = author.MainCategory,
                DateOfBirth = author.DateOfBirth,
                Courses = author.Courses.Select(course => CourseMapper.ToCourse(course, Guid.Empty)).ToList()
            };
        }
    }
}