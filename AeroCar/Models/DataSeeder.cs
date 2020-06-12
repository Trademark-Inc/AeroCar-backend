using AeroCar.Models.Admin;
using AeroCar.Models.Avio;
using AeroCar.Models.Car;
using AeroCar.Models.Users;
using AeroCar.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace AeroCar.Models
{
    public static class DataSeeder
    {
        public static async Task CreateRolesAndAdmin(IServiceProvider serviceProvider, IConfiguration configuration)
        {
            //adding customs roles
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var userManager = serviceProvider.GetRequiredService<UserManager<RegularUser>>();

            string[] roleNames = { "SystemAdmin", "AvioAdmin", "CarAdmin", "RegularUser" };

            foreach (var roleName in roleNames)
            {
                var roleExist = await roleManager.RoleExistsAsync(roleName);
                if (!roleExist) await roleManager.CreateAsync(new IdentityRole(roleName));
            }

            // creating a super user who could maintain the web app
            var admin = new RegularUser
            {
                UserName = configuration.GetSection("AdminData")["Username"],
                EmailConfirmed = true,
                PhoneNumberConfirmed = true,
                Status = UserStatus.Activated
            };

            string userPassword = configuration.GetSection("AdminData")["Password"];
            var foundAdmin = await userManager.FindByNameAsync(admin.UserName) != null;

            if (!foundAdmin)
            {
                var createdAdmin = await userManager.CreateAsync(admin, userPassword);
                if (createdAdmin.Succeeded)
                {
                    await userManager.AddToRoleAsync(admin, "SystemAdmin");
                }
            }
        }

        public static async Task AddDefaultAvioCompanies(IServiceProvider serviceProvider, IConfiguration configuration)
        {
            var avioService = serviceProvider.GetRequiredService<AvioService>();
            var destinationService = serviceProvider.GetRequiredService<DestinationService>();
            var aeroplaneService = serviceProvider.GetRequiredService<AeroplaneService>();
            var seatService = serviceProvider.GetRequiredService<SeatService>();

            string[,] avioCompanies = { { "Wizz Air", "Boulevard of Broken Dreams 33", "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Sed mollis arcu et ipsum semper consectetur. Donec sit amet magna augue. Curabitur rhoncus, lacus ut rhoncus pretium, lectus risus sagittis urna, vel imperdiet velit nunc sed sem. Suspendisse ac rutrum neque. Quisque sed mollis enim. Etiam quis eros tempor, dapibus justo eu, blandit libero. Curabitur elementum tincidunt eros ut luctus. Etiam nec felis consequat, tincidunt nisl non, posuere magna. Mauris vel ipsum mauris. Phasellus non ante ornare, tristique est vitae, fringilla odio. Vivamus ultricies lacus et pharetra placerat. Phasellus auctor efficitur sem eget convallis. Sed eu ante sapien. Fusce ac enim in libero rhoncus blandit ut lacinia eros. Sed rhoncus non libero eu hendrerit. Maecenas eget venenatis nisi, vel blandit dolor. " },
                                        { "Emirates", "Boulevard of Awesome Dreams 22", "Aenean consectetur eleifend nulla, ac rutrum turpis vulputate vel. Praesent posuere magna turpis, et tincidunt libero dapibus ac. Nullam euismod felis eget lacus placerat, id fringilla neque sagittis. Curabitur a aliquam ante, quis pharetra tellus. Donec ante dui, sollicitudin quis ex non, tristique tincidunt urna. Duis blandit bibendum ex. In nulla ligula, consectetur quis diam at, laoreet sagittis nisl. Curabitur imperdiet laoreet purus ullamcorper pretium. Pellentesque a sodales nisl, ac laoreet enim. Fusce rhoncus non libero ut tempor. Phasellus eget condimentum sapien, ac fringilla nulla. Duis volutpat tortor ac iaculis faucibus. Aenean dignissim massa dolor, vel efficitur orci molestie a. Cras dignissim molestie nisl, sit amet euismod elit commodo a. Curabitur blandit bibendum fermentum. Fusce vel odio quam." },
                                        { "Qatar Airways", "Damians House 11", "Phasellus posuere ligula eget mollis hendrerit. Donec finibus orci ut porttitor semper. Ut nec porta augue. Sed libero est, vestibulum tincidunt ipsum eget, laoreet commodo elit. Quisque accumsan aliquam tempus. Ut vestibulum gravida magna, eget semper felis rutrum ornare. In quis lobortis dolor, sed hendrerit velit. Nullam lacinia in nulla non vestibulum. Ut ipsum leo, ullamcorper vel mollis vitae, tempor vitae risus. Maecenas id vulputate purus, id congue erat. Pellentesque habitant morbi tristique senectus et netus et malesuada fames ac turpis egestas. Praesent a ullamcorper orci. Duis eu purus feugiat, euismod urna sed, commodo purus. Morbi quis tortor id dui dictum lacinia. Proin porttitor est id turpis pharetra condimentum. Integer at magna nec leo placerat tincidunt sed quis quam. " },
                                        { "EVA Air", "Neverlands Street 44", "Donec cursus luctus est, malesuada tempor tellus laoreet eget. Vestibulum viverra elit a ex gravida lobortis. Nulla aliquet nunc nec aliquam facilisis. Quisque nec sapien ultricies, tempus leo in, sollicitudin arcu. Integer in gravida ipsum. Maecenas vel ex a quam pulvinar scelerisque ac vel turpis. Vestibulum ante ipsum primis in faucibus orci luctus et ultrices posuere cubilia curae; Pellentesque eleifend sollicitudin velit sed fermentum. Donec congue nisi mi. Suspendisse at facilisis tortor, sagittis mattis justo. Curabitur molestie dictum venenatis. In maximus cursus orci sed iaculis. Praesent posuere vulputate elementum. " } };

            for (int i = 0; i < avioCompanies.GetLength(0); ++i)
            {
                var companyExists = await avioService.CompanyExists(avioCompanies[i, 0]);
                if (!companyExists)
                {
                    AvioCompanyProfile profile = new AvioCompanyProfile()
                    {
                        Name = avioCompanies[i, 0],
                        Address = avioCompanies[i, 1],
                        PromoDescription = avioCompanies[i, 2]
                    };

                    // add destinations
                    var dest = new List<Destination>();
                    string[,] destinations = { { "Belgrade", "44.786568", "20.448921" },
                                       { "Tokyo", "35.689487", "139.691711" },
                                       { "New York", "40.712776", "-74.005974" },
                                       { "Berlin", "52.520008", "13.404954" },
                                       { "Rome", "41.9028", "12.4964" },
                                       { "Zurich", "47.3768880", "8.541694" } };

                    for (int j = 0; j < destinations.GetLength(0); ++j)
                    {
                        Destination destination = new Destination()
                        {
                            Name = destinations[j, 0],
                            Latitude = double.Parse(destinations[j, 1], CultureInfo.InvariantCulture),
                            Longitude = double.Parse(destinations[j, 2], CultureInfo.InvariantCulture),
                        };

                        dest.Add(destination);
                    }

                    AvioCompany company = new AvioCompany()
                    {
                        Destinations = dest
                    };

                    await avioService.CreateCompany(company, profile);

                    // create planes for companies
                    var aeroplaneA380 = new Aeroplane()
                    {
                        AvioCompanyId = company.AvioCompanyId,
                        Name = "Airbus A380"
                    };

                    var aeroplane737 = new Aeroplane()
                    {
                        AvioCompanyId = company.AvioCompanyId,
                        Name = "Boeing 737"
                    };

                    if (!await aeroplaneService.AeroplaneExists(company.AvioCompanyId, aeroplaneA380.Name))
                    {
                        await aeroplaneService.AddAeroplane(aeroplaneA380);
                    }

                    if (!await aeroplaneService.AeroplaneExists(company.AvioCompanyId, aeroplane737.Name))
                    {
                        await aeroplaneService.AddAeroplane(aeroplane737);
                    }

                    // create seats for planes
                    var seatsA380 = new Seats()
                    {
                        AeroplaneId = aeroplaneA380.AeroplaneId,
                        SeatCount = 240,
                        InOneRow = 6
                    };

                    var seats737 = new Seats()
                    {
                        AeroplaneId = aeroplane737.AeroplaneId,
                        SeatCount = 320,
                        InOneRow = 8
                    };

                    if (!await seatService.SeatsExist(aeroplaneA380.AeroplaneId))
                    {
                        await seatService.AddSeats(seatsA380);
                    }

                    if (!await seatService.SeatsExist(aeroplane737.AeroplaneId))
                    {
                        await seatService.AddSeats(seats737);
                    }
                }
            }
        }

        public static async Task AddDefaultCarCompanies(IServiceProvider serviceProvider, IConfiguration configuration)
        {
            var rentACarService = serviceProvider.GetRequiredService<RentACarService>();

            string[,] carCompanies = { { "Fox Rent A Car", "Boulevard of Broken Dreams 33", "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Sed mollis arcu et ipsum semper consectetur. Donec sit amet magna augue. Curabitur rhoncus, lacus ut rhoncus pretium, lectus risus sagittis urna, vel imperdiet velit nunc sed sem. Suspendisse ac rutrum neque. Quisque sed mollis enim. Etiam quis eros tempor, dapibus justo eu, blandit libero. Curabitur elementum tincidunt eros ut luctus. Etiam nec felis consequat, tincidunt nisl non, posuere magna. Mauris vel ipsum mauris. Phasellus non ante ornare, tristique est vitae, fringilla odio. Vivamus ultricies lacus et pharetra placerat. Phasellus auctor efficitur sem eget convallis. Sed eu ante sapien. Fusce ac enim in libero rhoncus blandit ut lacinia eros. Sed rhoncus non libero eu hendrerit. Maecenas eget venenatis nisi, vel blandit dolor. " },
                                       { "Europcar", "Boulevard of Awesome Dreams 22", "Aenean consectetur eleifend nulla, ac rutrum turpis vulputate vel. Praesent posuere magna turpis, et tincidunt libero dapibus ac. Nullam euismod felis eget lacus placerat, id fringilla neque sagittis. Curabitur a aliquam ante, quis pharetra tellus. Donec ante dui, sollicitudin quis ex non, tristique tincidunt urna. Duis blandit bibendum ex. In nulla ligula, consectetur quis diam at, laoreet sagittis nisl. Curabitur imperdiet laoreet purus ullamcorper pretium. Pellentesque a sodales nisl, ac laoreet enim. Fusce rhoncus non libero ut tempor. Phasellus eget condimentum sapien, ac fringilla nulla. Duis volutpat tortor ac iaculis faucibus. Aenean dignissim massa dolor, vel efficitur orci molestie a. Cras dignissim molestie nisl, sit amet euismod elit commodo a. Curabitur blandit bibendum fermentum. Fusce vel odio quam." },
                                       { "Advantage Rent-A-Car", "Damians House 11", "Phasellus posuere ligula eget mollis hendrerit. Donec finibus orci ut porttitor semper. Ut nec porta augue. Sed libero est, vestibulum tincidunt ipsum eget, laoreet commodo elit. Quisque accumsan aliquam tempus. Ut vestibulum gravida magna, eget semper felis rutrum ornare. In quis lobortis dolor, sed hendrerit velit. Nullam lacinia in nulla non vestibulum. Ut ipsum leo, ullamcorper vel mollis vitae, tempor vitae risus. Maecenas id vulputate purus, id congue erat. Pellentesque habitant morbi tristique senectus et netus et malesuada fames ac turpis egestas. Praesent a ullamcorper orci. Duis eu purus feugiat, euismod urna sed, commodo purus. Morbi quis tortor id dui dictum lacinia. Proin porttitor est id turpis pharetra condimentum. Integer at magna nec leo placerat tincidunt sed quis quam. " },
                                       { "E-Z Rent-A-Car", "Neverlands Street 44", "Donec cursus luctus est, malesuada tempor tellus laoreet eget. Vestibulum viverra elit a ex gravida lobortis. Nulla aliquet nunc nec aliquam facilisis. Quisque nec sapien ultricies, tempus leo in, sollicitudin arcu. Integer in gravida ipsum. Maecenas vel ex a quam pulvinar scelerisque ac vel turpis. Vestibulum ante ipsum primis in faucibus orci luctus et ultrices posuere cubilia curae; Pellentesque eleifend sollicitudin velit sed fermentum. Donec congue nisi mi. Suspendisse at facilisis tortor, sagittis mattis justo. Curabitur molestie dictum venenatis. In maximus cursus orci sed iaculis. Praesent posuere vulputate elementum. " } };

            for (int i = 0; i < carCompanies.GetLength(0); ++i)
            {
                var companyExists = await rentACarService.CompanyExists(carCompanies[i, 0]);
                if (!companyExists)
                {
                    CarCompanyProfile profile = new CarCompanyProfile()
                    {
                        Name = carCompanies[i, 0],
                        Address = carCompanies[i, 1],
                        PromoDescription = carCompanies[i, 2]
                    };

                    await rentACarService.CreateCompany(profile);
                }
            }
        }

        public static async Task AddDefaultAvioCompanyAdmins(IServiceProvider serviceProvider, IConfiguration configuration)
        {
            var adminService = serviceProvider.GetRequiredService<AvioAdminService>();
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var userManager = serviceProvider.GetRequiredService<UserManager<RegularUser>>();

            string[,] admins = { { "mmagic_avio", "markomales@yandex.com", "1", "Mmagic12!" } };
            for (int i = 0; i < admins.GetLength(0); ++i)
            {
                var adminExists = await adminService.AdminExists(admins[i, 0]);
                if (!adminExists)
                {
                    RegularUser user = new RegularUser()
                    {
                        UserName = admins[i, 0],
                        Email = admins[i, 1],
                        Status = UserStatus.Activated
                    };

                    var foundAdmin = await userManager.FindByNameAsync(user.UserName) != null;

                    if (!foundAdmin)
                    {
                        var createdAdmin = await userManager.CreateAsync(user, admins[i, 3]);
                        if (createdAdmin.Succeeded)
                        {
                            await userManager.AddToRoleAsync(user, "AvioAdmin");

                            AvioAdmin admin = new AvioAdmin()
                            {
                                UserId = user.Id,
                                AvioCompanyId = long.Parse(admins[i, 2])
                            };

                            await adminService.RegisterAdmin(user.Id, admin);
                        }
                    }
                }
            }
        }

        public static async Task AddDefaultCarCompanyAdmins(IServiceProvider serviceProvider, IConfiguration configuration)
        {
            var adminService = serviceProvider.GetRequiredService<CarAdminService>();
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var userManager = serviceProvider.GetRequiredService<UserManager<RegularUser>>();

            string[,] admins = { { "mmagic_car", "markomales@yandex.com", "1", "Mmagic12!" } };
            for (int i = 0; i < admins.GetLength(0); ++i)
            {
                var adminExists = await adminService.AdminExists(admins[i, 0]);
                if (!adminExists)
                {
                    RegularUser user = new RegularUser()
                    {
                        UserName = admins[i, 0],
                        Email = admins[i, 1],
                        Status = UserStatus.Activated
                    };

                    var foundAdmin = await userManager.FindByNameAsync(user.UserName) != null;

                    if (!foundAdmin)
                    {
                        var createdAdmin = await userManager.CreateAsync(user, admins[i, 3]);
                        if (createdAdmin.Succeeded)
                        {
                            await userManager.AddToRoleAsync(user, "CarAdmin");

                            CarAdmin admin = new CarAdmin()
                            {
                                UserId = user.Id,
                                CarCompanyId = long.Parse(admins[i, 2])
                            };

                            await adminService.RegisterAdmin(user.Id, admin);
                        }
                    }
                }
            }
        }
    }
}
