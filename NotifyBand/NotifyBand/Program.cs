using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using NotifyBand.Infrastructure;
using NotifyBand.Models;

namespace NotifyBand
{
    internal class Program
    {
        private static User user;
        private static readonly object lockObject = new object();

        private static void Main()
        {
            var communicationLayer = new CommunicationLayer();
            var token = communicationLayer.GetToken().Result;

            GetFullUserInfo(token, communicationLayer);
            ShowFullUserInfo();

            Task.Run(() => CheckForNotification());

            while (true)
            {
                var isExit = ShowAdminPannel(token, communicationLayer);

                if (isExit)
                {
                    return;
                }
            }
        }

        private static bool ShowAdminPannel(string token, CommunicationLayer communicationLayer)
        {
            ShowOneHourAppointments();
            Console.WriteLine("-----------------------");
            Console.WriteLine("Choose action: ");
            Console.WriteLine("1 - Update user data");
            Console.WriteLine("2 - Show full user data");
            Console.WriteLine("3 - Exit");

            var successInput = int.TryParse(Console.ReadLine(), out int choise);

            if (!successInput)
            {
                Console.WriteLine("Invalid input! Try again!");
                return false;
            }

            switch (choise)
            {
                case 1:
                    GetFullUserInfo(token, communicationLayer);
                    Console.WriteLine("User data updated.");
                    break;
                case 2:
                    ShowFullUserInfo();
                    break;
                case 3:
                    return true;
                default:
                    return false;
            }

            Console.WriteLine("-----------------------");

            return false;
        }

        public static void ShowOneHourAppointments()
        {
            var futureHoureAppointments = user.Appointments
                    .Where(x => x.DateTime > DateTime.Now && (x.DateTime - DateTime.Now).Hours < 1)
                    .OrderBy(x => x.DateTime);

            if (futureHoureAppointments.Any())
            {
                Console.WriteLine("Appointments in one hour: ");

                foreach (var appointment in futureHoureAppointments)
                {
                    Console.WriteLine($"- Course: {appointment.Course.Name}, Date: {appointment.DateTime}");
                }
            }
            
        }

        public static void ShowFullUserInfo()
        {
            Console.WriteLine($"Hello {user.Name}");
            Console.WriteLine("Your courses: ");

            foreach (var course in user.Courses)
            {
                Console.WriteLine($"- {course.Name}");
            }

            Console.WriteLine("Your future appointments: ");

            foreach (var appointment in user.Appointments.Where(x=>x.DateTime > DateTime.Now))
            {
                Console.WriteLine($"- Course: {appointment.Course.Name}, Date: {appointment.DateTime}");
            }
        }

        public static void GetFullUserInfo(string token, CommunicationLayer communicationLayer)
        {
            lock (lockObject)
            {
                user = communicationLayer.GetUserInfo(token).Result;
                user.Courses = communicationLayer.GetUserCoursesInfo(token).Result;
                user.Appointments = communicationLayer.GetUserAppointmentsInfo(token).Result;
                user.Appointments = user.Appointments.OrderBy(x => x.DateTime).ToList();
            }
        }

        public static void CheckForNotification()
        {
            while (true)
            {
                Thread.Sleep(4000);

                var nearestAppointment = user.Appointments
                    .Where(x => x.DateTime > DateTime.Now)
                    .OrderBy(x => x.DateTime)
                    .FirstOrDefault();

                if (nearestAppointment != null && (nearestAppointment.DateTime - DateTime.Now).Hours < 1)
                {
                    Console.Beep();
                }
            }
        }
    }
}
