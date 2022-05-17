using System;
using Microsoft.EntityFrameworkCore;

namespace BeerProject.Utility.Model
{
    public partial class EntityContext : DbContext
    {
        public EntityContext()
        {
        }

        public EntityContext(DbContextOptions options)
            : base(options)
        {
        }
        public DbSet<Bar> Bars { get; set; }
        public DbSet<Beer> Beers { get; set; }
        public DbSet<Brewery> Brewerys { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Beer>(entity =>
            {
                entity.ToTable("Beer");

                entity.Property(e => e.BeerID).ValueGeneratedNever();

                entity.Property(e => e.Name).HasColumnType("VARCHAR(500)");

                entity.Property(e => e.PercentageAlcoholByVolume).HasColumnType("DECIMAL");
            });

            modelBuilder.Entity<Brewery>(entity =>
            {
                entity.ToTable("Brewery");

                entity.Property(e => e.BreweryID).ValueGeneratedNever();

                entity.Property(e => e.Name).HasColumnType("VARCHAR(500)");
            });

            modelBuilder.Entity<Bar>(entity =>
            {
                entity.ToTable("Bar");

                entity.Property(e => e.BarID).ValueGeneratedNever();

                entity.Property(e => e.Name).HasColumnType("VARCHAR(500)");

                entity.Property(e => e.Address).HasColumnType("VARCHAR(5000)");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
