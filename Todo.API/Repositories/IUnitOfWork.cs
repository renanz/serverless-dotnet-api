namespace Todo.API.Repositories
{
    public interface IUnitOfWork
    {
        IAuthorRepository AuthorRepository { get;  }
        ICourseRepository CourseRepository { get;  }
        void Commit();
        void Rollback();
    }
}