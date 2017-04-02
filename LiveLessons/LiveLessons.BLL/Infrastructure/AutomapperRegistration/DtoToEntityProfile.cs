using AutoMapper;
using LiveLessons.BLL.DTO;
using LiveLessons.DAL.Entities;

namespace LiveLessons.BLL.Infrastructure.AutomapperRegistration
{
    public class DtoToEntityProfile : Profile
    {
        public DtoToEntityProfile()
        {
            CreateMap<UserDto, User>();
            CreateMap<CourseDto, Course>()
                .ForMember(x => x.Teacher, options => options.Ignore());
            CreateMap<MessageDto, Message>();
            CreateMap<AppointmentDto, Appointment>()
                .ForMember(x => x.Student, options => options.Ignore())
                .ForMember(x => x.Course, options => options.Ignore()); ;
        }
    }
}
