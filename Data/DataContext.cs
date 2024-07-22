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
    }
}