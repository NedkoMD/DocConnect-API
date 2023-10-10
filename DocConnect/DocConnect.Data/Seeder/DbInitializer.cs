using DocConnect.Data.Abstraction.Helpers;
using DocConnect.Data.Abstraction.Seeder;
using DocConnect.Data.Models.Entities;
using DocConnect.Data.Models.Utilities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace DocConnect.Data.Seeder
{
    public class DbInitializer : IDbInitializer
    {
        private readonly DocConnectContext _docConnectContext;
        private readonly RoleManager<Role> _roleManager;
        private readonly IDocConnectUserManager _docConnectUserManager;

        public DbInitializer(
            DocConnectContext docConnectContext,
            RoleManager<Role> roleManager,
            IDocConnectUserManager docConnectUserManager)
        {
            _docConnectContext = docConnectContext;
            _roleManager = roleManager;
            _docConnectUserManager = docConnectUserManager;
        }

        public async Task InitializeAsync()
        {
            await SeedSpecialtiesAsync();
            await SeedRolesAsync();
            await SeedUsersAsync();
        }

        private async Task SeedSpecialtiesAsync()
        {
            var databaseIsNotEmpty = await _docConnectContext.Specialities.AnyAsync();

            if (databaseIsNotEmpty)
            {
                return;
            }

            #region ListOfSpecialities
            var specialties = new List<Speciality>()
            {
                new Speciality
                {
                    Name = "Cardiology",
                    ImageUrl = "https://images.pexels.com/photos/7659564/pexels-photo-7659564.jpeg?auto=compress&cs=tinysrgb&w=1260&h=750&dpr=1"
                },
                new Speciality
                {
                    Name = "Orthopedics",
                    ImageUrl = "https://images.pexels.com/photos/7446990/pexels-photo-7446990.jpeg?auto=compress&cs=tinysrgb&w=1260&h=750&dpr=1"
                },
                new Speciality
                {
                    Name = "Neurology",
                    ImageUrl = "https://img.freepik.com/free-photo/doctor-reading-brain-mri-x-ray-result_53876-13389.jpg?w=740&t=st=1677172789~exp=1677173389~hmac=2d0be25306f47a4f0b4aa04d50dac96dd242ad9af01f8b8a9e5ed90385ffaadc"
                },
                new Speciality
                {
                    Name = "Oncology",
                    ImageUrl = "https://img.freepik.com/free-photo/middle-aged-woman-with-skin-cancer-talking-with-her-doctor_23-2148988517.jpg?w=1380&t=st=1677172867~exp=1677173467~hmac=05adb54dceafc6fce6f602ff87188ba97ebfb7463a91cbf48f7651f7e798d2a5"
                },
                new Speciality
                {
                    Name = "Gastroenterology",
                    ImageUrl = "https://img.freepik.com/free-photo/this-pain-stomach-is-unbearable_329181-2191.jpg?w=1380&t=st=1677172914~exp=1677173514~hmac=b03857f0631c3fd1b4f4ac82b219a5d68dd98e2eed413f5f7c1ffc57a05c4455"
                },
                new Speciality
                {
                    Name = "Dermatology",
                    ImageUrl = "https://images.pexels.com/photos/5069432/pexels-photo-5069432.jpeg?auto=compress&cs=tinysrgb&w=1260&h=750&dpr=1"
                },
                new Speciality
                {
                    Name = "Pediatrics",
                    ImageUrl = "https://img.freepik.com/free-photo/doctor-doing-their-work-pediatrics-office_23-2149224121.jpg?w=1380&t=st=1677173488~exp=1677174088~hmac=c7742512982c2d4bed8e1b407380e3ae5854843d43dd54716cd6aaf9c11b55aa"
                },
                new Speciality
                {
                    Name = "Urology",
                    ImageUrl = "https://img.freepik.com/free-photo/medical-exam_1098-16897.jpg?w=1380&t=st=1677173522~exp=1677174122~hmac=019e51a1790adec4e1ce26eb0c3fe193fece36e400270fd08a406bb8d5d0023c"
                },
                new Speciality
                {
                    Name = "Ophthalmology",
                    ImageUrl = "https://images.pexels.com/photos/5765827/pexels-photo-5765827.jpeg?auto=compress&cs=tinysrgb&w=1260&h=750&dpr=1"
                }
            };
            #endregion

            _docConnectContext.Specialities.AddRange(specialties);
            await _docConnectContext.SaveChangesAsync();
        }

        private async Task SeedRolesAsync()
        {
            if (await _docConnectContext.Roles.AnyAsync())
            {
                return;
            }

            await _roleManager.CreateAsync(new Role(RoleConstants.AdminRole));
            await _roleManager.CreateAsync(new Role(RoleConstants.UserRole));
        }

        private async Task SeedUsersAsync()
        {
            if (await _docConnectContext.Users.AnyAsync())
            {
                return;
            }

            var adminUser = new User
            {
                FirstName = "Nedko",
                LastName = "Dobromirov",
                Email = "admin@mentormate.com",
                UserName = "admin@mentormate.com"
            };
            var adminPassword = "Admin123!";

            var regularUser = new User
            {
                FirstName = "Mihail",
                LastName = "Nikolov",
                Email = "user@mentormate.com",
                UserName = "user@mentormate.com"
            };
            var regularPassword = "User123!";

            await _docConnectUserManager.CreateAdminAsync(adminUser, adminPassword);
            await _docConnectUserManager.CreateUserAsync(regularUser, regularPassword);
        }
    }
}
