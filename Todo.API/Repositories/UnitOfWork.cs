using System;
using Todo.API.DbContexts;

namespace Todo.API.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly CourseLibraryContext _context;
        private readonly IAuthorRepository _authorRepository;
        private readonly ICourseRepository _courseRepository;

        public UnitOfWork(CourseLibraryContext context, IAuthorRepository authorRepository, ICourseRepository courseRepository)
        {
            _context = context;
            _authorRepository = authorRepository ?? throw new ArgumentNullException(nameof(authorRepository));
            _courseRepository = courseRepository ?? throw new ArgumentNullException(nameof(courseRepository));
        }

        public IAuthorRepository AuthorRepository
        {
            get { return _authorRepository; }
        }
        
        public ICourseRepository CourseRepository
        {
            get { return _courseRepository; }
        }

        public void Commit()
        {
            _context.SaveChanges();
        }

        public void Rollback()
        {
            _context.Dispose();
        }
    }
}