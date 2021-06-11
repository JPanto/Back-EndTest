using Back_EndTest.Enums;
using Back_EndTest.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Back_EndTest.Data
{
    public class TestDbContext : DbContext
    {
        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<Status> Statuses { get; set; }

        public TestDbContext(DbContextOptions<TestDbContext> options): base (options)
        {
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=paginationDb;Trusted_Connection=True;MultipleActiveResultSets=true");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Status>(entity =>
            {
                entity.HasKey(e => e.id_status)
                    .HasName("PrimaryKey_id_status");

                entity.ToTable("status");

                entity.Property(e => e.id_status).HasColumnName("id_status");

                entity.Property(e => e.status)
                    .IsRequired()
                    .HasMaxLength(45)
                    .HasColumnName("status");

                entity.HasData(StatusEnum.Abierto,
                    StatusEnum.Cerrado);
            });

            modelBuilder.Entity<Ticket>(entity =>
            {
                entity.HasKey(e => e.id_ticket)
                    .HasName("PrimaryKey_id_ticket");

                entity.ToTable("ticket");

                entity.Property(e => e.id_ticket).HasColumnName("id_ticket");

                entity.Property(e => e.user)
                    .IsRequired()
                    .HasMaxLength(80)
                    .HasColumnName("user");

                entity.Property(e => e.creation_date)
                    .HasColumnType("datetime")
                    .HasColumnName("creation_date");

                entity.Property(e => e.id_status_fk).HasColumnName("id_status_fk");

                entity.HasOne(d => d.status)
                    .WithMany(p => p.Tickets)
                    .HasForeignKey(d => d.id_status_fk)
                    .HasConstraintName("status_ibfk_1"); ;
            });
 
        }

    }
}
