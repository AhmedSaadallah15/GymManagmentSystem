using GymManagmentDAL.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagmentDAL.Data.Configuration
{
    internal class GymUserConfiguration<T> : IEntityTypeConfiguration<T> where T : GymUser
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<T> builder)
        {
            builder.Property(p => p.Name)
                    .HasColumnType("varchar")
                    .HasMaxLength(50);

            builder.Property(p => p.Email)
                  .HasColumnType("varchar")
                  .HasMaxLength(100);


            builder.ToTable(tb =>
            {
                tb.HasCheckConstraint("GymUserValidEmail", "Email like '_%@_%._%'");

                tb.HasCheckConstraint("GymUserValidPhone", "Phone like '01%' and Phone not like '%[^0-9]%'");
            });

            builder.HasIndex(p => p.Email)
                    .IsUnique();

            builder.Property(p => p.Phone)
                 .HasColumnType("varchar")
                 .HasMaxLength(11);


            builder.HasIndex(p => p.Phone)
                  .IsUnique();


       


            builder.OwnsOne(x => x.Address, a =>
            {
                a.Property(a=>a.Street)
                 .HasColumnName("Street")
                 .HasColumnType("varchar")
                 .HasMaxLength(30);


                a.Property(a => a.City)
                  .HasColumnName("City")
                  .HasColumnType("varchar")
                  .HasMaxLength(30);

                a.Property(a => a.BuildingNumber)
               .HasColumnName("BuildingNumber");

            });

        }
    }
}
