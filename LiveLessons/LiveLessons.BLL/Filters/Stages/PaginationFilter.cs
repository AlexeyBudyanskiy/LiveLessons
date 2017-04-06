using System.Linq;
using LiveLessons.BLL.Interfaces;
using LiveLessons.DAL.Entities;

namespace LiveLessons.BLL.Filters.Stages
{
    public class PaginationFilter : IFilter<IQueryable<Course>>
    {
        private readonly int page;
        private readonly int itemsPerPage;

        public PaginationFilter(int page, int itemsPerPage)
        {
            this.page = page;
            this.itemsPerPage = itemsPerPage;
        }

        public IQueryable<Course> Execute(IQueryable<Course> input)
        {
            input = input.Skip(page * itemsPerPage).Take(itemsPerPage);

            return input;
        }
    }
}
