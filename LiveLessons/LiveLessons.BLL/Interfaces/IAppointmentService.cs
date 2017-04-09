using System.Collections.Generic;
using LiveLessons.BLL.DTO;

namespace LiveLessons.BLL.Interfaces
{
    public interface IAppointmentService
    {
        AppointmentDto Get(int id);
        IEnumerable<AppointmentDto> GetAll();
        IEnumerable<AppointmentDto> GetByProfileId(string profileId);
        void Create(AppointmentDto appointmentDto);
        void Edit(AppointmentDto appointmentDto);
        void Delete(int id);
    }
}
