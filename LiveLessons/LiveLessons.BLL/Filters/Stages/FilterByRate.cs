using System.Linq;
using LiveLessons.BLL.Interfaces;
using LiveLessons.DAL.Entities;

namespace LiveLessons.BLL.Filters.Stages
{
    class FilterByRate : IFilter<IQueryable<Course>>
    {
        private readonly decimal min;
        private readonly decimal max;
        public FilterByRate(decimal min, decimal max)
        {
            this.min = min;
            this.max = max;
        }

        public IQueryable<Course> Execute(IQueryable<Course> input)
        {
            input = input.Where(course => course.Rate >= min && course.Rate <= max);

            return input;
        }
    }
}
