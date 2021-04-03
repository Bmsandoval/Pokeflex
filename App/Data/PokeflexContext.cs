using App.Models;
using Microsoft.EntityFrameworkCore;

namespace App.Data
{
    public class PokeflexContext : DbContext
    {
        public PokeflexContext(DbContextOptions<PokeflexContext> options) : base(options)
        {
        }

        public virtual DbSet<Pokemon> Pokemons { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Pokemon>().ToTable("Pokemon");
        }
    }
}