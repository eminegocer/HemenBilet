using HemenBiletProje.Entities;
using System.Data.Entity;
using System.Net.NetworkInformation;
using HemenBiletProje.Models;
using System.Data.Entity;
using HemenBiletProje.TicketFactory;
namespace HemenBiletProje.Models
{

    public class FlightContext : DbContext
    {
        public FlightContext() : base("name=FlightContext")
        {
            // Veritabanı değişikliklerini otomatik uygula
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<FlightContext, Migrations.Configuration>());
        }
        public DbSet<Flight> Flights { get; set; }
        public DbSet<Seat> Seats { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Ticket> TicketCards { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            // Flight - Seat ilişkisi
            modelBuilder.Entity<Flight>()
                .HasMany(f => f.Seats)
                .WithRequired(s => s.Flight)
                .HasForeignKey(s => s.FlightId);
            // User - Seat ilişkisi
            modelBuilder.Entity<Seat>()
                .HasOptional(s => s.User)
                .WithMany()
                .HasForeignKey(s => s.UserId);
            base.OnModelCreating(modelBuilder);
        }
    }

}