using Microsoft.EntityFrameworkCore;
using SmartHomeApi.Data.Models;

namespace SmartHomeApi.Data
{
    public sealed class SmartHomeApiContext : DbContext
    {
        public DbSet<Device> Devices { get; set; }
        public DbSet<Room> Rooms { get; set; }

        public SmartHomeApiContext(DbContextOptions<SmartHomeApiContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            base.OnConfiguring(builder);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Device>().ToTable("Devices");
            builder.Entity<Room>().ToTable("Rooms");
        }
    }
}
