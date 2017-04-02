using System;
using LiveLessons.DAL.Entities;

namespace LiveLessons.DAL.Interfaces
{
    public interface IUnitOfWork : IDisposable
    { 
		IRepository<User> Users { get; }

		IRepository<Course> Courses { get; }

		IRepository<Message> Messages { get; }

		IRepository<Appointment> Appointments { get; }

        void Save();
    }
}
