using Microsoft.EntityFrameworkCore;
using LMU.RacingLeague.Models;

namespace LMU.RacingLeague
{
    public class LMUDbContext : DbContext
    {
        public LMUDbContext(DbContextOptions<LMUDbContext> options) : base(options) { }

        public DbSet<Driver> Drivers { get; set; }
        public DbSet<Team> Teams { get; set; }
        public DbSet<Car> Cars { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configurazioni
            modelBuilder.Entity<Driver>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Category).HasConversion<string>();
                entity.HasIndex(e => new { e.FirstName, e.LastName }).IsUnique();
            });

            modelBuilder.Entity<Team>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.HasIndex(e => e.Name).IsUnique();
            });

            modelBuilder.Entity<Car>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Category).HasConversion<string>();
            });

            // Dati di esempio
            SeedData(modelBuilder);
        }

        private void SeedData(ModelBuilder modelBuilder)
        {
            // Piloti di esempio
            modelBuilder.Entity<Driver>().HasData(
                new Driver { Id = 1, FirstName = "Fernando", LastName = "Alonso", ShortName = "ALO", Nationality = "Spain", Category = DriverCategory.Platinum },
                new Driver { Id = 2, FirstName = "Sebastien", LastName = "Ogier", ShortName = "OGI", Nationality = "France", Category = DriverCategory.Gold },
                new Driver { Id = 3, FirstName = "Mike", LastName = "Conway", ShortName = "CON", Nationality = "UK", Category = DriverCategory.Gold },
                new Driver { Id = 4, FirstName = "Nyck", LastName = "de Vries", ShortName = "DEV", Nationality = "Netherlands", Category = DriverCategory.Gold },
                new Driver { Id = 5, FirstName = "Antonio", LastName = "Giovinazzi", ShortName = "GIO", Nationality = "Italy", Category = DriverCategory.Silver }
            );

            // Team di esempio
            modelBuilder.Entity<Team>().HasData(
                new Team { Id = 1, Name = "Toyota Gazoo Racing", ShortName = "TGR", Country = "Japan", PrimaryColor = "#FF0000", SecondaryColor = "#FFFFFF" },
                new Team { Id = 2, Name = "Porsche Penske Motorsport", ShortName = "PPM", Country = "Germany", PrimaryColor = "#000000", SecondaryColor = "#FFFF00" },
                new Team { Id = 3, Name = "Ferrari AF Corse", ShortName = "FAF", Country = "Italy", PrimaryColor = "#DC143C", SecondaryColor = "#FFFF00" },
                new Team { Id = 4, Name = "Aston Martin Racing", ShortName = "AMR", Country = "UK", PrimaryColor = "#00594F", SecondaryColor = "#FFFFFF" }
            );

            // Auto di esempio
            modelBuilder.Entity<Car>().HasData(
                new Car { Id = 1, Name = "GR010 Hybrid", Manufacturer = "Toyota", Category = CarCategory.LMP1, Year = 2024 },
                new Car { Id = 2, Name = "963", Manufacturer = "Porsche", Category = CarCategory.LMP1, Year = 2024 },
                new Car { Id = 3, Name = "499P", Manufacturer = "Ferrari", Category = CarCategory.LMP1, Year = 2024 },
                new Car { Id = 4, Name = "Oreca 07", Manufacturer = "Alpine", Category = CarCategory.LMP2, Year = 2024 },
                new Car { Id = 5, Name = "488 GTE", Manufacturer = "Ferrari", Category = CarCategory.GTEPro, Year = 2024 },
                new Car { Id = 6, Name = "Vantage GTE", Manufacturer = "Aston Martin", Category = CarCategory.GTEAm, Year = 2024 }
            );
        }
    }
}