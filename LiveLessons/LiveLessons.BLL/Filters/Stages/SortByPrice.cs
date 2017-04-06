using System.Linq;
using LiveLessons.BLL.Interfaces;
using LiveLessons.DAL.Entities;

namespace LiveLessons.BLL.Filters.Stages
{
    public class SortByPrice : IFilter<IQueryable<Course>>
    {
        public IQueryable<Course> Execute(IQueryable<Course> input)
        {
            input = input.OrderBy(course => course.Price);

            return input;
        }
    }
}
