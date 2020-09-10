using System;
using System.Collections.Generic;
using Todo.API.Entities;

namespace Todo.API.Repositories
{
    public interface ICourseRepository : IRepository<Course>
    {
        IEnumerable<Course> GetAuthorCourses(Guid authorId);
        Course GetAuthorCourse(Guid authorId, Guid id);
    }
}