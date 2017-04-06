using System.Linq;
using LiveLessons.BLL.Interfaces;
using LiveLessons.DAL.Entities;

namespace LiveLessons.BLL.Filters.Stages
{
    public class FilterByPrice : IFilter<IQueryable<Course>>
    {
        private readonly decimal min;
        private readonly decimal max;
        public FilterByPrice(decimal min, decimal max)
        {
            this.min = min;
            this.max = max;
        }

        public IQueryable<Course> Execute(IQueryable<Course> input)
        {
            input = input.Where(course => course.Price >= min && course.Price <= max);

            return input;
        }
    }
}
