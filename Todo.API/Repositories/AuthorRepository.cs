using System.Collections.Generic;
using System.Linq;
using Todo.API.DbContexts;
using Todo.API.Entities;
using Todo.API.ResourceParameters;

namespace Todo.API.Repositories
{
    public class AuthorRepository : Repository<Author>, IAuthorRepository
    {
        public AuthorRepository(CourseLibraryContext context) : base(context)
        {
        }

        public IEnumerable<Author> GetAll(AuthorsResourceParameters authorsResourceParameters)
        {
            if (string.IsNullOrWhiteSpace(authorsResourceParameters.MainCategory) && string.IsNullOrWhiteSpace(authorsResourceParameters.SearchQuery))
            {
                return base.GetAll();
            }

            var collection = _entity as IQueryable<Author>;

            if (!string.IsNullOrWhiteSpace(authorsResourceParameters.MainCategory))
            {
                collection = collection.Where(a => a.MainCategory == authorsResourceParameters.MainCategory.Trim());
            }

            if (!string.IsNullOrWhiteSpace(authorsResourceParameters.SearchQuery))
            {
                var searchQuery = authorsResourceParameters.SearchQuery.Trim();
                collection = collection.Where(a => a.MainCategory.Contains(searchQuery) ||
                                                   a.FirstName.Contains(searchQuery) ||
                                                   a.LastName.Contains(searchQuery));
            }

            return collection.ToList<Author>();
        }
    }
}