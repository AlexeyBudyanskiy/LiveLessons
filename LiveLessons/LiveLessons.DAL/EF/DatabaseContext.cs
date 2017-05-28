using System.Data.Entity;
using LiveLessons.DAL.Entities;

namespace LiveLessons.DAL.EF
{
    public class DatabaseContext : DbContext
    {
        static DatabaseContext()
        {
            // Database.SetInitializer(new StoreDbInitializer());
        }

        public DatabaseContext(string connectionString) : base(connectionString)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<Appointment> Appointments { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
