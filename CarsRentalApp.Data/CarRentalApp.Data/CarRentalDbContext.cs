using CarRentalApp.Data.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CarRentalApp.Data
{
    public class CarRentalDbContext : IdentityDbContext<User>
    {
        public CarRentalDbContext(DbContextOptions<CarRentalDbContext> options)
            : base(options)
        {
        }

        public DbSet<Car> Cars { get; set; }
        public DbSet<Rental> Rentals { get; set; }
        public DbSet<Feature> Features { get; set; }
        public DbSet<CarFeature> CarFeatures { get; set; }
        public DbSet<User> Users { get; set; } // User DbSet'i eklendi
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //  Rental tablosundaki TotalCost için hassasiyet ayarý
            modelBuilder.Entity<Rental>()
                .Property(r => r.TotalCost)
                .HasPrecision(18, 2); // 18 basamak, 2 ondalýk 

            //  Car tablosundaki DailyPrice ve DailyRate için hassasiyet ayarý
            modelBuilder.Entity<Car>()
                .Property(c => c.DailyPrice)
                .HasPrecision(18, 2); // 18 basamak, 2 ondalýk

            modelBuilder.Entity<Car>()
                .Property(c => c.DailyRate)
                .HasPrecision(18, 2); // 18 basamak, 2 ondalýk

            modelBuilder.Entity<Rental>()
                .HasOne(r => r.User)
                .WithMany()
                .HasForeignKey(r => r.UserId);

            modelBuilder.Entity<Rental>()
                .HasOne(r => r.Car)
                .WithMany(c => c.Rentals)
                .HasForeignKey(r => r.CarId);

            modelBuilder.Entity<CarFeature>()
                .HasKey(cf => new { cf.CarId, cf.FeatureId });

            modelBuilder.Entity<CarFeature>()
                .HasOne(cf => cf.Car)
                .WithMany(c => c.CarFeatures)
                .HasForeignKey(cf => cf.CarId);

            modelBuilder.Entity<CarFeature>()
                .HasOne(cf => cf.Feature)
                .WithMany(f => f.CarFeatures)
                .HasForeignKey(cf => cf.FeatureId);
        }

    }
}

