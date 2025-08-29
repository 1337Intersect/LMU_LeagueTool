using System;
using System.ComponentModel.DataAnnotations;

namespace LMU.RacingLeague.Models
{
    public class Driver
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string FirstName { get; set; } = string.Empty;

        [Required]
        [MaxLength(100)]
        public string LastName { get; set; } = string.Empty;

        [MaxLength(50)]
        public string? ShortName { get; set; }

        [Required]
        [MaxLength(50)]
        public string Nationality { get; set; } = string.Empty;

        public DriverCategory Category { get; set; } = DriverCategory.Bronze;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public string FullName => $"{FirstName} {LastName}";
        public string DisplayName => !string.IsNullOrEmpty(ShortName) ? ShortName : FullName;
    }

    public class Team
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(200)]
        public string Name { get; set; } = string.Empty;

        [MaxLength(50)]
        public string? ShortName { get; set; }

        [Required]
        [MaxLength(50)]
        public string Country { get; set; } = string.Empty;

        [MaxLength(7)]
        public string? PrimaryColor { get; set; }

        [MaxLength(7)]
        public string? SecondaryColor { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public string DisplayName => !string.IsNullOrEmpty(ShortName) ? ShortName : Name;
    }

    public class Car
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(200)]
        public string Name { get; set; } = string.Empty;

        [Required]
        [MaxLength(100)]
        public string Manufacturer { get; set; } = string.Empty;

        public CarCategory Category { get; set; } = CarCategory.LMP2;

        public int Year { get; set; } = DateTime.Now.Year;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public string DisplayName => $"{Manufacturer} {Name} ({Year})";
    }

    public enum DriverCategory
    {
        Bronze,
        Silver,
        Gold,
        Platinum
    }

    public enum CarCategory
    {
        LMP1,
        LMP2,
        GTEPro,
        GTEAm
    }
}