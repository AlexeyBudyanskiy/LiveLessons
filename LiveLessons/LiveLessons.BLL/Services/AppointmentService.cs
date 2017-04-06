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
    public class AppointmentService : IAppointmentService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public AppointmentService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }
        
        public AppointmentDto Get(int id)
        {
            var appointment = unitOfWork.Appointments.Get(id);
            var appointmentDto = mapper.Map<AppointmentDto>(appointment);

            return appointmentDto;
        }

        public IEnumerable<AppointmentDto> GetAll()
        {
            var appointments = unitOfWork.Appointments.GetAll().ToList();
            var appointmentsDto = mapper.Map<IEnumerable<AppointmentDto>>(appointments);

            return appointmentsDto;
        }

        public void Create(AppointmentDto appointmentDto)
        {
            var appointment = mapper.Map<Appointment>(appointmentDto);
            appointment.Course = unitOfWork.Courses.Get(appointmentDto.Course.Id);
            appointment.Student = unitOfWork.Users.Get(appointmentDto.Student.Id);

            unitOfWork.Appointments.Create(appointment);
            unitOfWork.Save();
        }

        public void Edit(AppointmentDto appointmentDto)
        {
            var updatingAppointment = unitOfWork.Appointments.Get(appointmentDto.Id);

            if (updatingAppointment == null)
            {
                throw new EntityNotFoundException($"There is no Appointment with id { appointmentDto.Id } in the database.", "Appointment");
            }

            Mapper.Map(appointmentDto, updatingAppointment);

            updatingAppointment.Course = unitOfWork.Courses.Get(appointmentDto.Course.Id);
            updatingAppointment.Student = unitOfWork.Users.Get(appointmentDto.Student.Id);

            unitOfWork.Appointments.Update(updatingAppointment);
            unitOfWork.Save();
        }

        public void Delete(int id)
        {
            unitOfWork.Appointments.Delete(id);
            unitOfWork.Save();
        }
    }
}