using AutoMapper;
using System.Collections.Generic;
using System.Linq;
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

        public CourseService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        
        public CourseDto Get(int id)
        {
            var course = _unitOfWork.Courses.Get(id);
            var courseDto = Mapper.Map<CourseDto>(course);

            return courseDto;
        }

        public CourseDto GetByUserId(int userId)
        {
            var course = _unitOfWork.Courses.Find(entity => entity.Teacher.Id.Equals(userId));
            var courseDto = Mapper.Map<CourseDto>(course);

            return courseDto;
        }

        public CourseDto GetByProfileId(string profileId)
        {
            var course = _unitOfWork.Courses.Find(entity => entity.Teacher.ProfileId.Equals(profileId));
            var courseDto = Mapper.Map<CourseDto>(course);

            return courseDto;
        }

        public IEnumerable<CourseDto> GetAll()
        {
            var courses = _unitOfWork.Courses.GetAll().ToList();
            var coursesDto = Mapper.Map<IEnumerable<CourseDto>>(courses);

            return coursesDto;
        }

        public void Create(CourseDto courseDto)
        {
            var course = Mapper.Map<Course>(courseDto);
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

            Mapper.Map(courseDto, updatingCourse);
            var teacher = _unitOfWork.Users.Find(user => user.ProfileId.Equals(courseDto.Teacher.ProfileId)).FirstOrDefault();
            updatingCourse.Teacher = teacher;

            _unitOfWork.Courses.Update(updatingCourse);
            _unitOfWork.Save();
        }

        public void Delete(int id)
        {
            _unitOfWork.Courses.Delete(id);
            _unitOfWork.Save();
        }
    }
}