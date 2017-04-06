using System;
using LiveLessons.DAL.EF;
using LiveLessons.DAL.Entities;
using LiveLessons.DAL.Interfaces;
using LiveLessons.DAL.Repositories;

namespace LiveLessons.DAL.UnitsOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DatabaseContext _databaseContext;
        private IRepository<User> _userRepository;
        private IRepository<Course> _courseRepository;
        private IRepository<Message> _messageRepository;
        private IRepository<Appointment> _appointmentRepository;

        private bool _disposed;

        public UnitOfWork(string databaseConnectionString)
        {
            _databaseContext = new DatabaseContext(databaseConnectionString);
        }

        public IRepository<User> Users => _userRepository ?? (_userRepository = new CommonRepository<User>(_databaseContext));

        public IRepository<Course> Courses => _courseRepository ?? (_courseRepository = new CommonRepository<Course>(_databaseContext));

        public IRepository<Message> Messages => _messageRepository ?? (_messageRepository = new CommonRepository<Message>(_databaseContext));

        public IRepository<Appointment> Appointments => _appointmentRepository ?? (_appointmentRepository = new CommonRepository<Appointment>(_databaseContext));

        public void Save()
        {
            _databaseContext.SaveChanges();
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
                _databaseContext.Dispose();
            }

            _disposed = true;
        }
    }
}
