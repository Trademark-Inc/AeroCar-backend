using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AeroCar.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using AeroCar.Models.Users;
using Microsoft.AspNetCore.Identity;
using AeroCar.Services;
using AeroCar.Repositories;
using Microsoft.Net.Http.Headers;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using AeroCar.Models.Admin;

namespace AeroCar
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy(name: "CorsPolicy",
                                  builder =>
                                  {
                                      builder.WithOrigins("http://localhost:4200")
                                      .AllowAnyHeader()
                                      .AllowCredentials();
                                  });
            });

            services.AddHttpContextAccessor();
            services.AddControllers();
            services.AddDbContext<ApplicationDbContext>(options => options.UseMySql(Configuration.GetConnectionString("IdentityConnection")));
            services.AddDefaultIdentity<RegularUser>().AddRoles<IdentityRole>().AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            var key = Encoding.ASCII.GetBytes(Configuration["AppSettings:Secret"]);
            services
                .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(x =>
                {
                    x.RequireHttpsMetadata = false;
                    x.SaveToken = true;
                    x.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(key),
                        ValidateIssuer = false,
                        ValidateAudience = false
                    };
                });

            services.AddAuthorization();

            services.AddTransient<DestinationService>();
            services.AddTransient<DestinationRepository>();

            services.AddTransient<AeroplaneService>();
            services.AddTransient<AeroplaneRepository>();

            services.AddTransient<SeatService>();
            services.AddTransient<SeatRepository>();

            services.AddTransient<FlightService>();
            services.AddTransient<FlightRepository>();

            services.AddTransient<FastReservationFlightTicketService>();
            services.AddTransient<FastReservationFlightTicketRepository>();

            services.AddTransient<PriceListItemService>();
            services.AddTransient<PriceListItemRepository>();

            services.AddTransient<AvioAdminService>();
            services.AddTransient<AvioAdminRepository>();

            services.AddTransient<VehicleService>();
            services.AddTransient<VehicleRepository>();

            services.AddTransient<OfficeService>();
            services.AddTransient<OfficeRepository>();

            services.AddTransient<CarAdminService>();
            services.AddTransient<CarAdminRepository>();

            services.AddTransient<UserService>();
            services.AddTransient<UserRepository>();

            services.AddTransient<AvioService>();
            services.AddTransient<AvioRepository>();

            services.AddTransient<RentACarService>();
            services.AddTransient<RentACarRepository>();

            services.AddTransient<ReservationService>();
            services.AddTransient<ReservationRepository>();

            services.Configure<IdentityOptions>(options =>
            {
                // Password settings.
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequiredLength = 6;

                // Lockout settings.
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
                options.Lockout.MaxFailedAccessAttempts = 5;
                options.Lockout.AllowedForNewUsers = true;

                // User settings.
                options.User.AllowedUserNameCharacters =
                "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
                options.User.RequireUniqueEmail = false;
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IServiceProvider serviceProvider)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors("CorsPolicy"); 
            
            app.UseHttpsRedirection();
            
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            // system admin
            DataSeeder.CreateRolesAndAdmin(serviceProvider, Configuration).Wait();

            // default companies
            DataSeeder.AddDefaultAvioCompanies(serviceProvider, Configuration).Wait();
            DataSeeder.AddDefaultCarCompanies(serviceProvider, Configuration).Wait();

            // default company admins
            DataSeeder.AddDefaultAvioCompanyAdmins(serviceProvider, Configuration).Wait();
            DataSeeder.AddDefaultCarCompanyAdmins(serviceProvider, Configuration).Wait();
        }
    }
}
