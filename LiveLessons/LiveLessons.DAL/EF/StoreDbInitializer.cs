using System;
using System.Collections.Generic;
using System.Data.Entity;
using LiveLessons.DAL.Entities;

namespace LiveLessons.DAL.EF
{
    public class StoreDbInitializer : DropCreateDatabaseAlways<DatabaseContext>
    {
        protected override void Seed(DatabaseContext db)
        {
            var user1 = new User
            {
                Id = 1,
                Name = "User 1",
                Age = 20,
                ProfileId = "profileId"
            };

            var user2 = new User
            {
                Id = 2,
                Name = "User 2",
                Age = 30,
                ProfileId = "profileId2"
            };

            db.Users.AddRange(new List<User> { user1, user2 });
            db.SaveChanges();

            var course1 = new Course
            {
                Id = 1,
                Name = "Course 1",
                Description = "Course 1 description",
                CoordX = 10,
                CoordY = 10,
                Photo = "photo",
                Price = 10,
                Rate = 4,
                Teacher = user1
            };

            var course2 = new Course
            {
                Id = 2,
                Name = "Course 2",
                Description = "Course 2 description",
                CoordX = 100,
                CoordY = 100,
                Photo = "photo",
                Price = 20,
                Rate = 10,
                Teacher = user2
            };

            var course3 = new Course
            {
                Id = 2,
                Name = "Alex Course",
                Description = "It is an alexCourse!",
                CoordX = 70,
                CoordY = 70,
                Photo = "photo",
                Price = 10,
                Rate = 10,
                Teacher = user2
            };

            var course4 = new Course
            {
                Id = 2,
                Name = "Digital Course",
                Description = "It is an digital course!",
                CoordX = 35,
                CoordY = 10,
                Photo = "photo",
                Price = 10,
                Rate = 10,
                Teacher = user2
            };

            db.Courses.AddRange(new List<Course> { course1, course2, course3, course4 });
            db.SaveChanges();

            var appointment1 = new Appointment
            {
                Id = 1,
                Course = course1,
                DateTime = DateTime.UtcNow,
                Student = user1
            };

            var appointmetn2 = new Appointment
            {
                Id = 2,
                Course = course2,
                DateTime = DateTime.UtcNow,
                Student = user2
            };

            db.Appointments.AddRange(new List<Appointment> { appointment1, appointmetn2 });
            db.SaveChanges();

            var message1 = new Message
            {
                Id = 1,
                Text = "Message 1",
                Course = course1,
                DateTime = DateTime.UtcNow,
                Reciever = user2,
                Sender = user1
            };

            var message2 = new Message
            {
                Id = 2,
                Text = "Message 2",
                Course = course1,
                DateTime = DateTime.UtcNow,
                Reciever = user1,
                Sender = user2
            };

            db.Messages.AddRange(new List<Message> { message1, message2 });
            db.SaveChanges();
        }
    }
}
