using System;
using AutoMapper;
using System.Collections.Generic;
using System.Linq;
using LiveLessons.BLL.DTO;
using LiveLessons.BLL.Exceptions;
using LiveLessons.BLL.Interfaces;
using LiveLessons.DAL.Entities;
using LiveLessons.DAL.Interfaces;
using System.Data.Entity.SqlServer;
using System.Data.Entity.Core.Objects;

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
            course.Teacher = _unitOfWork.Users.Find(user => user.ProfileId.Equals(courseDto.Teacher.ProfileId)).FirstOrDefault();

            _unitOfWork.Courses.Create(course);
            _unitOfWork.Save();
        }

        public void Edit(CourseDto courseDto)
        {
            var updatingCourse = _unitOfWork.Courses.Get(courseDto.Id);

            if (updatingCourse == null)
            {
                throw new EntityNotFoundException($"There is no Course with id { courseDto.Id } in the database.", "Course");
            }

            _mapper.Map(courseDto, updatingCourse);
            var teacher = _unitOfWork.Users.Find(user => user.ProfileId.Equals(courseDto.Teacher.ProfileId)).FirstOrDefault();
            updatingCourse.Teacher = teacher;

            _unitOfWork.Courses.Update(updatingCourse);
            _unitOfWork.Save();
        }

        public IEnumerable<CourseDto> FindNearest(double userCoordX, double userCoordY)
        {
            var courses = _unitOfWork.Courses.GetAll();
            var orderedCourses = OrderByDistance(courses, userCoordX, userCoordY).ToList();

            var coursesDto = _mapper.Map<IEnumerable<CourseDto>>(orderedCourses);

            return coursesDto;
        }

        public IEnumerable<CourseDto> Search(double userCoordX, double userCoordY, string searchString)
        {
            //var courses = _unitOfWork.Courses.GetAll().Where(
            //    x => x.Name.IndexOf(searchString, StringComparison.Ordinal) != -1
            //         || x.Description.IndexOf(searchString, StringComparison.Ordinal) != -1
            //         || x.Description.IndexOf(searchString, StringComparison.Ordinal) != -1);

            var courses = _unitOfWork.Courses.GetAll().Where(
                x => x.Name.Contains(searchString)
                     || x.Description.Contains(searchString)
                     || x.Description.Contains(searchString));

            var orderedCourses = OrderByDistance(courses, userCoordX, userCoordY).ToList();

            var coursesDto = _mapper.Map<IEnumerable<CourseDto>>(orderedCourses);

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
    }
}