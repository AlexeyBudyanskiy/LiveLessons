using AutoMapper;
using LiveLesson.WEB.ViewModels;
using LiveLesson.WEB.ViewModels.Appointment;
using LiveLesson.WEB.ViewModels.Course;
using LiveLesson.WEB.ViewModels.Message;
using LiveLesson.WEB.ViewModels.User;
using LiveLessons.BLL.DTO;

namespace LiveLesson.WEB.AutomapperRegistrations
{
    public class ViewModelToDtoProfile : Profile
    {
        public ViewModelToDtoProfile()
        {
            CreateMap<UserViewModel, UserDto>();
            CreateMap<CourseViewModel, CourseDto>();
            CreateMap<CreateCourseViewModel, CourseDto>();
            CreateMap<MessageViewModel, MessageDto>();
            CreateMap<CreateMessageViewModel, MessageDto>()
                .BeforeMap((model, dto) => dto.Course = new CourseDto {Id = model.CourseId})
                .BeforeMap((model, dto) => dto.Reciever = new UserDto {Id = model.RecieverId});
            CreateMap<AppointmentViewModel, AppointmentDto>();
            CreateMap<CreateAppointmentViewModel, AppointmentDto>()
                .BeforeMap((model, dto) => dto.Course = new CourseDto { Id = model.CourseId })
                .BeforeMap((model, dto) => dto.Student = new UserDto { Id = model.StudentId });
        }
    }
}