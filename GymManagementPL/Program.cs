using AutoMapper;
using GymManagementBLL.MappingProfiles;
using GymManagementBLL.services.AttachmentService;
using GymManagementBLL.services.Classes;
using GymManagementBLL.services.Interfaces;
using GymManagementDAL.Data.DataSeed;
using GymManagementDAL.Entities;
using GymManagementDAL.Repositories.Classes;
using GymManagementDAL.Repositories.Interfaces;
using GymManagementDAL.UnitOfWork;
using GymManagmentDAL.Data.Context;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace GymManagementPL
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            builder.Services.AddDbContext<GymDbContext>(
                options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
            );

            #region Services
            //builder.Services.AddScoped(typeof(IGenericReporitory<>), typeof(GenericRepository<>));
            //builder.Services.AddScoped<IPlanRepository, PlanRepository>();
            builder.Services.AddScoped<IUnitOfWork, UnitOfWorks>();
            builder.Services.AddScoped<ISessionRepository, SessionRepository>();
            builder.Services.AddScoped<IMembershipRepository, MembershipRepository>();
            builder.Services.AddScoped<IMemberSessionRepository, MemberSessionRepository>();
            builder.Services.AddAutoMapper(x => x.AddProfile(new MappingProfiles()));
            builder.Services.AddScoped<IAnalyticsServices, AnalyticsServices>();
            builder.Services.AddScoped<IMemberServices, MemberServices>();
            builder.Services.AddScoped<ITrainerServices, TrainerServices>();
            builder.Services.AddScoped<IPlanServicse, PlanServicse>();
            builder.Services.AddScoped<ISessionServicse, SessionServicse>();
            builder.Services.AddScoped<IMembershipServices, MembershipServices>();
            builder.Services.AddScoped<IMemberSessionServices, MemberSessionServices>();
            builder.Services.AddScoped<IAccountServices, AccountServices>();
            builder.Services.AddScoped<IAttachmentService, AttachmentService>();
            builder.Services.AddIdentity<ApplicationUser, IdentityRole>(config =>
            {
                config.User.RequireUniqueEmail = true;

            }).AddEntityFrameworkStores<GymDbContext>();

            builder.Services.ConfigureApplicationCookie(opt =>
            {
                opt.LogoutPath = "/Account/Login";
                opt.AccessDeniedPath = "/Account/AccessDenied";
            });


            //builder.Services.AddIdentityCore<ApplicationUser>()
            //    .AddEntityFrameworkStores<GymDbContext>();
            //only the user and the password  

            #endregion


            var app = builder.Build();

            #region DataSeeding
            var scope = app.Services.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<GymDbContext>();
            var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            var pendingMigrations = dbContext.Database.GetPendingMigrations();
            if (pendingMigrations?.Any() ?? false)
            {
                dbContext.Database.Migrate();
            }
            GymDbcontextSeeding.SeedData(dbContext);

            IdentityDbContextSeeding.SeedData(roleManager , userManager);
            #endregion


            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.MapStaticAssets();
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Account}/{action=Login}/{id?}")
                .WithStaticAssets();

            app.Run();
        }
    }
}
