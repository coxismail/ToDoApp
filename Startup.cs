using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Owin;
using System;
using ToDoAPP.Models;

[assembly: OwinStartupAttribute(typeof(ToDoAPP.Startup))]
namespace ToDoAPP
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            GenerateDefaults();
        }
        private void GenerateDefaults()
        {
            ApplicationDbContext db = new ApplicationDbContext();

            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(db));


            string[] roleList = { "systemAdmin", "companyAdmin", "user", "customer", "visitor","subscriber" };

            foreach (var singleRole in roleList)
            {
                IdentityRole role = new IdentityRole();
                if (!roleManager.RoleExists(singleRole))
                {
                    role.Id = Guid.NewGuid().ToString();
                    role.Name = singleRole;
                    roleManager.Create(role);
                }
            }
            var defaultuser = new ApplicationUser();
            if (userManager.FindByName("coxismail.bd@gmail.com") == null)
            {
                defaultuser.UserName = "coxismail.bd@gmail.com";
                defaultuser.Email = "coxismail.bd@gmail.com";
                defaultuser.EmailConfirmed = true;
                defaultuser.UserDetails = new UserDetails() {FirstName = "Mohammad", LastName = "Ismail", Genders = Gender.Male, PhotosUrl="~/Image/user_logo/user.png", Designation = Designation.Engineer, MobileNumber = "01829291440", Islocked=false, Status = true  };
                string userPWD = "Aa@12345";
                var checkuser = userManager.Create(defaultuser, userPWD);

                if (checkuser.Succeeded)
                {
                    var Res = userManager.AddToRole(defaultuser.Id, "systemAdmin");
                }
            }
        }
    }
}
