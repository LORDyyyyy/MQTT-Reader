using Microsoft.EntityFrameworkCore;
using App.Models;

namespace App.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options)
            : base(options)
        {
        }

        public DbSet<Device> devices { get; set; }
        public DbSet<Branch> branches { get; set; }
        public DbSet<ReadingLKP> readingLKPs { get; set; }
        public DbSet<ActualReadings> actualReadings { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Branch>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).ValueGeneratedOnAdd();
                entity.HasIndex(e => e.Id).IsUnique();

                entity.Property(e => e.Address).IsRequired();
                entity.Property(e => e.PhoneNumber).IsRequired();
                entity.Property(e => e.Email).IsRequired();
                entity.Property(e => e.PostalCode).IsRequired();
            });
        }
    }
}