using AutoMapper;
using LiveLesson.WEB.ViewModels;
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