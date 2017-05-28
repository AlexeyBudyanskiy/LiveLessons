using System;
using LiveLessons.DAL.EF;
using LiveLessons.DAL.Entities;
using LiveLessons.DAL.Interfaces;
using LiveLessons.DAL.Repositories;

namespace LiveLessons.DAL.UnitsOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DatabaseContext databaseContext;
        private IRepository<User> userRepository;
        private IRepository<Course> courseRepository;
        private IRepository<Message> messageRepository;
        private IRepository<Appointment> appointmentRepository;

        private bool _disposed;

        public UnitOfWork(string databaseConnectionString)
        {
            databaseContext = new DatabaseContext(databaseConnectionString);
        }

        public IRepository<User> Users => 
            userRepository ?? (userRepository = new CommonRepository<User>(databaseContext));

        public IRepository<Course> Courses => 
            courseRepository ?? (courseRepository = new CommonRepository<Course>(databaseContext));

        public IRepository<Message> Messages => 
            messageRepository ?? (messageRepository = new CommonRepository<Message>(databaseContext));

        public IRepository<Appointment> Appointments => 
            appointmentRepository ?? (appointmentRepository = new CommonRepository<Appointment>(databaseContext));

        public void Save()
        {
            databaseContext.SaveChanges();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (_disposed)
            {
                return;
            }

            if (disposing)
            {
                databaseContext.Dispose();
            }

            _disposed = true;
        }
    }
}
