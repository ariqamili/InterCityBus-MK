using InterCityBus_MK.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace InterCityBus_MK.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
        public DbSet<Company> Companies { get; set; }
        public DbSet<Station> Stations { get; set; }
        public DbSet<Trip> Trips { get; set; }
        public DbSet<Stop> Stops { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Trip>()
                .HasOne(t => t.FromStation)
                .WithMany()
                .HasForeignKey(t => t.FromStationId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Trip>()
                .HasOne(t => t.ToStation)
                .WithMany()
                .HasForeignKey(t => t.ToStationId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Trip>()
                .HasOne(t => t.Company)
                .WithMany(c => c.Trips)
                .HasForeignKey(t => t.CompanyId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}

