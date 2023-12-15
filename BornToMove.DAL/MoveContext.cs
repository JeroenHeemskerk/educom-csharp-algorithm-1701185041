using Microsoft.EntityFrameworkCore;
using System;

namespace BornToMove.DAL
{
    public class MoveContext : DbContext
    {
        public DbSet<Move> Move { get; set; }
        public DbSet<MoveRating> MoveRating { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            builder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=BornToMove;Trusted_Connection=true;TrustServerCertificate=true;");
            base.OnConfiguring(builder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Move>().HasData(new { Id = 1, Name = "Push up", Description = "Ga horizontaal liggen op teentoppen en handen. Laat het lijf langzaam zakken tot de neus de grond bijna raakt. Duw het lijf terug nu omhoog tot de ellebogen bijna gestrekt zijn. Vervolgens weer laten zakken. Doe dit 20 keer zonder tussenpauzes.", SweatRate = 3 },
                                                new { Id = 2, Name = "Planking", Description = "Ga horizontaal liggen op teentoppen en onderarmen. Houdt deze positie 1 minuut vast.", SweatRate = 3 },
                                                new { Id = 3, Name = "Squat", Description = "Ga staan met gestrekte armen. Zak door de knieën tot de billen de grond bijna raken. Ga weer volledig gestrekt staan. Herhaal dit 20 keer zonder tussenpauzes.", SweatRate = 5 });

            modelBuilder.Entity<MoveRating>().HasData(new { Id = 1, MoveId = 1, Rating = 5.00D, Intensity = 5.00D },
                                                      new { Id = 2, MoveId = 2, Rating = 4.00D, Intensity = 4.00D },
                                                      new { Id = 3, MoveId = 3, Rating = 5.00D, Intensity = 3.00D },
                                                      new { Id = 4, MoveId = 1, Rating = 1.00D, Intensity = 5.00D });
        }

        public void ApplyMigrations()
        {
            if (!Move.Any())
            {
                Database.Migrate();
            }
        }
    }
}
