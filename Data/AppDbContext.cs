using Microsoft.EntityFrameworkCore;
using AhfHivApi.Models;

namespace AhfHivApi.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Country> Countries { get; set; }
    public DbSet<HivHealthIndicator> HivHealthIndicators { get; set; }
    public DbSet<ClimateRiskIndicator> ClimateRiskIndicators { get; set; }
    public DbSet<ProgrammePerformance> ProgrammePerformances { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Country — primary key and unique index on CountryCode
        modelBuilder.Entity<Country>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.HasIndex(e => e.CountryCode).IsUnique();
            entity.Property(e => e.CountryCode).IsRequired().HasMaxLength(3);
            entity.Property(e => e.CountryName).IsRequired().HasMaxLength(100);
            entity.Property(e => e.Region).IsRequired().HasMaxLength(50);
        });

        // HivHealthIndicator — relationship to Country
        modelBuilder.Entity<HivHealthIndicator>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.HasIndex(e => new { e.CountryCode, e.Year }).IsUnique();
            entity.Property(e => e.CountryCode).IsRequired().HasMaxLength(3);
            entity.HasOne(e => e.Country)
                  .WithMany(c => c.HivHealthIndicators)
                  .HasForeignKey(e => e.CountryCode)
                  .HasPrincipalKey(c => c.CountryCode)
                  .OnDelete(DeleteBehavior.Cascade);
        });

        // ClimateRiskIndicator — relationship to Country
        modelBuilder.Entity<ClimateRiskIndicator>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.HasIndex(e => new { e.CountryCode, e.Year }).IsUnique();
            entity.Property(e => e.CountryCode).IsRequired().HasMaxLength(3);
            entity.HasOne(e => e.Country)
                  .WithMany(c => c.ClimateRiskIndicators)
                  .HasForeignKey(e => e.CountryCode)
                  .HasPrincipalKey(c => c.CountryCode)
                  .OnDelete(DeleteBehavior.Cascade);
        });

        // ProgrammePerformance — relationship to Country
        modelBuilder.Entity<ProgrammePerformance>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.HasIndex(e => new { e.CountryCode, e.Year }).IsUnique();
            entity.Property(e => e.CountryCode).IsRequired().HasMaxLength(3);
            entity.HasOne(e => e.Country)
                  .WithMany(c => c.ProgrammePerformances)
                  .HasForeignKey(e => e.CountryCode)
                  .HasPrincipalKey(c => c.CountryCode)
                  .OnDelete(DeleteBehavior.Cascade);
        });
    }
}