using System.Collections.Generic;
using Todo.API.Entities;
using Todo.API.ResourceParameters;

namespace Todo.API.Repositories
{
    public interface IAuthorRepository : IRepository<Author>
    {
        IEnumerable<Author> GetAll(AuthorsResourceParameters authorsResourceParameters);
    }
}