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
        public static IDbContext NewUniqueContext(
            string callingClassName,
            Mocker mocker
            )
        {
            var contextType =
                Environment.GetEnvironmentVariable("DotnetTestDbType", EnvironmentVariableTarget.Process)
                ?? "";

            switch (contextType)
            {
                case "inmemory":
                    return new InMemoryDbContext(callingClassName, mocker);
                case "native":
                    return new NativeDbContext(callingClassName,  mocker);
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

        private void SetConnectionString(string dbUniquifier)
        {
            var connectionStringBuilder = new SqliteConnectionStringBuilder
            {
                DataSource = dbUniquifier,
                Mode = SqliteOpenMode.Memory,
            };
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
        public InMemoryDbContext(string dbUniquifier, Mocker mocker)
        {
            SetConnectionString(dbUniquifier);
            BuildOptions();
            PokeflexContext = new PokeflexContext(ContextOptions);
            PokeflexContext.CreateEmptyViaDelete();
            // PokeflexContext.Database.EnsureDeleted();
            // PokeflexContext.ChangeTracker.Clear();
            // PokeflexContext.Database.EnsureCreated();
            
            foreach (var group in mocker)
            {
                foreach (var poke in group.Pokemons)
                {
                    PokeflexContext.Pokemons.Add(poke);
                }

                if (group.Id == 0) continue;
                group.Id = default;
                PokeflexContext.Groups.Add(group);
            }
            PokeflexContext.SaveChanges();
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

        private void BuildOptions(string dbUniquifier)
        {
            var builder = new DbContextOptionsBuilder<PokeflexContext>();
            builder.UseSqlServer(ConnectionString);
            ContextOptions = builder.Options;

        }
        
        public NativeDbContext(string dbNameUniquifier, Mocker mocker)
        {
            // SetConnectionString();
            // BuildOptions(dbNameUniquifier);
            // PokeflexContext = new PokeflexContext(ContextOptions);
            // PokeflexContext.CreateEmptyViaDelete();
            // PokeflexContext.Pokemons.BulkInsert(pokemons);
        }
    }
}