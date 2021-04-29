using App.Models;
using App.Services.Pokeflex;
using Microsoft.EntityFrameworkCore;

namespace App.Data
{
    public class PokeflexContext : DbContext
    {
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<UserGroup> UserGroups { get; set; }
        public virtual DbSet<Group> Groups { get; set; }
        public virtual DbSet<Pokemon> Pokemons { get; set; }
        
        public PokeflexContext(DbContextOptions<PokeflexContext> options) : base(options) {}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Pokemon>()
                .HasOne<Group>(p=>p.Group).WithMany(g => g.Pokemons).HasForeignKey(p => p.GroupId);
            
            modelBuilder.Entity<UserGroup>()
                .HasOne<Group>(ug=>ug.Group).WithMany(u => u.UserGroups).HasForeignKey(ug => ug.GroupId);
            modelBuilder.Entity<UserGroup>()
                .HasOne<User>(ug=>ug.User).WithMany(u => u.UserGroups).HasForeignKey(ug => ug.UserId);
        }
    }
}