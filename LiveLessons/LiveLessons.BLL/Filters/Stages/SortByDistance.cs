using System.Data.Entity.SqlServer;
using System.Linq;
using LiveLessons.BLL.Interfaces;
using LiveLessons.DAL.Entities;

namespace LiveLessons.BLL.Filters.Stages
{
    public class SortByDistance : IFilter<IQueryable<Course>>
    {
        private readonly double userCoordX;
        private readonly double userCoordY;

        public SortByDistance(double userCoordX, double userCoordY)
        {
            this.userCoordX = userCoordX;
            this.userCoordY = userCoordY;
        }

        public IQueryable<Course> Execute(IQueryable<Course> input)
        {
            input = input.OrderBy(
                x => SqlFunctions.SquareRoot(
                    ((x.CoordX - userCoordX) * (x.CoordX - userCoordX))
                    + ((x.CoordY - userCoordY) * (x.CoordY - userCoordY))));

            return input;
        }
    }
}
