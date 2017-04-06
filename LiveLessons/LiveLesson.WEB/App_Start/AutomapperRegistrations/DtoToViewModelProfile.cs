using AutoMapper;
using LiveLesson.WEB.ViewModels;
using LiveLesson.WEB.ViewModels.Appointment;
using LiveLesson.WEB.ViewModels.Course;
using LiveLesson.WEB.ViewModels.Message;
using LiveLesson.WEB.ViewModels.User;
using LiveLessons.BLL.DTO;

namespace LiveLesson.WEB.AutomapperRegistrations
{
    public class DtoToViewModelProfile : Profile
    {
        public DtoToViewModelProfile()
        {
            CreateMap<UserDto, UserViewModel>();
            CreateMap<CourseDto, CourseViewModel>();
            CreateMap<MessageDto, MessageViewModel>();
            CreateMap<AppointmentDto, AppointmentViewModel>();
        }
    }
}