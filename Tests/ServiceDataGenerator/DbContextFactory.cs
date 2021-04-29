using System;
using System.Collections.Generic;
using System.Linq;
using App.Data;
using App.Models;
using EntityFrameworkCoreMock;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using TestSupport.EfHelpers;
using TestSupport.Helpers;

namespace Tests.ServiceDataGenerator
{
    public interface IDbContext
    {
        public PokeflexContext PokeflexContext { get; set; }
    }

    public static class DbContextFactory
    {
        public static IDbContext NewDbContext(
            Pokemon[] pokemons = default,
            User[] users=default,
            Group[] groups=default,
            UserGroup[] userGroups=default
            )
        {
            var contextType =
                Environment.GetEnvironmentVariable("DotnetTestDbType", EnvironmentVariableTarget.Process)
                ?? "";

            pokemons ??= Array.Empty<Pokemon>();
            users ??= Array.Empty<User>();
            groups ??= Array.Empty<Group>();
            userGroups ??= Array.Empty<UserGroup>();
            switch (contextType)
            {
                case "inmemory":
                    return new InMemoryDbContext(pokemons, users, groups, userGroups);
                case "native":
                    return new NativeDbContext(pokemons, users, groups, userGroups);
                default:
                    throw new ArgumentException("invalid option for env var PokeflexTestDbType %s", contextType);
            }
        }
    }

    public class InMemoryDbContext : IDbContext 
    {
        public PokeflexContext PokeflexContext { get; set; }
        private string ConnectionString { get; set; }
        private DbContextOptions<PokeflexContext> ContextOptions { get; set; }

        private void SetConnectionString()
        {
            var connectionStringBuilder = new SqliteConnectionStringBuilder { DataSource = ":memory:" };
            ConnectionString = connectionStringBuilder.ToString();
        }

        private void BuildOptions()
        {
            var connection = new SqliteConnection(ConnectionString);
            connection.Open();
            var builder = new DbContextOptionsBuilder<PokeflexContext>();
            builder.UseSqlite(connection);
            ContextOptions = builder.Options;
        }
        public InMemoryDbContext(Pokemon[] pokemons, User[] users, Group[] groups, UserGroup[] userGroups)
        {
            SetConnectionString();
            BuildOptions();
            PokeflexContext = new PokeflexContext(ContextOptions);
            PokeflexContext.Database.EnsureDeleted();
            PokeflexContext.ChangeTracker.Clear();
            PokeflexContext.Database.EnsureCreated();

            HashSet<int> groupIds = new();
            foreach (var p in pokemons)
            {
                if (p.GroupId is null) PokeflexContext.Pokemons.Add(p);
                else if (groups is null) groupIds.Add((int)p.GroupId);
            }

            foreach (var @group in
                groups ?? (
                    from gId in groupIds
                    select new Group {Id = gId})
                .ToArray())
            {
                @group.Pokemons = new List<Pokemon>();
                foreach (var pokemon in pokemons)
                {
                    if (pokemon.GroupId != @group.Id) continue;
                    pokemon.Group = @group;
                    PokeflexContext.Pokemons.Add(pokemon);
                    @group.Pokemons.Add(pokemon);
                }
                PokeflexContext.Groups.Add(group);
            }
            PokeflexContext.BulkSaveChanges();
        }
    }
    
    public class NativeDbContext : IDbContext 
    {
        public PokeflexContext PokeflexContext { get; set; }
        private string ConnectionString { get; set; }
        private DbContextOptions<PokeflexContext> ContextOptions { get; set; }
        private IConfigurationRoot Config { get; set; }
        
        private void SetConnectionString()
        {
            var callingProjectPath = TestData.GetCallingAssemblyTopLevelDir();
            Config = new ConfigurationBuilder()
                .SetBasePath(callingProjectPath)
                .AddJsonFile("appsettings.Local.json").Build();
            ConnectionString = Config.GetConnectionString("DefaultConnection");
        }

        private void BuildOptions()
        {
            var builder = new DbContextOptionsBuilder<PokeflexContext>();
            builder.UseSqlServer(ConnectionString);
            ContextOptions = builder.Options;

        }
        
        public NativeDbContext(Pokemon[] pokemons, User[] users, Group[] groups, UserGroup[] userGroups)
        {
            SetConnectionString();
            BuildOptions();
            PokeflexContext = new PokeflexContext(ContextOptions);
            PokeflexContext.CreateEmptyViaDelete();
            PokeflexContext.Pokemons.BulkInsert(pokemons);
        }
    }
}