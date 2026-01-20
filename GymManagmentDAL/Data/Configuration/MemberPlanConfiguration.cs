using GymManagmentDAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagmentDAL.Data.Configuration
{
    internal class MemberPlanConfiguration : IEntityTypeConfiguration<MemberPlan>
    {
        public void Configure(EntityTypeBuilder<MemberPlan> builder)
        {
            builder.Property(b => b.CreatedAt)
                .HasColumnName("StartDate")
                .HasDefaultValueSql("GETDATE()");


            builder.HasKey(p => new
            {
                p.MemberId,
                p.PlanId
            });


            builder.Ignore(p => p.Id);

        }
    }
}
