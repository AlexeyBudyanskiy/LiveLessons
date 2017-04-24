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
                Name = "Hanna Olisen",
                Age = 20,
                ProfileId = "profileId"
            };

            var user2 = new User
            {
                Id = 2,
                Name = "Juggin Anderson",
                Age = 30,
                ProfileId = "profileId2"
            };

            var user3 = new User
            {
                Id = 2,
                Name = "Dan Hopkins",
                Age = 30,
                ProfileId = "profileId3"
            };

            db.Users.AddRange(new List<User> { user1, user2, user3 });
            db.SaveChanges();

            var course1 = new Course
            {
                Id = 1,
                Name = "Cream of mushroom soop coocking",
                Description = "Cream of mushroom soup is a simple type of soup where a basic roux is thinned with cream or milk and then mushrooms and/or mushroom broth are added. It is well known in North America as a common type of condensed canned soup. Cream of mushroom soup is often used as a base ingredient in casseroles and comfort foods. This use is similar to that of a mushroom-flavored gravy.",
                CoordX = 10,
                CoordY = 10,
                Photo = "course-coocking.jpg",
                Price = 14,
                Rate = 17,
                Teacher = user1
            };

            var course2 = new Course
            {
                Id = 2,
                Name = "Black hourse riding",
                Description = "It is unclear exactly when horses were first ridden because early domestication did not create noticeable physical changes in the horse. However, there is strong circumstantial evidence that horse were ridden by people of the Botai culture during the copper age, circa 3600-3100 BCE.[1] The earliest evidence suggesting horses were ridden dates to about 3500 BCE, where evidence from horse skills found at site in Kazakhstan indicated that they had worn some type of bit. Wear facets of 3 mm or more were found on seven horse premolars in two sites, Botai and Kozhai 1, dated about 3500–3000 BCE.[2][3] It is theorized that people herding animals first rode horses for this purpose, presumably bareback, and probably used soft materials such as rope or possibly bone to create rudimentary bridles and hackamores.[4] However, the earliest definitive evidence of horses being ridden dates to art and textual evidence dating to about 2000-1500 BCE.",
                CoordX = 100,
                CoordY = 100,
                Photo = "course-hourse.jpeg",
                Price = 45,
                Rate = 24,
                Teacher = user2
            };

            var course3 = new Course
            {
                Id = 3,
                Name = "Wooman car driving",
                Description = "The world's first long distance road trip by automobile[2] took place in Germany in August 1888 when Bertha Benz, the wife of Karl Benz, the inventor of the first patented motor car (the Benz Patent-Motorwagen), travelled from Mannheim to Pforzheim[3] (a distance of 106 km or 66 miles)[4] and back in the third experimental Benz motor car, which had a maximum speed of 10 mph or 16 km/h, with her two teenage sons Richard and Eugen but without the consent and knowledge of her husband. Her official reason was that she wanted to visit her mother, but unofficially she intended to generate publicity for her husband's invention, which had only been taken on short test drives before. The automobile took off greatly afterwards and the Benz's family business eventually evolved into the present day Mercedes-Benz company",
                CoordX = 70,
                CoordY = 70,
                Photo = "course-driving.jpeg",
                Price = 20,
                Rate = 10,
                Teacher = user1
            };

            var course4 = new Course
            {
                Id = 4,
                Name = "How to understand Indian English?",
                Description = "According to the 2005 India Human Development Survey,[7] of the 41,554 surveyed households reported that 72 percent of men (29,918) did not speak any English, 28 percent (11,635) spoke at least some English, and 5 percent (2,077, roughly 17.9% of those who spoke at least some English) spoke fluent English. Among women, the corresponding percentages were 83 percent (34,489) speaking no English, 17 percent (7,064) speaking at least some English, and 3 percent (1,246, roughly 17.6% of those who spoke at least some English) speaking English fluently.[8] According to statistics of District Information System for Education (DISE) of National University of Educational Planning and Administration under Ministry of Human Resource Development, Government of India, enrollment in English-medium schools increased by 50% between 2008–09 and 2013–14. The number of English-medium school students in India increased from over 15 million in 2008–09 to 29 million by 2013–14.",
                CoordX = 35,
                CoordY = 10,
                Photo = "course-indian-english.jpg",
                Price = 10,
                Rate = 8,
                Teacher = user3
            };

            var course5 = new Course
            {
                Id = 5,
                Name = "Microservices architecture deep diving",
                Description = "You are developing a server-side enterprise application. It must support a variety of different clients including desktop browsers, mobile browsers and native mobile applications. The application might also expose an API for 3rd parties to consume. It might also integrate with other applications via either web services or a message broker. The application handles requests (HTTP requests and messages) by executing business logic; accessing a database; exchanging messages with other systems; and returning a HTML/JSON/XML response. There are logical components corresponding to different functional areas of the application.",
                CoordX = 35,
                CoordY = 10,
                Photo = "course-mcroservices.jpg",
                Price = 35,
                Rate = 8,
                Teacher = user3
            };

            db.Courses.AddRange(new List<Course> { course1, course2, course3, course4, course5 });
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

            var appointmetn3 = new Appointment
            {
                Id = 3,
                Course = course3,
                DateTime = DateTime.UtcNow,
                Student = user2
            };

            var appointmetn4 = new Appointment
            {
                Id = 4,
                Course = course4,
                DateTime = DateTime.UtcNow,
                Student = user2
            };

            var appointmetn5 = new Appointment
            {
                Id = 5,
                Course = course5,
                DateTime = DateTime.UtcNow,
                Student = user2
            };

            var appointmetn6 = new Appointment
            {
                Id = 6,
                Course = course2,
                DateTime = DateTime.UtcNow,
                Student = user2
            };

            db.Appointments.AddRange(new List<Appointment>
            {
                appointment1,
                appointmetn2,
                appointmetn3,
                appointmetn4,
                appointmetn5,
                appointmetn6
            });
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
