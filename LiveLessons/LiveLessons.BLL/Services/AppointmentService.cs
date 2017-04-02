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
    public class AppointmentService : IAppointmentService
    {
        private readonly IUnitOfWork _unitOfWork;

        public AppointmentService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        
        public AppointmentDto Get(int id)
        {
            var appointment = _unitOfWork.Appointments.Get(id);
            var appointmentDto = Mapper.Map<AppointmentDto>(appointment);

            return appointmentDto;
        }

        public IEnumerable<AppointmentDto> GetAll()
        {
            var appointments = _unitOfWork.Appointments.GetAll().ToList();
            var appointmentsDto = Mapper.Map<IEnumerable<AppointmentDto>>(appointments);

            return appointmentsDto;
        }

        public void Create(AppointmentDto appointmentDto)
        {
            var appointment = Mapper.Map<Appointment>(appointmentDto);
            appointment.Course = _unitOfWork.Courses.Get(appointmentDto.Course.Id);
            appointment.Student = _unitOfWork.Users.Get(appointmentDto.Student.Id);

            _unitOfWork.Appointments.Create(appointment);
            _unitOfWork.Save();
        }

        public void Edit(AppointmentDto appointmentDto)
        {
            var updatingAppointment = _unitOfWork.Appointments.Get(appointmentDto.Id);

            if (updatingAppointment == null)
            {
                throw new EntityNotFoundException($"There is no Appointment with id { appointmentDto.Id } in the database.", "Appointment");
            }

            Mapper.Map(appointmentDto, updatingAppointment);

            updatingAppointment.Course = _unitOfWork.Courses.Get(appointmentDto.Course.Id);
            updatingAppointment.Student = _unitOfWork.Users.Get(appointmentDto.Student.Id);

            _unitOfWork.Appointments.Update(updatingAppointment);
            _unitOfWork.Save();
        }

        public void Delete(int id)
        {
            _unitOfWork.Appointments.Delete(id);
            _unitOfWork.Save();
        }
    }
}