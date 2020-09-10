using Todo.API.Entities;
using System;
using System.Collections.Generic;
using Todo.API.ResourceParameters;

namespace Todo.API.Repositories
{
    public interface ICourseLibraryRepository
    {    
        IEnumerable<Course> GetCourses(Guid authorId);
        Course GetCourse(Guid authorId, Guid courseId);
        void AddCourse(Guid authorId, Course course);
        void UpdateCourse(Course course);
        void DeleteCourse(Course course);
        IEnumerable<Author> GetAuthors();
        Author GetAuthor(Guid authorId);
        IEnumerable<Author> GetAuthors(IEnumerable<Guid> authorIds);
        void AddAuthor(Author author);
        IEnumerable<Author> GetAuthors(AuthorsResourceParameters authorsResourceParameters);
        void DeleteAuthor(Author author);
        void UpdateAuthor(Author author);
        bool AuthorExists(Guid authorId);
        bool Save();
    }
}
