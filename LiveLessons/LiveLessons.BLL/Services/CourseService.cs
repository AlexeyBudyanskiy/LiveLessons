using System.Data.Entity.SqlServer;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using LiveLessons.BLL.DTO;
using LiveLessons.BLL.Exceptions;
using LiveLessons.BLL.Interfaces;
using LiveLessons.DAL.Entities;
using LiveLessons.DAL.Interfaces;

namespace LiveLessons.BLL.Services
{
    public class CourseService : ICourseService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CourseService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public CourseDto Get(int id)
        {
            var course = _unitOfWork.Courses.Get(id);
            var courseDto = _mapper.Map<CourseDto>(course);

            return courseDto;
        }

        public CourseDto GetByUserId(int userId)
        {
            var course = _unitOfWork.Courses.Find(entity => entity.Teacher.Id.Equals(userId));
            var courseDto = _mapper.Map<CourseDto>(course);

            return courseDto;
        }

        public CourseDto GetByProfileId(string profileId)
        {
            var course = _unitOfWork.Courses.Find(entity => entity.Teacher.ProfileId.Equals(profileId));
            var courseDto = _mapper.Map<CourseDto>(course);

            return courseDto;
        }

        public IEnumerable<CourseDto> GetAll()
        {
            var courses = _unitOfWork.Courses.GetAll().ToList();
            var coursesDto = _mapper.Map<IEnumerable<CourseDto>>(courses);

            return coursesDto;
        }

        public void Create(CourseDto courseDto)
        {
            var course = _mapper.Map<Course>(courseDto);
            course.Teacher =
                _unitOfWork.Users.Find(user => user.ProfileId.Equals(courseDto.Teacher.ProfileId)).FirstOrDefault();

            _unitOfWork.Courses.Create(course);
            _unitOfWork.Save();
        }

        public void Edit(CourseDto courseDto)
        {
            var updatingCourse = _unitOfWork.Courses.Get(courseDto.Id);

            if (updatingCourse == null)
            {
                var exceptionMessage = $"There is no Course with id {courseDto.Id} in the database.";
                throw new EntityNotFoundException(exceptionMessage, "Course");
            }

            _mapper.Map(courseDto, updatingCourse);
            var teacher =
                _unitOfWork.Users.Find(user => user.ProfileId.Equals(courseDto.Teacher.ProfileId)).FirstOrDefault();
            updatingCourse.Teacher = teacher;

            _unitOfWork.Courses.Update(updatingCourse);
            _unitOfWork.Save();
        }

        public IEnumerable<CourseDto> FindNearest(double userCoordX, double userCoordY, int page, int itemsPerPage)
        {
            var courses = _unitOfWork.Courses.GetAll();
            var orderedCourses = OrderByDistance(courses, userCoordX, userCoordY);
            var paginatedCourses = Paginate(orderedCourses, page, itemsPerPage).ToList();
            var coursesDto = _mapper.Map<IEnumerable<CourseDto>>(paginatedCourses);

            return coursesDto;
        }

        public IEnumerable<CourseDto> Search(
            double userCoordX,
            double userCoordY,
            string searchString,
            int page,
            int itemsPerPage)
        {
            var courses = _unitOfWork.Courses.GetAll().Where(
                x => x.Name.ToLower().Contains(searchString.ToLower())
                     || x.Description.ToLower().Contains(searchString.ToLower())
                     || x.Description.ToLower().Contains(searchString.ToLower()));

            var orderedCourses = OrderByDistance(courses, userCoordX, userCoordY);
            var paginatedCourses = Paginate(orderedCourses, page, itemsPerPage).ToList();
            var coursesDto = _mapper.Map<IEnumerable<CourseDto>>(paginatedCourses);

            return coursesDto;
        }

        public void Delete(int id)
        {
            _unitOfWork.Courses.Delete(id);
            _unitOfWork.Save();
        }

        private IQueryable<Course> OrderByDistance(IQueryable<Course> courses, double userCoordX, double userCoordY)
        {
            var orderedCourses = courses.OrderBy(
                x => SqlFunctions.SquareRoot(
                    (x.CoordX - userCoordX) * (x.CoordX - userCoordX)
                    + (x.CoordY - userCoordY) * (x.CoordY - userCoordY)));

            return orderedCourses;
        }

        private IQueryable<Course> Paginate(IQueryable<Course> courses, int page, int itemsPerPage)
        {
            courses = courses.Skip(page * itemsPerPage).Take(itemsPerPage);

            return courses;
        }
    }
}