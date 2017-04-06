using System.Linq;
using LiveLessons.BLL.Interfaces;
using LiveLessons.DAL.Entities;

namespace LiveLessons.BLL.Filters.Stages
{
    public class SearchBySubstring : IFilter<IQueryable<Course>>
    {
        private readonly string searchString;

        public SearchBySubstring(string searchString)
        {
            this.searchString = searchString;
        }

        public IQueryable<Course> Execute(IQueryable<Course> input)
        {
            input = input.Where(
               x => x.Name.ToLower().Contains(searchString.ToLower())
                    || x.Description.ToLower().Contains(searchString.ToLower())
                    || x.Description.ToLower().Contains(searchString.ToLower()));

            return input;
        }
    }
}
