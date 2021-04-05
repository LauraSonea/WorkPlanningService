using Microsoft.EntityFrameworkCore;
using System;
using WorkerApi.Domain.Entities;

namespace WorkerApi.Data.Database
{
    public class WorkerContext: DbContext
    {
        public WorkerContext()
        {

        }

        public WorkerContext(DbContextOptions<WorkerContext> options)
            : base(options)
        {

            var workers = new[]
            { new  Worker
                 {
                Id = Guid.Parse("0f8fad5b-d9cb-469f-a165-70867728950e"),
                    FirstName = "Laura",
                    LastName = "Sonea",
                    Age = 35
                },
                new Worker
                {
                    Id = Guid.Parse("7c9e6679-7425-40de-944b-e07fc1f90ae7"),
                    FirstName = "Catalin",
                    LastName = "Corea",
                    Age = 41
                },
                new Worker
                {
                    Id = Guid.Parse("7c9e6679-7425-40de-944b-e07fc1f90a77"),
                    FirstName = "Old",
                    LastName = "Worker",
                    Age = 60
                }
            };

            Worker.AddRange(workers);
            SaveChanges();
        }

        public DbSet<Worker> Worker { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "1.0.0-servicing-10001");

            modelBuilder.Entity<Worker>(entity =>
            {
                entity.Property(e => e.Id).HasDefaultValueSql("(newid())");

                entity.Property(e => e.FirstName).IsRequired();

                entity.Property(e => e.LastName).IsRequired();
            });
        }
    }
}
