using App.Models;
using App.Services.Pokeflex;
using Microsoft.EntityFrameworkCore;

namespace App.Data
{
    public class PokeflexContext : DbContext
    {
        public PokeflexContext(DbContextOptions<PokeflexContext> options) : base(options)
        {
        }

        public virtual DbSet<Pokemon> Pokemons { get; set; }
        public virtual DbSet<Flexmon> Flexmons { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Pokemon>().ToTable("Pokemons");
            modelBuilder.Entity<Flexmon>().ToTable("Flexmons");
        }
    }
}