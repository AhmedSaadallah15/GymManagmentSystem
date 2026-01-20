using GymManagementDAL.Entities;
using GymManagmentDAL.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace GymManagmentDAL.Data.Context;

public class GymDbContext : IdentityDbContext<ApplicationUser>
{

    public GymDbContext(DbContextOptions<GymDbContext> options) : base(options)
    {
        
    }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        modelBuilder.Entity<ApplicationUser>(e =>
        {
            e.Property(e => e.FristName)
            .HasColumnType("varchar")
            .HasMaxLength(50);
        });

        modelBuilder.Entity<ApplicationUser>(e =>
        {
            e.Property(e => e.LastName)
            .HasColumnType("varchar")
            .HasMaxLength(50);
        });
    }


    public DbSet<Member> Members { get; set; }
    public DbSet<Trainer> Trainers { get; set; }
    public DbSet<Category> Categorys { get; set; }
    public DbSet<HealthRecord> HealthRecord { get; set; }
    public DbSet<Plan> Plans { get; set; }
    public DbSet<Session> Sessions { get; set; }
    public DbSet<MemberPlan> MemberPlans { get; set; }
    public DbSet<MemberSession> MemberSessions { get; set; }



}
