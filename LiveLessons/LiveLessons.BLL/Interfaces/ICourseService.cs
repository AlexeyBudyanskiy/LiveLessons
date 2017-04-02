using System.Collections.Generic;
using LiveLessons.BLL.DTO;

namespace LiveLessons.BLL.Interfaces
{
    public interface ICourseService
    {
        CourseDto Get(int id);
        IEnumerable<CourseDto> GetAll();
        CourseDto GetByUserId(int userId);
        CourseDto GetByProfileId(string profileId);
        void Create(CourseDto courseDto);
        void Edit(CourseDto courseDto);
        void Delete(int id);
    }
}