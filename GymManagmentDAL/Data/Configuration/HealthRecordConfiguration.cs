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
    internal class HealthRecordConfiguration : IEntityTypeConfiguration<HealthRecord>
    {
        public void Configure(EntityTypeBuilder<HealthRecord> builder)
        {
            builder.ToTable(name: "Members");

            builder.HasOne<Member>()
                .WithOne(h => h.HealthRecord)
                .HasForeignKey<HealthRecord>(h => h.Id);

            builder.Ignore(x => x.CreatedAt);

        }
    }
}
