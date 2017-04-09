using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web.Mvc;
using LiveLessons.BLL.DTO;
using LiveLessons.BLL.Interfaces;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace LiveLesson.WEB.Models
{
    public class ApplicationUser : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager, string authenticationType)
        {
            var userIdentity = await manager.CreateIdentityAsync(this, authenticationType);

            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        static ApplicationDbContext()
        {
            Database
                .SetInitializer(new ApplicationDbInitializer(DependencyResolver.Current.GetService<IUserService>()));
        }

        public ApplicationDbContext()
            : base("UserConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }

    public class ApplicationDbInitializer : DropCreateDatabaseAlways<ApplicationDbContext>
    {
        private readonly IUserService userService;

        public ApplicationDbInitializer(IUserService userService)
        {
            this.userService = userService;
        }

        protected override void Seed(ApplicationDbContext db)
        {
            var userManager = new ApplicationUserManager(new UserStore<ApplicationUser>(db));
            var user = new ApplicationUser { UserName = "admin", Email = "admin@admin" };
            var pass = "Admin1234";
            var result = userManager.Create(user, pass);

            if (result.Succeeded)
            {
                var profileId = userManager.Find(user.UserName, pass).Id;
                var userDto = new UserDto
                {
                    Name = "Admin",
                    Age = 20,
                    ProfileId = profileId
                };

                userService.Create(userDto);
            }

            base.Seed(db);
        }
    }
}