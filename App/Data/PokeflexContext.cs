using System;
using System.Linq;
using System.Linq.Expressions;
using App.Models;
using App.Services.Pokeflex;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace App.Data
{
    public class PokeflexContext : IdentityDbContext
    {
        public virtual DbSet<AppUser> AppUsers { get; set; }
        public virtual DbSet<UserGroup> UserGroups { get; set; }
        public virtual DbSet<Group> Groups { get; set; }
        public virtual DbSet<Pokemon> Pokemons { get; set; }
        
        public PokeflexContext(DbContextOptions<PokeflexContext> options) : base(options) {}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            
            modelBuilder
                .Entity<Pokemon>()
                .HasOne<Group>(p=>p.Group)
                .WithMany(g => g.Pokemons)
                .HasForeignKey(p => p.GroupId);
            modelBuilder
                .Entity<UserGroup>()
                .HasOne<Group>(ug=>ug.Group)
                .WithMany(u => u.UserGroups)
                .HasForeignKey(ug => ug.GroupId);
            modelBuilder
                .Entity<UserGroup>()
                .HasOne<AppUser>(ug=>ug.AppUser)
                .WithMany(u => u.UserGroups)
                .HasForeignKey(ug => ug.AppUserId);

            modelBuilder.HasDbFunction(() => Udfs.MakeRange(default, default)).HasName("MakeRange");
            // !!KLUGE!! The following code is supposed to work but is broken, documentation:
            //      https://docs.microsoft.com/en-us/ef/core/querying/user-defined-function-mapping
            //      https://github.com/dotnet/efcore/issues/23408
            modelBuilder.HasDbFunction(() => SelectFlexmon(default, default)).HasName("SelectFlexmon");
            // BEGIN KLUGE
            modelBuilder.Entity<Pokemon>().ToTable("Pokemons");
            // END KLUGE
            
        }

        public IQueryable<Pokemon> SelectFlexmon(int group, int number) =>
            FromExpression(() => SelectFlexmon(group, number));
    }
}