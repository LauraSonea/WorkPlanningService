using Microsoft.EntityFrameworkCore;
using System;
using WorkPlanningApi.Domain.Entities;

namespace WorkPlanningApi.Data.Database
{
    public class ShiftContext: DbContext
    {
        public ShiftContext()
        {

        }
        public ShiftContext(DbContextOptions<ShiftContext> options)
          : base(options)
        {
            var shifts = new[]
            {
                new Shift
                {
                    Id = Guid.Parse("9f35b48d-cb87-4783-bfdb-21e36012930a"),
                    StartDate = new DateTime(2021, 04, 05),
                    EndDate  = new DateTime(2021, 04, 06),
                    WorkerGuid = Guid.Parse("d3e3137e-ccc9-488c-9e89-50ba354738c2"),
                    WorkerFullName = "Laura Sonea"
                },
                new Shift
                {
                    Id = Guid.Parse("bffcf83a-0224-4a7c-a278-5aae00a02c1e"),
                    StartDate = new DateTime(2021, 04, 06),
                    EndDate  = new DateTime(2021, 04, 07),
                    WorkerGuid = Guid.Parse("d3e3137e-ccc9-488c-9e89-50ba354738c2"),
                    WorkerFullName = "Laura Sonea"
                },
            };

            Shift.AddRange(shifts);
            SaveChanges();
        }

        public virtual DbSet<Shift> Shift { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "1.0.0-servicing-10001");

            modelBuilder.Entity<Shift>(entity =>
            {
                entity.Property(e => e.Id).HasDefaultValueSql("(newid())");

                entity.Property(e => e.WorkerFullName).IsRequired();
            });
        }
    }
}
