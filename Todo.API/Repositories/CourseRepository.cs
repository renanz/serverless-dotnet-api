using System;
using System.Collections.Generic;
using Todo.API.DbContexts;
using Todo.API.Entities;
using Todo.API.Mappers;

namespace Todo.API.Repositories
{
    public class CourseRepository  : Repository<Course>, ICourseRepository
    {
        public CourseRepository(CourseLibraryContext context) : base(context)
        {
        }

        public IEnumerable<Course> GetAuthorCourses(Guid authorId)
        {
            return base.GetAll(course => course.AuthorId == authorId);
        }

        public Course GetAuthorCourse(Guid authorId, Guid id)
        {
            return base.GetOne(course => course.AuthorId == authorId && course.Id == id);
        }
    }
}