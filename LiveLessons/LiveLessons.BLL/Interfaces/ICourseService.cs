using System.Collections.Generic;
using LiveLessons.BLL.DTO;

namespace LiveLessons.BLL.Interfaces
{
    public interface ICourseService
    {
        CourseDto Get(int id);
        IEnumerable<CourseDto> GetAll();
        CourseDto GetByUserId(int userId);
        IEnumerable<CourseDto> GetByProfileId(string profileId);
        IEnumerable<CourseDto> FindNearest(double userCoordX, double userCoordY, int page, int itemsPerPage);
        void Create(CourseDto courseDto);
        void Edit(CourseDto courseDto);
        void Delete(int id);

        IEnumerable<CourseDto> Search(
            double userCoordX,
            double userCoordY,
            string searchString,
            int page,
            int itemsPerPage);
    }
}