using Microsoft.EntityFrameworkCore;

namespace BornToMove.DAL
{
    public class MoveContext : DbContext
    {
        public DbSet<Move> Move { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            builder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=BornToMove;Trusted_Connection=true;TrustServerCertificate=true;");
            base.OnConfiguring(builder);
        }
    }
}
